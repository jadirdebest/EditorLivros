using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TesteEditoraLivros.Domain.Core.Interfaces.Repositories
{
    //Assim como fiz no model, eu estipulo quais regras minhas interfaces que irão ser bem definidas devem seguir, eu gosto de 
    //utilizar dessa maneira pois como no model, eu reaproveito os métodos que serão padrões pra todas as interfaces de repositorio
    public interface IRepositoryBase<T> where T : class
    {
        void Add(T obj);
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();
        void Update(T obj);
        void Remove(T obj);
    }
}
