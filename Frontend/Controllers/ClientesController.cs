using Frontend.Models;
using Frontend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Frontend.Controllers
{
    public class ClientesController : Controller
    {
        private readonly ClientesService _clientesService;

        public ClientesController(ClientesService clientesService)
        {
            _clientesService = clientesService;
        }

        public async Task<IActionResult> Index(string searchString)
        {
            var clientes = await _clientesService.GetClientesAsync();

            if (!string.IsNullOrEmpty(searchString))
            {
                clientes = clientes
                    .Where(c => c.Nombres.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            ViewBag.SearchString = searchString;
            return View(clientes);
        }

        public async Task<IActionResult> CreateOrEdit(int? id)
        {
            if (id == null)
                return View(new ClienteViewModel());

            var cliente = await _clientesService.GetClienteByIdAsync(id.Value);
            return View(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrEdit(ClienteViewModel cliente)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(cliente);

                bool success = cliente.Id == 0
                    ? await _clientesService.CreateClienteAsync(cliente)
                    : await _clientesService.UpdateClienteAsync(cliente.Id, cliente);

                if (success)
                    return RedirectToAction(nameof(Index));

                ViewBag.Error = "No se pudo guardar el cliente.";
                return View(cliente);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error: {ex.Message}";
                return View(cliente);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _clientesService.DeleteClienteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}