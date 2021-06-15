using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Upload_File_To_SQL_Server.Data;
using Upload_File_To_SQL_Server.Interfaces;
using Upload_File_To_SQL_Server.Models;

namespace Upload_File_To_SQL_Server.Services
{

    public class FileService : IFileService
    {
        private readonly FileContext _context;
        public FileService(FileContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<File>> GetAll()
        {
            return await (from c in _context.File
                          select c).ToListAsync();
        }

        public async Task<File> GetFile(string Id)
        {
            return await (from c in _context.File
                          where c.Id == Id
                          select c).FirstOrDefaultAsync();
        }

        public async Task<bool> AddFile(File file)
        {
            try
            {
                file.Id = Guid.NewGuid().ToString();
                _context.File.Add(file);
                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
