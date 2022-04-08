using Dapper;
using FirstProjectWithAngularCore.Entities.Books;
using FirstProjectWithAngularCore.IRepository.Books;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstProjectWithAngularInfrastrucure.Repository.Books
{
    public class BooksRepository : IBooksRepository
    {
        private readonly IConfiguration _config;
        public BooksRepository(IConfiguration config)
        {
            _config = config;
        }
        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        public async Task<int> Create(BookModel obj)
        {
            int BookID;
            using (IDbConnection con = Connection)
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@BookID", obj.BookID);
                parameters.Add("@BookName", obj.BookName);
                parameters.Add("@Author", obj.Author);
                parameters.Add("@BookPrice", obj.BookPrice);
                parameters.Add("@BookIDOut", DbType.Int32, direction: ParameterDirection.Output);

                await con.ExecuteAsync("spBooks_Create", parameters, commandType: CommandType.StoredProcedure);
                BookID = parameters.Get<int>("BookIDOut");
            }
            return BookID;
        }
        public async Task<List<BookModel>> Read()
        {
            IEnumerable<BookModel> books;
            using (IDbConnection con = Connection)
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DynamicParameters parameters = new DynamicParameters();
                books = await con.QueryAsync<BookModel>("spBooks_GetAll", parameters, commandType: CommandType.StoredProcedure);
                return books.ToList();
            }
        }
        public async Task<int> Delete(int BookID)
        {
            using (IDbConnection con = Connection)
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@BookID", BookID);

                return await con.ExecuteAsync("spBooks_Delete", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public async Task<int> UpdatePhotoPath(int BookID, string PhotoPath)
        {
            int rowAffected;
            using (IDbConnection con = Connection)
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@BookID", BookID);
                parameters.Add("@PhotoPath", PhotoPath);

                rowAffected = await con.ExecuteAsync("spBooks_UpdateFile", parameters, commandType: CommandType.StoredProcedure);
            }

            return rowAffected;
        }
    }
}
