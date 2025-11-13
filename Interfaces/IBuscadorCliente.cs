namespace Interfaces
{
    public interface IBuscadorCliente<T>
    {
        Task<List<T>> Search(string nombre);
    }
}
