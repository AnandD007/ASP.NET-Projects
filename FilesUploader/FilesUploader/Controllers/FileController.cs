using FilesUploader.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;


namespace FileUploader.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        public string DirectoryOriginPath = "D:\\c drive downloads\\FilesUploader";
        [HttpPost("Upload/{directoryName}")]

        /// <summary>
        /// Upload the file data by taking as Input of choose uploding file and existing directory
        /// </summary>
        /// <response code="200">  If File is Successfully Uploaded</response>
        /// <response code="400">  If File Controller parameter is Missing</response>
        /// <response code="404">  If Controller or File or Directory not Found</response>
        ///
        /// <param name="directoryName"> Enter Your Directory name: </param>
        /// <param name="file"> Upload Your File: </param>
        public IActionResult Upload([FromForm] FileUploaderHandling file, string directoryName)
        {
            var fileName = Path.GetFileName(file.FilesUploader.FileName);
            if (fileName == null || fileName.Length == 0)
                return BadRequest(fileName+" File Field is empty");
            Console.WriteLine(file.FilesUploader.Length);
            if (file.FilesUploader.Length > (10 * 1024 * 1024))
            {
                try
                {
                    var path = Path.Combine(DirectoryOriginPath, directoryName);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                        using (FileStream stream = System.IO.File.Create(Path.Combine(DirectoryOriginPath + "\\" + directoryName, file.FilesUploader.FileName)))
                        {
                            file.FilesUploader.CopyTo(stream);
                        }
                    }
                    else
                    {
                        if (System.IO.File.Exists(path + "\\" + fileName))
                        {
                            return BadRequest(fileName + " File already Exist in given directory");
                        }
                        using (FileStream stream = System.IO.File.OpenWrite(Path.Combine(DirectoryOriginPath + "\\" + directoryName, file.FilesUploader.FileName)))
                        {
                            file.FilesUploader.CopyTo(stream);
                        }
                    }
                    return Ok($"File {fileName} uploaded successfully");
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
            else
            {
                return BadRequest(file.FilesUploader.FileName+" File size is below 10MB.");
            }
        }
        /// <summary>
        /// Gets all the file data by taking as Input of filename and existing directory
        /// </summary>
        /// <response code="200">  If File is Acceptable in downloadable format</response>
        /// <response code="400">  If File Controller parameter is Missing</response>
        /// <response code="404">  If Controller or File or Directory not Found</response>
        ///
        /// <param name="fileName">Enter Your File name: </param>
        /// <param name="directoryName">Enter Your Directory: </param>
        [HttpGet("download/{fileName}, download/{directoryName}")]
        public IActionResult DownloadAsync(string fileName, string directoryName)
        {
            try
            {
                var path = Path.Combine(DirectoryOriginPath, directoryName, fileName);

                if (!System.IO.File.Exists(path))
                    return NotFound($"File {fileName} not found");

                var memory = new MemoryStream();

                using (var stream = new FileStream(path, FileMode.Open))
                {
                    stream.CopyTo(memory);
                }

                memory.Position = 0;

                return File(memory, GetContentType(path), fileName);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        /// <summary>
        /// Deletes all the file data by taking as Input of filename and existing directory
        /// </summary>
        /// <response code="200">  If File is Successfully deleted</response>
        /// <response code="400">  If File Controller parameter is Missing</response>
        /// <response code="404">  If Controller or File or Directory not Found</response>
        ///
        /// <param name="fileName"> Enter Your file name: </param>
        /// <param name="directoryName"> Enter Your Directory name: </param>
        [HttpDelete("{fileName},{directoryName}")]
        public IActionResult Delete(string fileName, string directoryName)
        {
            try
            {
                var path = Path.Combine(DirectoryOriginPath, directoryName, fileName);

                if (!System.IO.File.Exists(path))
                    return NotFound($"File {fileName} not found");

                System.IO.File.Delete(path);

                return Ok($"File {fileName} deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        private string GetContentType(string path)
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            if (!provider.TryGetContentType(path, out contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }
    }
}

