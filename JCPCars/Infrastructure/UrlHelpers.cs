using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JCPCars.Infrastructure
{
    public static class UrlHelpers
    {   
        public static string CarIconPath(this UrlHelper helper, string carIconFilename)
        {
            var carIconFolder = AppConfig.CarFolderRelative;
            var path = Path.Combine(carIconFolder, carIconFilename);
            var absolutePath = helper.Content(path);

            return absolutePath;
        }
    }
}