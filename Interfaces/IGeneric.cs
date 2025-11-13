using Model;

namespace Interfaces
{
    public interface IGeneric<T> where T : EntidadBase
    {
        public Task<List<T>> GetAll();

        public Task<T> GetById(int id);

        public Task<(bool isSuccess, string msg)> Insert(T item);

        public Task<(bool isSuccess, string msg)> Update(T item);

        public Task<(bool isSuccess, string msg)> Delete(int id);
    }
}
