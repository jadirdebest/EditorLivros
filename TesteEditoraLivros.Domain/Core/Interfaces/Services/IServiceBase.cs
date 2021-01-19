
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TesteEditoraLivros.Domain.Core.Interfaces.Services
{
    //Eu particulamente gosto de criar serviços e repositorios base (genérico), apesar de ser dividido na comunidade que isso é uma pratica errada
    //pois nas boas práticas de programação, até mesmo no SOLID, isso fere que as interfaces e classes devem ter seu papel bem definido
    //porem ajuda muito quando se vai criar as interfaces e repositorios específicos.

    public interface IServiceBase<T> where T : class
    {
        void Add(T obj);
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();
        void Update(T obj);
        void Remove(T obj);
    }
}
