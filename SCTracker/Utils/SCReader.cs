using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Neo;

namespace SCTracker.Utils
{
    class SCReader
    {
        private String SC_FileName { get; set; }
        private Boolean SC_IsBin { get; set; }

        public SCReader(String FileName, Boolean IsBin)
        {
            SC_FileName = FileName;
            SC_IsBin = IsBin;
        }

        public String ReadAllScript()
        { 
            if(SC_IsBin)
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
                return data.ToHexString();
            }


            String text = "";
            try
            {
                text = File.ReadAllText(SC_FileName);
            }
            catch (FileNotFoundException e)
            {
                return "File not found : " + e.FileName;
            }
            catch (IOException e)
            {
                return e.Message;
            }

            return text;


        }

        // Method that overrides the base class (System.Object) implementation.
        public override string ToString()
        {
            return "Reading " + SC_FileName;
        }
    }
}
