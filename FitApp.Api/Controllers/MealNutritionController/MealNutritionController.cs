using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FitApp.Api.Controllers.MealNutritionController.Model;
using FitApp.Api.Exceptions;
using FitApp.Api.Helper;
using FitApp.Api.Service;
using FitApp.MealNutritionRepository.Model;
using FitApp.NutritionRepository.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitApp.Api.Controllers.MealNutritionController
{
    [ApiController]
    [Route("[controller]")]
    public class MealNutritionController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public MealNutritionController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        /// <summary>
        /// Create a New MealNutrition
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /createMealNutrition
        ///     
        /// </remarks>
        /// <param name="createMealNutritionModel"></param>
        /// <returns>Ok</returns>
        /// <response code="200">Returns ok</response>
        /// <response code="400">If the createMealNutritionModel is null or empty</response>
        [HttpPost("/createMealNutrition")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateMealNutrition([FromBody]CreateMealNutritionModel createMealNutritionModel)
        {
            if (createMealNutritionModel == null) throw new ApiException.ValueCannotBeNullOrEmptyException(nameof(createMealNutritionModel));
            Nutrition nutrition = await _applicationService.GetNutrition(createMealNutritionModel.NutritionId);
            if (nutrition == null)
            {
                throw new ApiException.NutritionIdIsNotExistException(createMealNutritionModel.NutritionId.ToString());
            }
            await _applicationService.CreateMealNutritionModel(createMealNutritionModel.ToCreateMealNutrition(nutrition));
            return StatusCode(StatusCodes.Status201Created);
        }

        /// <summary>
        /// Get MealNutrition By NutritionId
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /getNutritionId
        /// 
        ///     
        /// </remarks>
        /// <param name="mealNutritionId"></param>
        /// <returns>Ok</returns>
        /// <response code="200">Returns ok</response>
        /// <response code="400">If the nutritionName is null or empty</response>
        [HttpGet("/getMealNutrition")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMealNutrition(Guid mealNutritionId)
        {
            if (mealNutritionId == default) throw new ApiException.ValueCannotBeNullOrEmptyException(nameof(mealNutritionId));
            MealNutrition mealNutrition = await _applicationService.GetMealNutritionByNutritionId(mealNutritionId);
            if (mealNutrition == null) return NotFound();
            return Ok(mealNutrition);
        }
        
        /// <summary>
        /// Get MealNutrition By NutritionId
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /GetAllMealNutritions
        /// 
        ///     
        /// </remarks>
        /// <returns>Ok</returns>
        /// <response code="200">Returns ok</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet("/getAllMealNutritions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> getAllMealNutritions()
        {
            List<MealNutrition> mealNutritions = await _applicationService.GetAllMealNutritions();
            return Ok(mealNutritions);
        }
    }
}