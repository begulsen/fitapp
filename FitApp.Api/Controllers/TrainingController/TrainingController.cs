using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitApp.ActivityRepository.Model;
using FitApp.Api.Controllers.TrainingController.Model;
using FitApp.Api.Exceptions;
using FitApp.Api.Helper;
using FitApp.Api.Service;
using FitApp.SetRepository.Model;
using FitApp.TrainingRepository.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitApp.Api.Controllers.TrainingController
{
    [ApiController]
    [Route("[controller]")]
    public class TrainingController : ControllerBase
    {
        private readonly IApplicationService _applicationService;
        public TrainingController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        /// <summary>
        /// Create a New Training
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /createTraining
        ///     
        /// </remarks>
        /// <param name="createTrainingModel"></param>
        /// <returns>Ok</returns>
        /// <response code="200">Returns ok</response>
        /// <response code="400">If the trainingModel is null or empty</response>
        [HttpPost("/createTraining")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateTrainingModel([FromBody]CreateTrainingModel createTrainingModel)
        {
            if (createTrainingModel == null) throw new ApiException.ValueCannotBeNullOrEmptyException(nameof(createTrainingModel));
            if (createTrainingModel.SetIds == null || !createTrainingModel.SetIds.Any()) throw new ApiException.ValueCannotBeNullOrEmptyException(nameof(createTrainingModel.SetIds));
            await _applicationService.CreateTraining(createTrainingModel.ToCreateTraining());
            return StatusCode(StatusCodes.Status201Created);
        }

        /// <summary>
        /// Create a New Training
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /createTraining
        ///     
        /// </remarks>
        /// <param name="trainingName"></param>
        /// <param name="setIds"></param>
        /// <returns>Ok</returns>
        /// <response code="200">Returns ok</response>
        /// <response code="400">If the trainingModel is null or empty</response>
        [HttpPost("/updateTraining")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateTrainingModel([FromHeader] string trainingName, [FromBody] List<Guid> setIds)
        {
            if (trainingName == null) throw new ApiException.ValueCannotBeNullOrEmptyException(nameof(trainingName));
            if (setIds == null || !setIds.Any()) throw new ApiException.ValueCannotBeNullOrEmptyException(nameof(setIds));
            
            Training training = await _applicationService.GetTraining(trainingName);
            if (training == null) throw new ApiException.TrainingNameIsNotExistException(nameof(trainingName));
            
            await _applicationService.UpdateTraining(training.Id, setIds);
            return StatusCode(StatusCodes.Status200OK);
        }
        
        /// <summary>
        /// Get Training by Name
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /getTrainingByName
        ///     
        /// </remarks>
        /// <param name="trainingName"></param>
        /// <returns>Ok</returns>
        /// <response code="200">Returns ok</response>
        /// <response code="400">If the trainingModel is null or empty</response>
        [HttpGet("/getTrainingByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTrainingByName([FromHeader] string trainingName)
        {
            if (trainingName == null) throw new ApiException.ValueCannotBeNullOrEmptyException(nameof(trainingName));
            
            Training training = await _applicationService.GetTraining(trainingName);
            if (training == null) throw new ApiException.TrainingNameIsNotExistException(nameof(trainingName));

            var response = new ResponseTrainingModel()
            {
                Name = training.Name,
                IsPopular = training.IsPopular,
                TrainingDifficulty = training.TrainingDifficulty,
                TrainingDuration = training.TrainingDuration
            };
            response.Sets = new List<SetResponse>();
            foreach (var setId in training.SetIds)
            {
                Set set = await _applicationService.GetSet(setId);
                if (set != null)
                {
                    Activity activity = await _applicationService.GetActivity(set.ActivityId);
                    response.Sets.Add(new SetResponse()
                    {
                        ActivityDifficulty = activity.Difficulty,
                        ActivityEquipment = activity.Equipment,
                        ActivityName = activity.Name,
                        ActivityNumber = set.ActivityNumber,
                        ActivityRepetition = set.ActivityRepetition,
                        SetName = set.Name,
                        ActivityStatus = activity.Status,
                        ActivityEffectiveZone = activity.EffectiveZone,
                        ActivityEffectiveZonePrimary = activity.EffectiveZonePrimary,
                        ActivityEffectiveZoneSecondary = activity.EffectiveZoneSecondary,
                    });
                }
            }

            return Ok(response);
        }
    }
}