using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebMotors.Domain.Entities.Base
{
    /// <summary>
    /// Classe Base de Entidade
    /// </summary>
    public abstract class EntidadeBase
    {
        public EntidadeBase()
        {
            this.ErrosValidacoes = new List<string>();
        }

        public int Id { get; set; }

        [NotMapped]
        public List<string> ErrosValidacoes { get; set; }

        /// <summary>
        /// Adicionar Erro de Validação na Entidade
        /// </summary>
        /// <param name="mensagem"></param>
        protected void AdicionarMsgErro(string mensagem)
        {
            if (this.ErrosValidacoes == null)
            {
                this.ErrosValidacoes = new List<string>();
            }

            this.ErrosValidacoes.Add(mensagem);
        }

        /// <summary>
        /// Retorna se Entidade é Válida
        /// </summary>
        /// <returns></returns>
        public abstract bool Validar();
    }
}
