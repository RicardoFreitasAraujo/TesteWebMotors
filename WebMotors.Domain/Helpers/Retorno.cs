using System;
using System.Collections.Generic;
using System.Text;

namespace WebMotors.Domain.Helpers
{
    /// <summary>
    /// Classe genérica que simplifica o retorno das camadas
    /// </summary>
    public class RetornoBackEnd
    {
        public RetornoBackEnd(bool sucesso, List<string> erros, object conteudo)
        {
            this.sucesso = sucesso;
            this.erros = erros;
            this.conteudo = conteudo;
        }

        public RetornoBackEnd(object conteudo)
        {
            this.sucesso = true;
            this.erros = new List<string>();
            this.conteudo = conteudo;
        }

        public RetornoBackEnd(Exception exception)
        {
            this.sucesso = false;
            this.erros = new List<string>();
            this.erros.Add(exception.Message);
            this.conteudo = exception;
        }

        public bool sucesso { get; set; }
        public List<string> erros { get; set; }
        public object conteudo { get; set; }


    }
}
