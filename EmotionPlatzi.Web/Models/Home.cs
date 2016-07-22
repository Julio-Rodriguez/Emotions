using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmotionPlatzi.Web.Models
{
    public class Home
    {
        public int Id { get; set; }
        public string WelcomeMessages { get; set; }
        public string FooterMessages { get; set; } = "Footer by Juan Carlos";
        public string MensajeCualquieraProbarGit { get; set; }
    }
}