using System.Collections.Generic;
using System.Threading.Tasks;
using FitApp.ActivityRepository.Model;
using FitApp.Api.Controllers.ActivityController.Model;
using FitApp.Api.Exceptions;
using FitApp.Api.Helper;
using FitApp.Api.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitApp.Api.Controllers.ActivityController
{
    [ApiController]
    [Route("[controller]")]
    public class ActivityController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public ActivityController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        /// <summary>
        /// Create a New Activity
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /createActivity
        ///     
        /// </remarks>
        /// <param name="createActivityModel"></param>
        /// <returns>Ok</returns>
        /// <response code="200">Returns ok</response>
        /// <response code="400">If the trainingModel is null or empty</response>
        [HttpPost("/createActivity")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateActivity([FromBody]CreateActivityModel createActivityModel)
        {
            if (createActivityModel == null) throw new ApiException.ValueCannotBeNullOrEmptyException(nameof(createActivityModel));
            await _applicationService.CreateActivity(createActivityModel.ToCreateActivity());
            return StatusCode(StatusCodes.Status201Created);
        }

        /// <summary>
        /// Get Activity By Name
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /getActivity
        ///     
        /// </remarks>
        /// <param name="activityName"></param>
        /// <returns>Ok</returns>
        /// <response code="200">Returns ok</response>
        /// <response code="400">If the activityName is null or empty</response>
        [HttpGet("/getActivity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetActivity(string activityName)
        {
            if (string.IsNullOrEmpty(activityName)) throw new ApiException.ValueCannotBeNullOrEmptyException(nameof(activityName));
            Activity activity = await _applicationService.GetActivityByName(activityName);
            if (activity == null) return NotFound();
            return Ok(activity);
        }

        /// <summary>
        /// Get All Activity
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /getAllActivities
        ///     
        /// </remarks>
        /// <param name="equipments"></param>
        /// <param name="effectiveZone"></param>
        /// <returns>Ok</returns>
        /// <response code="200">Returns ok</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet("/getAllActivities")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllActivities([FromQuery]List<string> equipments, string effectiveZone)
        {
            List<Activity> activities = await _applicationService.GetAllActivities(equipments, effectiveZone);
            return Ok(activities);
        }
    }
}