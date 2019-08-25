using WebMotors.Domain.Entities.Base;
using WebMotors.Domain.Helpers;

namespace WebMotors.Domain.Entities
{
    public class Anuncio : EntidadeBase
    {
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Versao { get; set; }
        public int Ano { get; set; }
        public int Quilometragem { get; set; }
        public string Observacao { get; set; }

        public override bool Validar()
        {
            //Solução "caseira" para validar entidade, há bibliotecas melhores para implementar

            this.ErrosValidacoes.Clear();
            if (Validador.Vazio(this.Marca)) this.AdicionarMsgErro("Campo marca não pode ser vazio");
            if (Validador.MaiorQue(this.Marca,45)) this.AdicionarMsgErro("Campo marca não pode ser maior que 45.");

            if (Validador.Vazio(this.Modelo)) this.AdicionarMsgErro("Campo modelo não pode ser vazio");
            if (Validador.MaiorQue(this.Modelo, 45)) this.AdicionarMsgErro("Campo modelo não pode ser maior que 45.");

            if (Validador.Vazio(this.Versao)) this.AdicionarMsgErro("Campo versao não pode ser vazio");
            if (Validador.MaiorQue(this.Versao, 45)) this.AdicionarMsgErro("Campo versao não pode ser maior que 45.");
            if (Validador.Vazio(this.Versao)) this.AdicionarMsgErro("Campo versao não pode ser vazio");

            if (Validador.Vazio(this.Ano)) this.AdicionarMsgErro("Campo ano não pode ser vazio");
            if (!Validador.TamanhoCaracteresExatamenteQue(this.Ano, 4)) this.AdicionarMsgErro("Campo ano deverá ter exatamente 4 caracteres");

            if (Validador.Vazio(this.Quilometragem)) this.AdicionarMsgErro("Campo quilometragem não pode ser vazio");
            if (Validador.Vazio(this.Observacao)) this.AdicionarMsgErro("Campo observacao não pode ser vazio");

            return (this.ErrosValidacoes.Count == 0);
        }
    }
}
