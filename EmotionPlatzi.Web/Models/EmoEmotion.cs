using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmotionPlatzi.Web.Models
{
    public class EmoEmotion
    {
        public int Id { get; set; }
        public float Score { get; set; }
        public int EmofaceId { get; set; }
        public EmotionEnum EmotionType { get; set; }
        public virtual EmoFace  Face{ get; set; }

    }

    public enum EmotionEnum
    {
      Anger,
      Contempt,
      Disgust,
      Fear,
      Happiness,
      Neutral,
      Sadness,
      Surprise,
      Undetermined
    }
}