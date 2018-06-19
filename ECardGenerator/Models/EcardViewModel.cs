using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECardGenerator.Models
{
    public class EcardViewModel
    {
        public int Id { get; set; }
        public string ToName { get; set; }
        public string ToEmail { get; set; }
        public string Message { get; set; }
        public string FroName { get; set; }
        public string FroEmail { get; set; }
        public int TemplateID { get; set; }
        public string TemplateName { get; set; }
        public string ImageName { get; set; }
        public string FontColor { get; set; }

        public EcardViewModel() {}

        public EcardViewModel(Ecard card)
        {
            this.Id = card.Id;
            this.ToName = card.ToName;
            this.ToEmail = card.ToEmail;
            this.Message = card.Message;
            this.FroName = card.FroName;
            this.FroEmail = card.FroEmail;
            this.TemplateID = card.TemplateID;
            this.TemplateName = card.TemplateName;
            this.ImageName = card.ImageName;
            this.FontColor = card.FontColor;
        }

    }
}