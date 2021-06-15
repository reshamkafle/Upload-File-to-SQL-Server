using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Upload_File_To_SQL_Server.Interfaces;

namespace Upload_File_To_SQL_Server.Controllers
{

    public class FileController : Controller
    {
        private readonly IFileService _fileService;
        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        public async Task<IActionResult> Index()
        {
            var files = await _fileService.GetAll();
            return View(files);
        }

        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public async Task UploadFile(IFormFile file)
        {
            if (file.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);

                    var newFile = new Models.File();
                    newFile.MimeType = file.ContentType;
                    newFile.FileName = Path.GetFileName(file.FileName);
                    newFile.Content = ms.ToArray();

                    await _fileService.AddFile(newFile);
                }
            }

            Response.Redirect("/File");
        }

        public async Task<FileResult> Download(string fileId)
        {
            var file = await _fileService.GetFile(fileId);
            return File(file.Content, file.MimeType);
        }
    }
}
