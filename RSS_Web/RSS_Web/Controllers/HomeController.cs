using RSS_Web.Abstract;
using RSS_Web.Database;
using RSS_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PagedList;

namespace RSS_Web.Controllers
{
    public class HomeController : Controller
    {
        private IRssRepository RssRepository { get; set; }

        public HomeController()
        {
            RssRepository = new RssRepository();
        }        

        public ActionResult Index(string source, string sortOrder, int? page)
        {
            var xabr = new { Name = "Хабр", Source = "Хабр / Интересные публикации" };
            var interfax = new { Name = "Интерфакс", Source = " " };
            var all = new { Name = "Все", Source = "All" };
            SelectList files = new SelectList(new List<Object>{ all, xabr, interfax}, "Source", "Name");
            ViewBag.Files = files;

            if(source == null && sortOrder == null)
            {
                return View();
            }

            ViewBag.Source = source;
            ViewBag.SortOrder = sortOrder;

            var news = RssRepository.News;
            if (source != "All")
            {
                news = news.Where(n => n.Source.Name == source);
            }
            switch (sortOrder)
            {
                case "source":
                    news = news.OrderBy(n => n.Source.Name);
                    break;
                case "date":
                    news = news.OrderBy(n => n.Date);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(news.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public ActionResult Index(SearchProperties model)
        {
            return RedirectToAction("Index", "Home", new { source = model.Source, sortOrder = model.SortOrder});
        }

        public ActionResult AjaxIndex()
        {
            var xabr = new { Name = "Хабр", Source = "Хабр / Интересные публикации" };
            var interfax = new { Name = "Интерфакс", Source = " " };
            var all = new { Name = "Все", Source = "All" };
            SelectList files = new SelectList(new List<Object> { all, xabr, interfax }, "Source", "Name");
            ViewBag.Files = files;
            return View();
        }

        [HttpPost]
        public ActionResult AjaxIndex(SearchProperties model)
        {
            ViewBag.Source = model.Source;
            ViewBag.SortOrder = model.SortOrder;

            var news = RssRepository.News;
            if (model.Source != "All")
            {
                news = news.Where(n => n.Source.Name == model.Source);
            }
            switch (model.SortOrder)
            {
                case "source":
                    news = news.OrderBy(n => n.Source.Name);
                    break;
                case "date":
                    news = news.OrderBy(n => n.Date);
                    break;
            }

            int pageSize = 10;
            int pageNumber = 1;
            return View("AjaxPost", news.ToPagedList(pageNumber, pageSize));
        }
    }
}