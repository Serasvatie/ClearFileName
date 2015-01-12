using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnderScoreToSpaceAndRemoveFile
{
    class Program
    {
        static void RuleFile(string file)
        {
            string extension = Path.GetExtension(file);
            if (extension == ".url")
                File.Delete(file);
        }

        static void RuleDirectory(string directory)
        {
            string newName = directory.Replace("_", " ");
            if (newName != directory)
                Directory.Move(directory, newName);
        }

        static void DirRecursive(string Dir)
        {
            foreach (string d in Directory.GetDirectories(Dir))
            {
                foreach (string f in Directory.GetFiles(d))
                {
                    RuleFile(f);
                }
                RuleDirectory(d);
            }
        }
        
        static void Main(string[] args)
        {
            try
            {
                if (args.Length != 1)
                    throw new Exception("Bad Number Argument");
                DirRecursive(args[0]);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : " + e.Message + "!");
            }
            Console.WriteLine("Success !");
            Console.ReadLine();
        }
    }
}
