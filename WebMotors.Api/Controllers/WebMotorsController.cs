using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMotors.Api.Integracao;
using WebMotors.Api.Models;

namespace WebMotors.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class WebMotorsController: ControllerBase
    {

        private IntegradorWebMotors integrador { get; set; }
        public WebMotorsController()
        {
            this.integrador = new IntegradorWebMotors();
        }

        [HttpGet]
        [Route("RetornarMarcas")]
        public async Task<ActionResult> RetornarMarcas()
        {
            List<MarcaViewModel> marcas = await this.integrador.RetornarMarcas();
            return Ok(marcas);
        }

        [HttpGet]
        [Route("RetornarModelos/{marcaID}")]
        public async Task<ActionResult> RetornarModelos(int marcaID)
        {
            List<ModeloViewModel> modelos = await this.integrador.RetornarModelos(marcaID);
            return Ok(modelos);
        }

        [HttpGet]
        [Route("RetornarVersoes/{modeloID}")]
        public async Task<ActionResult> RetornarVersoes(int modeloID)
        {
            List<VersaoViewModel> versoes = await this.integrador.RetornarVersoes(modeloID);
            return Ok(versoes);
        }


    }
}
