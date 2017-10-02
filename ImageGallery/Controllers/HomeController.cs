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
        ImageEntitie context = new ImageEntitie();
        
        [HttpGet]
        public FileContentResult GetImage(int imageId)
        { 
            Image image = context.Image.FirstOrDefault(i => i.Id == imageId);
            if(image != null) return File(image.ImageData, image.ImageMimeType);
            return null;
        }
    }
}