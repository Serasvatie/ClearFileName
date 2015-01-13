using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClearFileName
{
    class Factory
    {
        private Boolean Recursive = false;
        private enum target { FILE = 1, DIRECTORY = 2, ALL = 0 };
        private target Cible;
        private string GeneralDiretory;

        private void RemoveSpecificFile(string file)
        {
            string extension = Path.GetExtension(file);
            if (extension == ".url")
                File.Delete(file);
        }

        private void ChangeNameDirectory(string directory)
        {
            string newName = directory.Replace("_", " ");
            if (newName != directory)
                Directory.Move(directory, newName);
        }

        private void ChangeNameFile(string file)
        {
            int index = file.LastIndexOf("\\");
            string newPath = file.Substring(0, index) + file.Substring(index).Replace("_", " ");
            if (newPath != file)
                File.Move(file, newPath);
        }

        public void DirRecursive(string defaultParam = "default")
        {
            foreach (string d in Directory.GetDirectories(defaultParam == "default" ? GeneralDiretory : defaultParam))
            {
                foreach (string f in Directory.GetFiles(d))
                {
                    if (Cible == target.ALL || Cible == target.FILE)
                        ChangeNameFile(f);
                    RemoveSpecificFile(f);
                }
                if (Recursive)
                    DirRecursive(d);
                if (Cible == target.ALL || Cible == target.DIRECTORY)
                    ChangeNameDirectory(d);
            }
        }

        public void ParseArgument(string[] args)
        {
            foreach (string arg in args)
            {
                if (arg.Substring(0, 2) == "--")
                {
                    if (arg == "--file")
                        Cible = target.FILE;
                    if (arg == "--directory")
                        Cible = target.DIRECTORY;
                    if (arg == "--all")
                        Cible = target.ALL;
                }
                else if (arg == "-d")
                    Recursive = true;
                else
                    GeneralDiretory = arg;
            }
        }
    }
}
