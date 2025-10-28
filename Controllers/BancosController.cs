using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using BoletosApi.Models;
using BoletosApi.Repositories;
using BoletosApi.Dtos;

namespace BoletosApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BancosController : ControllerBase
    {
        private readonly IBancoRepository _repo;

        public BancosController(IBancoRepository repo)
        {
            _repo = repo;
        }

        // POST /api/Bancos
        [HttpPost]
        public async Task<IActionResult> Create(BancoCreateDto dto)
        {
            var banco = new Banco
            {
                Id = dto.Id,
                Nome = dto.Nome,
                Codigo = dto.Codigo,
                PercentualJuros = dto.PercentualJuros
            };

            await _repo.AddAsync(banco);
            return CreatedAtAction(nameof(GetById), new { id = banco.Id }, banco);
        }

        // GET /api/Bancos
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var bancos = await _repo.GetAllAsync();
            return Ok(bancos);
        }

        // GET /api/Bancos/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var banco = await _repo.GetByIdAsync(id);
            if (banco == null)
                return NotFound();
            return Ok(banco);
        }

        // GET /api/Bancos/codigo/{codigo}
        [HttpGet("codigo/{codigo}")]
        public async Task<IActionResult> GetByCodigo(string codigo)
        {
            var banco = await _repo.GetByCodigoAsync(codigo);
            if (banco == null)
                return NotFound();
            return Ok(banco);
        }

        // PUT /api/Bancos/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, BancoCreateDto dto)
        {
            var bancoExistente = await _repo.GetByIdAsync(id);
            if (bancoExistente == null)
                return NotFound();

            bancoExistente.Nome = dto.Nome;
            bancoExistente.Codigo = dto.Codigo;
            bancoExistente.PercentualJuros = dto.PercentualJuros;

            await _repo.UpdateAsync(bancoExistente);
            return Ok(bancoExistente);
        }

        // DELETE /api/Bancos/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var banco = await _repo.GetByIdAsync(id);
            if (banco == null)
                return NotFound();

            await _repo.DeleteAsync(banco);
            return NoContent();
        }
    }
}
