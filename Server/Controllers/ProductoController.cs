using System;
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
    public class ProductoController : ControllerBase
    {
        private readonly JardineriaContext _context;

        public ProductoController(JardineriaContext context)
        {
            _context = context;
        }

        [HttpGet("gama/{gama_producto}")]
        public async Task<IActionResult> Get(string gama_producto)
        {
            var lista = new List<ProductoDTO>();

            foreach (var item in await _context.Productos.ToListAsync())
            {
                if (!(gama_producto == "All"))
                {
                    if (item.IdGama == gama_producto)
                    {
                        lista.Add(new ProductoDTO
                        {
                            Id = item.Id,
                            Nombre = item.Nombre,
                            IdGama = item.IdGama,
                            Dimensiones = item.Dimensiones,
                            Proveedor = item.Proveedor,
                            DescriptionProduct = item.DescriptionProduct,
                            CantidadEnStock = item.CantidadEnStock,
                            PrecioVenta = item.PrecioVenta,
                            PrecioProveedor = item.PrecioProveedor,
                            ImagenName = item.ImagenName
                        });

                        Console.WriteLine(item.Id + ", " + item.Nombre);
                    }
                }

                else {
                    lista.Add(new ProductoDTO
                    {
                        Id = item.Id,
                        Nombre = item.Nombre,
                        IdGama = item.IdGama,
                        Dimensiones = item.Dimensiones,
                        Proveedor = item.Proveedor,
                        DescriptionProduct = item.DescriptionProduct,
                        CantidadEnStock = item.CantidadEnStock,
                        PrecioVenta = item.PrecioVenta,
                        PrecioProveedor = item.PrecioProveedor,
                        ImagenName = item.ImagenName
                    });

                    Console.WriteLine(item.Id + ", " + item.Nombre);
                }
            }

            return Ok(lista);
        }
    }
}
