using System.Net;
using System.Threading.Tasks;
using FitApp.Api.Exceptions;
using FitApp.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace FitApp.Api.Middleware
{
    public class ApiError : System.Exception
    {
        public int Code { get; set; }
        public string Message { get; set; }
        
        public ApiError()
        {
            
        }

        public ApiError(ApiException.BadRequestException exception)
        {
            Code = (int) exception.Code;
            Message = exception.Message;
        }
        
        public ApiError(ApiException.ConflictException exception)
        {
            Code = (int) exception.Code;
            Message = exception.Message;
        }
    }

    public class ApiExceptionResponse
    {
        public ApiError Error { get; set; }
    }

    public class ErrorHandling
    {
        readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings();

        private readonly RequestDelegate _next;

        public ErrorHandling(RequestDelegate next)
        {
            _serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (BusinessException ex)
            {
                await HandleBusinessExceptionAsync(context, ex);
            }
            catch (ApiException.BadRequestException ex)
            {
                await HandleBadRequestExceptionAsync(context, ex);
            }
            catch (System.Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleBusinessExceptionAsync(HttpContext context, BusinessException exception)
        {
            var code = MapStatusCode(exception);

            var result = JsonConvert.SerializeObject(new ApiExceptionResponse()
            {
                Error = new ApiError()
                {
                    Code = (int)code,
                    Message = exception.Message
                }
            }, _serializerSettings);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }

        private Task HandleBadRequestExceptionAsync(HttpContext context, ApiException.BadRequestException exception)
        {            
            var responseCode = MapStatusCode(exception);
            var result = JsonConvert.SerializeObject(new ApiExceptionResponse()
            {
                Error = new ApiError()
                {
                    Code = (int)exception.Code,
                    Message = exception.Message
                }
            }, _serializerSettings);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)responseCode;
            return context.Response.WriteAsync(result);
        }

        private Task HandleExceptionAsync(HttpContext context, System.Exception exception)
        {
            var code = MapStatusCode(exception);

            var result = JsonConvert.SerializeObject(new ApiExceptionResponse()
            {
                Error = new ApiError()
                {
                    Code = (int)code,
                    Message = exception.Message
                }
            }, _serializerSettings);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }

        private HttpStatusCode MapStatusCode(System.Exception exception)
        {
            switch (exception)
            {
                case BusinessException _:
                    return HttpStatusCode.BadRequest;
                case ApiException.BadRequestException _:
                    return HttpStatusCode.BadRequest;
                case ApiException.ConflictException _:
                    return HttpStatusCode.Conflict;
                default:
                    return HttpStatusCode.InternalServerError;
            }
        }
    }
}