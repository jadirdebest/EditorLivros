using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TesteEditoraLivros.Domain.Core.Interfaces.Repositories;
using TesteEditoraLivros.Domain.Core.Interfaces.Services;

namespace TesteEditoraLivros.Domain.Services
{
    //Crio também um service base que será uma classe abstrata, pois os serviços que heradarem poderão utilizar os métodos 
    //ja definidos no escopo da interace
    public abstract class ServiceBase<T> : IServiceBase<T> where T : class
    {
        private readonly IRepositoryBase<T> _repository;

        public ServiceBase(IRepositoryBase<T> repository)
        {
            _repository = repository;
        }

        public virtual void Add(T obj)
        {
            _repository.Add(obj);
        }

        public virtual Task<IEnumerable<T>> GetAll()
        {
            return _repository.GetAll();
        }

        public virtual Task<T> GetById(int id)
        {
            return _repository.GetById(id);
        }

        public virtual void Remove(T obj)
        {
            _repository.Remove(obj);
        }

        public virtual void Update(T obj)
        {
            _repository.Update(obj);
        }
    }
}
