using ProdutosApi.Models;

namespace ProdutosApi.Repositories
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<Produto>> ListarProdutosAsync(string nome, int page, int pageSize);
        Task<Produto> ObterProdutoPorIdAsync(int id);
        
    }
}
