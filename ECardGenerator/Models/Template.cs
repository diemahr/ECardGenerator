using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECardGenerator.Models
{
    public class Template
    {
        public int Id { get; set; }
        public string TemplateName { get; set; }
        public string ImageName { get; set; }
        public string FontColor { get; set; }
    }
}