using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitApp.Api.Exceptions;
using FitApp.Api.Helper;
using FitApp.Api.Service;
using FitApp.MealNutritionRepository.Model;
using FitApp.MealRepository.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitApp.Api.Controllers.MealController
{
    [ApiController]
    [Route("[controller]")]
    public class MealController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public MealController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        /// <summary>
        /// Create a New Meal
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /createMeal
        ///     
        /// </remarks>
        /// <param name="createMealModel"></param>
        /// <returns>Ok</returns>
        /// <response code="200">Returns ok</response>
        /// <response code="400">If the meal is null or empty</response>
        [HttpPost("/createMeal")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateMeal([FromBody] CreateMealModel createMealModel)
        {
            if (createMealModel == null) throw new ApiException.ValueCannotBeNullOrEmptyException(nameof(createMealModel));
            List<MealNutrition> mealNutritions = await _applicationService.GetMealNutritions(createMealModel.MealNutritionIds);
            if (mealNutritions == null || !mealNutritions.Any())
            {
                throw new ApiException.MealNutritionIdIsNotExistException(nameof(mealNutritions));
            }

            Meal meal = await _applicationService.GetMealByName(createMealModel.Name);
            if (meal != null)
            {
                throw new ApiException.MealNameAlreadyExist(nameof(meal.Name));
            }
            await _applicationService.CreateMeal(createMealModel.ToCreateMeal());
            return StatusCode(StatusCodes.Status201Created);
        }

        /// <summary>
        /// Get Meal By Name
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /getMeal
        ///     
        /// </remarks>
        /// <param name="mealName"></param>
        /// <returns>Ok</returns>
        /// <response code="200">Returns ok</response>
        /// <response code="400">If the mealName is null or empty</response>
        [HttpGet("/getMeal")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMeal(string mealName)
        {
            if (string.IsNullOrEmpty(mealName)) throw new ApiException.ValueCannotBeNullOrEmptyException(nameof(mealName));
            Meal meal = await _applicationService.GetMealByName(mealName);
            if (meal == null) return NotFound();
            return Ok(meal);
        }
    }
}