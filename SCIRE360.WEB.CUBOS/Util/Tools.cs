using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;

namespace SCIRE360.WEB.CUBOS.Util
{
    public class Tools
    {
        public bool MasterConfig(DataTable Config, String Flag)
        {
            bool MasterFlag = false;
            bool FlagMaster = false;
            try
            {
                DataRow[] result = Config.Select("codi_webform = '" + Flag + "'");
                foreach (DataRow row in result)
                {
                    //Console.WriteLine("{0}", row[0]);
                    FlagMaster = Convert.ToBoolean(row[8].ToString());
                    if (FlagMaster == true) { MasterFlag = true; }
                    else if (FlagMaster == false) { MasterFlag = false; }
                }
            }
            catch (Exception ex)
            {
                return MasterFlag;
            }
            return MasterFlag;
        }
    }
}