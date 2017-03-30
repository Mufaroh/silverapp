using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace AdminImage.Controllers
{
    public class GalleyController : Controller
    {
        // GET: Galley
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult Image()
        {

            List<ImageGallery> all = new List<ImageGallery>();
            using (DonorsEntities de = new DonorsEntities())
            {
                all = de.ImageGalleries.ToList();
            }
                return View(all);


        }


        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(ImageGallery ig)
        {
            //Apply the validation here
            if (ig.File.ContentLength>(2*1024*1024))
            {
                ModelState.AddModelError("CustomError", "File can not be greater than 2mbz");


                return View(ig);
            }
            if(!(ig.File.ContentType !="image/jpeg"|| ig.File.ContentType != "image/gif"))
            {
                ModelState.AddModelError("CustomError" , "File type allowed:jpeg and gif");
            }
            ig.FileName = ig.FileName;
            ig.ImgSize = ig.File.ContentLength;

            byte[] data = new byte[ig.File.ContentLength];
            ig.File.InputStream.Read(data, 0, ig.File.ContentLength);

            ig.ImageData = data;
            using(DonorsEntities de=new DonorsEntities())
            {
                de.ImageGalleries.Add(ig);
                de.SaveChanges();
            }
             
            return RedirectToAction("Image");
        }
    }
}