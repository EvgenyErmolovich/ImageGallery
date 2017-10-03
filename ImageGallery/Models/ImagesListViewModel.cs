using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImageGallery.Models
{
    public class ImagesListViewModel
    {
        public IEnumerable<Image> Images { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}