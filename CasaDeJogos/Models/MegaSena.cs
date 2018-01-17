using CasaDeJogos.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CasaDeJogos.Models
{
    public class MegaSena : ILoteria
    {
        #region PropriedadesDaClasse
        public TipoDeLoteria NomeDaLoteria
        {
            get { return TipoDeLoteria.MegaSena; }
        }
        public string GanhadorMaximo
        {
            get { return "Mega"; }
        }
        public string GanhadorSecundario
        {
            get { return "Quina"; }
        }
        public string GanhadorTerciario
        {
            get { return "Quadra"; }
        }
        public int AcertosParaGanhar
        {
            get { return 6; }
        }
        public int FimDosNumerosApostados
        {
            get { return 60; }
        }

        public int InicioDosNumerosApostados
        {
            get { return 1; }
        }
        #endregion
        public void sorteioAutomatico(int qtdApostas)
        {
            OperaLoteria.geradorDeAposta(false, qtdApostas, InicioDosNumerosApostados, FimDosNumerosApostados, AcertosParaGanhar, NomeDaLoteria);
        }
        public void SalvarAposta(string[] numeros)
        {
            int[] apostas = new int[numeros.Length];
            for (int i = 0; i < numeros.Length; i++)
            {
                apostas[i] = Convert.ToInt32(numeros[i]);
            }
            OperaLoteria.criaAposta(false, NomeDaLoteria, apostas);
        }
        public Aposta sortearGanhador()
        {
            return OperaLoteria.geradorDeAposta(true, 1, InicioDosNumerosApostados, FimDosNumerosApostados, AcertosParaGanhar, NomeDaLoteria).First();
        }

        public List<Aposta> ApostasFeitas()
        {
            return OperaLoteria.ApostasVigentes(NomeDaLoteria);
        }

        public List<Aposta> UltimosSorteios()
        {
            return OperaLoteria.ListaSorteios(NomeDaLoteria);
        }
    }
}