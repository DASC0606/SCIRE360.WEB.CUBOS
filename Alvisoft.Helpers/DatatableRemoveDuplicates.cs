using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.IO;
using System.Data.SqlClient;
using System.Data.Common;
using System.Configuration;
using Alvisoft.Helpers;
using System.Xml.Serialization;
using System.Text;
using DBHelper;
using DBHelper.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Collections.ObjectModel;
namespace Alvisoft.Helpers
{
    public class DatatableRemoveDuplicates
    {


        public static void RemoveDuplicatesFromDataTable(ref DataTable table, List<string> keyColumns)
        {

            Dictionary<string, string> uniquenessDict = new Dictionary<string, string>(table.Rows.Count);

            StringBuilder stringBuilder = null;

            int rowIndex = 0;

            DataRow row;

            DataRowCollection rows = table.Rows;

            while (rowIndex < rows.Count - 1)
            {

                row = rows[rowIndex];

                stringBuilder = new StringBuilder();

                foreach (string colname in keyColumns)
                {

                    stringBuilder.Append((Convert.ToString( row[keyColumns.IndexOf(colname)])));

                }

                if (uniquenessDict.ContainsKey(stringBuilder.ToString()))
                {

                    rows.Remove(row);

                }

                else
                {

                    uniquenessDict.Add(stringBuilder.ToString(), string.Empty);

                    rowIndex++;

                }

            }

        }


        /// <summary>
        /// Removes duplicate rows from given DataTable
        /// </summary>
        /// <param name="tbl">Table to scan for duplicate rows</param>
        /// <param name="KeyColumns">An array of DataColumns
        ///   containing the columns to match for duplicates</param>
        public static DataTable RemoveDuplicates(DataTable tbl,
                                             DataColumn[] keyColumns)
        {
            int rowNdx = 0;
            while (rowNdx < tbl.Rows.Count - 1)
            {
                DataRow[] dups = new DataRow[tbl.Rows.Count];
                dups=FindDups(tbl, rowNdx, keyColumns);
                if (dups.Length > 0)
                {
                    foreach (DataRow dup in dups)
                    {
                        tbl.Rows.Remove(dup);
                    }
                }
                else
                {
                    rowNdx++;
                }
            }

            return tbl;
        }

        private static DataRow[] FindDups(DataTable tbl,
                                          int sourceNdx,
                                          DataColumn[] keyColumns)
        {
            ArrayList retVal = new ArrayList();

            DataRow sourceRow =   tbl.Rows[sourceNdx];
            for (int i = sourceNdx + 1; i < tbl.Rows.Count; i++)
            {
                DataRow targetRow = tbl.Rows[i];
                if (IsDup(sourceRow, targetRow, keyColumns))
                {
                    retVal.Add(targetRow);
                }
            }
            return (DataRow[])retVal.ToArray(typeof(DataRow));
        }

        private static bool IsDup(DataRow sourceRow,
                                  DataRow targetRow,
                                  DataColumn[] keyColumns)
        {
            bool retVal = true;
           
            foreach (DataColumn column in keyColumns)
            {
                           
                retVal = retVal && sourceRow[column.ColumnName].Equals(targetRow[column.ColumnName]);
                if (!retVal) break;
            }
            return retVal;
        }

    }
}