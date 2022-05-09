using System.ComponentModel.DataAnnotations;
using FitApp.Api.Helper.AttributeHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitApp.Api.Controllers.UserController.Model
{
    public class UploadImageModel
    {
        [Required(ErrorMessage = "Please select a file.")]
        [DataType(DataType.Upload)]
        [MaxFileSize(10 * 1024 * 1024)]
        [MinFileSize(5 * 128 * 128)]
        [AllowedExtensions(new[] { ".jpg", ".png" })]
        [FromForm] public IFormFile Image { get; set; }
    }
}