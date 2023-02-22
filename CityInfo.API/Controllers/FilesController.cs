using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace CityInfo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly FileExtensionContentTypeProvider _fileExtensionContentTypeProvider;
        public FilesController(FileExtensionContentTypeProvider fileExtensionContentTypeProvider)
        {
            _fileExtensionContentTypeProvider = fileExtensionContentTypeProvider
                ?? throw new System.ArgumentNullException(nameof(fileExtensionContentTypeProvider));
        }

        [HttpGet("file")]
        public ActionResult GetFiles()
        {
            var pathToFiles = "Suzy Oborokumo.pptx";
            if (!System.IO.File.Exists(pathToFiles)) return NotFound();

            if (!_fileExtensionContentTypeProvider.TryGetContentType(pathToFiles, out var contentType))
            {
                contentType = "application/octet-stream";
            }
            var bytes = System.IO.File.ReadAllBytes(pathToFiles);
            return File(bytes, contentType, Path.GetFileName(pathToFiles));

        }
    }
}
