using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Plantify.Server.Models;
using Microsoft.EntityFrameworkCore;
using Plantify.Shared;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;

namespace Plantify.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly JardineriaContext _context;

        public PedidoController(JardineriaContext context)
        {
            this._context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Post(PedidoDTO pedidoDto)
        {
            try
            {
                var mdPedido = new Pedido();
                var mdDetallePedidos = new List<DetallePedido>();

                mdPedido.FechaPedido = pedidoDto.FechaPedido;
                mdPedido.FechaEsperada = pedidoDto.FechaEsperada;
                mdPedido.FechaEntrega = pedidoDto.FechaEntrega;
                mdPedido.Estado = pedidoDto.Estado;
                mdPedido.Comentarios = pedidoDto.Comentarios;
                //mdPedido.NombrePedido = pedidoDto.NombrePedido;
                mdPedido.HiddenClient = pedidoDto.HiddenClient;
                mdPedido.IdClienteNavigation = await _context.Clientes.FindAsync(pedidoDto.Cliente?.Id);

                if (mdPedido.IdClienteNavigation == null)
                {
                    return BadRequest("Pedido incorrecto."); // HTTP 400 ("Bad Request"). Solicitud mal formada o no es válida.
                }

                foreach (var item in pedidoDto.DetallePedidos)
                {
                    mdDetallePedidos.Add(new DetallePedido
                    {
                        IdProducto = item.Producto.Id,
                        Cantidad = item.Cantidad,
                        PrecioUnidad = item.PrecioUnidad,
                        NumeroLinea = item.NumeroLinea,
                        Subtotal = item.Subtotal
                    });
                }

                mdPedido.DetallePedidos = mdDetallePedidos;

                var maxId = await _context.Pedidos.MaxAsync(p => (long?)p.Id) ?? 0;
                mdPedido.Id = maxId + 1;

                mdPedido.NombrePedido = $"{pedidoDto.NombrePedido}_id_{maxId + 1}";

                _context.Pedidos.Add(mdPedido);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
