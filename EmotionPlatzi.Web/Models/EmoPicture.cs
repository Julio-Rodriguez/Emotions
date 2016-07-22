using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmotionPlatzi.Web.Models
{
    public class EmoPicture
    {
        public int Id { get; set; } 
        //se agrega un atributo a Id con lo siguiente
        [Display (Name="Nombre")]      
        public string Name { get; set; }
        public string Path { get; set; }

        public virtual ObservableCollection<EmoFace> Faces { get; set; }
    }
}