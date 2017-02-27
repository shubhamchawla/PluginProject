using PluginContract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace LoadingPlugins.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "hello";
            string path = @"D:\TutorialProjects\PluginProject\LoadingPlugins\LoadingPlugins\plugins";
            string[] dllFileNames = null;
            if (Directory.Exists(path))
            {
                dllFileNames = Directory.GetFiles(path, "*.dll");
            }

            ICollection<Assembly> assemblies = new List<Assembly>(dllFileNames.Length);
            foreach (string dllFile in dllFileNames)
            {
                AssemblyName an = AssemblyName.GetAssemblyName(dllFile);
                Assembly assembly = Assembly.Load(an);
                assemblies.Add(assembly);
            }

            Type pluginType = typeof(IOperations);
            ICollection<Type> pluginTypes = new List<Type>();
            foreach (Assembly assembly in assemblies)
            {
                if (assembly != null)
                {
                    Type[] types = assembly.GetTypes();
                    foreach (Type type in types)
                    {
                        if (type.IsInterface || type.IsAbstract)
                        {
                            continue;
                        }
                        else
                        {
                            if (type.GetInterface(pluginType.FullName) != null)
                            {
                                pluginTypes.Add(type);
                            }
                        }
                    }
                }
            }
            ICollection<IOperations> plugins = new List<IOperations>(pluginTypes.Count);
            foreach (Type type in pluginTypes)
            {
                dynamic plugin = (IOperations)Activator.CreateInstance(type);
                plugins.Add(plugin);
            }

            var _Plugins = new Dictionary<string, IOperations>();
            //ICollection<IOperations> plugins = PluginLoader.LoadPlugins("Plugins");
            foreach (var item in plugins)
            {
                _Plugins.Add(item.name, item);
            }
            string key = _Plugins.First().Key;
            if (_Plugins.ContainsKey(key))
            {
                IOperations plugin = _Plugins[key];
                ViewBag.Message = plugin.sum();
            }

            return View();
        }
    }
}