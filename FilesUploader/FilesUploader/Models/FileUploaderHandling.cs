using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace FilesUploader.Models
{
    public class FileUploaderHandling
    {
        [Required]
        public IFormFile FilesUploader { get; set; }
    }
}