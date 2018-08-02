using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCTracker.Utils
{
    class SCReader
    {
        private String SC_FileName { get; set; }

        public SCReader(String FileName)
        {
            SC_FileName = FileName;
        }

        public String ReadAllScript()
        {
            byte[] data = new byte[0];
            try
            {
                data = File.ReadAllBytes(SC_FileName);
            }
            catch (FileNotFoundException e)
            {
                return "File not found : " + e.FileName;
            }
            catch (IOException e)
            {
                return e.Message;
            }
            return Encoding.UTF8.GetString(data);
        }

        // Method that overrides the base class (System.Object) implementation.
        public override string ToString()
        {
            return "Reading " + SC_FileName;
        }
    }
}
