﻿using EmotionPlatzi.Web.Models;
using EmotionPlatzi.Web.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EmotionPlatzi.Web.Controllers
{
    public class EmoUploaderController : Controller
    {
        string serverFolderPath;
        string key;
        EmotionPlatziWebContext db = new EmotionPlatziWebContext();

        //con esta variable se manda la foto a la APi   
        EmotionHelper emoHelper;
        
        public EmoUploaderController()
        {            
            serverFolderPath = ConfigurationManager.AppSettings["UPLOAD_DIR"];
            key = ConfigurationManager.AppSettings["EMOTION_KEY"];
            emoHelper = new EmotionHelper(key);
        }


        // GET: EmoUploader
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(HttpPostedFileBase file)
        {
            if (file?.ContentLength > 0)
            {
                var picturename = Guid.NewGuid().ToString();
                picturename += Path.GetExtension(file.FileName);
                var route = Server.MapPath(serverFolderPath);

                route += "/" + picturename;
                file.SaveAs(route);

                var emoPicture = await emoHelper.DetectAndExtractFacesAsync(file.InputStream);

                emoPicture.Name = file.FileName;
                emoPicture.Path = serverFolderPath + "/" + picturename;

                db.EmoPictures.Add(emoPicture);
                await db.SaveChangesAsync();

                return RedirectToAction("Details","EmoPictures",new { Id = emoPicture.Id});
            }
            return View();
        }

    }
}