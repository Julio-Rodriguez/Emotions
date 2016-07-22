using EmotionPlatzi.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmotionPlatzi.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.WelcomeMessage = "Hola Mundo";
            ViewBag.ValorEntero = "Valor entero";
            return View();
        }
        public ActionResult IndexAlt()
        {
            var modelo = new Home();
            modelo.WelcomeMessages = "Hola mundo desde el modelo";
            modelo.MensajeCualquieraProbarGit = "Bienbenido Julio";
            /*YO mero molero desde Neptuno*/

            return View(modelo);
        }
        public ActionResult Vista()
        {
            ViewBag.Ente = 2;
            return View();
        }
    }
}