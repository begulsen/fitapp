using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FitApp.Api.Controllers.SetController.Model;
using FitApp.Api.Exceptions;
using FitApp.Api.Helper;
using FitApp.Api.Service;
using FitApp.SetRepository.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitApp.Api.Controllers.SetController
{
    [ApiController]
    [Route("[controller]")]
    public class SetController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public SetController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        /// <summary>
        /// Create a New Set
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /createSet
        ///     
        /// </remarks>
        /// <param name="createSetModel"></param>
        /// <returns>Ok</returns>
        /// <response code="200">Returns ok</response>
        /// <response code="400">If the trainingModel is null or empty</response>
        [HttpPost("/createSet")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateSet([FromBody]CreateSetModel createSetModel)
        {
            if (createSetModel == null) throw new ApiException.ValueCannotBeNullOrEmptyException(nameof(createSetModel));
            var activity = await _applicationService.GetActivity(createSetModel.ActivityId);
            if (activity == null)
            {
                return BadRequest(new ApiException.ActivityCannotExistException(createSetModel.ActivityId));
            }
            await _applicationService.CreateSet(createSetModel.ToCreateSet());
            return StatusCode(StatusCodes.Status201Created);
        }
        
        /// <summary>
        /// Get All Sets
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /createSet
        ///     
        /// </remarks>
        /// <returns>Ok</returns>
        /// <response code="200">Returns ok</response>
        /// <response code="400">If the trainingModel is null or empty</response>
        [HttpGet("/getSets")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSets()
        {
            List<Set> sets = await _applicationService.GetAllSets();
            return Ok(sets);
        }
        
        /// <summary>
        /// Get Set by name
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /getSetByName
        ///     
        /// </remarks>
        /// <returns>Ok</returns>
        /// <response code="200">Returns ok</response>
        /// <response code="400">If the trainingModel is null or empty</response>
        [HttpGet("/getSetByName")]
        [ProducesResponseType(typeof(Set), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSetByName(string setName)
        {
            if (string.IsNullOrEmpty(setName)) 
                return BadRequest(new ApiException.ValueCannotBeNullOrEmptyException(nameof(setName)));
            Set set = await _applicationService.GetSetByName(setName);
            return Ok(set);
        }

        /// <summary>
        /// Update Set
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /updateSet
        ///     
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="updateSetModel"></param>
        /// <returns>Accepted</returns>
        /// <response code="200">Returns ok</response>
        /// <response code="400">If the trainingModel is null or empty</response>
        [HttpPost("/{id}/updateSet")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateSet(Guid id, [FromBody]UpdateSetModel updateSetModel)
        {
            if (id == default) 
                return BadRequest(new ApiException.UserIdIsNotValidException(nameof(id)));
            if (updateSetModel == null) 
                return BadRequest(new ApiException.ValueCannotBeNullOrEmptyException(nameof(updateSetModel)));

            Set set = await _applicationService.GetSet(id);
            if (set == null) 
                return BadRequest(new ApiException.SetIdIsNotExistException(nameof(id)));
            await _applicationService.UpdateSet(updateSetModel.ToUpdateSet(set));
            return Accepted(StatusCodes.Status202Accepted);
        }
    }
}