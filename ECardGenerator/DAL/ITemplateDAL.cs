using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECardGenerator.Models;

namespace ECardGenerator.DAL
{
    public interface ITemplateDAL
    {
        IList<Template> GetTemplates(int id, string name, string imageName, string fontColor);
        Ecard CreateUserECard(string toName, string froName, string toEmail, string froEmail,
                                    string message, int templateID);
        EcardViewModel RetrieveECardVM(Ecard ecard);

    }
}
