using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebMotors.Api.Models;

namespace WebMotors.Api.Integracao
{
    public class IntegradorWebMotors
    {
        private readonly IConfiguration _config;

        private string endPointAPI { get; set; }
        private HttpClient http { get; set; }

        public IntegradorWebMotors()
        {
            //this._config = config;
            this.http = new HttpClient();
            this.endPointAPI = "http://desafioonline.webmotors.com.br/api/OnlineChallenge"; //Refatorar
        }

        /// <summary>
        /// Retornar Marcas
        /// </summary>
        /// <returns></returns>
        public async Task<List<MarcaViewModel>> RetornarMarcas()
        {
            HttpResponseMessage resposta = await this.http.GetAsync(this.endPointAPI + "/Make");
            resposta.EnsureSuccessStatusCode();
            List<MarcaViewModel> retorno = await resposta.Content.ReadAsAsync<List<MarcaViewModel>>();
            return retorno;
        }

        /// <summary>
        /// Retornar Modelos
        /// </summary>
        /// <param name="marcaID">Id da Marca</param>
        /// <returns></returns>
        public async Task<List<ModeloViewModel>> RetornarModelos(int marcaID)
        {
            HttpResponseMessage resposta = await this.http.GetAsync(this.endPointAPI + "/Model?MakeID=" + marcaID.ToString());
            resposta.EnsureSuccessStatusCode();
            List<ModeloViewModel> retorno = await resposta.Content.ReadAsAsync<List<ModeloViewModel>>();
            return retorno;
        }

        /// <summary>
        /// Retornar Versões
        /// <param name="modeloID">Id do modelo</param>
        /// <returns></returns>
        public async Task<List<VersaoViewModel>> RetornarVersoes(int modeloID)
        {
            HttpResponseMessage resposta = await this.http.GetAsync(this.endPointAPI + "/Version?ModelID=" + modeloID.ToString());
            resposta.EnsureSuccessStatusCode();
            List<VersaoViewModel> retorno = await resposta.Content.ReadAsAsync<List<VersaoViewModel>>();
            return retorno;
        }
    }
}
