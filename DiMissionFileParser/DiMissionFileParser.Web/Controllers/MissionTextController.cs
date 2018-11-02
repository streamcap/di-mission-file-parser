using System.Linq;
using DiMissionfileParser.Parser.Functions;
using DiMissionfileParser.Parser.Models;
using Microsoft.AspNetCore.Mvc;

namespace DiMissionFileParser.Web.Controllers
{
    public class MissionTextController : Controller
    {
        private readonly MissionTextDataContext context;

        public MissionTextController()
        {
            var path = @"C:\temp\apache_texts\texts";
            context = new MissionTextDataContext(path);
        }


        // GET: MissionText
        public ActionResult Index()
        {
            return View(context.MissionTexts);
        }

        // GET: MissionText/Details/5
        public ActionResult Details(string id)
        {
            var file = context.MissionTexts.Single(s => s.FileName == id);
            return GetDetailsView(file);
        }

        private ViewResult GetDetailsView(MissionText file)
        {
            return View(file.GetType().Name, file);
        }
    }
}