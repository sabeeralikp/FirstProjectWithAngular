using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstProjectWithAngularCore.Entities.Books;


namespace FirstProjectWithAngularCore.IRepository.Books
{
    public interface IBooksRepository
    {
        Task<int> Create(BookModel obj);
        Task<List<BookModel>> Read();
        Task<int> Delete(int BookID);
        Task<int> UpdatePhotoPath(int BookID, string PhotoPath);
    }
}
