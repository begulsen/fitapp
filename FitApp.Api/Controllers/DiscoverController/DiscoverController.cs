using System.Collections.Generic;
using System.Threading.Tasks;
using FitApp.Api.Service;
using FitApp.MenuRepository.Model;
using FitApp.TrainingRepository.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitApp.Api.Controllers.DiscoverController
{
    [ApiController]
    [Route("[controller]")]
    public class DiscoverController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public DiscoverController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        /// <summary>
        /// Get All Training For Discover
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /getTrainings
        ///     
        /// </remarks>
        /// <returns>Ok</returns>
        /// <response code="200">Returns ok</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet("/getAllTrainings")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllTrainings()
        {
            List<Training> trainings = await _applicationService.GetAllTrainings();
            return Ok(trainings);
        }
        
        /// <summary>
        /// Get All Menus For Discover
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /getMenus
        ///     
        /// </remarks>
        /// <returns>Ok</returns>
        /// <response code="200">Returns ok</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet("/getAllMenus")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllMenus()
        {
            List<Menu> menus = await _applicationService.GetAllMenus();
            return Ok(menus);
        }
    }
}