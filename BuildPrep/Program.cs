using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BuildPrep
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Build Prep - Copyright (C) 2015 JSandusky");
                Console.WriteLine("    Usage:");
                Console.WriteLine("buildprep <source-tree> <urshell-dir>");
                Console.WriteLine(" ");
                Console.WriteLine("    Purpose:");
                Console.WriteLine("Copies libraries and headers from Urho3D source tree to the destination directory");
                Console.WriteLine("For the purpose of using project relative paths");
                return;
            }
            string sourceTreeDir = args[0];
            string writeDir = args[1];

            // Copy Urho3D libraries
            string sourceTreeLibDir = System.IO.Path.Combine(sourceTreeDir, "lib");
            string writeLibDir = System.IO.Path.Combine(writeDir, "lib");
            foreach (string fileName in System.IO.Directory.EnumerateFiles(sourceTreeLibDir))
                System.IO.File.Copy(fileName, System.IO.Path.Combine(writeLibDir, System.IO.Path.GetFileName(fileName)), true);

            // Copy Urho3D headers
            string sourceTreeIncDir = System.IO.Path.Combine(sourceTreeDir, "include");
            string writeIncDir = System.IO.Path.Combine(writeDir, "include");
            copyDir(sourceTreeIncDir, writeIncDir);

            //// This is an alternative of instead configuring the project path, this has issues
            //string backendDir = System.IO.Path.Combine(writeDir, "Backend");
            //string backendProjectFile = System.IO.Path.Combine(backendDir, "Backend.vcxproj");
            //if (System.IO.File.Exists(backendProjectFile))
            //{
            //    XmlDocument doc = new XmlDocument();
            //    doc.Load(backendProjectFile);
            //
            //    foreach (XmlElement elem in doc.DocumentElement.ChildNodes)
            //    {
            //        if (elem.Name.Equals("ItemDefinitionGroup"))
            //        {
            //            foreach (XmlElement subElem in elem.ChildNodes)
            //            {
            //                if (subElem.Name.Equals("AdditionalIncludeDirectories"))
            //                {
            //                      subElem.InnerText = sourceTreeIncDir + ";" + sourceTreeIncDir + "\Urho3D\ThirdParty;" + sourceTreeIncDir + "\Urho3D\ThirdParty\Bullet;" + sourceTreeIncDir + "\Urho3D\ThirdParty\Lua";
            //                }
            //                else if (subElem.Name.Equals("AdditionalLibraryDirectories"))
            //                {
            //                      subElem.InnerText = sourceTreeLibDir;
            //                }
            //            }
            //        }
            //    }
            //
            //    doc.Save(backendProjectFile);
            //}
        }

        static void copyDir(string source, string dest)
        {
            foreach (string filename in System.IO.Directory.EnumerateFiles(source))
                System.IO.File.Copy(filename, System.IO.Path.Combine(dest, System.IO.Path.GetFileName(filename)), true);
            foreach (string dir in System.IO.Directory.EnumerateDirectories(source))
                copyDir(dir, System.IO.Path.Combine(dest, System.IO.Path.GetFileName(dir)));
        }
    }
}
