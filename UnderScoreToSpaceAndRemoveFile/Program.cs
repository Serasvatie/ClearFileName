using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnderScoreToSpaceAndRemoveFile
{
    class Program
    {

        static void Main(string[] args)
        {
            try
            {
                if (args.Length < 2 && args.Length > 3)
                    throw new Exception("Bad Number Argument");
                Factory General = new Factory();
                General.ParseArgument(args);
                General.DirRecursive();
                Console.WriteLine("Success !");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : " + e.Message + "!");
            }
            Console.ReadLine();
        }
    }
}
