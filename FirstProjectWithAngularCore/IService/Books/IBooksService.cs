using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstProjectWithAngularCore.Entities.Books;
using Microsoft.AspNetCore.Http;

namespace FirstProjectWithAngularCore.IService.Books
{
    public interface IBooksService
    {
        Task<int> Create(BookModel obj, IFormFile PhotoPath);
        Task<List<BookModel>> Read();
        Task<int> Delete(int BookID);
    }
}
