using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using System.Text;


namespace Alvisoft.Helpers
{

    public static class DataTableExtensions
    {
        //ToString() Extensions:
        public static string ToCsvString(this DataTable dt)
        {
            return DataTableFunctions.ToCsv(dt);
        }
        public static string ToHtmlString(this DataTable dt)
        {
            return DataTableFunctions.ToHtml(dt);
        }
        public static string ToXmlString(this DataTable dt)
        {
            return DataTableFunctions.ToXml(dt);
        }

        public static string ToTableEmailString(this DataTable dt)
        {
            return DataTableFunctions.ToTableEmail(dt);
        }
        //ToFile() Extensions
        //public static void ToXmlFile(this DataTable dt)
        //{
        //    CommonTools.WriteFile(CommonTools.SaveFileName("XML File", 
        //      "xml (*.xml)|*.xml|All Files (*.*)|*.*"), DataTableFunctions.ToXml(dt));
        //}
        //public static void ToCsvFile(this DataTable dt)
        //{
        //    CommonTools.WriteFile(CommonTools.SaveFileName("Csv File", 
        //      "csv (*.csv)|*.csv|All Files (*.*)|*.*"), DataTableFunctions.ToCsv(dt));
        //}
        //public static void ToHtmlFile(this DataTable dt)
        //{
        //    CommonTools.WriteFile(CommonTools.SaveFileName("Html File", 
        //      "html (*.html)|*.html|All Files (*.*)|*.*"), DataTableFunctions.ToHtml(dt));
        //}
        
        //ToClipBoard() Extensions
        //public static void ToXmlClipboard(this DataTable dt)
        //{
        //    Clipboard.SetText(DataTableFunctions.ToXml(dt));
        //}
        //public static void ToCsvClipboard(this DataTable dt)
        //{
        //    Clipboard.SetText(DataTableFunctions.ToCsv(dt));
        //}
        //public static void ToHtmlClipboard(this DataTable dt)
        //{
        //    Clipboard.SetText(DataTableFunctions.ToHtml(dt));
        //}
        /// <summary>
        /// Common Tool Class
        /// </summary>
        private static class CommonTools
        {
            //public static string SaveFileName(string title, string filter)
            //{
            //    var  fd = new SaveFileDialog();
            //    fd.Title = title;
            //    fd.Filter = filter;
            //    fd.ShowDialog();
            //    return fd.FileName;
            //}
            public static void WriteFile(string filename, string content)
            {
                var sr = new StreamWriter(filename);
                sr.Write(content);
                sr.Flush();
                sr.Close();
            }
        }
        /// <summary>
        /// DataTable Converter Class
        /// </summary>
        private static class DataTableFunctions
        {
            public static String ToCsv(DataTable dt)
            {
                var sb = new StringBuilder();
                //Add Header Header
                for (var x = 0; x < dt.Columns.Count; x++)
                {
                    if (x != 0) sb.Append(";");
                    sb.Append(dt.Columns[x].ColumnName);
                }
                sb.AppendLine();
                //Add Rows
                foreach (DataRow row in dt.Rows)
                {
                    for (var x = 0; x < dt.Columns.Count; x++)
                    {
                        if (x != 0) sb.Append(";");
                        sb.Append(row[dt.Columns[x]]);
                    }
                    sb.AppendLine();
                }
                return sb.ToString();
            }
            public static string ToXml(DataTable dt)
            {
                var writer = new StringWriter();
                var name = dt.TableName;
                if (name == string.Empty)
                {
                    dt.TableName = "XMLTABLE";
                }
                dt.WriteXml(writer, true);
                dt.TableName = name;
                return writer.ToString();
            }
            public static string ToHtml(DataTable dt)
            {
                if (dt == null)
                {
                    throw new ArgumentNullException("dt");
                }
                var builder = new StringBuilder();
                builder.Append("<html xmlns='http://www.w3.org/1999/xhtml'>");
                builder.Append("<head>");
                builder.Append("<title>");
                builder.Append("Page-");
                builder.Append(Guid.NewGuid().ToString());
                builder.Append("</title>");
                builder.Append("</head>");
                builder.Append("<body>");
                builder.Append("<table border='1px' cellpadding='5' cellspacing='0' ");
                builder.Append("style='border: solid 1px Silver; font-size: x-small;'>");
                builder.Append("<tr align='left' valign='top'>");
                foreach (DataColumn c in dt.Columns)
                {
                    builder.Append("<td align='left' valign='top'>");
                    builder.Append(c.ColumnName);
                    builder.Append("</td>");
                }
                builder.Append("</tr>");
                foreach (DataRow r in dt.Rows)
                {
                    builder.Append("<tr align='left' valign='top'>");
                    foreach (DataColumn c in dt.Columns)
                    {
                        builder.Append("<td align='left' valign='top'>");
                        builder.Append(r[c.ColumnName]);
                        builder.Append("</td>");
                    }
                    builder.Append("</tr>");
                }
                builder.Append("</table>");
                builder.Append("</body>");
                builder.Append("</html>");
                return builder.ToString();
            }


            public static string ToTableEmail(DataTable dt)
            {
                if (dt == null)
                {
                    throw new ArgumentNullException("dt");
                }
                var builder = new StringBuilder();


                builder.Append("<font style='font-size:10.0pt; font-family:Calibri;'>");
                builder.Append("<BR><BR><BR>");
                //sets the table border, cell spacing, border color, font of the text, background, foreground, font height

                builder.Append("<table border='1px' border='1' bgColor='White' borderColor='Silver' cellpadding='5' cellspacing='0' ");
                builder.Append("style='border: solid 1px Silver; font-family:Calibri; font-size: x-small;'>");
                builder.Append("<tr align='left' valign='top'>");
                foreach (DataColumn c in dt.Columns)
                {
                    builder.Append("<td align='left' valign='top'>");
                    builder.Append(c.ColumnName);
                    builder.Append("</td>");
                }
                builder.Append("</tr>");
                foreach (DataRow r in dt.Rows)
                {
                    builder.Append("<tr align='left' valign='top'>");
                    foreach (DataColumn c in dt.Columns)
                    {
                        builder.Append("<td align='left' valign='top'>");
                        builder.Append(r[c.ColumnName]);
                        builder.Append("</td>");
                    }
                    builder.Append("</tr>");
                }
                builder.Append("</table>");
                builder.Append("</font>");
                
                return builder.ToString();
            }

        }
    }
}