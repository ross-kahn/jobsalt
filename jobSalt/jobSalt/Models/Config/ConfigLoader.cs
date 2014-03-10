using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.IO;
using System.Diagnostics;

namespace jobSalt.Models.Config
{
    public class ConfigLoader
    {
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