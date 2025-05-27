using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Plantify.Server.Models;
using Plantify.Shared;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Plantify.Server.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ListPedidoController : ControllerBase
    {
        private readonly JardineriaContext _context;

        public ListPedidoController(JardineriaContext context)
        {
            _context = context;
        }

        [HttpGet("pedidosUser/{idCliente}")]
        public async Task<IActionResult> Get(string idCliente)
        {
            var lista = new List<PedidoDTO>();

            foreach (var item in await _context.Pedidos.ToListAsync())
            {

                if (Convert.ToString(item.IdCliente) == idCliente)
                {

                    var DetallePedidosDTO = new List<DetallePedidoDTO>();

                    foreach (var det in item.DetallePedidos)
                    {
                        DetallePedidosDTO.Add(new DetallePedidoDTO
                        {
                            Cantidad = det.Cantidad,
                            PrecioUnidad = det.PrecioUnidad,
                            NumeroLinea = det.NumeroLinea,
                            Subtotal = det.Subtotal,
                            Producto = new ProductoDTO
                            {
                                Id = det.IdProductoNavigation?.Id,
                                Nombre = det.IdProductoNavigation?.Nombre,
                                IdGama = det.IdProductoNavigation?.IdGama,
                                Dimensiones = det.IdProductoNavigation?.Dimensiones,
                                Proveedor = det.IdProductoNavigation?.Proveedor,
                                DescriptionProduct = det.IdProductoNavigation?.DescriptionProduct,
                                CantidadEnStock = det.IdProductoNavigation?.CantidadEnStock,
                                PrecioVenta = det.IdProductoNavigation?.PrecioVenta,
                                PrecioProveedor = det.IdProductoNavigation?.PrecioProveedor,
                                ImagenName = det.IdProductoNavigation?.ImagenName
                            }
                        });
                    }

                    lista.Add(new PedidoDTO
                    {
                        FechaPedido = item.FechaPedido,
                        FechaEsperada = item.FechaEsperada,
                        FechaEntrega = item.FechaEntrega,
                        Estado = item.Estado,
                        Comentarios = item.Comentarios,
                        NombrePedido = item.NombrePedido,
                        HiddenClient = item.HiddenClient,
                        DetallePedidos = DetallePedidosDTO,
                        Cliente = new ClienteDTO
                        {
                            Id = item.IdClienteNavigation?.Id ?? 0,
                            NombreCliente = item.IdClienteNavigation?.NombreCliente ?? "",
                            NombreContacto = item.IdClienteNavigation?.NombreContacto,
                            ApellidoContacto = item.IdClienteNavigation?.ApellidoContacto,
                            Telefono = item.IdClienteNavigation?.Telefono,
                            Fax = item.IdClienteNavigation?.Fax,
                            LineaDireccion1 = item.IdClienteNavigation?.LineaDireccion1,
                            LineaDireccion2 = item.IdClienteNavigation?.LineaDireccion2,
                            Ciudad = item.IdClienteNavigation?.Ciudad,
                            Region = item.IdClienteNavigation?.Region,
                            Pais = item.IdClienteNavigation?.Pais,
                            CodigoPostal = item.IdClienteNavigation?.CodigoPostal,
                            CodigoEmpleadoRepVentas = item.IdClienteNavigation?.CodigoEmpleadoRepVentas,
                            LimiteCredito = item.IdClienteNavigation?.LimiteCredito,
                        }
                    });

                    Console.WriteLine(item.Id + ", " + item.NombrePedido);
                }
            }

            return Ok(lista);
        }

        [HttpPut("updatePedido/{namePedido}")]
        public async Task<IActionResult> UpdatePedido(string namePedido)
        {
            try
            {
                var pedidoEspecifico = await _context.Pedidos.FirstOrDefaultAsync(p => p.NombrePedido == namePedido);

                if (pedidoEspecifico == null)
                {
                    return NotFound(); // 404
                }

                pedidoEspecifico.HiddenClient = true;

                // Opción 1 - si solo modificas una propiedad:
                _context.Entry(pedidoEspecifico).Property(p => p.HiddenClient).IsModified = true;

                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                // Aquí deberías loguear el error real.
                return StatusCode(500, "Ocurrió un error al actualizar el pedido.");
            }
        }

        [HttpGet("getPedido/{namePedido}")]
        public async Task<ActionResult<PedidoDTO?>> GetPedido(string namePedido)
        {
            var pedido = await _context.Pedidos
                    .Include(p => p.IdClienteNavigation)
                    .Include(p => p.DetallePedidos)
                        .ThenInclude(dp => dp.IdProductoNavigation)
                    .FirstOrDefaultAsync(p => p.NombrePedido == namePedido);


            if (pedido == null)
            {
                return NotFound(); // 404 si no encontró el pedido.
            }

            var DetallePedidosDTO = new List<DetallePedidoDTO>();

            foreach (var det in pedido.DetallePedidos)
            {
                DetallePedidosDTO.Add(new DetallePedidoDTO
                {
                    Cantidad = det.Cantidad,
                    PrecioUnidad = det.PrecioUnidad,
                    NumeroLinea = det.NumeroLinea,
                    Subtotal = det.Subtotal,
                    Producto = new ProductoDTO
                    {
                        Id = det.IdProductoNavigation?.Id,
                        Nombre = det.IdProductoNavigation?.Nombre,
                        IdGama = det.IdProductoNavigation?.IdGama,
                        Dimensiones = det.IdProductoNavigation?.Dimensiones,
                        Proveedor = det.IdProductoNavigation?.Proveedor,
                        DescriptionProduct = det.IdProductoNavigation?.DescriptionProduct,
                        CantidadEnStock = det.IdProductoNavigation?.CantidadEnStock,
                        PrecioVenta = det.IdProductoNavigation?.PrecioVenta,
                        PrecioProveedor = det.IdProductoNavigation?.PrecioProveedor,
                        ImagenName = det.IdProductoNavigation?.ImagenName
                    }
                });
            }

            // Convertimos a DTO:
            var pedidoDTO = new PedidoDTO
            {
                FechaPedido = pedido.FechaPedido,
                FechaEsperada = pedido.FechaEsperada,
                FechaEntrega = pedido.FechaEntrega,
                Estado = pedido.Estado,
                Comentarios = pedido.Comentarios,
                NombrePedido = pedido.NombrePedido,
                HiddenClient = pedido.HiddenClient,
                DetallePedidos = DetallePedidosDTO,
                Cliente = new ClienteDTO
                {
                    Id = pedido.IdClienteNavigation?.Id ?? 0,
                    NombreCliente = pedido.IdClienteNavigation?.NombreCliente ?? "",
                    NombreContacto = pedido.IdClienteNavigation?.NombreContacto,
                    ApellidoContacto = pedido.IdClienteNavigation?.ApellidoContacto,
                    Telefono = pedido.IdClienteNavigation?.Telefono,
                    Fax = pedido.IdClienteNavigation?.Fax,
                    LineaDireccion1 = pedido.IdClienteNavigation?.LineaDireccion1,
                    LineaDireccion2 = pedido.IdClienteNavigation?.LineaDireccion2,
                    Ciudad = pedido.IdClienteNavigation?.Ciudad,
                    Region = pedido.IdClienteNavigation?.Region,
                    Pais = pedido.IdClienteNavigation?.Pais,
                    CodigoPostal = pedido.IdClienteNavigation?.CodigoPostal,
                    CodigoEmpleadoRepVentas = pedido.IdClienteNavigation?.CodigoEmpleadoRepVentas,
                    LimiteCredito = pedido.IdClienteNavigation?.LimiteCredito,
                }
            };

            Console.WriteLine($"Aqui: {pedido.IdClienteNavigation?.NombreCliente}");
            Console.WriteLine($"Se retorna del controlador el pedido: {pedidoDTO.NombrePedido} del cliente con el nombre: {pedidoDTO.Cliente?.NombreCliente ?? "N/A"}");

            return Ok(pedidoDTO);
        }
    }
}
