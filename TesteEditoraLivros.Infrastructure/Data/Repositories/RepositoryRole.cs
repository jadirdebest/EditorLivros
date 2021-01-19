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
    public class RepositoryRole : IRepositoryBase<Role>, IRepositoryRole
    {
        public void Add(Role obj)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Role>> GetAll()
        {
            string sql = "select * from Roles";

            using (SqlConnection conn = new SqlConnection(Connection.SqlConnectionString))
            {
                conn.Open();
                return await conn.QueryAsync<Role>(sql);
            }
        }

        public async Task<Role> GetById(int id)
        {
            string sql = "select * from Roles where Id = @Id";

            using (SqlConnection conn = new SqlConnection(Connection.SqlConnectionString))
            {
                conn.Open();
                return await conn.QueryFirstOrDefaultAsync<Role>(sql, new { Id = id });
            }
        }

        public void Remove(Role obj)
        {
            throw new NotImplementedException();
        }

        public void Update(Role obj)
        {
            throw new NotImplementedException();
        }
    }
}
