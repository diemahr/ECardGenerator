using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECardGenerator.Models
{
    public class Ecard
    {
        //member variables
        private Ecard _ecard;

        //Properties
        //public int Id { get; set; }
        public string ToName { get; set; }
        public string ToEmail { get; set; }
        public string Message { get; set; }
        public string FroName { get; set; }
        public string FroEmail { get; set; }
        public int TemplateID { get; set; }


        //Constructors
        public Ecard()
        {
        }

        public Ecard(Ecard ecard)
        {
            this._ecard = ecard;
        }

        public Ecard(string toName, string froName, string toEmail, string froEmail,
                                    string message, int templateID)
        {
            //this.Id = Id;
            this.ToName = toName;
            this.FroName = froName;
            this.ToEmail = toEmail;
            this.FroEmail = froEmail;
            this.Message = message;
            this.TemplateID = templateID;
 
        }

    }
}