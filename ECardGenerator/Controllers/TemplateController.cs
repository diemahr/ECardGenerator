using ECardGenerator.Models;
using ECardGenerator.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;

namespace ECardGenerator.Controllers
{
    public class TemplateController : Controller
    {
        private ITemplateDAL _dal;
        public Ecard _eCard;
        //public EcardViewModel _eCardVM;

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
        public ActionResult FormResults(string toName, string froName, string toEmail, string froEmail,
                                    string message, int templateID)
        {
            try
            {
                _eCard = _dal.CreateUserECard(toName, froName, toEmail, froEmail, message, templateID);           
            }
            catch (SqlException)
            {
                return RedirectToAction("Index", "Template");
            }
            return RedirectToAction("Details", "Template", _eCard);

        }

        public ActionResult Details(Ecard _eCard)
        {
            EcardViewModel _eCardVM = new EcardViewModel(_eCard);
            try
            {                
                _eCardVM = _dal.RetrieveECardVM(_eCard);                
            }
            catch (SqlException)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(_eCardVM);
        }

    }
}