using System;
using System.Collections.Generic;
using System.Text;

namespace WitnessProject.Models
{
    class Incident
    {
        public int Id { get; set; }
        public int WitnesId { get; set; }
        public string Titile { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int Date { get; set; }
        public string ImagePath { get; set; }
        public object ImageArray { get; set; }

        public string FullImagPath
        {
            get
            {
                if (string.IsNullOrEmpty(ImagePath))
                {
                    return string.Empty;
                }
                return  String.Format("https://witnes.azurewebsites.net{0}", ImagePath.Substring(1));
            }
        }
    }
}
