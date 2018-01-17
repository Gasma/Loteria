using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CasaDeJogos.Models
{
    public class Aposta
    {
        [Display(Name = "Data e Hora")]
        public DateTime dataDaAposta { get; set; }

        [Display(Name = "Controle")]
        public int CodAposta { get; set; }

        [Display(Name = "Numeros Apostados")]
        public List<NumeroApostado> numerosDaAposta { get; set; }

        [Display(Name = "Tipo de Loteria")]
        public int tipoLoteria { get; set; }

        [Display(Name = "Resultado")]
        public int Resultado { get; set; }

        [Display(Name = "Quantidade de Acertos")]
        public int QuantidadeAcertos { get; set; }

        public bool Sorteio { get; set; }
        public int CodSorteio { get; set; }
    }
}