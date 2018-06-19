using ECardGenerator.Models;
using ECardGenerator.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECardGenerator.Controllers
{
    public class TemplateController : Controller
    {
        private ITemplateDAL _dal;
        public TemplateController(ITemplateDAL dal)
        {
            _dal = dal;
        }

        // GET: Template
        public ActionResult Index(Template template)
        {
            IList<Template> templates = _dal.GetTemplates(template.Id, template.TemplateName, template.ImageName, template.FontColor);
            return View(templates);
        }

        public ActionResult Form()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FormResults(Ecard ecard)
        {
            EcardViewModel vm = new EcardViewModel(ecard);

            return View(vm);
        }
    }
}