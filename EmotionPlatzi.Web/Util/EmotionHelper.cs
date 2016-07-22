using EmotionPlatzi.Web.Models;
using Microsoft.ProjectOxford.Emotion;
using Microsoft.ProjectOxford.Emotion.Contract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Threading.Tasks;

namespace EmotionPlatzi.Web.Util
{
    public class EmotionHelper
    {
        public EmotionServiceClient emoClient;

        //constructor para inicializar la librería emociones
        public EmotionHelper(string key)
        {
            emoClient = new EmotionServiceClient(key);
            
        }
        public async Task<EmoPicture> DetectAndExtractFacesAsync (Stream imageStream)
        {
            //se crea un arreglo de emociones de la librería importada
            Emotion[] emotions = await emoClient.RecognizeAsync(imageStream);

            //se declara un objeto de tipo del objeto raiz de nuestro proyecto
            EmoPicture emopicture = new EmoPicture();
            //se le asignan los faces por que el nombre y el pat ya lo tiene asignado en el controlador
            emopicture.Faces = DetectAndExtractFaces(emotions, emopicture);


            return emopicture;
        }

        private ObservableCollection<EmoFace> DetectAndExtractFaces(Emotion[] emotions, EmoPicture emopicture)
        {
            //aqui declaramos un objeto que se usará para agregar datos a todas las clases
            var listaFaces = new ObservableCollection<EmoFace>();

            
            foreach (var emotion in emotions)
            {
                var emoface = new EmoFace()
                {
                    X = emotion.FaceRectangle.Left,
                    Y = emotion.FaceRectangle.Top,
                    Width = emotion.FaceRectangle.Width,
                    Height = emotion.FaceRectangle.Height,
                    Picture = emopicture
                };                

                emoface.Emotion = ProcessEmotion(emotion.Scores,emoface);
                listaFaces.Add(emoface);
            }
            return listaFaces;
        }

        //esta lista nos devuelve un arreglo de las emociones float
        private ObservableCollection<EmoEmotion> ProcessEmotion(Scores scores, EmoFace emoface)
        {
            //declaramos que será modificando EmoEmotion
            //Esta lista guardará el valor de todos los estados de animo de nuestra lista emoemotion
            var EmotionList = new ObservableCollection<EmoEmotion>();
            //obtenemos un reflejo y captamos todos los atributos publicos o instancias
            var properties = scores.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            //obtenemos de una instancia where las propiedades que son de tipo float
            //con LinQ
            var filterproperties = properties.Where(p => p.PropertyType == typeof(float));
            //con select comun sería lo mismo así--->
            //var filterproperties = from p in properties
            //                       where p.PropertyType == typeof(float)
            //                       select p;

            var emotype = EmotionEnum.Undetermined;
            foreach (var prop in filterproperties)
            {
                //se obtiene el nombre de esa propiedad
                //y el nombre de esa propiedad, lo conviertes al emotionyipe
                if(!Enum.TryParse<EmotionEnum>(prop.Name, out emotype))
                    emotype = EmotionEnum.Undetermined;
                //saque de esa propiedad el valor
                var emoEmotion = new EmoEmotion();
                //indicandole de que objeto ->scores<- tenía que sacar el valor
                emoEmotion.Score = (float)prop.GetValue(scores);
                emoEmotion.EmotionType = emotype;
                emoEmotion.Face = emoface;

                EmotionList.Add(emoEmotion);
            }

            return EmotionList;
        }
    }
}