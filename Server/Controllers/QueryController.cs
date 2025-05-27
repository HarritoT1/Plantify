using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Plantify.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QueryController : ControllerBase
    {
        private readonly string connectionString = "TrustServerCertificate=True; SERVER=DESKTOP-A6PIOKC; DATABASE=Jardineria; USER=sa; PASSWORD=123789;";

        // Lista de queries o procedimientos almacenados
        private readonly List<string> Querys = new()
        {
            // CONSULTAS GENERALES:
            "SELECT * FROM credencial;", // id = 0
            "SELECT * FROM cliente;", // id = 1
            "SELECT * FROM pedido;", // id = 2
            "SELECT * FROM detalle_pedido;", // id = 3
            "SELECT * FROM producto;", // id = 4
            "SELECT * FROM gama_producto;", // id = 5

            // CONSULTAS SOBRE UNA TABLA:
            "EXEC spClientesxPais_Spain_TODOS_ASC", // id = 6
            "EXEC spEstadosxPedido_Distintos_ASC", // id = 7
            "EXEC spPedidosFueraDeTiempo_FechaEsperada_DESC", // id = 8
            "EXEC spProductosOrnamentalesxStock_PrecioVenta_DESC", // id = 9
            "EXEC spClientesMadridxRepresentante_ID_ASC", // id = 10

            // CONSULTAS MULTITABLA (INNER JOIN):
            "EXEC spClientesConSusPedidos_NombrePedidoEstado_ASC", // id = 11
            "EXEC spClientesSinPedidos_NombreCliente_ASC", // id = 12
            "EXEC spPedidosDetalladosxCliente_NombrePedidoProducto_CANTIDAD", // id = 13
            "EXEC spTop5ProductosMasComprados_NombreProveedor_Cantidad_DESC", // id = 14
            "EXEC spPedidosConProveedorYPrecio_FechaProveedor_ASC", // id = 15
            "EXEC spClientesConEntregasTardias_NombreCliente_DESC", // id = 16
            "EXEC spGamasPorCliente_NombreGama_DISTINCT", // id = 17

            // CONSULTAS MULTITABLA (LEFT JOIN):
            "EXEC spClientesSinPedidosLEFT_IDNombre_ASC", // id = 18
            "EXEC spClientesConPedidosRetrasadosLEFT_NombreCliente_ASC", // id = 19
            "EXEC spProductosSinDescripcion_NombreID_ASC", // id = 20
            "EXEC spPedidosConClienteAsociadoLEFT_TODOS_ASC", // id = 21
            "EXEC spProductosNuncaPedidosLEFT_IDNombre_DESC", // id = 22
            "EXEC spClientesPedidosSinStockSuficiente_NombreCliente_DESC", // id = 23

            // CONSULTAS RESUMEN:
            "EXEC spNumClientesPorPais_Pais_Total_DESC", // id = 24
            "EXEC spNumPedidosPorEstado_Estado_Total_DESC", // id = 25
            "EXEC spPrecioVentaMaxMinProducto_MAXMIN", // id = 26
            "EXEC spTotalClientes_COUNT_ASC", // id = 27
            "EXEC spClientesMadrid_COUNT_ASC", // id = 28
            "EXEC spNumClientesCiudadesM_Ciudad_TOTAL_DESC", // id = 29
            "EXEC spClientesMas5ProductosUltimoPedido_RANK_DESC", // id = 30
            "EXEC spFechasPrimeraUltimaCompraPorCliente_Nombre_ASC", // id = 31
            "EXEC spProductosDiferentesPorPedido_ID_COUNT_DESC", // id = 32
            "EXEC spCantidadTotalProductosPorPedido_ID_SUM_DESC", // id = 33
            "EXEC spTop20ProductosMasVendidos_Nombre_TOTAL_DESC", // id = 34
            "EXEC spFacturacionTotal_SUBTOTAL_IVA_TOTAL", // id = 35
            "EXEC spFacturacionPorProducto_ID_SUBTOTAL_TOTAL", // id = 36
            "EXEC spFacturacionPorProducto_OR_ID_SUBTOTAL_TOTAL", // id = 37
            "EXEC spProductosMas3000_Nombre_Unidades_Facturacion_DESC", // id = 38
            "EXEC spPagosTotalesPorAño_TOTAL_DESC" // id = 39
        };

        [HttpGet("{idQuery}")]
        public async Task<IActionResult> GetQuery(int idQuery)
        {
            Console.WriteLine($"El cliente intenta consultar la Query con el id: {idQuery}");
            if (idQuery < 0 || idQuery >= Querys.Count)
                return BadRequest("ID de consulta inválido.");

            var dt = new DataTable();

            using (var conex = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand())
            {
                cmd.CommandText = idQuery >= 6 ? Querys[idQuery].Substring(5).Trim() : Querys[idQuery];
                cmd.Connection = conex;
                // Ajusta el tipo si es procedimiento almacenado:
                cmd.CommandType = Querys[idQuery].StartsWith("EXEC") ? CommandType.StoredProcedure : CommandType.Text;

                await conex.OpenAsync();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    dt.Load(reader);
                }
            }

            // Convertir DataTable a List<Dictionary<string, object>> para JSON:
            var rows = new List<Dictionary<string, object>>();
            foreach (DataRow row in dt.Rows)
            {
                var dict = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    dict[col.ColumnName] = row[col];
                    Console.WriteLine($"Columna: {col.ColumnName}, Valor: {dict[col.ColumnName]}");
                }
                rows.Add(dict);
                Console.WriteLine("\n");
            }

            return Ok(rows);
        }

        /*
         new List<Dictionary<string, object>>
            {
                new Dictionary<string, object> {
                    { "Id", 1 },
                    { "Nombre", "Juan" },
                    { "Edad", 25 }
                },
                new Dictionary<string, object> {
                    { "Id", 2 },
                    { "Nombre", "Mariana" },
                    { "Edad", 30 }
                }
            }
         */
    }

}
