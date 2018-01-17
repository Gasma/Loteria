using CasaDeJogos.Models;
using System.Collections.Generic;

namespace CasaDeJogos.BusinessLayer
{
    interface ILoteria
    {
        int AcertosParaGanhar { get; }
        int InicioDosNumerosApostados { get; }
        int FimDosNumerosApostados { get; }
        TipoDeLoteria NomeDaLoteria { get; }
        string GanhadorMaximo { get; }
        string GanhadorSecundario { get; }
        string GanhadorTerciario { get; }
        void sorteioAutomatico(int qtdApostas);
        Aposta sortearGanhador();
        void SalvarAposta(string[] numeros);
        List<Aposta> ApostasFeitas();
        List<Aposta> UltimosSorteios();
    }
}
