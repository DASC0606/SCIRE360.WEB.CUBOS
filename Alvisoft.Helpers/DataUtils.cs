using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Reflection;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Alvisoft.Helpers
{
    public sealed class DataUtils
    {
        public static DataTable ListarColumnastabla(DataTable dt)
        {


            DataTable tabColumns = new DataTable();
            tabColumns.Columns.Add("COLUMNS");

            foreach (DataColumn item in dt.Columns)
            {
                if (item.DataType.FullName.Equals("System.String"))
                {
                    DataRow dr = tabColumns.NewRow();
                    dr[0] = item.ColumnName;
                    tabColumns.Rows.Add(dr);

                }
            }
            return tabColumns;
        }

        public static DataTable ListarColumnastabla(DataGridView dg)
        {


            DataTable tabColumns = new DataTable();
            tabColumns.Columns.Add("COLUMNSGRID");
            tabColumns.Columns.Add("COLUMNSTABLA");

            foreach (DataGridViewColumn item in dg.Columns)
            {
                if (item.Visible)
                {
                DataRow dr = tabColumns.NewRow();
                dr[0] = item.HeaderText;
                dr[1] = item.DataPropertyName; 
                tabColumns.Rows.Add(dr);

                }
            }
            return tabColumns;
        }

        private static Image RedimensionarImagen(Stream stream)
        {
            // Se crea un objeto Image, que contiene las propiedades de la imagen
            Image img = Image.FromStream(stream);

            // Tamaño máximo de la imagen (altura o anchura)
            const int max = 200;

            int h = img.Height;
            int w = img.Width;
            int newH, newW;

            if (h > w && h > max)
            {
                // Si la imagen es vertical y la altura es mayor que max,
                // se redefinen las dimensiones.
                newH = max;
                newW = (w * max) / h;
            }
            else if (w > h && w > max)
            {
                // Si la imagen es horizontal y la anchura es mayor que max,
                // se redefinen las dimensiones.
                newW = max;
                newH = (h * max) / w;
            }
            else
            {
                newH = h;
                newW = w;
            }
            if (h != newH && w != newW)
            {
                // Si las dimensiones cambiaron, se modifica la imagen
                Bitmap newImg = new Bitmap(img, newW, newH);
                Graphics g = Graphics.FromImage(newImg);
                g.InterpolationMode =
                  System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
                g.DrawImage(img, 0, 0, newImg.Width, newImg.Height);
                return newImg;
            }
            else
                return img;
        }

        public static string AbrevNombreTrip(string v_nombre){
            string[] nombres = v_nombre.Split(' ');
            if (nombres.Length >= 3) {
                return nombres[2].Substring(0, 1)  + ". " + nombres[0];
            }
            if (nombres.Length == 1) {
                return nombres[0];
            }
            return "";
        }

        #region Convert to Int From Time Format
        public static int ConvertToIntFormTimeFormat(string vTimeFormat)
        {
            if (vTimeFormat.Equals("")) {
                vTimeFormat = "0:00";
            }
            int hour = 0;
            int minute = 0;

            var Sep = vTimeFormat.IndexOf(':');
            hour = Convert.ToInt32(vTimeFormat.Substring(0, Sep));
            minute = Convert.ToInt32(vTimeFormat.Substring(Sep + 1));

            // Calcula las horas de los minutos
            var hourIncre = Convert.ToInt32(minute / 60);
            var minutosreales = minute % 60;
            hour = hour + hourIncre;
            minute = minutosreales;

            return hour * 60 + minute;
        }

        public static double fn_conv_horas_decimal(int phoras, int pminutos)
        {
            int minutos = phoras * 60 + pminutos;
            int partInt = minutos / 60;
            int minRes = minutos % 60;

            // Obteniendo la parte decimal
            decimal minDec = Convert.ToDecimal(minRes) / 60;
            // Obteniendo la parte entera decimal
            // Saber cuantas decimas hay en el resto de minutos
            int resDecInt = minRes / 6;
            double DecAdd = 0;
            int resDec = minRes % 6;

            // parte Decimal 
            int parDec = minRes / 10;

            switch (parDec){
                case 0:
                    if ((parDec * 10 + resDec) <= 2)
                        DecAdd = Convert.ToDouble(resDecInt) * 0.1;
                    else
                        DecAdd = (Convert.ToDouble(resDecInt) + 1) * 0.1;break;
                case 1 :
                    if ((parDec * 10 + resDec) <= 12)
                        DecAdd = Convert.ToDouble(resDecInt) * 0.1;
                    else
                        DecAdd = (Convert.ToDouble(resDecInt) + 1) * 0.1;break;
                case 2:
                    if ((parDec * 10 + resDec) <= 22)
                        DecAdd = Convert.ToDouble(resDecInt) * 0.1;
                    else
                        DecAdd = (Convert.ToDouble(resDecInt) + 1) * 0.1; break;
                case 3:
                    if ((parDec * 10 + resDec) <= 32)
                        DecAdd = Convert.ToDouble(resDecInt) * 0.1;
                    else
                        DecAdd = (Convert.ToDouble(resDecInt) + 1) * 0.1; break;
                case 4:
                    if ((parDec * 10 + resDec) <= 42)
                        DecAdd = Convert.ToDouble(resDecInt) * 0.1;
                    else
                        DecAdd = (Convert.ToDouble(resDecInt) + 1) * 0.1; break;
                case 5:
                    if ((parDec * 10 + resDec) <= 52)
                        DecAdd = Convert.ToDouble(resDecInt) * 0.1;
                    else
                        DecAdd = (Convert.ToDouble(resDecInt) + 1) * 0.1; break;
            }
            double res = partInt + DecAdd;
            return res;

            //double unitConvert = 0.01666666666666666666666666666667;
            //int minutos = (phoras * 60) + pminutos;

            //double minutosDec = minutos * unitConvert;
            //return minutosDec;

            //double vRetorno = 0;
            //int vTMin = 0;
            //int vResto;
            //vTMin = Math.DivRem(pminutos, 3, out vResto);
            //vRetorno = ((vTMin * 1.66) + ((pminutos - vTMin) * 1.67)) / 100;
            //vRetorno = vRetorno + phoras;
            //return vRetorno;
        }

        public static int fn_conv_decimal_horas(double ptiempo)
        {
            int vRetorno = 0;
            int vhoras;
            vhoras = (int)Math.Truncate(ptiempo);
            vRetorno = (int)Math.Truncate((ptiempo - vhoras) * 300 / 5);

            return vRetorno;
        }

        public static int fn_conv_decimal_minutos(double ptiempo)
        {
            int vRetorno = 0;
            vRetorno = (int)Math.Truncate(ptiempo);
            return vRetorno;

        }

        public static string DoubleToHours(double val)
        {

            double hours = Math.Floor(val);
            double minutes = (val - hours) * 60.0;

            int Hours = (int)Math.Floor(hours);
            int Minutes = (int)Math.Floor(minutes);


            return Hours + ":" + string.Format("{0:00}", Minutes);
        }

        public static int ConverDecimalToSeconds(decimal dInputValue)
        {
            int resp = (int)(dInputValue * 3600);
            return resp;
        }

        public static decimal ConverSecondsToDecimal(int dInputValue)
        {
            decimal resp = (decimal)(dInputValue / 3600);
            resp = Math.Round(resp, MidpointRounding.ToEven);
            return resp;
        }
        #endregion

        #region Convert to Time Format From Int
        public static string ConvertToTimeFormatFromInt(int iMinutes)
        {
            int hour = 0;
            int minute = 0;

            // Calcula las horas de los minutos
            var hourIncre = Convert.ToInt32(iMinutes / 60);
            var minutosreales = iMinutes % 60;
            hour = hour + hourIncre;
            minute = minutosreales;
            string format = string.Format("{0:00}", hour) + ":" + string.Format("{0:00}", minute);
            return format;
        }        

        #endregion

        #region Compare two DataTables and return a DataTable with Different Records

        /// <summary> 
        /// Compare 2 DataTables and Return 1 Datatable with Rows (Difference). 
        /// </summary> 
        /// <param name="DataTable">FirstDataTable=Source Datatable </param> 
        /// <param name="DataTable">SecondDataTable=Destination Datatable</param> 
        /// <returns>DataTable</returns> 
        /// Example Usage:
        /// DataTable dt;
        /// dt = GetDifferentRecords ( FirstDataTable, SecondDataTable );
        ///    if ( dt.Rows.Count == 0 )
        ///        // "Equal" 
        ///    else
        ///        // "Not Equal"
        ///  --------------------------------------------------------------------   
        ///  

        private int GetDiferenceToCompareRows(DataTable FirstTable, DataTable SecondTable)
        {
            int result = 0;

            result = FirstTable.Rows.Count;

            foreach (DataRow row1 in FirstTable.Rows)
            {
                foreach (DataRow row2 in SecondTable.Rows)
                {
                    var array1 = row1.ItemArray;
                    var array2 = row2.ItemArray;

                    if (array1.SequenceEqual(array2))
                    {
                        result--;
                    }
                    else
                    {
                        // Console.WriteLine("Not equal: {0} {1}", row1["Drug"], row2["Drug"]);
                    }
                }
            }

            return result;
        }



        public DataTable GetDifferentRecords(DataTable FirstDataTable, DataTable SecondDataTable)
        {

            DataTable ResultDataTable = new DataTable("ResultDataTable");

            using (DataSet ds = new DataSet())
            {
                //Add tables   
                ds.Tables.AddRange(new DataTable[] { FirstDataTable.Copy(), SecondDataTable.Copy() });

                //Get Columns for DataRelation   
                DataColumn[] firstColumns = new DataColumn[ds.Tables[0].Columns.Count];
                for (int i = 0; i < firstColumns.Length; i++)
                {
                    firstColumns[i] = ds.Tables[0].Columns[i];
                }

                DataColumn[] secondColumns = new DataColumn[ds.Tables[1].Columns.Count];
                for (int i = 0; i < secondColumns.Length; i++)
                {
                    secondColumns[i] = ds.Tables[1].Columns[i];
                }

                //Create DataRelation   
                DataRelation r1 = new DataRelation(string.Empty, firstColumns, secondColumns, false);
                ds.Relations.Add(r1);

                DataRelation r2 = new DataRelation(string.Empty, secondColumns, firstColumns, false);
                ds.Relations.Add(r2);

                //Create columns for return table   
                for (int i = 0; i < FirstDataTable.Columns.Count; i++)
                {
                    ResultDataTable.Columns.Add(FirstDataTable.Columns[i].ColumnName, FirstDataTable.Columns[i].DataType);
                }

                //If FirstDataTable Row not in SecondDataTable, Add to ResultDataTable.   
                ResultDataTable.BeginLoadData();
                foreach (DataRow parentrow in ds.Tables[0].Rows)
                {
                    DataRow[] childrows = parentrow.GetChildRows(r1);
                    if (childrows == null || childrows.Length == 0)
                        ResultDataTable.LoadDataRow(parentrow.ItemArray, true);
                }

                //If SecondDataTable Row not in FirstDataTable, Add to ResultDataTable.   

                //foreach ( DataRow parentrow in ds.Tables [ 1 ].Rows )
                //    {
                //    DataRow[] childrows = parentrow.GetChildRows ( r2 );
                //    if ( childrows == null || childrows.Length == 0 )
                //        ResultDataTable.LoadDataRow ( parentrow.ItemArray, true );
                //    }

                //Important - For Revision Inverse-- 

                ResultDataTable.EndLoadData();
            }

            return ResultDataTable;
        }


        #endregion

        #region Compare two DataTables and return a DataTable with Different Records First/Second Table

        /// <summary> 
        /// Compare 2 DataTables and Return 1 Datatable with Rows (Difference). 
        /// </summary> 
        /// <param name="DataTable">FirstDataTable=Source Datatable </param> 
        /// <param name="DataTable">SecondDataTable=Destination Datatable</param> 
        /// <returns>DataTable</returns> 
        /// Example Usage:
        /// DataTable dt;
        /// dt = GetDifferentRecords ( FirstDataTable, SecondDataTable );
        ///    if ( dt.Rows.Count == 0 )
        ///        // "Equal" 
        ///    else
        ///        // "Not Equal"
        ///  --------------------------------------------------------------------   
        ///  


        public static DataTable GetDifferentRecordsInFirstTable(DataTable FirstDataTable, DataTable SecondDataTable)
        {

            //Create Empty Table   
            DataTable ResultDataTable = new DataTable("ResultDataTable");

            //use a Dataset to make use of a DataRelation object   
            using (DataSet ds = new DataSet())
            {
                //Add tables   
                ds.Tables.AddRange(new DataTable[] { FirstDataTable.Copy(), SecondDataTable.Copy() });

                //Get Columns for DataRelation   
                DataColumn[] firstColumns = new DataColumn[ds.Tables[0].Columns.Count];
                for (int i = 0; i < firstColumns.Length; i++)
                {
                    firstColumns[i] = ds.Tables[0].Columns[i];
                }

                DataColumn[] secondColumns = new DataColumn[ds.Tables[1].Columns.Count];
                for (int i = 0; i < secondColumns.Length; i++)
                {
                    secondColumns[i] = ds.Tables[1].Columns[i];
                }

                //Create DataRelation   
                DataRelation r1 = new DataRelation(string.Empty, firstColumns, secondColumns, false);
                ds.Relations.Add(r1);

                DataRelation r2 = new DataRelation(string.Empty, secondColumns, firstColumns, false);
                ds.Relations.Add(r2);

                //Create columns for return table   
                for (int i = 0; i < FirstDataTable.Columns.Count; i++)
                {
                    ResultDataTable.Columns.Add(FirstDataTable.Columns[i].ColumnName, FirstDataTable.Columns[i].DataType);
                }

                //If FirstDataTable Row not in SecondDataTable, Add to ResultDataTable.   
                ResultDataTable.BeginLoadData();
                foreach (DataRow parentrow in ds.Tables[0].Rows)
                {
                    DataRow[] childrows = parentrow.GetChildRows(r1);
                    if (childrows == null || childrows.Length == 0)
                        ResultDataTable.LoadDataRow(parentrow.ItemArray, true);
                }

                //If SecondDataTable Row not in FirstDataTable, Add to ResultDataTable.   
                foreach (DataRow parentrow in ds.Tables[1].Rows)
                {
                    DataRow[] childrows = parentrow.GetChildRows(r2);
                    if (childrows == null || childrows.Length == 0)
                        ResultDataTable.LoadDataRow(parentrow.ItemArray, true);
                }
                ResultDataTable.EndLoadData();
            }

            return ResultDataTable;
        }





        public static DataTable GetDifferentRecordsInSecondTable(DataTable First, DataTable Second)
        {

            DataTable table = new DataTable("Difference");
            DataTable table1 = new DataTable();
            try
            {
                //Must use a Dataset to make use of a DataRelation object
                using (DataSet ds4 = new DataSet())
                {
                    //Add tables
                    ds4.Tables.AddRange(new DataTable[] { First.Copy(), Second.Copy() });

                    //Get Columns for DataRelation
                    DataColumn[] firstcolumns = new DataColumn[ds4.Tables[0].Columns.Count];

                    for (int i = 0; i < firstcolumns.Length; i++)
                    {
                        firstcolumns[i] = ds4.Tables[0].Columns[i];
                    }

                    DataColumn[] secondcolumns = new DataColumn[ds4.Tables[1].Columns.Count];

                    for (int i = 0; i < secondcolumns.Length; i++)
                    {
                        secondcolumns[i] = ds4.Tables[1].Columns[i];
                    }

                    //Create DataRelation
                    DataRelation r = new DataRelation(string.Empty, firstcolumns, secondcolumns, false);

                    ds4.Relations.Add(r);

                    //Create columns for return table
                    for (int i = 0; i < First.Columns.Count; i++)
                    {
                        table.Columns.Add(First.Columns[i].ColumnName, First.Columns[i].DataType);
                    }

                    //If First Row not in Second, Add to return table.
                    table.BeginLoadData();

                    foreach (DataRow parentrow in ds4.Tables[0].Rows)
                    {
                        DataRow[] childrows = parentrow.GetChildRows(r);

                        if (childrows == null || childrows.Length == 0)
                            table.LoadDataRow(parentrow.ItemArray, true);
                        table1.LoadDataRow(childrows, false);

                    }

                    table.EndLoadData();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return table;
        }

        #endregion

        #region Datatable Result Distinct

        private static DataTable SelectDistinct(DataTable SourceTable, params string[] FieldNames)
        {
            object[] lastValues;
            DataTable newTable;
            DataRow[] orderedRows;

            if (FieldNames == null || FieldNames.Length == 0)
                throw new ArgumentNullException("FieldNames");

            lastValues = new object[FieldNames.Length];
            newTable = new DataTable();

            foreach (string fieldName in FieldNames)
                newTable.Columns.Add(fieldName, SourceTable.Columns[fieldName].DataType);

            orderedRows = SourceTable.Select("", string.Join(", ", FieldNames));

            foreach (DataRow row in orderedRows)
            {
                if (!fieldValuesAreEqual(lastValues, row, FieldNames))
                {
                    newTable.Rows.Add(createRowClone(row, newTable.NewRow(), FieldNames));

                    setLastValues(lastValues, row, FieldNames);
                }
            }

            return newTable;
        }

        private static bool fieldValuesAreEqual(object[] lastValues, DataRow currentRow, string[] fieldNames)
        {
            bool areEqual = true;

            for (int i = 0; i < fieldNames.Length; i++)
            {
                if (lastValues[i] == null || !lastValues[i].Equals(currentRow[fieldNames[i]]))
                {
                    areEqual = false;
                    break;
                }
            }

            return areEqual;
        }

        private static DataRow createRowClone(DataRow sourceRow, DataRow newRow, string[] fieldNames)
        {
            foreach (string field in fieldNames)
                newRow[field] = sourceRow[field];

            return newRow;
        }

        private static void setLastValues(object[] lastValues, DataRow sourceRow, string[] fieldNames)
        {
            for (int i = 0; i < fieldNames.Length; i++)
                lastValues[i] = sourceRow[fieldNames[i]];
        }

        #endregion

        #region ConvertToGenericListToDataTable
        /// <summary> 
        /// Return Datatable and Converts a generic List<> into a DataTable. 
        /// </summary> 
        /// <param name="list"></param> 
        /// Example:  DataTable  dt = DataUtils.ConvertTo ( list );
        /// <returns>DataTable</returns> 

        public static DataTable ConvertTo<T>(IList<T> list)
        {
            DataTable table = CreateTable<T>();
            Type entityType = typeof(T);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

            foreach (T item in list)
            {
                DataRow row = table.NewRow();

                foreach (PropertyDescriptor prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;

                }

                table.Rows.Add(row);
            }

            return table;
        }

        public static DataTable CreateTable<T>()
        {
            Type entityType = typeof(T);
            DataTable table = new DataTable(entityType.Name);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

            foreach (PropertyDescriptor prop in properties)
            {

                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

            }

            return table;
        }

        public static DataSet CreateDataset<T>(IList<T> list, string strName)
        {
            DataSet DbMenu;
            DataTable dt;


            DbMenu = new DataSet();
            dt = ConvertTo(list);
            DbMenu.DataSetName = strName;
            DbMenu.Tables.Add(dt);

            return DbMenu;

        }


        #endregion

        #region DataTableToList

        public static List<T> ConvertTo<T>(DataTable table)
        {
            List<T> list = new List<T>();
            if (table == null)
            {
                return null;
            }

            List<DataRow> rows = new List<DataRow>();

            foreach (DataRow row in table.Rows)
            {
                T item = CreateItem<T>(row);
                list.Add(item);
            }

            return list;
        }

        public static T CreateItem<T>(DataRow row)
        {
            Type objType = typeof(T);
            T obj = Activator.CreateInstance<T>();
            if (row != null)
            {
                foreach (DataColumn column in row.Table.Columns)
                {
                    PropertyInfo prop = objType.GetProperty(column.ColumnName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                    object value = row[column.ColumnName];
                    if (value.Equals(DBNull.Value))
                        value = null;

                    prop.SetValue(obj, Convert.ChangeType(value, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType), null);

                }
            }

            return obj;
        }

        #endregion

        #region GenericListToDataTable
        /// <summary> 
        /// Converts a generic List<> into a DataTable. 
        /// </summary> 
        /// <param name="list"></param> 
        /// <returns>DataTable</returns> 
        public static DataTable GenericListToDataTable<T>(List<T> list)
        {
            DataTable dt = null;
            Type listType = list.GetType();
            if (listType.IsGenericType)
            {
                //determine the underlying type the List<> contains 
                Type elementType = listType.GetGenericArguments()[0];

                //create empty table -- give it a name in case 
                //it needs to be serialized 
                dt = new DataTable(elementType.Name + "List");

                //define the table -- add a column for each public 
                //property or field 
                MemberInfo[] miArray = elementType.GetMembers(
                    BindingFlags.Public | BindingFlags.Instance);
                foreach (MemberInfo mi in miArray)
                {
                    if (mi.MemberType == MemberTypes.Property)
                    {
                        PropertyInfo pi = mi as PropertyInfo;
                        dt.Columns.Add(pi.Name, pi.PropertyType);
                    }
                    else if (mi.MemberType == MemberTypes.Field)
                    {
                        FieldInfo fi = mi as FieldInfo;
                        dt.Columns.Add(fi.Name, fi.FieldType);
                    }
                }

                //populate the table 
                IList il = list as IList;
                foreach (object record in il)
                {
                    int i = 0;
                    object[] fieldValues = new object[dt.Columns.Count];
                    foreach (DataColumn c in dt.Columns)
                    {
                        MemberInfo mi = elementType.GetMember(c.ColumnName)[0];
                        if (mi.MemberType == MemberTypes.Property)
                        {
                            PropertyInfo pi = mi as PropertyInfo;
                            fieldValues[i] = pi.GetValue(record, null);
                        }
                        else if (mi.MemberType == MemberTypes.Field)
                        {
                            FieldInfo fi = mi as FieldInfo;
                            fieldValues[i] = fi.GetValue(record);
                        }
                        i++;
                    }
                    dt.Rows.Add(fieldValues);
                }
            }
            return dt;
        }


        public static DataTable ListToDataTable<T>(List<T> list)
        {
            DataTable dt = new DataTable();
            foreach (PropertyInfo info in typeof(T).GetProperties())
            {
                dt.Columns.Add(new DataColumn(info.Name, info.PropertyType));
            }
            foreach (T t in list)
            {
                DataRow row = dt.NewRow();
                foreach (PropertyInfo info in typeof(T).GetProperties())
                {
                    row[info.Name] = info.GetValue(t, null);
                }
                dt.Rows.Add(row);
            }
            return dt;

        }


        #endregion

        public static List<T> EnumToList<T>()
        {
            Type enumType = typeof(T);

            // Can't use type constraints on value types, so have to do check like this
            if (enumType.BaseType != typeof(Enum))
                throw new ArgumentException("T must be of type System.Enum");

            Array enumValArray = Enum.GetValues(enumType);

            List<T> enumValList = new List<T>(enumValArray.Length);

            foreach (int val in enumValArray)
            {
                enumValList.Add((T)Enum.Parse(enumType, val.ToString()));
            }

            return enumValList;
        }

    }
}


