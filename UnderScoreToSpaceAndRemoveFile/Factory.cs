using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnderScoreToSpaceAndRemoveFile
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

        private void ChangeName(string directory)
        {
            string newName = directory.Replace("_", " ");
            if (newName != directory)
                Directory.Move(directory, newName);
        }

        public void DirRecursive()
        {
            foreach (string d in Directory.GetDirectories(GeneralDiretory))
            {
                foreach (string f in Directory.GetFiles(d))
                {
                    RemoveSpecificFile(f);
                    if (Cible == target.ALL || Cible == target.FILE)
                        ChangeName(f);
                }
                if (Cible == target.ALL || Cible == target.DIRECTORY)
                    ChangeName(d);
                if (Recursive)
                    DirRecursive();
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
