using System;
using System.Collections.Generic;
using System.Text;

namespace WitnessProject.Models
{
    class Incident
    {


            public int Id { get; set; }
            public string Titile { get; set; }
            public string Description { get; set; }
            public string Location { get; set; }
            public float Longitude { get; set; }
            public float Latitude { get; set; }
            public int Date { get; set; }
            public string ImagePath { get; set; }
            public object ImageArray { get; set; }

    }
}
