using CasaDeJogos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CasaDeJogos.Controllers
{
    public class HomeController : Controller
    {
        MegaSena mega = new MegaSena();
        public ActionResult Index()
        {
            ViewBag.Minino = mega.InicioDosNumerosApostados;
            ViewBag.Maximo = mega.FimDosNumerosApostados;
            ViewBag.Jogos = mega.AcertosParaGanhar;
            List<Aposta> apostas = new List<Aposta>();
            apostas = mega.ApostasFeitas();
            ViewBag.Apostas = apostas;
            return View();
        }
        public ActionResult Surpresinha(string txt7)
        {
            int quant = Convert.ToInt32(txt7);
            if (quant < 0)
                ModelState.AddModelError("", "Deve ser um numero maior que 0.");
            else
                mega.sorteioAutomatico(quant);

            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult SalvaJogos(string txt1, string txt2, string txt3, string txt4, string txt5, string txt6)
        {
            string[] results = { txt1, txt2, txt3, txt4, txt5, txt6 };
            if (results.Distinct().Count() != results.Count())
                ModelState.AddModelError("", "Não sao permitidos numeros duplicados no mesmo jogo. A aposta não pode ser cadastrada.");
            else
                mega.SalvarAposta(results);

            return RedirectToAction("Index");
        }



    }
}