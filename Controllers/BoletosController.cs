using Microsoft.AspNetCore.Mvc;
using BoletosApi.Models;
using BoletosApi.Repositories;
using BoletosApi.Dtos;
using AutoMapper;

namespace BoletosApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BoletosController : ControllerBase
    {
        private readonly IBoletoRepository _repo;

        public BoletosController(IBoletoRepository repo)
        {
            _repo = repo;
        }

        // POST /api/Boletos
        [HttpPost]
        public async Task<IActionResult> Create(BoletoCreateDto dto)
        {
            var boleto = new Boleto
            {
                Id = dto.Id,
                NomePagador = dto.NomePagador,
                CpfCnpjPagador = dto.CpfCnpjPagador,
                NomeBeneficiario = dto.NomeBeneficiario,
                CpfCnpjBeneficiario = dto.CpfCnpjBeneficiario,
                Valor = dto.Valor,
                DataVencimento = dto.DataVencimento,
                Observacao = dto.Observacao,
                BancoId = dto.BancoId
            };

            boleto.DataVencimento = DateTime.SpecifyKind(boleto.DataVencimento, DateTimeKind.Unspecified);
            await _repo.AddAsync(boleto);
            return CreatedAtAction(nameof(GetById), new { id = boleto.Id }, boleto);
        }

        // GET /api/Boletos
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var boletos = await _repo.GetAllAsync();
            return Ok(boletos);
        }

        // GET /api/Boletos/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var boleto = await _repo.GetByIdAsync(id);
            if (boleto == null)
                return NotFound();
            return Ok(boleto);
        }

        // PUT /api/Boletos/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, BoletoCreateDto dto)
        {
            var boletoExistente = await _repo.GetByIdAsync(id);
            if (boletoExistente == null)
                return NotFound();

            boletoExistente.NomePagador = dto.NomePagador;
            boletoExistente.CpfCnpjPagador = dto.CpfCnpjPagador;
            boletoExistente.NomeBeneficiario = dto.NomeBeneficiario;
            boletoExistente.CpfCnpjBeneficiario = dto.CpfCnpjBeneficiario;
            boletoExistente.Valor = dto.Valor;
            boletoExistente.DataVencimento = dto.DataVencimento;
            boletoExistente.Observacao = dto.Observacao;
            boletoExistente.BancoId = dto.BancoId;

            await _repo.UpdateAsync(boletoExistente);
            return Ok(boletoExistente);
        }

        // DELETE /api/Boletos/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var boleto = await _repo.GetByIdAsync(id);
            if (boleto == null)
                return NotFound();

            await _repo.DeleteAsync(boleto);
            return NoContent();
        }
    }
}