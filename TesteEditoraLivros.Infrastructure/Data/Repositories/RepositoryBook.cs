using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TesteEditoraLivros.Domain.Core.Interfaces.Repositories;
using TesteEditoraLivros.Domain.Models;
using System.Linq;
using System.Data.SqlClient;
using TesteEditoraLivros.Infrastructure.Configurations;
using Dapper;

namespace TesteEditoraLivros.Infrastructure.Data.Repositories
{
    public class RepositoryBook : IRepositoryBase<Book>, IRepositoryBook
    {
        public async void Add(Book obj)
        {
            string sql = "insert into Book(Name,PublishingCompany,Resume,Author,ISBNCode,PublicationDate,UrlImage,CreatedOn,ModifiedOn,Active) " +
                 "values(@Name,@PublishingCompany,@Resume,@Author,@ISBNCode,@PublicationDate,@UrlImage,SYSDATETIME(),SYSDATETIME(),1)";

            using(SqlConnection conn  = new SqlConnection(Connection.SqlConnectionString))
            {
                conn.Open();
                await conn.ExecuteAsync(sql, obj);
            }
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            string sql = "select * from Book";
            using(SqlConnection conn = new SqlConnection(Connection.SqlConnectionString))
            {
                conn.Open();
                return await conn.QueryAsync<Book>(sql);
            }
        }

        public async Task<Book> GetById(int id)
        {
            string sql = "select * from Book where Id = @Id";
            using (SqlConnection conn = new SqlConnection(Connection.SqlConnectionString))
            {
                conn.Open();
                return await conn.QueryFirstOrDefaultAsync<Book>(sql, new { Id = id});
            }
        }

        public async Task<IEnumerable<Book>> GetByNameOrAuthor(string keyWord)
        {
            string sql = "select * from Book " +
                         "where Name like @KeyWord or Author like @KeyWord";
            using (SqlConnection conn = new SqlConnection(Connection.SqlConnectionString))
            {
                conn.Open();
                return await conn.QueryAsync<Book>(sql, new { KeyWord = keyWord + "%" });
            }
        }

        public async Task<IEnumerable<Book>> GetByNameOrAuthorWithRangeDate(string keyWord, DateTime initialDate, DateTime finalDate)
        {
            string sql = "select * from Book " +
                         "where (Name like @KeyWord or Author like @KeyWord) " +
                         "and PublicationDate between @Initial and @Final";
            using (SqlConnection conn = new SqlConnection(Connection.SqlConnectionString))
            {
                conn.Open();
                return await conn.QueryAsync<Book>(sql, new { KeyWord = keyWord + "%",  Initial = initialDate, Final = finalDate });
            }
        }

        public async Task<IEnumerable<Book>> GetByRangeDate(DateTime initialDate, DateTime finalDate)
        {
            string sql = "select * from Book " +
                         "where PublicationDate between @Initial and @Final";
            using (SqlConnection conn = new SqlConnection(Connection.SqlConnectionString))
            {
                conn.Open();
                return await conn.QueryAsync<Book>(sql, new { Initial = initialDate , Final = finalDate });
            }
        }

        public async void Remove(Book obj)
        {
            string sql = "delete from Book where Id = @Id";

            using (SqlConnection conn = new SqlConnection(Connection.SqlConnectionString))
            {
                conn.Open();
                await conn.ExecuteAsync(sql, obj);
            }
        }

        public async void Update(Book obj)
        {
            string sql = "update Book set " +
                         "Name = @Name," +
                         "PublishingCompany = @PublishingCompany, " +
                         "Author = @Author, " +
                         "Resume = @Resume, " +
                         "ISBNCode = @ISBNCode," +
                         "PublicationDate = @PublicationDate, " +
                         "UrlImage = @UrlImage," +
                         "ModifiedOn = SYSDATETIME() " +
                         "where Id = @Id";

            using (SqlConnection conn = new SqlConnection(Connection.SqlConnectionString))
            {
                conn.Open();
                await conn.ExecuteAsync(sql, obj);
            }
        }
    }
}
