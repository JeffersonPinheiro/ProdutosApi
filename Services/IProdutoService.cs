using ProdutosApi.Models;

namespace ProdutosApi.Services
{
    public interface IProdutoService
    {
        Task<IEnumerable<Produto>> ObterProdutos(string nome, int page, int pageSize);
        Task<Produto> ObterProdutoPorId(int id);    
    }
}
