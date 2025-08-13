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

        public async Task<IEnumerable<Produto>> AdicionarProdutoAsync(Produto produto)
        {
            var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var parametros = new { Nome = produto.Nome, Preco = produto.Preco, Estoque = produto.Estoque };
            var novoProduto = await connection.QueryAsync<Produto>("INSERT INTO PRODUTOS VALUES (@Nome, @Preco, @Produto)", parametros, commandType: CommandType.Text);
            return novoProduto;
        }

        public Task<IEnumerable<Produto>> AtualizarProdutoAsync(Produto produto)
        {
            var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var parametros = new { Id = produto.Id, Nome = produto.Nome, Preco = produto.Preco, Estoque = produto.Estoque };
            return connection.QueryAsync<Produto>("UPDATE PRODUTOS SET NOME = @Nome, PRECO = @Preco, ESTOQUE = @Estoque WHERE ID = @Id; SELECT * FROM PRODUTOS WHERE ID = @Id", parametros, commandType: CommandType.Text);
        }

        public Task<Produto?> ExcluirProduto(int id)
        {
            var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var parametros = new { Id = id };
            return connection.QueryFirstOrDefaultAsync<Produto>("DELETE FROM PRODUTOS WHERE ID = @Id; SELECT * FROM PRODUTOS WHERE ID = @Id", parametros, commandType: CommandType.Text);
        }

        public async Task<IEnumerable<Produto>> ListarProdutosAsync(string nome, int page, int pagesize)
        {
            var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
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
