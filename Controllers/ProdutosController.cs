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

        //[HttpPost]
        //public async Task<IActionResult> Post([FromBody] Produto produto)
        //{
        //    // Implementar lógica para adicionar um novo produto
        //    return CreatedAtAction(nameof(Get), new { id = produto.Id }, produto);
        //}

        //[HttpPut]
        //public async Task<IActionResult> Put([FromBody] Produto produto)
        //{
        //    // Implementar lógica para atualizar um produto existente
        //    var produtoExistente = await
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    // Implementar lógica para excluir um produto
        //    return NoContent();
        //}
    }
}
