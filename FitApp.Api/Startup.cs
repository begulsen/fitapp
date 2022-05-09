using System;
using System.Linq;
using System.Text.Json.Serialization;
using FitApp.ActivityRepository;
using FitApp.Api.Helper.ImageHelper;
using FitApp.Api.Middleware;
using FitApp.Api.Service;
using FitApp.MealNutritionRepository;
using FitApp.MealRepository;
using FitApp.MenuRepository;
using FitApp.NutritionRepository;
using FitApp.SetRepository;
using FitApp.TrainingRepository;
using FitApp.UserPrivateDietDetailRepository;
using FitApp.UserPrivateDietRepository;
using FitApp.UserPrivateTrainingDetailRepository;
using FitApp.UserPrivateTrainingRepository;
using FitApp.UserRepository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FitApp.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                    .AddJsonOptions(options => 
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));;
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FitApp.Api", Version = "v1", Description = "Fit App API"});
                c.OperationFilter<SwaggerFileOperationFilter>();
            });

            services.AddUserRepository();
            services.AddTrainingRepository();
            services.AddSetRepository();
            services.AddActivityRepository();
            services.AddNutritionRepository();
            services.AddMealNutritionRepository();
            services.AddMealRepository();
            services.AddMenuRepository();
            services.AddUserPrivateTrainingRepository();
            services.AddUserPrivateTrainingDetailRepository();
            services.AddUserPrivateDietRepository();
            services.AddUserPrivateDietDetailRepository();
            services.AddSingleton<IApplicationService, ApplicationService>();
            services.AddSingleton(new ImageHelper());
            services.AddSingleton<IFileHelper, FileHelper>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHttpsRedirection();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context => { await context.Response.WriteAsync("FitApp Api"); });
            });
            app.UseMiddleware<ErrorHandling>();
            // Enable Swagger middleware and endpoint
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.

            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "FitApp API"); });
            app.UseStaticFiles();
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            app.UseCors();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });            
        }
        
        public class SwaggerFileOperationFilter : IOperationFilter  
        {  
            public void Apply(OpenApiOperation operation, OperationFilterContext context)  
            {  
                var fileUploadMime = "multipart/form-data";  
                if (operation.RequestBody == null || !operation.RequestBody.Content.Any(x => x.Key.Equals(fileUploadMime, StringComparison.InvariantCultureIgnoreCase)))  
                    return;  
  
                var fileParams = context.MethodInfo.GetParameters().Where(p => p.ParameterType == typeof(IFormFile));  
                operation.RequestBody.Content[fileUploadMime].Schema.Properties =  
                    fileParams.ToDictionary(k => k.Name, v => new OpenApiSchema()  
                    {  
                        Type = "string",  
                        Format = "binary"  
                    });
            }  
        }
    }
}