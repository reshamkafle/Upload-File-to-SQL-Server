using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Upload_File_To_SQL_Server.Models;

namespace Upload_File_To_SQL_Server.Interfaces
{
   public interface IFileService
    {
        public Task<bool> AddFile(File file);
        public Task<IEnumerable<File>> GetAll();
        public Task<File> GetFile(string Id);
    }
}
