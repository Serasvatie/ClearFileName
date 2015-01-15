using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClearFileNameWPF
{
    class Factory
    {
        private Boolean Recursive = false;
        public enum target { FILE = 1, DIRECTORY = 2, ALL = 0 };
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
            int index = directory.LastIndexOf("\\");
            string newName = directory.Substring(0, index) + directory.Substring(index).Replace("_", " ");
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

        private void DirRecursive(string defaultParam = "default")
        {
            foreach (string f in Directory.GetFiles(defaultParam == "default" ? GeneralDiretory : defaultParam))
            {
                if (Cible == target.ALL || Cible == target.FILE)
                    ChangeNameFile(f);
                RemoveSpecificFile(f);
            }
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

        public void InitAndDoIt(string path, Boolean rec, target cible)
        {
            GeneralDiretory = path;
            Recursive = rec;
            Cible = cible;
            DirRecursive();
        }
    }
}
