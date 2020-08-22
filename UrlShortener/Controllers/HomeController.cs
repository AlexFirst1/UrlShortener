using System;
using System.Web;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Domain;
using UrlShortener.Models;
using Microsoft.AspNetCore.Http;

namespace UrlShortener.Controllers
{
    public class HomeController : Controller
    {
        private readonly UrlsRepository urlsRepository;
        public HomeController(UrlsRepository urlsRepository)
        {
            this.urlsRepository = urlsRepository;
        }

        public IActionResult Index()
        {
            var model = urlsRepository.GetUrls();
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // GET: Url/Edit/5
        public ActionResult Edit(int id)
        {
            UrlModel model = id == default ? new UrlModel() : urlsRepository.GetUrlById(id);
            return View(model);
        }

        // POST: Url/Edit/5
        [HttpPost]
        public ActionResult Edit(UrlModel model)
        {
            if (ModelState.IsValid)
            {
                urlsRepository.SaveUrl(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Url/Delete/5        
        public ActionResult Delete(int id)
        {
            urlsRepository.DeleteUrl(new UrlModel { Id = id });
            return RedirectToAction("Index");
        }

        public string ShortUrl(string url)
        {
            var req = Request;
            return urlsRepository.ShortUrl(url);
        }
        public void RedirectLink()
        {
            string url = Request.Method;//.QueryString;//.Replace("/", "");//["aspxerrorpath"]
            try
            {
                string longUrl = urlsRepository.RedirectLink(url);
                Response.Redirect(longUrl, true);
            }
            catch (Exception e)
            {
                throw new Exception($"Ошибка: {e}");
            }
        }
    }
}
