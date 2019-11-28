using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiceConvert
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var path in args)
            {
                if (!File.Exists(path))
                    continue;

                string pathLower = path.ToLower();
                
                if (pathLower.EndsWith(".hit"))
                {
                    var model = HITFile.LoadHIT(path);
                    model.SaveOBJ(path.Replace(".hit", ".hit.obj"));
                }

                if (pathLower.EndsWith(".hit.obj"))
                {
                    var model = HITFile.LoadOBJ(path);
                    model.SaveHIT(path.Replace(".hit.obj", ".hit"));
                }
                
                if (pathLower.EndsWith(".chpath"))
                {
                    var model = CHPATHFile.LoadCHPATH(path);
                    model.SaveOBJ(path.Replace(".chpath", ".chpath.obj"));
                }
            }
        }
    }
}
