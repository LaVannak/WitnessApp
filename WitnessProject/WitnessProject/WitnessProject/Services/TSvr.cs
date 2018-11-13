using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using WitnessProject.Models;

namespace WitnessProject.Services
{
    //TSvr = Transver Services
    //A static varible service to transver all static varible
    //accross the application
    public class TSvr 
    {
        public static int WId;
        List<Witness> witnesses = new List<Witness>();
    }
}
