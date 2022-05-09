using System.Collections.Generic;

namespace FitApp.Api.Controllers.MenuController.Model
{
    public class CreateMenuModel
    {
        public string Name { get; set; }
        public List<FitApp.MenuRepository.Model.Day> Days { get; set; }
    }
}