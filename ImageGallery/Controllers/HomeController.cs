using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ImageGallery.Models;
using System.IO;

namespace ImageGallery.Controllers
{
    public class HomeController : Controller
    {
        Images db = new Images();

        public ActionResult Index()
        {
            return View(db.Image);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Image pic, HttpPostedFileBase uploadImage)
        {
            if (ModelState.IsValid && uploadImage != null)
            {
                byte[] imageData = null;
                using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                {
                    imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                }
                pic.ImageData = imageData;
                pic.ImageMimeType = uploadImage.ContentType;

                db.Image.Add(pic);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(pic);
        }
    }
}