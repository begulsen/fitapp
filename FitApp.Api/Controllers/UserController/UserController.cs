using System;
using System.IO;
using System.Threading.Tasks;
using FitApp.Api.Controllers.UserController.Model;
using FitApp.Api.Exceptions;
using FitApp.Api.Helper;
using FitApp.Api.Helper.ImageHelper;
using FitApp.Api.Middleware;
using FitApp.Api.Service;
using FitApp.UserRepository.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitApp.Api.Controllers.UserController
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IApplicationService _applicationService;
        private readonly ImageHelper _imageHelper;

        public UserController(IApplicationService applicationService, ImageHelper imageHelper)
        {
            _applicationService = applicationService;
            _imageHelper = imageHelper;
        }

        /// <summary>
        /// Create a New User
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /createUser
        ///     {
        ///         "CustomerMail" : "string"
        ///         "CustomerName" : "string"
        ///         "CustomerSurname" : "string"
        ///         "PhoneNumber" : "string"
        ///         "BirthDate" : "2019-01-02T20:05:56.105Z"
        ///         "Password" : "string"
        ///         "Height" : "182.3"
        ///         "Weight" : "85.9"
        ///         "WorkoutRate" : {
        ///             None -> 0
        ///             TwoTimesWeek -> 1
        ///             FourTimesWeek -> 2
        ///             MoreThanFourTimesWeek -> 3
        ///         }
        ///         "UserStatus" : {
        ///             Basic -> 0
        ///             Premium -> 1
        ///         }
        ///         "WorkoutExperience" : {
        ///             Starter -> 0
        ///             Average -> 1
        ///             Pro -> 2
        ///         }
        ///         "Goal" : {
        ///             Power -> 0
        ///             Fit -> 1
        ///             Muscle -> 2
        ///             WeightLoss -> 3
        ///         },
        ///         "PersonalWorkoutProgram" : {
        ///             None -> 0
        ///             Pending -> 1
        ///             Approve -> 2
        ///         },
        ///         "PersonalDietProgram" : {
        ///             None -> 0
        ///             Pending -> 1
        ///             Approve -> 2
        ///         },
        ///     }
        /// </remarks>
        /// <param name="createUserModel"></param>
        /// <returns>Ok</returns>
        /// <response code="201">Returns the newly created user</response>
        /// <response code="400">If the model is null or empty</response>
        /// <response code="500"></response>
        [HttpPost("/createUser")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public ActionResult CreateUser([FromBody] CreateUserModel createUserModel)
        {
            if (createUserModel == null) return BadRequest(new ApiError(new ApiException.ValueCannotBeNullOrEmptyException(nameof(createUserModel))));
            User user = _applicationService.GetUserByMail(createUserModel.CustomerMail).GetAwaiter().GetResult();
            if (user != null) return BadRequest(new ApiError(new ApiException.UserExist(createUserModel.CustomerMail)));
            Guid customerId = Guid.NewGuid();
            _applicationService.CreateUser(createUserModel.ToCreateUser(customerId)).GetAwaiter().GetResult();
            return StatusCode(201, customerId.ToString());
        }
        
        /// <summary>
        /// Create a New User
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /createUser
        ///     {
        ///         "CustomerMail" : "string"
        ///         "CustomerName" : "string"
        ///         "CustomerSurname" : "string"
        ///         "PhoneNumber" : "string"
        ///         "BirthDate" : "2019-01-02T20:05:56.105Z"
        ///         "Height" : "182.3"
        ///         "Weight" : "85.9"
        ///         "WorkoutRate" : {
        ///             None -> 0
        ///             TwoTimesWeek -> 1
        ///             FourTimesWeek -> 2
        ///             MoreThanFourTimesWeek -> 3
        ///         }
        ///         "UserStatus" : {
        ///             Basic -> 0
        ///             Premium -> 1
        ///         }
        ///         "WorkoutExperience" : {
        ///             Starter -> 0
        ///             Average -> 1
        ///             Pro -> 2
        ///         }
        ///         "Goal" : {
        ///             Power -> 0
        ///             Fit -> 1
        ///             Muscle -> 2
        ///             WeightLoss -> 3
        ///         },
        ///         "PersonalWorkoutProgram" : {
        ///             None -> 0
        ///             Pending -> 1
        ///             Approve -> 2
        ///         },
        ///         "PersonalDietProgram" : {
        ///             None -> 0
        ///             Pending -> 1
        ///             Approve -> 2
        ///         },
        ///     }
        /// </remarks>
        /// <param name="createUserWithSocialMediaModel"></param>
        /// <returns>Ok</returns>
        /// <response code="201">Returns the newly created user</response>
        /// <response code="400">If the model is null or empty</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost("/createUserWithSocialMedia")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public ActionResult CreateUserWithSocialMediaModel([FromBody] CreateUserWithSocialMediaModel createUserWithSocialMediaModel)
        {
            if (createUserWithSocialMediaModel == null) return BadRequest(new ApiError(new ApiException.ValueCannotBeNullOrEmptyException(nameof(createUserWithSocialMediaModel))));
            User user = _applicationService.GetUserByMail(createUserWithSocialMediaModel.CustomerMail).GetAwaiter().GetResult();
            if (user != null) return BadRequest(new ApiError(new ApiException.UserExist(createUserWithSocialMediaModel.CustomerMail)));
            _applicationService.CreateUser(createUserWithSocialMediaModel.ToCreateUser(Guid.NewGuid())).GetAwaiter().GetResult();
            return StatusCode((int) 201, "User created");
        }

        /// <summary>
        /// Get User
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /getUser
        ///{
        ///          "content": "string",
        ///          "displayCustomerName": true,
        ///          "star": 0,
        ///          "media": [
        ///              "string"
        ///          ],
        ///          "deleteMediaUrls": [
        ///              "string"
        ///          ],
        ///          "featureStar": [
        ///          {
        ///              "featureId": "string",
        ///              "star": 0
        ///          }
        ///          ]
        ///      }
        /// </remarks>
        /// <returns>Ok</returns>
        /// <response code="201">Returns the newly created deeplink</response>
        /// <response code="400">If the url is null or empty</response>
        [HttpGet("/{id}/get")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUser(Guid id)
        {
            User user = await _applicationService.GetUser(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        /// <summary>
        /// Get User
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /
        /// 
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="updateUserModel"></param>
        /// <returns>Ok</returns>
        /// <response code="201">Returns the newly created deeplink</response>
        /// <response code="400">If the url is null or empty</response>
        [HttpPost("/{id}/update")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserModel updateUserModel) 
        { 
            if (id == default) return BadRequest(new ApiError(new ApiException.UserIdIsNotValidException(nameof(id))));
            if (updateUserModel == null) 
                return BadRequest(new ApiError(new ApiException.ValueCannotBeNullOrEmptyException(nameof(updateUserModel))));


            User user = await _applicationService.GetUser(id);
            if (user == null) throw new ApiException.UserIdIsNotExistException(nameof(id));
            await _applicationService.UpdateUser(updateUserModel.ToUpdateUser(user));
            return Accepted(StatusCodes.Status202Accepted);
        }

        /// <summary>
        /// Get User
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /{id}/updateUserProfilePhoto
        /// 
        /// </remarks>
        /// <returns>Ok</returns>
        /// <response code="201">Returns the newly created deeplink</response>
        /// <response code="400">If the url is null or empty</response>
        [HttpPost("/{id:guid}/uploadProfileImage")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<string> UploadUserProfileImage([FromRoute] Guid id, [FromForm] IFormFile image)
        {
            _imageHelper.ValidateMedia(image, out ApiException.BadRequestException exception);
            if (exception != null) throw exception;
            string directory = "images/" + id + "/";
            const string imageName = "profile-image";
            string fullPath = directory + imageName + Path.GetExtension(image.FileName);
            try
            {
                if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
                DirectoryInfo d = new DirectoryInfo(directory);
                FileInfo[] imageFiles = d.GetFiles();
                FileInfo oldImage = null;
                foreach (var imageFile in imageFiles)
                {
                    if (Path.GetFileNameWithoutExtension(imageFile.Name) == imageName)
                    {
                        oldImage = imageFile;
                        break;
                    }
                }
                oldImage?.Delete();
                using (FileStream filestream = System.IO.File.Create(fullPath))
                {
                    image.CopyTo(filestream);
                    filestream.Flush();
                    return Task.FromResult(fullPath);
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult(ex.ToString());
            }
        }
        
        /// <summary>
        /// Get User Photo
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /{id}/getUserPhoto
        /// 
        /// </remarks>
        /// <returns>Ok</returns>
        /// <response code="200">Returns the newly created deeplink</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet("/{id:guid}/getProfileImage")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public FileStreamResult GetUserPhoto([FromRoute] Guid id)
        {
            var path = "images/" + id + "/"; 
            DirectoryInfo d = new DirectoryInfo(path);
            FileInfo[] imageFiles = d.GetFiles();
            FileInfo userImage = null;
            foreach (var image in imageFiles)
            {
                if (image.Extension == ".png" || image.Extension == ".jpg" || image.Extension == ".jpeg")
                {
                    if (image.FullName.Contains("profile-image"))
                    {
                        userImage = image;   
                    }
                }
            }
            if (userImage == null) throw new ApiException.UserImageIsNotExistException();
            FileStream imageStr = System.IO.File.OpenRead(path + "profile-image" + userImage.Extension);
            return File(imageStr, "image/jpeg");        
        }
        
        /// <summary>
        /// Delete User Photo
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /{id}/deleteProfileImage
        /// 
        /// </remarks>
        /// <returns>Ok</returns>
        /// <response code="200">Returns the newly created deeplink</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete("/{id:guid}/deleteProfileImage")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteProfileImage([FromRoute] Guid id)
        {
            var path = "images/" + id + "/"; 
            DirectoryInfo d = new DirectoryInfo(path);
            FileInfo[] imageFiles = d.GetFiles();
            FileInfo userImage = null;
            foreach (var image in imageFiles)
            {
                if (image.Extension == ".png" || image.Extension == ".jpg" || image.Extension == ".jpeg")
                {
                    userImage = image;
                }
            }

            if (userImage == null)
            {
                throw new ApiException.UserImageIsNotExistException();
            }
            userImage.Delete();
            return Ok();
        }
        
        /// <summary>
        /// Delete User Photo
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /{id}/deleteUser
        /// 
        /// </remarks>
        /// <returns>Ok</returns>
        /// <response co"de="200">Returns the newly created deeplink</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete("/{id:guid}/deleteUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteUser([FromRoute] Guid id)
        {
            if (id == default) return BadRequest(new ApiError(new ApiException.UserIdIsNotValidException(nameof(id))));
            User user = _applicationService.GetUser(id).GetAwaiter().GetResult();
            if (user == null) return BadRequest(new ApiError(new ApiException.UserNotExist(id.ToString())));
            _applicationService.DeleteUser(id).GetAwaiter().GetResult();
            _applicationService.DeleteUserPrivateDiet(id).GetAwaiter().GetResult();
            _applicationService.DeleteUserPrivateDietDetail(id).GetAwaiter().GetResult();
            _applicationService.DeleteUserPrivateTraining(id).GetAwaiter().GetResult();
            _applicationService.DeleteUserPrivateTrainingDetail(id).GetAwaiter().GetResult();
            
            
            var path = "images/" + id + "/"; 
            DirectoryInfo d = new DirectoryInfo(path);
            if (d.Exists) 
                d.Delete(true);
            return Ok();
        }
        
        /// <summary>
        /// User Login
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /login
        /// 
        /// </remarks>
        /// <returns>Ok</returns>
        /// <response code="200">Return ok if successfully log in</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost("/login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Login(string mail, string password)
        {
            if (mail == default) throw new ApiException.ValueCannotBeNullOrEmptyException(nameof(mail));
            if (password == default) throw new ApiException.ValueCannotBeNullOrEmptyException(nameof(password));
            User user = _applicationService.GetUserByMail(mail).GetAwaiter().GetResult();
            if (user == null) return NotFound(new ApiError(new ApiException.UserNotExist(mail)));
            if (user.Password == null) return BadRequest(new ApiError(new ApiException.UserRegisterWithSocialException(mail)));
            if (user.Password == password) return Ok(); 
            return BadRequest("Password didnt match");
        }
        
        /// <summary>
        /// User Login
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /loginWithSocail
        /// 
        /// </remarks>
        /// <returns>Ok</returns>
        /// <response code="200">Return ok if successfully log in</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost("/loginWithSocial")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult LoginWithSocial(string mail)
        {
            if (mail == default) throw new ApiException.ValueCannotBeNullOrEmptyException(nameof(mail));
            User user = _applicationService.GetUserByMail(mail).GetAwaiter().GetResult();
            if (user == null) return NotFound(new ApiError(new ApiException.UserNotExist(mail))); 
            return Ok(); 
        }
    }
}