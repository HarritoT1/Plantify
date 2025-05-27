using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantify.Shared
{
    public class ProductoDTO
    {
        //public string Id { get; set; } = null!;
        public string? Id { get; set; }

        //public string Nombre { get; set; } = null!;
        public string? Nombre { get; set; } 

        public string? IdGama { get; set; }

        public string? Dimensiones { get; set; }

        public string? Proveedor { get; set; }

        public string? DescriptionProduct { get; set; }

        public short? CantidadEnStock { get; set; }

        public decimal? PrecioVenta { get; set; }

        public decimal? PrecioProveedor { get; set; }

        public string? ImagenName { get; set; }
    }
}
