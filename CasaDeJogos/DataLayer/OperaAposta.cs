using CasaDeJogos.BusinessLayer;
using CasaDeJogos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CasaDeJogos.DataLayer
{
    public class OperaAposta
    {
        ApostaDal apostaDal = new ApostaDal();
        NumeroApostadoDal numeroDal = new NumeroApostadoDal();
        ResultadoDal resultado = new ResultadoDal();
        TipoLoteriaDal tipoLoteria = new TipoLoteriaDal();
        public static void SalvaAposta(Aposta aposta, int[] numeros)
        {
            int codAposta = new ApostaDal().Salvar(aposta);
            int quantNumeros = numeros.Length;
            List<NumeroApostado> numerosAposta = new List<NumeroApostado>();
            for (int i = 0; i < quantNumeros; i++)
            {
                NumeroApostado novoNumero =  new NumeroApostado
                {
                    CodAposta = codAposta,
                    numeroApostado = numeros[i]
                };
                novoNumero.CodNumeroApostado = new NumeroApostadoDal().Salvar(novoNumero);
                numerosAposta.Add(novoNumero);
            }
            aposta.numerosDaAposta = numerosAposta;
            aposta.CodAposta = codAposta;
            if (aposta.Sorteio)
            {
                aposta.numerosDaAposta = numerosAposta;
                aposta.CodAposta = codAposta;
                RealizaSorteio(aposta);
            }
        }

        private static void RealizaSorteio(Aposta aposta)
        {
            List<Aposta> apostaVigentes = ApostasVigentes((int)aposta.tipoLoteria, (int)ResultadoSorteio.Apostado);
            int counter = 0;
            for (int i = 0; i < apostaVigentes.Count; i++)
            {
                for (int j = 0; j < apostaVigentes[i].numerosDaAposta.Count; j++)
                {
                    for (int z = 0; z < aposta.numerosDaAposta.Count; z++)
                    {
                        if (apostaVigentes[i].numerosDaAposta[j].numeroApostado == aposta.numerosDaAposta[z].numeroApostado)
                            counter++;
                    }
                }
                apostaVigentes[i].QuantidadeAcertos = counter;
                if ((counter == aposta.numerosDaAposta.Count) || 
                    (counter == (aposta.numerosDaAposta.Count - 1)) ||
                    (counter == (aposta.numerosDaAposta.Count - 2)))
                    { apostaVigentes[i].Resultado = (int)ResultadoSorteio.Ganhou; }
                else
                    { apostaVigentes[i].Resultado = (int)ResultadoSorteio.Perdeu; }
                apostaVigentes[i].CodSorteio = aposta.CodAposta;
                counter = 0;
                new ApostaDal().Salvar(apostaVigentes[i]);
            }
        }

        public static List<Aposta> ApostasVigentes(int nomeDaLoteria, int apostado)
        {
            List<Aposta> apostas = new ApostaDal().ListarDados(" idCodLoteria = " + nomeDaLoteria + " and idCodResultado = " + apostado);
            if (apostas != null)
            {
                for (int i = 0; i < apostas.Count; i++)
                {
                    apostas[i].numerosDaAposta = new NumeroApostadoDal().ListarDados(" codAposta = " + apostas[i].CodAposta);
                }
            }
            return apostas;
        }

        public static List<Aposta> ListaSorteios(int nomeDaLoteria)
        {
            List<Aposta> apostas = new ApostaDal().ListarDados(" idCodLoteria = " + nomeDaLoteria + " and sorteio = 1");
            if (apostas != null)
            {
                for (int i = 0; i < apostas.Count; i++)
                {
                    apostas[i].numerosDaAposta = new NumeroApostadoDal().ListarDados(" codAposta = " + apostas[i].CodAposta);
                }
            }
            return apostas;
        }
    }
}