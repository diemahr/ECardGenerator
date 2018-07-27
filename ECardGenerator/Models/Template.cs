using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECardGenerator.DAL;


namespace ECardGenerator.Models
{
    public class Template
    {
        public int Id { get; set; }
        public string TemplateName { get; set; }
        public string ImageName { get; set; }
        public string FontColor { get; set; }

        public Template() { }

        public Template(int templateID)
        {
            this.Id = Id;
            this.TemplateName = TemplateName;
            this.ImageName = ImageName;
            this.FontColor = FontColor;
        }

    }


}