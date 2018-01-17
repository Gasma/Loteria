using CasaDeJogos.DataLayer;
using CasaDeJogos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CasaDeJogos.BusinessLayer
{
    public class OperaLoteria
    {
        Dictionary<int, string> TipoLoterias = new Dictionary<int, string>();
        Dictionary<int, string> ResultadoSorteio = new Dictionary<int, string>();
        public static List<Aposta> geradorDeAposta(bool sorteio, int qtdApostas, int inicioIntervalo, int fimIntervalo, int qtdNumeros, TipoDeLoteria tipoLoteria)
        {
            List<Aposta> apostas = new List<Aposta>();
            for (int i = 0; i < qtdApostas; i++)
            {
                int[] numeros = sorteioNumeros(qtdNumeros, inicioIntervalo, fimIntervalo);
                apostas.Add(criaAposta(sorteio, tipoLoteria, numeros));
            }
            return apostas;
        }
        private static int[] sorteioNumeros(int qtdNumeros, int inicioIntervalo, int fimIntervalo)
        {
            Random rdn = new Random();
            int[] numeros = new int[qtdNumeros];
            for (int j = 0; j < qtdNumeros; j++)
            {
                while(true)
                {
                    int teste = rdn.Next(inicioIntervalo, fimIntervalo);
                    if (!numeros.Contains(teste))
                    {
                        numeros[j] = teste;
                        break;
                    }
                }
            }
            return numeros;
        }
        /// <summary>
        /// Cria uma aposta
        /// </summary>
        /// <param name="sorteio">True se essa aposta for o sorteio e false se for uma aposta normal</param>
        /// <param name="tipoLoteria">O nome da Loteria que essa aposta pertence</param>
        /// <param name="numeros">Os numeros da aposta</param>
        /// <returns></returns>
        public static Aposta criaAposta(bool sorteio, TipoDeLoteria tipoLoteria, int[] numeros)
        {

            Aposta aposta = new Aposta
            {
                dataDaAposta = DateTime.Now,
                //horaDaAposta = DateTime.Now.TimeOfDay,
                tipoLoteria = (int)tipoLoteria,
                Resultado = (int)BusinessLayer.ResultadoSorteio.Apostado,
                Sorteio = sorteio
            };
            OperaAposta.SalvaAposta(aposta, numeros);
            return aposta;
        }

        public static List<Aposta> ListaSorteios(TipoDeLoteria nomeDaLoteria)
        {
            return OperaAposta.ListaSorteios((int)nomeDaLoteria);
        }

        public static List<Aposta> ApostasVigentes(TipoDeLoteria nomeDaLoteria)
        {
            return OperaAposta.ApostasVigentes((int)nomeDaLoteria, (int)BusinessLayer.ResultadoSorteio.Apostado);
        }


    }
}