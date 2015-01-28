using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClearFileNameWPF
{
    class FactoryRemove
    {
        private bool Recursive;
        private string GeneralDirectory;
        private List<string> ExtensiontToRemove;
        private string error = "\n";

        public FactoryRemove(string directory, bool recursive, List<string> list)
        {
            GeneralDirectory = directory;
            Recursive = recursive;
            ExtensiontToRemove = list;
        }

        private void RemoveSpecificFile(string f)
        {
            foreach (string s in ExtensiontToRemove)
            {
                try
                {
                    if (Path.GetExtension(f) == s)
                        File.Delete(f);
                } catch (Exception e) {
                    error = error + e.Source + " " + e.Message + "\n";
                }
            }
        }

        private void DirRecursive(string defaultParam = "default")
        {
            if (Recursive)
            {
                try
                {
                    foreach (string d in Directory.GetDirectories(defaultParam == "default" ? GeneralDirectory : defaultParam))
                        DirRecursive(d);
                } catch (Exception e)
                {
                    error = error + e.Source + " " + e.Message + "\n";
                }
            }
            try
            {
                foreach (string f in Directory.GetFiles(defaultParam == "default" ? GeneralDirectory : defaultParam))
                {
                    RemoveSpecificFile(f);
                }
            }
            catch (Exception e)
            {
                error = error + e.Source + " " + e.Message + "\n";
            }
        }

        public string Run()
        {
            DirRecursive();
            return error;
        }
    }
}
