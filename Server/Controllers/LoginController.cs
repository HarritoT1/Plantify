using System;
using System.Collections.Generic;
using System.Drawing;
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
    public class LoginController : ControllerBase
    {
        private readonly JardineriaContext _context;

        public LoginController(JardineriaContext context)
        {
            _context = context;
        }

        [HttpPost("validateUser")]
        public async Task<ActionResult<ClienteDTO?>> Post(CredencialDTO credenciales)
        {
            var credencial = await _context.Credencials
                .FirstOrDefaultAsync(c => c.Username == credenciales.Username && c.Pass == credenciales.Pass);

            if (credencial == null)
            {
                return NotFound(); // 404 si no encontró usuario.
            }

            var cliente = await _context.Clientes.FindAsync(credencial.IdCliente);

            if (cliente == null)
            {
                return NotFound(); // 404 si no encontró cliente.
            }

            // Convertimos a DTO:
            var clienteDTO = new ClienteDTO
            {
                Id = cliente.Id,
                NombreCliente = cliente.NombreCliente,
                NombreContacto = cliente.NombreContacto,
                ApellidoContacto = cliente.ApellidoContacto,
                Telefono = cliente.Telefono,
                Fax = cliente.Fax,
                LineaDireccion1 = cliente.LineaDireccion1,
                LineaDireccion2 = cliente.LineaDireccion2,
                Ciudad = cliente.Ciudad,
                Region = cliente.Region,
                Pais = cliente.Pais,
                CodigoPostal = cliente.CodigoPostal,
                CodigoEmpleadoRepVentas = cliente.CodigoEmpleadoRepVentas,
                LimiteCredito = cliente.LimiteCredito,
            };

            return Ok(clienteDTO);
        }

        [HttpPost("isadmin")]
        public async Task<IActionResult> IsAdmin(CredencialDTO credenciales)
        {
            try
            {
                var isadmin = await _context.Credencials.FirstOrDefaultAsync(c => c.Username == credenciales.Username && c.Pass == credenciales.Pass);

                if (isadmin == null)
                {
                    return NotFound(); // 404
                }

                return Ok();
            }
            catch (Exception ex)
            {
                // Aquí deberías loguear el error real.
                return StatusCode(500, "Ocurrió un error al autenticar administrador.");
            }
        }
    }
}
