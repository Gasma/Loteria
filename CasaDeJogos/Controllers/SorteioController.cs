using CasaDeJogos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CasaDeJogos.Controllers
{
    public class SorteioController : Controller
    {
        MegaSena mega = new MegaSena();
        public ActionResult Index()
        {
            List<Aposta> sorteios = mega.UltimosSorteios();
            ViewBag.Sorteio = sorteios;
            return View();
        }
        public ActionResult Sortear()
        {
            mega.sortearGanhador();
            return RedirectToAction("Index");
        }
    }
}
