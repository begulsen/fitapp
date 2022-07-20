using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FitApp.Api.Controllers.UserPrivateDietController.Model;
using FitApp.Api.Exceptions;
using FitApp.Api.Helper;
using FitApp.Api.Helper.ImageHelper;
using FitApp.Api.Middleware;
using FitApp.Api.Service;
using FitApp.UserPrivateDietDetailRepository.Model;
using FitApp.UserPrivateDietRepository.Model;
using FitApp.UserRepository.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitApp.Api.Controllers.UserPrivateDietController
{
    [ApiController]
    [Route("[controller]")]
    public class UserPrivateDietController : ControllerBase
    {
        private readonly IApplicationService _applicationService;
        private readonly ImageHelper _imageHelper;
        private readonly IFileHelper _fileHelper;

        public UserPrivateDietController(IApplicationService applicationService, ImageHelper imageHelper,
            IFileHelper fileHelper)
        {
            _applicationService = applicationService;
            _imageHelper = imageHelper;
            _fileHelper = fileHelper;
        }

        /// <summary>
        /// Create a New User Private Diet
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /create
        ///     
        /// </remarks>
        /// <param name="userId"></param>
        /// <param name="createModel"></param>
        /// <returns>Ok</returns>
        /// <response code="200">Returns ok</response>
        /// <response code="400">Request model is empty</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost("/createUserPrivateDiet")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateUserPrivateDiet([FromHeader(Name = "user-id")] Guid userId,
            [FromForm] CreateUserPrivateDietModel createModel)
        {
            if (createModel == null) throw new ApiException.ValueCannotBeNullOrEmptyException(nameof(createModel));
            User user = await _applicationService.GetUser(userId);
            if (user == null) throw new ApiException.UserIdIsNotExistException(nameof(user.Id));

            UserPrivateDiet existingModel = await _applicationService.GetUserPrivateDiet(userId);
            await _applicationService.CreateUserPrivateDiet(createModel.ToUserPrivateDiet(userId));
            var count = 0;
            var images = new List<IFormFile>()
            {
                createModel.Image1,
                createModel.Image2,
                createModel.Image3
            };
            foreach (var image in images)
            {
                if (image != null)
                {
                    _imageHelper.ValidateMedia(image, out ApiException.BadRequestException exception);
                    if (exception != null) throw exception;
                    count++;
                    _fileHelper.DeleteUserPrivateImages(userId, count, "diet");
                    _fileHelper.UploadUserPrivateImages(userId, image, count, "diet");   
                }
            }

            if (existingModel == null)
            {
                return StatusCode(StatusCodes.Status201Created);
            }

            UserPrivateDietDetail detailModel = await _applicationService.GetUserPrivateDietDetail(userId);
            var dietData = new Diet
            {
                Goal = existingModel.Goal,
                AdditionalInformation = existingModel.AdditionalInformation,
                DislikedFoods = existingModel.DislikedFoods,
                LikedFoods = existingModel.LikedFoods,
                Disease = existingModel.Disease,
                StartDate = existingModel.StartDate,
                EndDate = existingModel.EndDate,
                UpdatedAt = existingModel.UpdatedAt
            };

            if (detailModel == null)
            {
                await _applicationService.CreateUserPrivateDietDetail(new UserPrivateDietDetail()
                {
                    Id = userId,
                    Diets = new List<Diet>()
                    {
                        dietData
                    },
                    CreatedAt = DateTime.Now
                });
            }
            else
            {
                detailModel.Diets.Add(dietData);
                await _applicationService.CreateUserPrivateDietDetail(new UserPrivateDietDetail()
                {
                    Id = userId,
                    Diets = detailModel.Diets,
                    CreatedAt = DateTime.Now
                });
            }

            return StatusCode(StatusCodes.Status201Created);
        }

        /// <summary>
        ///  0
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /updateDate
        ///     
        /// </remarks>
        /// <param name="userId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns>Ok</returns>
        /// <response code="200">Returns ok</response>
        /// <response code="400">Request model is empty</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost("/updateDietDate")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateDietDate([FromHeader(Name = "user-id")] Guid userId, DateTime startDate,
            DateTime endDate)
        {
            User user = await _applicationService.GetUser(userId);
            if (user == null) 
                return BadRequest(new ApiException.UserIdIsNotExistException(nameof(userId)));
            UserPrivateDiet model = await _applicationService.GetUserPrivateDiet(userId);
            if (model == null) return BadRequest( new ApiException.UserPrivateDietIsNotExistException(userId));

            model.StartDate = startDate;
            model.EndDate = endDate;
            model.UpdatedAt = DateTime.Now;

            await _applicationService.CreateUserPrivateDiet(model);
            return Ok();
        }

        /// <summary>
        ///  0
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /assignPrivateDiet
        ///     
        /// </remarks>
        /// <param name="userId"></param>
        /// <param name="weeksMenuNameDictionary"></param>
        /// <returns>Ok</returns>
        /// <response code="200">Returns ok</response>
        /// <response code="400">Request model is empty</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost("/assignPrivateDiet")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> AssignPrivateDiet([FromHeader(Name = "user-id")] Guid userId, Dictionary<int, string> weeksMenuNameDictionary)
        {
            User user = await _applicationService.GetUser(userId);
            if (user == null) 
                return BadRequest(new ApiException.UserIdIsNotExistException(nameof(userId)));
            if (weeksMenuNameDictionary == null || weeksMenuNameDictionary.Count == 0)
                return BadRequest(new ApiException.UserIdIsNotExistException(nameof(userId)));
            UserPrivateDiet model = await _applicationService.GetUserPrivateDiet(userId);
            if (model == null) 
                return BadRequest(new ApiError(new ApiException.UserPrivateDietIsNotExistException(userId)));

            model.UpdatedAt = DateTime.Now;
            model.WeeksMenuNameDictionary = weeksMenuNameDictionary;
            await _applicationService.CreateUserPrivateDiet(model);
            return Ok();
        }
        
        /// <summary>
        ///  0
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     Get /getUserPrivateTraining
        ///     
        /// </remarks>
        /// <param name="userId"></param>
        /// <returns>Ok</returns>
        /// <response code="200">Returns ok</response>
        /// <response code="400">Request model is empty</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet("/getDiet")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetUserPrivateDiet(Guid userId)
        {
            User user = await _applicationService.GetUser(userId);
            if (user == null) return BadRequest(new ApiException.UserIdIsNotExistException(nameof(userId)));
            var model = await _applicationService.GetUserPrivateDiet(userId);
            if (model == null) return BadRequest(new ApiException.UserPrivateDietIsNotExistException(userId));
            return Ok(model);
        }
    }
}