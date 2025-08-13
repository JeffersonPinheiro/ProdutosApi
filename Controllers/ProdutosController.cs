using Microsoft.AspNetCore.Mvc;
using ProdutosApi.Models;
using ProdutosApi.Services;

namespace ProdutosApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly ProdutoService _service;

        public ProdutosController(ProdutoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Produtos([FromQuery] string nome, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var produtos = await _service.ObterProdutos(nome, page, pageSize);
            return Ok(produtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ProdutoPorId(int id)
        {
            var produto = await _service.ObterProdutoPorId(id);
            if(produto == null)
            {
                return NotFound("Este produto não existe na base de dados.");
            }
            return Ok(produto);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Produto produto)
        {
            var produtoNovo = await _service.AdicionarProdutoAsync(produto);
            if (produto == null)
            {
                return BadRequest("Erro ao adicionar o produto.");
            }
            return produtoNovo.Any() ? Ok(produtoNovo) : BadRequest("Produto já existe na base de dados.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existeProduto = await _service.ObterProdutoPorId(id);
            if (existeProduto == null)
            {
                return NotFound("Este produto não existe na base de dados.");
            }
            else if (existeProduto != null)
            {
                var produtoExcluido = await _service.ExcluirProduto(id);
                return produtoExcluido != null ? Ok(produtoExcluido) : BadRequest("Erro ao excluir o produto.");
            }
            return Ok("Produto excluído com sucesso.");
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Produto produto)
        {
            var existeProduto = await _service.ObterProdutoPorId(produto.Id);
            if (existeProduto == null)
            {
                return NotFound("Este produto não existe na base de dados.");
            }
            else if (existeProduto != null)
            {
                var produtoAtualizado = await _service.AtualizarProdutoAsync(produto);
                return produtoAtualizado.Any() ? Ok(produtoAtualizado) : BadRequest("Erro ao atualizar o produto.");
            }
            return Ok("Produto atualizado com sucesso.");
        }
    }
}
