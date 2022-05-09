using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitApp.Api.Controllers.MenuController.Model;
using FitApp.Api.Exceptions;
using FitApp.Api.Service;
using FitApp.MealNutritionRepository.Model;
using FitApp.MealRepository.Model;
using FitApp.MenuRepository.Model;
using FitApp.NutritionRepository.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Day = FitApp.Api.Controllers.MenuController.Model.Day;

namespace FitApp.Api.Controllers.MenuController
{
    [ApiController]
    [Route("[controller]")]
    public class MenuController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public MenuController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        /// <summary>
        /// Create a New Menu
        /// </summary>
        /// <remarks>
        /// Sample request: s
        /// 
        ///     POST /createMenu
        ///     
        /// </remarks>
        /// <returns>Ok</returns>
        /// <response code="200">Returns ok</response>
        /// <response code="400">If the name is null or empty</response>
        [HttpPost("/createMenu")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateMenu(string name, List<FitApp.MenuRepository.Model.Day> days)
        {
            if (name == null) throw new ApiException.ValueCannotBeNullOrEmptyException(nameof(name));
            if (days == null || !days.Any()) throw new ApiException.ValueCannotBeNullOrEmptyException(nameof(days));
            if (days.Count != 7) throw new ApiException.MenuMustHave7Days(days.Count);
            Menu menu = await _applicationService.GetMenuByName(name);
            if (menu != null) throw new ApiException.MenuAlreadyExist(name);
            await _applicationService.CreateMenu(name, days);
            return StatusCode(StatusCodes.Status201Created);
        }

        /// <summary>
        /// Get Menu By Name
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /getMenu
        ///
        /// </remarks>
        /// <param name="menuName"></param>
        /// <returns>Ok</returns>
        /// <response code="200">Returns ok</response>
        /// <response code="400">If the menuName is null or empty</response>
        [HttpGet("/getMenu")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMenuByName(string menuName)
        {
            if (string.IsNullOrEmpty(menuName)) throw new ApiException.ValueCannotBeNullOrEmptyException(nameof(menuName));
            Menu menu = await _applicationService.GetMenuByName(menuName);
            if (menu == null) return NotFound();
            var mealGuidList = new List<Guid>();
            foreach (var day in menu.Days)
            {
                mealGuidList.AddRange(day.MealIds);
            }
            
            List<Meal> meals = await _applicationService.GetMeals(mealGuidList);
            
            var mealNutritionGuidList = new List<Guid>();
            foreach (var meal in meals)
            {
                mealNutritionGuidList.AddRange(meal.MealNutritionIds);
            }
            
            List<MealNutrition> mealNutritions = await _applicationService.GetMealNutritions(mealNutritionGuidList);
            var nutritionIds = mealNutritions.Select(mealNutrition => mealNutrition.NutritionId).ToList();
            List<Nutrition> nutritions = await _applicationService.GetNutritionsByIdList(nutritionIds);


            ResponseMenuModel response = new ResponseMenuModel();
            response.Days = new List<Day>();
            foreach (var day in menu.Days)
            {
                var mealName = "";
                var mealType = "";
                var nutritionName = "";
                var nutritionUnit = "";
                var mealNutritionTotalCalories = 0;
                var mealNutritionTotalProteins = 0;
                var isVisible = false;
                var menuMealResponses = new List<MenuMealResponse>();
                foreach (var mealId in day.MealIds)
                {
                    foreach (var meal in meals.Where(meal => mealId == meal.Id))
                    {
                        mealName = meal.Name;
                        mealType = meal.Type;
                        isVisible = meal.IsVisible;
                        foreach (var mealNutritionId in meal.MealNutritionIds)
                        {
                            foreach (var mealNutrition in mealNutritions.Where(mealNutrition => mealNutrition.Id == mealNutritionId))
                            {
                                mealNutritionTotalCalories = mealNutrition.TotalCalories;
                                mealNutritionTotalProteins = mealNutrition.TotalProtein;
                                foreach (var nutrition in nutritions.Where(nutrition => nutrition.Id == mealNutrition.NutritionId))
                                {
                                    nutritionName = nutrition.Name;
                                    nutritionUnit = nutrition.Unit;
                                }
                            }
                        }
                        menuMealResponses.Add(new MenuMealResponse()
                        {
                            MealName = mealName,
                            MealType = mealType,
                            NutritionName = nutritionName,
                            NutritionUnit = nutritionUnit,
                            NutritionTotalCalories = mealNutritionTotalCalories,
                            NutritionTotalProtein = mealNutritionTotalProteins,
                            IsVisible = isVisible
                        });
                    }
                }
                response.Days.Add(new Day()
                    {
                        MealList = menuMealResponses,
                        DailyTotalCalories = menuMealResponses.Sum(item => item.NutritionTotalCalories),
                        DailyTotalProtein = menuMealResponses.Sum(item => item.NutritionTotalProtein)
                    }
                );
            }
            return Ok(response);
        }
    }
}