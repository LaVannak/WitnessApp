using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WitnessAPI.Models
{
    public class Incident
    {
        public int Id { get; set; }
        public string Titile { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int Date { get; set; }
        public string ImagePath { get; set; }
        public int WitnesID { get; set; }

        [NotMapped]
        public byte[] ImageArray { get; set; }
    }
}