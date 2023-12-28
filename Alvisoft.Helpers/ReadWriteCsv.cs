using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;
using System.IO;

namespace Alvisoft.Helpers
{
    public class ReadWriteCsv
    {


        public static void CreateCSVFile(DataTable dt, string strFilePath)
        {

            StreamWriter sw = new StreamWriter(strFilePath, false);

            // First we will write the headers.

            //DataTable dt = m_dsProducts.Tables[0];

            int iColCount = dt.Columns.Count;

            for (int i = 0; i < iColCount; i++)
            {
                sw.Write(dt.Columns[i]);
                if (i < iColCount - 1)
                {
                    sw.Write(",");
                }
            }
            sw.Write(sw.NewLine);
            // Now write all the rows.
            foreach (DataRow dr in dt.Rows)
            {
                for (int i = 0; i < iColCount; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        sw.Write(" " + dr[i].ToString());
                    }
                    if (i < iColCount - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();

        }

        public static string createCSV(DataSet ds)
        {
            StringBuilder sb = new StringBuilder();
           StringWriter sw = new StringWriter();
            DataTable dt = null;
            int intHdrSp = 2;
            int intTabSp = 4;
            int tabCnt = 0;
            while (tabCnt < ds.Tables.Count)
            {
                dt = ds.Tables[tabCnt];
                int iColCount = dt.Columns.Count;
                int i = 0;
                while (i < iColCount)
                {
                    sb.Append(dt.Columns[i]);
                    if (i < iColCount - 1)
                    {
                        sb.Append(",");
                    }
                    System.Math.Min(System.Threading.Interlocked.Increment(ref i), i - 1);
                }
                i = 0;
                while (i < intHdrSp)
                {
                    sb.Append(sw.NewLine);
                    System.Math.Min(System.Threading.Interlocked.Increment(ref i), i - 1);
                }

                foreach (DataRow dr in dt.Rows)
                {
                    i = 0;
                    while (i < iColCount)
                    {
                        if (!System.Convert.IsDBNull(dr[i]))
                        {
                            sb.Append(dr[i].ToString());
                        }
                        if (i < iColCount - 1)
                        {
                            sb.Append(",");
                        }
                        System.Math.Min(System.Threading.Interlocked.Increment(ref i), i - 1);
                    }
                    sb.Append(sw.NewLine);
                }
                i = 0;
                while (i < intTabSp)
                {
                    sb.Append(sw.NewLine);
                    System.Math.Min(System.Threading.Interlocked.Increment(ref i), i - 1);
                }
                System.Math.Min(System.Threading.Interlocked.Increment(ref tabCnt), tabCnt - 1);
            }
            return sb.ToString();
        }


        public static string ToCSV(DataTable dataTable)
        {
            //create the stringbuilder that would hold our data
            StringBuilder sb = new StringBuilder();
            //check if there are columns in our datatable
            if (dataTable.Columns.Count != 0)
            {
                //loop thru each of the columns so that we could build the headers
                //for each field in our datatable
                foreach (DataColumn column in dataTable.Columns)
                {
                    //append the column name followed by our separator
                    sb.Append(column.ColumnName + ',');
                }
                //append a carriage return
                sb.Append("\r\n");
                //loop thru each row of our datatable
                foreach (DataRow row in dataTable.Rows)
                {
                    //loop thru each column in our datatable
                    foreach (DataColumn column in dataTable.Columns)
                    {
                        //get the value for tht row on the specified column
                        // and append our separator
                        sb.Append(row[column].ToString() + ',');
                    }
                    //append a carriage return
                    sb.Append("\r\n");
                }
            }
            //return our values
            return sb.ToString();
        }



        public static object GetCSV(System.Web.UI.Page page, string strCSV, string strFname)
        {
            page.Response.ContentEncoding = System.Text.Encoding.Default;
            page.Response.Charset = "iso-8859-1";
            page.Response.AddHeader("Content-Disposition", "attachment; filename=" + strFname);
            page.Response.AddHeader("Content-Length", strCSV.Length.ToString());
            page.Response.ContentType = "application/vnd.ms-excel";
            page.Response.Write(strCSV);
            page.Response.Flush();
            page.Response.End();

            return page;
        }
        
        public static object export2CSV(System.Web.UI.Page page, DataSet ds, string strFname)
        {
            string strCSV = createCSV(ds);
            return GetCSV(page, strCSV, strFname + ".csv");
        }

        public static object export2CSV(System.Web.UI.Page page, DataSet ds)
        {
            string strCSV = createCSV(ds);
            return  GetCSV(page, strCSV, "report.csv");
        }

        public static string GetWriteableValue(object o)
        {
            if (o == null || o == Convert.DBNull)
            {
                return "";
            }
            else if (o.ToString().IndexOf(",") == -1)
            {
                return o.ToString();
            }
            else
            {
                return "\"" + o.ToString() + "\"";

            }
        }
        
        public static void ProduceCSV(DataTable dt, System.IO.StreamWriter file, bool WriteHeader)
        {
            if (WriteHeader)
            {
                string[] arr = new String[dt.Columns.Count];
                for (int i = 0; i <= dt.Columns.Count - 1; i++)
                {
                    arr[i] = dt.Columns[i].ColumnName;
                    arr[i] = GetWriteableValue(arr[i]);
                }

                file.WriteLine(string.Join(",", arr));
            }

            for (int j = 0; j <= dt.Rows.Count - 1; j++)
            {
                string[] dataArr = new String[dt.Columns.Count];
                for (int i = 0; i <= dt.Columns.Count - 1; i++)
                {
                    object o = dt.Rows[j][i];
                    dataArr[i] = GetWriteableValue(o);
                }
                file.Write(string.Join(",", dataArr));
            }
        }

    }
}