using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using TesteEditoraLivros.Domain.Core.Interfaces.Repositories;
using TesteEditoraLivros.Domain.Models;
using TesteEditoraLivros.Infrastructure.Configurations;

namespace TesteEditoraLivros.Infrastructure.Data.Repositories
{
    public class RepositoryRegister : IRepositoryBase<Register>, IRepositoryRegister
    {
        public async void Add(Register obj)
        {
            string sql = "insert into Register(UserId,Name,NickName,AvatarUrl,CreatedOn,ModifiedOn,Active) " +
                         "values (@UserId,@Name,@NickName,@AvatarUrl,SYSDATETIME(),SYSDATETIME(),1)";

            using (SqlConnection conn = new SqlConnection(Connection.SqlConnectionString))
            {
               conn.Open();
               await conn.ExecuteAsync(sql,obj);
            }
        }

        public async Task<IEnumerable<Register>> GetAll()
        {
            string sql = "select xa.Id,xa.Name,xa.UserId,xa.AvatarUrl,xa.NickName,xb.RoleId,xb.Email " +
                         "from Register xa " +
                         "join \"User\" xb on xb.Id = xa.UserId ";

            using (SqlConnection conn = new SqlConnection(Connection.SqlConnectionString))
            {
                conn.Open();
                return await conn.QueryAsync<Register>(sql);
            }
        }

        public async Task<Register> GetById(int id)
        {
            string sql = "select * " +
                      "from Register xa " +
                      "join \"User\" xb on xb.Id = xa.UserId " +
                      "where xa.Id = @Id";

            using (SqlConnection conn = new SqlConnection(Connection.SqlConnectionString))
            {
                conn.Open();
                return await conn.QueryFirstAsync<Register>(sql,new { Id = id });
            }
        }

        public async void Remove(Register obj)
        {
            string sql = "delete from Register where Id = @Id";

            using (SqlConnection conn = new SqlConnection(Connection.SqlConnectionString))
            {
                conn.Open();
                await conn.ExecuteAsync(sql, obj);
            }
        }

        public async void Update(Register obj)
        {
            string sql = "update Register set " +
                          "Name = @Name," +
                          "NickName = @NickName," +
                          "AvatarUrl = @AvatarUrl," +
                          "ModifiedOn = SYSDATETIME() " +
                          "where Id = @Id";

            using (SqlConnection conn = new SqlConnection(Connection.SqlConnectionString))
            {
                try
                {
                    conn.Open();
                    await conn.ExecuteAsync(sql, obj);
                }
                catch (Exception ex)
                {
                    throw;
                }
             
            }
        }
    }
}
