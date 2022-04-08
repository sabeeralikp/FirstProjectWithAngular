using FirstProjectWithAngularCore.Entities.Books;
using FirstProjectWithAngularCore.IRepository.Books;
using FirstProjectWithAngularCore.IService.Books;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstProjectWithAngularInfrastrucure.Service.Books
{
    public class BookService : IBooksService
    {
        private readonly IBooksRepository _repo;
        public BookService(IBooksRepository repo)
        {
            _repo = repo;
        }

        public Task<int> Create(BookModel obj, IFormFile PhotoPath)
        {
            int BookID = _repo.Create(obj).Result;
            if (BookID > 0)
            {
                var folderName = Path.Combine("Photo", "Books");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (PhotoPath != null && PhotoPath.Length > 0)
                {

                    var extension = Path.GetExtension(PhotoPath.FileName);
                    var fileName = BookID.ToString() + extension; //ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);

                    if (File.Exists(fullPath) == true)
                    {
                        File.Delete(fullPath);
                    }
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        PhotoPath.CopyTo(stream);
                        _repo.UpdatePhotoPath(BookID, fileName);
                    }
                }
            }
            return Task.FromResult(BookID);
        }
        public Task<int> Delete(int BookID)
        {
            int rowsAffected = _repo.Delete(BookID).Result;
            return Task.FromResult(rowsAffected);
        }
        public Task<List<BookModel>> Read()
        {
            return _repo.Read();
        }

    }
}
