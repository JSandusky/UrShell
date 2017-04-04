using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginLibrary
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class HandlesExtensions : Attribute
    {
        public HandlesExtensions(string[] list)
        {
            Extensions = list;
        }

        public string[] Extensions { get; private set; }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class FileFiltered : Attribute
    {
        public FileFiltered(string filter) { Filter = filter; }
        public string Filter { get; private set; }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class TypeInfo : Attribute
    {
        public TypeInfo(string name, string image)
        {
            Name = name;
            Image = image;
        }

        public string Name { get; private set; }
        public string Image { get; private set; }
    }
}
