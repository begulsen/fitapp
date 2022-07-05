using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FitApp.Api.Controllers.UserPrivateTrainingController.Model;
using FitApp.Api.Exceptions;
using FitApp.Api.Helper;
using FitApp.Api.Helper.ImageHelper;
using FitApp.Api.Middleware;
using FitApp.Api.Service;
using FitApp.UserPrivateTrainingDetailRepository.Model;
using FitApp.UserPrivateTrainingRepository.Model;
using FitApp.UserRepository.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nest;
using Training = FitApp.UserPrivateTrainingDetailRepository.Model.Training;

namespace FitApp.Api.Controllers.UserPrivateTrainingController
{
    [ApiController]
    [Route("[controller]")]
    public class UserPrivateTrainingController : ControllerBase
    {
        private readonly IApplicationService _applicationService;
        private readonly ImageHelper _imageHelper;
        private readonly IFileHelper _fileHelper;

        public UserPrivateTrainingController(IApplicationService applicationService, ImageHelper imageHelper, IFileHelper fileHelper)
        {
            _applicationService = applicationService;
            _imageHelper = imageHelper;
            _fileHelper = fileHelper;
        }

        /// <summary>
        /// Create a New User Private Training
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /createUserPrivateTraining
        ///     
        /// </remarks>
        /// <param name="userId"></param>
        /// <param name="createModel"></param>
        /// <returns>Ok</returns>
        /// <response code="200">Returns ok</response>
        /// <response code="400">Request model is empty</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost("/create")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateUserPrivateTraining([FromHeader(Name = "user-id")]Guid userId, [FromForm]CreateUserPrivateTrainingModel createModel)
        {
            if (createModel == null) return BadRequest(new ApiException.ValueCannotBeNullOrEmptyException(nameof(createModel)));
            User user = await _applicationService.GetUser(userId);
            if (user == null) return BadRequest(new ApiException.UserIdIsNotExistException(nameof(userId)));

            UserPrivateTraining existingModel = await _applicationService.GetUserPrivateTraining(userId);
            await _applicationService.CreateUserPrivateTraining(createModel.ToUserPrivateTraining(userId));
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
                    _fileHelper.DeleteUserPrivateImages(userId, count, "training");
                    _fileHelper.UploadUserPrivateImages(userId, image, count, "training");   
                }
            }
            if (existingModel == null)
            {
                return StatusCode(StatusCodes.Status201Created);
            }
            
            UserPrivateTrainingDetail detailModel = await _applicationService.GetUserPrivateTrainingDetail(userId);
            var trainingData = new Training()
            {
                Disease = existingModel.Disease,
                Goal = existingModel.Goal,
                Equipment = existingModel.Equipment,
                Level = existingModel.Level,
                AdditionalInformation = existingModel.AdditionalInformation,
                PrimaryZone = existingModel.PrimaryZone,
                TrainingLocation = existingModel.TrainingLocation,
                TrainingDayCountOfWeek = existingModel.TrainingDayCountOfWeek,
                StartDate = existingModel.StartDate,
                EndDate = existingModel.EndDate,
                UpdatedAt = existingModel.UpdatedAt
            };

            if (detailModel == null)
            {
                await _applicationService.CreateUserPrivateTrainingDetail(new UserPrivateTrainingDetail()
                {
                    Id = userId,
                    Trainings = new List<Training>()
                    {
                        trainingData
                    },
                    CreatedAt = DateTime.Now
                });
            }
            else
            {
                detailModel.Trainings.Add(trainingData);
                await _applicationService.CreateUserPrivateTrainingDetail(new UserPrivateTrainingDetail()
                {
                    Id = userId,
                    Trainings = detailModel.Trainings,
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
        ///     Get /getUserPrivateTraining
        ///     
        /// </remarks>
        /// <param name="userId"></param>
        /// <returns>Ok</returns>
        /// <response code="200">Returns ok</response>
        /// <response code="400">Request model is empty</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet("/getTraining")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetUserPrivateTraining(Guid userId)
        {
            User user = await _applicationService.GetUser(userId);
            if (user == null) return BadRequest(new ApiException.UserIdIsNotExistException(nameof(userId)));
            UserPrivateTraining model = await _applicationService.GetUserPrivateTraining(userId);
            if (model == null) return BadRequest(new ApiException.UserPrivateTrainingIsNotExistException(userId));
            return Ok(model);
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
        [HttpPost("/updateTrainingDate")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateTrainingDate([FromHeader(Name = "user-id")]Guid userId, DateTime startDate, DateTime endDate)
        {
            User user = await _applicationService.GetUser(userId);
            if (user == null) return BadRequest(new ApiException.UserIdIsNotExistException(nameof(userId)));
            UserPrivateTraining model = await _applicationService.GetUserPrivateTraining(userId);
            if (model == null) return BadRequest(new ApiException.UserPrivateTrainingIsNotExistException(userId));

            model.StartDate = startDate;
            model.EndDate = endDate;
            model.UpdatedAt = DateTime.Now;

            await _applicationService.CreateUserPrivateTraining(model);
            return Ok();
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /updateDate
        ///     
        /// </remarks>
        /// <param name="userId"></param>
        /// <param name="weeksTrainingList"></param>
        /// <returns>Ok</returns>
        /// <response code="200">Returns ok</response>
        /// <response code="400">Request model is empty</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost("/assignTrainings")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> AssignTrainings([FromHeader(Name = "user-id")]Guid userId, Dictionary<int, List<string>> weeksTrainingList)
        {
            User user = await _applicationService.GetUser(userId);

            if (user == null) return BadRequest(new ApiException.UserIdIsNotExistException(nameof(userId)));

            UserPrivateTraining model = await _applicationService.GetUserPrivateTraining(userId);
            if (model == null) return BadRequest(new ApiException.UserPrivateTrainingIsNotExistException(userId));

            model.UpdatedAt = DateTime.Now;
            model.WeeksTrainingList = weeksTrainingList;

            await _applicationService.CreateUserPrivateTraining(model);
            return Ok();
        }
    }
}