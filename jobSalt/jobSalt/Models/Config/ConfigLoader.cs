using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.IO;
using System.Diagnostics;
using System.Xml.Serialization;

namespace jobSalt.Models.Config
{
    public class ConfigLoader
    {
        public static object write_lock = new object();

        public static void SaveConfig<Type>(Type configObject, string configName)
        {
            lock (write_lock)
            {
                string path = Path.GetFullPath(System.Web.HttpContext.Current.Server.MapPath("/") + "Configurations\\" + configName);

                if (!path.StartsWith(System.Web.HttpContext.Current.Server.MapPath("/") + "Configurations\\"))
                    return;

                StreamWriter stream = new StreamWriter(path);

                XmlSerializer serializer = new XmlSerializer(typeof(Type));
                serializer.Serialize(stream, configObject);
                stream.Close();
            }
        }

        public static Type OpenConfig<Type>(string configName)
        {
            string path = Path.GetFullPath(System.Web.HttpContext.Current.Server.MapPath("/") + "Configurations\\" + configName);

            if (!path.StartsWith(System.Web.HttpContext.Current.Server.MapPath("/") + "Configurations\\"))
                return (Type)Activator.CreateInstance<Type>();

            if (!File.Exists(path))
                return (Type)Activator.CreateInstance<Type>();

            StreamReader stream = new StreamReader(path);

            XmlSerializer serializer = new XmlSerializer(typeof(Type));
            Type result = (Type)serializer.Deserialize(stream);
            stream.Close();
            return result;
        }

        public static SiteConfig SiteConfig
        {
            get
            {
                return OpenConfig<SiteConfig>("SiteConfig.xml");
            }
            set
            {
                SaveConfig<SiteConfig>(value, "SiteConfig.xml");
            }
        }
        public static AuthenticationConfig AuthenticationConfig
        {
            get
            {
                return OpenConfig<AuthenticationConfig>("Authentication.xml");
            }
            set
            {
                SaveConfig<AuthenticationConfig>(value, "Authentication.xml");
            }
        }
    }
}