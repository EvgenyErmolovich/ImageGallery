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
        public int PageSize = 5;

        public ActionResult Index(int page = 1)
        {
            ImagesListViewModel model = new ImagesListViewModel
            {
                Images = db.Image
                .OrderBy(p => p.Id)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = db.Image.Count()
                }
            };
            return View(model);
        }
        [HttpGet]
        public FileContentResult GetImage(int id){
            Image image = db.Image.FirstOrDefault(i => i.Id == id);
            if (image != null) return File(image.ImageData, image.ImageMimeType);
            return null;
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