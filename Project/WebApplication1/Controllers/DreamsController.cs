using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using System.Web.Caching;

namespace WebApplication1.Controllers
{
    public class DreamsController : Controller
    {

        DreamsEntities _db = new DreamsEntities();

        //public ActionResult Index()
        //{

        //    var dreams = _db.Dreams.ToList();
        //    return View(dreams);
        //}

        public const int RecordsPerPage = 6;

        public DreamsController()
        {
            ViewBag.RecordsPerPage = RecordsPerPage;
        }

        public ActionResult Index()
        {
            return RedirectToAction("GetDreams");
        }

        public ActionResult GetDreams(int? pageNum)
        {
            pageNum = pageNum ?? 0;
            ViewBag.IsEndOfRecords = false;

            if (Request.IsAjaxRequest())
            {
                var dreams = GetRecordsForPage(pageNum.Value);
                ViewBag.IsEndOfRecords = (dreams.Any()) && ((pageNum.Value * RecordsPerPage) >= dreams.Last().Key);
                return PartialView("_Dreams", dreams);
            }
            else
            {
                LoadAllDreams();
                ViewBag.Dreams = GetRecordsForPage(pageNum.Value);
                return View("Index"); 
            }
        }
        public void LoadAllDreams()
        {
            
            var dreams = _db.Dreams.ToList();
            
            int dreamIndex = 1;
            HttpContext.Cache["Dreams"] = dreams.ToDictionary(x => dreamIndex++, x => x);
            ViewBag.TotalNumberDreams = dreams.Count();

            

        }

        public Dictionary<int, Dream> GetRecordsForPage(int pageNum)
        {
            Dictionary<int, Dream> _dreams = (HttpContext.Cache["Dreams"] as Dictionary<int, Dream>);

            int from = (pageNum * RecordsPerPage);
            int to = from + RecordsPerPage;

            return _dreams
                .Where(x => x.Key > from && x.Key <= to)
                .OrderBy(x => x.Key)
                .ToDictionary(x => x.Key, x => x.Value);

           
        }
    }
}
