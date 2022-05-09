using System.Collections;
using System.IO;
using FitApp.Api.Exceptions;
using Microsoft.AspNetCore.Http;

namespace FitApp.Api.Helper.ImageHelper
{
    public class ImageHelper
    {
        private readonly string[] _extensions = {".jpg",".png", ".jpeg"};
        private readonly long _minFileSize = 32 * 32 * 10;
        private readonly long _maxFileSize = 1024 * 1024 * 10;
        public void ValidateMedia(IFormFile image, out ApiException.BadRequestException exception)
        {
            exception = null;
            if (image != null)
            {
                var extension = Path.GetExtension(image.FileName);
                if (!((IList)_extensions).Contains(extension.ToLower()))
                {
                    exception = new ApiException.MediaExtensionNotValidException();
                    return;
                }

                if (image.Length < _minFileSize)
                {
                    exception = new ApiException.MediaMinSizeNotValidException();
                    return;
                }
                
                if (image.Length > _maxFileSize)
                {
                    exception = new ApiException.MediaMaxSizeNotValidException();
                    return;
                }
            }
        }
    }
}