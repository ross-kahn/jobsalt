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
        public static void SaveConfig<Type>(Type configObject, string configName)
        {
            string path = Path.GetFullPath(System.Web.HttpContext.Current.Server.MapPath("/") + "Configurations\\" + configName);

            if (!path.StartsWith(System.Web.HttpContext.Current.Server.MapPath("/") + "Configurations\\"))
                return;

            StreamWriter stream = new StreamWriter(path);

            XmlSerializer serializer = new XmlSerializer(typeof(Type));
            serializer.Serialize(stream, configObject);
            stream.Close();
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

        public static Dictionary<string, string[]> LoadConfig(string configFileName)
        {
            string path = Path.GetFullPath(System.Web.HttpContext.Current.Server.MapPath("/") + "Models\\Config\\" + configFileName);

            if (!path.StartsWith(System.Web.HttpContext.Current.Server.MapPath("/") + "Models\\Config\\"))
                return null;

            XmlDocument xml = new XmlDocument();
            xml.Load(path);

            Dictionary<string, List<string>> rawConfig = new Dictionary<string, List<string>>();
            Dictionary<string, string[]> config = new Dictionary<string, string[]>();

            loadChildNodes(xml.LastChild, "", rawConfig);

            foreach (string key in rawConfig.Keys)
            {
                config[key.Trim('.')] = rawConfig[key].ToArray();
            }

            return config;
        }

        private static void loadChildNodes(XmlNode node, string prefix, Dictionary<string, List<string>> dict)
        {
            foreach(XmlNode childNode in node)
            {
                if(childNode.HasChildNodes)
                {
                    loadChildNodes(childNode, prefix + "." + node.Name, dict);
                    continue;
                }
                if (!dict.ContainsKey(prefix + "." + node.Name))
                    dict[prefix + "." + node.Name] = new List<string>();
                dict[prefix + "." + node.Name].Add(node.InnerText);
            }
        }
    }
}