using Dapper;
using Microsoft.Data.SqlClient;
using ProdutosApi.Models;

namespace ProdutosApi.Logs
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _config;

        public LoggingMiddleware(RequestDelegate next, IConfiguration config)
        {
            _next = next;
            _config = config;
        }

        public async Task Invoke(HttpContext context)
        {
            var ip = context.Connection.RemoteIpAddress?.ToString();
            var endpoint = context.Request.Path;
            var metodo = context.Request.Method;

            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            var log = new Log
            {
                Endpoint = endpoint,
                Metodo = metodo,
                DataHora = DateTime.UtcNow,
                IP = ip
            };
            var sql = "INSERT INTO Logs (Endpoint, Metodo, DataHora, IP) VALUES (@Endpoint, @Metodo, @DataHora, @IP)";
            await connection.ExecuteAsync(sql, log);

            await _next(context);
        }
    }
}
