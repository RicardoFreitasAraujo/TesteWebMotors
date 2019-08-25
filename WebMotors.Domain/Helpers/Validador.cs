using System;
using System.Collections.Generic;
using System.Text;

namespace WebMotors.Domain.Helpers
{
    /// <summary>
    /// Classe simples de Validação
    /// </summary>
    public static class Validador
    {
        public static bool Numerico(Object Expression)
        {
            if (Expression == null || Expression is DateTime)
                return false;

            if (Expression is Int16 || Expression is Int32 || Expression is Int64 || Expression is Decimal || Expression is Single || Expression is Double || Expression is Boolean)
                return true;

            try
            {
                if (Expression is string)
                    Double.Parse(Expression as string);
                else
                    Double.Parse(Expression.ToString());
                return true;
            }
            catch { } 
            return false;
        }

        public static bool Vazio(string valor)
        {
            return String.IsNullOrEmpty(valor);
        }

        public static bool Vazio(int valor)
        {
            return (valor == 0);
        }

        public static bool MaiorQue(string valor, int tamanhoMaximo)
        {
            return (valor.Length > tamanhoMaximo);
        }

        public static bool TamanhoCaracteresExatamenteQue(int valor, int tamanhoCaracteres)
        {
            string valorStr = valor.ToString();
            return (valorStr.Length == tamanhoCaracteres);
        }
    }
}
