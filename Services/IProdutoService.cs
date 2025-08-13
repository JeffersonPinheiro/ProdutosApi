using ProdutosApi.Models;

namespace ProdutosApi.Services
{
    public interface IProdutoService
    {
        Task<IEnumerable<Produto>> ObterProdutos(string nome, int page, int pageSize);
        Task<Produto> ObterProdutoPorId(int id);
        Task<IEnumerable<Produto>> AdicionarProdutoAsync(Produto produto);
        Task<Produto?> ExcluirProduto(int id);
        Task<IEnumerable<Produto>> AtualizarProdutoAsync(Produto produto);
    }
}
