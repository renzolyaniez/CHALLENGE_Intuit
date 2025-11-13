using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    namespace Model
    {
        [Table("ErrorLogs")]
        public class ErrorLog
        {
            public int Id { get; set; }

            // UTC para consistencia
            public DateTime Timestamp { get; set; } = DateTime.UtcNow;

            // Nivel (por ejemplo "Error")
            public string Level { get; set; }

            // Mensaje principal de la excepción
            public string Message { get; set; }

            // Stack trace completo
            public string? StackTrace { get; set; }

            // Ruta solicitada (/api/clientes)
            public string? Path { get; set; }

            // Query string si la hay
            public string? QueryString { get; set; }

            // Usuario (si hay autenticación)
            public string? User { get; set; }

            // Mensaje de inner exception concatenado (si aplica)
            public string? InnerException { get; set; }

            // Datos adicionales en JSON (headers, body resumen, etc.)
            public string? AdditionalData { get; set; }

            // Trace identifier / correlation id para localizar la petición
            public string? CorrelationId { get; set; }
        }
    }
}
