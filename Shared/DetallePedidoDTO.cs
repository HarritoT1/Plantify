using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantify.Shared
{
    public class DetallePedidoDTO
    {
        public int Cantidad { get; set; }

        public decimal? PrecioUnidad { get; set; }

        public short? NumeroLinea { get; set; }

        public decimal Subtotal { get; set; }

        public virtual ProductoDTO Producto { get; set; }
    }
}
