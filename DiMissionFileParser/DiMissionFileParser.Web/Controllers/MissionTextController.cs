using System;
using System.IO;
using System.Linq;
using DiMissionfileParser.Parser.Functions;
using DiMissionfileParser.Parser.Models;
using Microsoft.AspNetCore.Mvc;

namespace DiMissionFileParser.Web.Controllers
{
    public class MissionTextController : Controller
    {
        private readonly MissionTextDataContext _context;

        public MissionTextController()
        {
            var dataDir = AppDomain.CurrentDomain.GetData("DataDirectory").ToString();
            var path = Path.Combine(dataDir, "apache_texts\\texts");
            _context = new MissionTextDataContext(path);
        }


        // GET: MissionText
        public ActionResult Index()
        {
            return View(_context.MissionTexts);
        }

        // GET: MissionText/Details/5
        public ActionResult Details(string id)
        {
            var file = _context.MissionTexts.Single(s => s.FileName == id);
            return GetDetailsView(file);
        }

        private ViewResult GetDetailsView(MissionText file)
        {
            return View(file.GetType().Name, file);
        }
    }
}