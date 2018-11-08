using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WitnessAPI.Helpers
{
    public class FileHelper
    {
        public static bool UploadPhoto( MemoryStream menStream, string folderName, string fileName)
        {
            try
            {
                menStream.Position = 0;
                var path = Path.Combine(HttpContext.Current.Server.MapPath(folderName), fileName);
                File.WriteAllBytes(path, menStream.ToArray());
            }
            catch
            {
                return false;
            }


            return true;
        }
    }
}