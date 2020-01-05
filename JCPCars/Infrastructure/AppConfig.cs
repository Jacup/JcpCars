using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace JCPCars.Infrastructure
{
    public class AppConfig
    {
        private static string _carFolderRelative = ConfigurationManager.AppSettings["CarFolder"];
        public static string CarFolderRelative
        {
            get
            {
                return _carFolderRelative;
            }
        }
    }
}