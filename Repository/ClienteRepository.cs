using Data;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Model;

namespace Repository
{
    public class ClienteRepository : ICliente
    {
        private readonly Contexto _context;

        public ClienteRepository(Contexto context)
        {
            _context = context;
        }

        public async Task<List<Cliente>> GetAll()
        {
            var a = await _context.Set<Cliente>().ToListAsync();

            return a;
        }

        public async Task<Cliente> GetById(int id)
        {
            var a = await _context.Set<Cliente>().FindAsync(id);

            return a;
        }

        public async Task<(bool isSuccess, string msg)> Insert(Cliente item)
        {
            try
            {
                var a = await _context.Set<Cliente>().AddAsync(item);
                await _context.SaveChangesAsync();
                return (true, "Success");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool isSuccess, string msg)> Update(Cliente item)
        {
            try
            {
                var a = _context.Set<Cliente>().Update(item);
                await _context.SaveChangesAsync();
                return (true, "Success");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool isSuccess, string msg)> Delete(int id)
        {
            try
            {
                var cliente = await _context.Set<Cliente>().FindAsync(id);

                if (cliente == null)
                {
                    return (false, "Cliente no encontrado");
                }

                _context.Set<Cliente>().Remove(cliente);
                await _context.SaveChangesAsync();

                return (true, "Eliminado correctamente");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<List<Cliente>> Search(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                // si el filtro está vacío, devolvemos todos los clientes
                return await GetAll();
            }

            nombre = nombre.ToLower();

            return await _context.Set<Cliente>()
                .Where(c => c.Nombres.ToLower().Contains(nombre))
                .ToListAsync();
        }
    }
}
