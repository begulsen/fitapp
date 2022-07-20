using System;
using System.Threading.Tasks;
using FitApp.Api.Controllers.NutritionController.Model;
using FitApp.Api.Exceptions;
using FitApp.Api.Helper;
using FitApp.Api.Service;
using FitApp.NutritionRepository.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitApp.Api.Controllers.NutritionController
{
    [ApiController]
    [Route("[controller]")]
    public class NutritionController: ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public NutritionController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        /// <summary>
        /// Create a New Nutrition
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /createNutrition
        ///     
        /// </remarks>
        /// <param name="createNutritionModel"></param>
        /// <returns>Ok</returns>
        /// <response code="200">Returns ok</response>
        /// <response code="400">If the nutritionModel is null or empty</response>
        [HttpPost("/createNutrition")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateNutrition([FromBody]CreateNutritionModel createNutritionModel)
        {
            if (createNutritionModel == null) 
                return BadRequest(new ApiException.NutritionIdIsNotExistException(nameof(createNutritionModel)));
            await _applicationService.CreateNutrition(createNutritionModel.ToCreateNutrition());
            return StatusCode(StatusCodes.Status201Created);
        }
        
        /// <summary>
        /// Get Nutrition By Name
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /getNutrition
        ///     
        /// </remarks>
        /// <param name="nutritionName"></param>
        /// <returns>Ok</returns>
        /// <response code="200">Returns ok</response>
        /// <response code="400">If the nutritionName is null or empty</response>
        [HttpGet("/getNutritionName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetNutrition(string nutritionName)
        {
            if (string.IsNullOrEmpty(nutritionName)) return BadRequest(new ApiException.NutritionIdIsNotExistException(nameof(nutritionName)));
            Nutrition nutrition = await _applicationService.GetNutritionByName(nutritionName);
            if (nutrition == null) return NotFound();
            return Ok(nutrition);
        }

        /// <summary>
        /// Update Nutrition
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /getNutrition
        ///     
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="updateNutritionModel"></param>
        /// <returns>Ok</returns>
        /// <response code="200">Returns ok</response>
        /// <response code="400">If the nutrition id is null or empty</response>
        /// <response code="404">If the nutrition id is not found</response>
        [HttpPost("/{id}/updateNutrition")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateNutrition(Guid id, [FromBody] UpdateNutritionModel updateNutritionModel)
        {
            if (id == default) throw new ApiException.UserIdIsNotValidException(nameof(id));
            if (updateNutritionModel == null) 
                return BadRequest(new ApiException.ValueCannotBeNullOrEmptyException(nameof(updateNutritionModel)));
            Nutrition nutrition = await _applicationService.GetNutrition(id);
            if (nutrition == null) 
                return BadRequest(new ApiException.NutritionIdIsNotExistException(nameof(nutrition)));
            await _applicationService.UpdateNutrition(updateNutritionModel.ToUpdateNutrition(nutrition));
            return Ok(StatusCodes.Status200OK);
        }
    }
}