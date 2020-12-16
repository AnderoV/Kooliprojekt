using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Kooliprojekt.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kooliprojekt.Controllers
{
    public class UploadController : Controller
    {
        private readonly ApplicationDbContext _context;
        readonly string storeName = "siim";

        public UploadController(ApplicationDbContext context)
        {
            _context = context;
           
        }

        public async Task<IActionResult> Index([FromServices]IFileClient fileClient)
        {
            var car = await _context.Cars.Include(i => i.Pictures).FirstOrDefaultAsync(m => m.Id == 1);
            return View(car);
        }
        public async Task<IActionResult> Upload(IFormFile[] files, [FromServices]IFileClient fileClient,int Id)
        {
            var car = await _context.Cars.Include(i => i.Pictures).FirstOrDefaultAsync(m => m.Id == Id);
            for (var i = 0; i < files.Length; i++)
            {
                var formFile = files[i];
                var fileName = System.IO.Path.GetFileName(formFile.FileName);

                using (var uploadedFile = formFile.OpenReadStream())
                {
                    await fileClient.SaveFile(storeName, fileName, uploadedFile);
                }
                if(files.Length > car.Pictures.Count)
               car.Pictures.Add(new Image());

                car.Pictures[i].Url = await fileClient.GetFileUrl(storeName, fileName);
                _context.Update(car);
                await _context.SaveChangesAsync();
              
            }
            //for (int i = 0; i < car.Pictures.Count; i++)
            //{
            //    if (car.Pictures[i].Url == null)
            //    {
            //        _context.Images.Remove(car.Pictures[i]);

            //    }
            //    await _context.SaveChangesAsync();
            //}
            return View();
        }

        public async Task<IActionResult> Delete(IFormFile[] files, [FromServices]IFileClient fileClient,int Id)
        {
            var Picture = await _context.Images.FirstOrDefaultAsync(m => m.Id == Id);

            _context.Images.Remove(Picture);
            await fileClient.DeleteFile(storeName, Picture.Url);
            await _context.SaveChangesAsync();
            return RedirectToAction("index", "Cars");
          
        }

    }
}