using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteEditoraLivros.Domain.Core.Interfaces.Repositories;
using TesteEditoraLivros.Domain.Models;
using TesteEditoraLivros.Infrastructure.Configurations;

namespace TesteEditoraLivros.Infrastructure.Data.Repositories
{
    public class RepositoryUser : IRepositoryBase<User>, IRepositoryUser
    {
        public async Task<User> AddWithReturn(User obj)
        {
            string sql = "insert into \"User\"(Email,Password,RoleId,CreatedOn,ModifiedOn,Active) " +
                         "values (@Email,@Password,@RoleId,SYSDATETIME(),SYSDATETIME(),1); select CAST(SCOPE_IDENTITY() as int)";

            using (SqlConnection conn = new SqlConnection(Connection.SqlConnectionString))
            {
                conn.Open();
                var id = await conn.QuerySingleAsync<int>(sql, obj);

                return await GetById(Convert.ToInt32(id));
            }
        }
        public async void Add(User obj)
        {
            string sql = "insert into \"User\"(Email,Password,RoleId,CreatedOn,ModifiedOn,Active) " +
                          "values (@Email,@Password,@RoleId,SYSDATETIME(),SYSDATETIME(),1)";

            using (SqlConnection conn = new SqlConnection(Connection.SqlConnectionString))
            {
                conn.Open();
                await conn.ExecuteAsync(sql, obj);
            }
        }
        public async Task<IEnumerable<User>> GetAll()
        {
            string sql = "select * from \"User\"";

            using (SqlConnection conn = new SqlConnection(Connection.SqlConnectionString))
            {
                conn.Open();
                return await conn.QueryAsync<User>(sql);
            }
        }
        public async Task<User> GetById(int id)
        {
            string sql = "select * from \"User\" where Id = @Id";

            using (SqlConnection conn = new SqlConnection(Connection.SqlConnectionString))
            {
                conn.Open();
                return await conn.QueryFirstAsync<User>(sql, new { Id = id });
            }
        }
        public async Task<User> GetByUserOrEmail(string wordIdentifier)
        {
            string sql = "select xa.RoleId, xa.Id, xa.Password,xa.Email, xa.Active " +
                        "from \"User\" xa " +
                        "join \"Register\" xb on xa.Id = xb.UserId " +
                        "where xb.NickName = @WORD or xa.Email = @WORD";

            using (SqlConnection conn = new SqlConnection(Connection.SqlConnectionString))
            {
                conn.Open();
                return await conn.QueryFirstOrDefaultAsync<User>(sql, new { WORD = wordIdentifier });
            }
        }
        public async void Remove(User obj)
        {
            string sql = "delete from \"User\" where Id = @Id";

            using (SqlConnection conn = new SqlConnection(Connection.SqlConnectionString))
            {
                conn.Open();
                await conn.ExecuteAsync(sql, obj);
            }
        }
        public async void Update(User obj)
        {
            string sql = "update \"User\" set " +
                          "RoleId = @RoleId, " +
                          "ModifiedOn = SYSDATETIME()" +
                          "where Id = @Id ";

            using (SqlConnection conn = new SqlConnection(Connection.SqlConnectionString))
            {
                conn.Open();
                await conn.ExecuteAsync(sql, obj);
            }
        }
       
    }
}
