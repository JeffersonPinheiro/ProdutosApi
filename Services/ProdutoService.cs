using ProdutosApi.Models;
using ProdutosApi.Repositories;

namespace ProdutosApi.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }   

        public Task<IEnumerable<Produto>> ObterProdutos(string nome, int page, int pagesize)
        {
            return _produtoRepository.ListarProdutosAsync(nome, page, pagesize);
        }

        public Task<Produto> ObterProdutoPorId(int id)
        {
            return _produtoRepository.ObterProdutoPorIdAsync(id);
        }

        public Task<IEnumerable<Produto>> AdicionarProdutoAsync(Produto produto)
        {
            return _produtoRepository.AdicionarProdutoAsync(produto);
        }

        public Task<Produto?> ExcluirProduto(int id)
        {
            return _produtoRepository.ExcluirProduto(id);
        }

        public Task<IEnumerable<Produto>> AtualizarProdutoAsync(Produto produto)
        {
            return _produtoRepository.AtualizarProdutoAsync(produto);
        }
    }
}
