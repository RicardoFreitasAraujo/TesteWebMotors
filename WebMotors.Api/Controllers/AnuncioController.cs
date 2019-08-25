using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMotors.Domain.Entities;
using WebMotors.Domain.Helpers;
using WebMotors.Domain.Interfaces.Services;

namespace WebMotors.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnuncioController : ControllerBase
    {

        private readonly IAnuncioService _servico;

        private IActionResult RetornarHttpBackEnd(RetornoBackEnd retorno, int CodigoHttp = 0)
        {
            if (retorno.sucesso)
            {
                if (CodigoHttp == 0)
                    return Ok(retorno.conteudo);
                else
                    return StatusCode(CodigoHttp, retorno.conteudo);
            }
            else
            {
                return BadRequest(retorno.conteudo);
            }
        }

        public AnuncioController(IAnuncioService servico)
        {
            this._servico = servico;
        }

        [HttpGet]
        public IActionResult Get()
        {
            RetornoBackEnd retorno = this._servico.RetornarTodos();
            return this.RetornarHttpBackEnd(retorno);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            RetornoBackEnd retorno = this._servico.LocalizarPorId(id);
            return this.RetornarHttpBackEnd(retorno);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Anuncio anuncio)
        {
            RetornoBackEnd retorno = this._servico.Adicionar(anuncio);
            return this.RetornarHttpBackEnd(retorno, 201);
        }

        [HttpPut]
        public IActionResult Put([FromRoute] int id, [FromBody] Anuncio anuncio)
        {
            RetornoBackEnd retorno = this._servico.Atualizar(anuncio);
            return this.RetornarHttpBackEnd(retorno);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            RetornoBackEnd retorno = this._servico.Excluir(id);
            return this.RetornarHttpBackEnd(retorno);
        }
    }
}
