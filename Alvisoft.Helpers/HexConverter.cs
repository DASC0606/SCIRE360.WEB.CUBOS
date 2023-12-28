using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.VisualBasic; // I'm using  this class for Hex Conversion

namespace Alvisoft.Helpers
{
    public class HexConverter
    {
        public string Data_Hex_Asc(string Data)
        {
            string Data1 = "";
            string sData = "";

            while (Data.Length > 0)
            {
                Data1 = System.Convert.ToChar(System.Convert.ToUInt32(Data.Substring(0, 2), 16)).ToString();
                sData = sData + Data1;
                Data = Data.Substring(2, Data.Length - 2);
            }
            return sData;
        }
        public string Data_Asc_Hex(string Data)
        {


            string sValue;
            string sHex = "";
            while (Data.Length > 0)
            {
                sValue = Conversion.Hex(Strings.Asc(Data.Substring(0, 1).ToString()));
                Data = Data.Substring(1, Data.Length - 1);
                sHex = sHex + sValue;
            }
            return sHex;
        }

    }
}