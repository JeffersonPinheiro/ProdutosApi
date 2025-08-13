using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using ProdutosApi.Models;

namespace ProdutosApi.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly IConfiguration _configuration;

        public ProdutoRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<Produto>> ListarProdutosAsync(string nome, int page, int pagesize)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var parametros = new { Nome = nome, Page = page, PageSize = pagesize };
            return await connection.QueryAsync<Produto>("sp_ListarProdutos", parametros, commandType: CommandType.StoredProcedure);
        }

        public async Task<Produto> ObterProdutoPorIdAsync(int id)
        {
            var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var parametros = new { Id = id };
            var produto = await connection.QueryFirstOrDefaultAsync<Produto>("SELECT * FROM PRODUTOS WHERE ID = @Id", parametros, commandType: CommandType.Text);
            return produto ?? throw new KeyNotFoundException($"Produto com ID {id} não encontrado.");
        }
    }
}
