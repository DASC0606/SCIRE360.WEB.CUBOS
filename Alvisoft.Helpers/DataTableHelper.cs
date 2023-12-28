using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Reflection;
using DBHelper;
using DBHelper.SqlClient;
using System.Xml.Serialization;

using System.IO;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;

using System.Reflection.Emit;
using System.Threading;




namespace Alvisoft.Helpers

    {    
        /*
        * Description: Convert DataTable to Ilist and by versa
        */

    
    public static class DataTableHelper
        {


       
        

       public static string MatchTableInsertUpdateToDatabase(string cnString, string fileNameRoute, string fieldsToCriteria, string fieldsSort, string TableNameInDatabase, int flag)
        {

            string strStatus = "";
            try
            {
                DataSet ds = new DataSet();
                DataSet dsTemp = new DataSet();
                DataTable dt = new DataTable();
                DataTable dtTemp = new DataTable();
                DataTable dtData = new DataTable();
                DataTable dtDifference = new DataTable();
                DataTable dtUnique = new DataTable();
                SqlTableHelper mobjTableHelper;
                SqlConnectionProvider mobjCnnProvider;

                mobjCnnProvider = new SqlConnectionProvider();
                mobjCnnProvider.ConnectionString = cnString;

                mobjTableHelper = new SqlTableHelper(TableNameInDatabase);
                mobjTableHelper.MainConnectionProvider = mobjCnnProvider;


                SqlConnection conn = new SqlConnection(cnString);
                //= CreateConnection ( "AvisoftWebService" );
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(DataSet));
                FileStream readStream = new FileStream(fileNameRoute, FileMode.Open);
                ds = (DataSet)xmlSerializer.Deserialize(readStream);
                readStream.Close();

                ds.Tables[0].DefaultView.Sort = fieldsSort;
                dt = ds.Tables[0];
                dt.TableName = TableNameInDatabase + "_temp";
                string[] columnNames = (from dc in dt.Columns.Cast<DataColumn>() select dc.ColumnName).ToArray();

                mobjTableHelper.Compile();

                mobjCnnProvider.OpenConnection();

                mobjTableHelper.Data.Clear();
                mobjTableHelper.SelectCriteria = fieldsToCriteria;

                dtUnique = mobjTableHelper.SelectSome();
                DataView dv = new DataView(dtUnique);
                dtTemp = dv.ToTable(true, columnNames);
                dtTemp.DefaultView.Sort = fieldsSort;

                mobjCnnProvider.CloseConnection();

                
                dtData = dt.Clone();
                dtDifference = dt.Clone();


                foreach (DataRow row1 in dt.Rows)
                {
                    foreach (DataRow row2 in dtTemp.Rows)
                    {
                        var array1 = row1.ItemArray;
                        var array2 = row2.ItemArray;

                        if (array1.SequenceEqual(array2))
                        {
                            //result--;
                            //     dt.Rows.Remove(row1);
                            //  dtTemp.Rows.Remove(row2);

                        }
                        else
                        {
                            if (Convert.ToString(row2["ruc"]).Trim() == Convert.ToString(row1["ruc"]).Trim())
                            {
                                dtDifference.ImportRow(row1);

                            }
                            else
                            {
                                dtData.ImportRow(row1);
                            }
                        }
                    }
                }



                if (dtDifference.Rows.Count > 0)
                {
                    // mobjTableHelper.UpdateCriteria = fieldsToCriteria;
                    dtUnique.Clear();
                    //  dtUnique = dtDifference.Copy();

                    mobjTableHelper.Data.Rows.Clear();
                    //select the records that need to update
                    mobjTableHelper.SelectCriteria = fieldsToCriteria;
                    dtUnique = mobjTableHelper.SelectSome();

                    for (int j = 0; j <= dtDifference.Rows.Count - 1; j++)
                    {
                        foreach (DataColumn dc in dt.Columns)
                        {
                            dtUnique.Rows[j].BeginEdit();
                            dtUnique.Rows[j][dc.ColumnName] = dtDifference.Rows[j][dc.ColumnName];
                        }
                    }


                    //  dtUnique = dt;
                    mobjTableHelper.UpdateCriteria = fieldsToCriteria;
                    mobjTableHelper.CriteriaType = DBCriteria.UseFilterExpression;
                    //  mobjTableHelper.CriteriaType = DBCriteria.UsePrimaryKey;
                    mobjCnnProvider.OpenConnection();
                    mobjTableHelper.Update();
                    mobjCnnProvider.CloseConnection();
                    dtUnique.AcceptChanges();
                    strStatus = "Se han actualizado " + dtTemp.Rows.Count + " registro(s) en la tabla '" + TableNameInDatabase + "'.";
                }
                else
                {
                    strStatus = "No se han detectado registros nuevos ni actualizados en su envío para la tabla '" + TableNameInDatabase + "'.";
                }


                if (dtData.Rows.Count > 0)
                {
                    dsTemp.Tables.Add(dtData);

                    strStatus = InsertDataSetBatch(cnString,dsTemp, TableNameInDatabase);
                    strStatus += " - Se han guardado " + dtData.Rows.Count + "  nuevos registros en la tabla '" + TableNameInDatabase + "'.";
                }





            }

            catch (Exception ex)
            {
                strStatus = "Ocurrio un error:" + ExceptionManager.DoLogAndGetFriendlyMessageBoxForException(ex);
            }

            return strStatus;

        }

        public static string InsertDataSetBatch(string cnString, DataSet ds, string TableNameInDatabase)
        {

            string strStatus = "";
            try
            {
                //    string cnString =  ConfigurationManager.ConnectionStrings [ "AvisoftWebService" ].ConnectionString;

                DataSet dsTemp = new DataSet();
                DataTable dt = new DataTable();
                DataTable dtTemp = new DataTable();


                SqlConnection conn = new SqlConnection(cnString);

                dt = ds.Tables[0];
                dt.TableName = TableNameInDatabase;

                conn.Open();

                IDbTransaction dbTransaction = conn.BeginTransaction();

                try
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        HelperBuildInsertSQL.InsertDataRow(row, cnString);
                    }

                    strStatus = "OK";
                    dbTransaction.Commit();
                }
                catch
                {
                    dbTransaction.Rollback();
                    dt.Dispose();
                    ds.Dispose();
                    //   conn.Close ( );
                    //   conn.Close ( );
                    throw;

                }

                finally
                {


                    dt.Dispose();
                    ds.Dispose();
                    //    conn.Close ( );
                    dbTransaction.Dispose();

                }
            }

            catch (Exception ex)
            {
                strStatus = "Ocurrio un error:" + ExceptionManager.DoLogAndGetFriendlyMessageBoxForException(ex);
            }

            return strStatus;

        }


        public static string GenericUpdateData( string  cnString, string tableName, string fieldsSelect, string fieldsToCriteria, string fieldCriteria, string FieldsToUpdate, DataRow dr, string FieldReturn, int flag)
        {
            string strStatus = "";
            SqlTableHelper mobjTableHelper;
            SqlConnectionProvider mobjCnnProvider;
        
            //Initialize table helper
            DataTable dto = new DataTable();
            
            //  DataRow row;
            //"Tbl_Personal_Scire_Plan"

            try
            {
                mobjCnnProvider = new SqlConnectionProvider();
                mobjCnnProvider.ConnectionString = cnString;

                mobjTableHelper = new SqlTableHelper(tableName);
                mobjTableHelper.MainConnectionProvider = mobjCnnProvider;

                mobjTableHelper.FieldsToSelect = fieldsSelect;
                mobjTableHelper.Compile();

                mobjCnnProvider.OpenConnection();

                mobjTableHelper.Data.Clear();
                mobjTableHelper.SelectCriteria = fieldsToCriteria;
                mobjTableHelper.Sort = fieldCriteria + "  ASC";

                dto = mobjTableHelper.SelectSome();
                mobjCnnProvider.CloseConnection();
                //DataView dv1 = new DataView(dto);
                //dto = dv1.ToTable(true, "ruc", "id_personal");

                mobjTableHelper.FieldsToUpdate = FieldsToUpdate;
                mobjTableHelper.Compile();

                //must clear the records first to avoid confusion with any previous selection
                mobjTableHelper.Data.Rows.Clear();
                //select the records that need to update
                mobjTableHelper.SelectCriteria = fieldsToCriteria;
                dto = mobjTableHelper.SelectSome();

                //   row = dto.Rows[0];
                if (dto.Rows.Count != 0)
                {

                    for (int j = 0; j <= dto.Rows.Count - 1; j++)
                    {
                        foreach (DataColumn dc in dr.Table.Columns)
                        {
                            dto.Rows[j].BeginEdit();
                            dto.Rows[j][dc.ColumnName] = dr[dc.ColumnName];
                        }
                    }

                    //update using primary key
                    mobjTableHelper.UpdateCriteria = fieldsToCriteria;
                    mobjTableHelper.CriteriaType = DBCriteria.UseFilterExpression;

                    //     mobjTableHelper.CriteriaType = DBCriteria.UsePrimaryKey;

                    mobjCnnProvider.OpenConnection();
                    mobjTableHelper.Update();
                    mobjCnnProvider.CloseConnection();

                    dto.AcceptChanges();

                    strStatus = "\n\nData enviada para la empresa :\n" + Convert.ToString(dto.Rows[0][FieldReturn]) + "."; 
                }
                else
                {
                    strStatus = "";
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.DoLogAndGetFriendlyMessageBoxForException(ex);

            }
            finally
            {
                dto.Dispose();
            }

            return strStatus;
        }

        /// <summary>
        /// Important/Importante:
        /// Return number rows FirstDataTable is not in SecondDataTable. 
        /// </summary>
        /// <param name="FirstDataTable">First DataTable variable .</param>
        /// <param name="SecondDataTable">Second DataTable variable</param>
        /// <returns> Integer </returns>
        public static int NumberCompareDataRows(DataTable FirstDataTable, DataTable SecondDataTable)
        {

            int result = FirstDataTable.Rows.Count;

            foreach (DataRow row1 in FirstDataTable.Rows)
            {
                foreach (DataRow row2 in SecondDataTable.Rows)
                {
                    var array1 = row1.ItemArray;
                    var array2 = row2.ItemArray;

                    if (array1.SequenceEqual(array2))
                    {
                        result--;
                    }
                    else
                    {
                        // 
                    }
                }
            }

            return result;
        }


        /// <summary>
        /// Important/Importante:
        /// Return number rows FirstDataTable is not in SecondDataTable. 
        /// </summary>
        /// <param name="FirstDataTable">First DataTable variable .</param>
        /// <param name="SecondDataTable">Second DataTable variable</param>
        /// <returns> Integer </returns>
        public static DataTable GetDifferenceToCompareDataRows(DataTable FirstDataTable, DataTable SecondDataTable)
        {

            int result = FirstDataTable.Rows.Count;
            DataTable dt = new DataTable();
            dt= FirstDataTable.Clone();

            foreach (DataRow row1 in FirstDataTable.Rows)
            {
                foreach (DataRow row2 in SecondDataTable.Rows)
                {
                    var array1 = row1.ItemArray;
                    var array2 = row2.ItemArray;

                    if (!array1.SequenceEqual(array2))
                    {
                        //result--;
                        dt.ImportRow(row1);

                    }
                    //else
                    //{
                    //    // 
                    //}
                }
            }

            return dt;
        }


        /**********************************************************************************/
        public static IList<T> ToList<T> ( this DataTable table ) where T : new ( )
            {
            IList<PropertyInfo> properties = typeof ( T ).GetProperties ( ).ToList ( );
            IList<T> result = new List<T> ( );

            foreach ( var row in table.Rows )
                {
                var item = CreateItemFromRow<T> ( ( DataRow ) row, properties );
                result.Add ( item );
                }

            return result;
            }

        private static T CreateItemFromRow<T> ( DataRow row, IList<PropertyInfo> properties ) where T : new ( )
            {
            T item = new T ( );
            foreach ( var property in properties )
                {
                property.SetValue ( item, row [ property.Name ], null );
                }
            return item;
            }




      /************************************************************************************/
        public static DataTable ConvertTo<T> ( IList<T> list )
            {
            DataTable table = CreateTable<T> ( );
            Type entityType = typeof ( T );
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties ( entityType );
            foreach ( T item in list )
                {
                DataRow row = table.NewRow ( );
                foreach ( PropertyDescriptor prop in properties )
                    row [ prop.Name ] = prop.GetValue ( item );
                table.Rows.Add ( row );
                }
            return table;
            }

        public static IList<T> ConvertTo<T> ( IList<DataRow> rows )
            {
            IList<T> list = null;
            if ( rows != null )
                {
                list = new List<T> ( );
                foreach ( DataRow row in rows )
                    {
                    T item = CreateItem<T> ( row );
                    list.Add ( item );
                    }
                }
            return list;
            }

        public static IList<T> ConvertTo<T> ( DataTable table )
            {
            if ( table == null )
                return null;

            List<DataRow> rows = new List<DataRow> ( );
            foreach ( DataRow row in table.Rows )
                rows.Add ( row );

            return ConvertTo<T> ( rows );
            }

        //Convert DataRow into T Object
        public static T CreateItem<T> ( DataRow row )
            {
            string columnName;
            T obj = default ( T );
            if ( row != null )
                {
                obj = Activator.CreateInstance<T> ( );
                foreach ( DataColumn column in row.Table.Columns )
                    {
                    columnName = column.ColumnName;
                    //Get property with same columnName
                    PropertyInfo prop = obj.GetType ( ).GetProperty ( columnName );
                    try
                        {
                        //Get value for the column
                        object value = ( row [ columnName ].GetType ( ) == typeof ( DBNull ) )
                        ? null : row [ columnName ];
                        //Set property value
                        prop.SetValue ( obj, value, null );
                        }
                    catch
                        {
                        throw;
                        //Catch whatever here
                        }
                    }
                }
            return obj;
            }

        public static DataTable CreateTable<T> ( )
            {
            Type entityType = typeof ( T );
            DataTable table = new DataTable ( entityType.Name );
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties ( entityType );

            foreach ( PropertyDescriptor prop in properties )
                table.Columns.Add ( prop.Name, prop.PropertyType );

            return table;
            }


        public static DataTable ConvertODSToDatatable(ObjectDataSource ods)
        {
            //var ds = new DataSet();
            DataView dv = new DataView();

            dv = (DataView)ods.Select();

            DataTable dt = new DataTable();

            if (dv != null && dv.Count > 0)
            {
                dt = dv.ToTable();
                // ds.Tables.Add(dt);
            }
            return dt;
        }

        /// <summary>
        /// Swaps the rows and columns of a nested sequence.
        /// </summary>
        /// <typeparam name="T">The type of elements in the sequence.</typeparam>
        /// <param name="source">The source sequence.</param>
        /// <returns>A sequence whose rows and columns are swapped.</returns>
        public static IEnumerable<IEnumerable<T>> Transpose<T>(
                 this IEnumerable<IEnumerable<T>> source)
        {
            return from row in source
                   from col in row.Select(
                       (x, i) => new KeyValuePair<int, T>(i, x))
                   group col.Value by col.Key into c
                   select c as IEnumerable<T>;
        }



        #region "Convert DataTable to List<dynamic>"

        public static List<dynamic> ToDynamicList(DataTable dt)
        {
            List<string> cols = (dt.Columns.Cast<DataColumn>()).Select(column => column.ColumnName).ToList();
            return ToDynamicList(ToDictionary(dt), getNewObject(cols));
        }
        public static List<Dictionary<string, object>> ToDictionary(DataTable dt)
        {
            var columns = dt.Columns.Cast<DataColumn>();
            var Temp = dt.AsEnumerable().Select(dataRow => columns.Select(column =>
                                 new { Column = column.ColumnName, Value = dataRow[column] })
                             .ToDictionary(data => data.Column, data => data.Value)).ToList();
            return Temp.ToList();
        }
        public static List<dynamic> ToDynamicList(List<Dictionary<string, object>> list, Type TypeObj)
        {
            dynamic temp = new List<dynamic>();
            foreach (Dictionary<string, object> step in list)
            {
                object Obj = Activator.CreateInstance(TypeObj);
                PropertyInfo[] properties = Obj.GetType().GetProperties();
                Dictionary<string, object> DictList = (Dictionary<string, object>)step;
                foreach (KeyValuePair<string, object> keyValuePair in DictList)
                {
                    foreach (PropertyInfo property in properties)
                    {
                        if (property.Name == keyValuePair.Key)
                        {
                            property.SetValue(Obj, keyValuePair.Value.ToString(), null);
                            break;
                        }
                    }
                }
                temp.Add(Obj);
            }
            return temp;
        }
        private static Type getNewObject(List<string> list)
        {
            AssemblyName assemblyName = new AssemblyName();
            assemblyName.Name = "tmpAssembly";
            AssemblyBuilder assemblyBuilder = Thread.GetDomain().DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            ModuleBuilder module = assemblyBuilder.DefineDynamicModule("tmpModule");
            TypeBuilder typeBuilder = module.DefineType("WebgridRowCellCollection", TypeAttributes.Public);
            foreach (string step in list)
            {
                string propertyName = step;
                FieldBuilder field = typeBuilder.DefineField(propertyName, typeof(string), FieldAttributes.Public);
                PropertyBuilder property = typeBuilder.DefineProperty(propertyName, System.Reflection.PropertyAttributes.None, typeof(string), new Type[] { typeof(string) });
                MethodAttributes GetSetAttr = MethodAttributes.Public | MethodAttributes.HideBySig;
                MethodBuilder currGetPropMthdBldr = typeBuilder.DefineMethod("get_value", GetSetAttr, typeof(string), Type.EmptyTypes);
                ILGenerator currGetIL = currGetPropMthdBldr.GetILGenerator();
                currGetIL.Emit(OpCodes.Ldarg_0);
                currGetIL.Emit(OpCodes.Ldfld, field);
                currGetIL.Emit(OpCodes.Ret);
                MethodBuilder currSetPropMthdBldr = typeBuilder.DefineMethod("set_value", GetSetAttr, null, new Type[] { typeof(string) });
                ILGenerator currSetIL = currSetPropMthdBldr.GetILGenerator();
                currSetIL.Emit(OpCodes.Ldarg_0);
                currSetIL.Emit(OpCodes.Ldarg_1);
                currSetIL.Emit(OpCodes.Stfld, field);
                currSetIL.Emit(OpCodes.Ret);
                property.SetGetMethod(currGetPropMthdBldr);
                property.SetSetMethod(currSetPropMthdBldr);
            }
            Type obj = typeBuilder.CreateType();
            return obj;
        }

        #endregion



#region "Pivot DataTable"

        public static DataTable DataTableToPivotItems(DataTable source, string keyField, string ColumnField, string ValueField)
        {
            var reqs = new List<string>();
            foreach (DataRow row in source.Rows)
            {
                var req = row[ColumnField].ToString().Replace(' ', '_');
                if (reqs.Contains(req)) continue;
                reqs.Add(req);
            }

            var pivot = new DataTable();
            pivot.Columns.Add(keyField);
            foreach (var req in reqs)
            {
                pivot.Columns.Add(req, typeof(string));
            }

            foreach (DataRow row in source.Rows)
            {
                //if ( row[keyField].ToString() == "")
                //{
                var rows = pivot.Select(keyField + "=" + row[keyField]);
                var pivotRow = pivot.NewRow();

                if (rows.Length == 1)
                {
                    pivotRow = rows[0];
                }
                else
                {
                    pivot.Rows.Add(pivotRow);
                }
                pivotRow[keyField] = row[keyField];
                pivotRow[row[ColumnField].ToString().Replace(' ', '_')] = row[ValueField];
                // break;
                //}
            }

            return pivot;
        }


#endregion



       public static DataTable JoinDataTables(DataTable t1, DataTable t2, params Func<DataRow, DataRow, bool>[] joinOn)
        {
            DataTable result = new DataTable();
            foreach (DataColumn col in t1.Columns)
            {
                if (result.Columns[col.ColumnName] == null)
                    result.Columns.Add(col.ColumnName, col.DataType);
            }
            foreach (DataColumn col in t2.Columns)
            {
                if (result.Columns[col.ColumnName] == null)
                    result.Columns.Add(col.ColumnName, col.DataType);
            }
            foreach (DataRow row1 in t1.Rows)
            {
                var joinRows = t2.AsEnumerable().Where(row2 =>
                {
                    foreach (var parameter in joinOn)
                    {
                        if (!parameter(row1, row2)) return false;
                    }
                    return true;
                });
                foreach (DataRow fromRow in joinRows)
                {
                    DataRow insertRow = result.NewRow();
                    foreach (DataColumn col1 in t1.Columns)
                    {
                        insertRow[col1.ColumnName] = row1[col1.ColumnName];
                    }
                    foreach (DataColumn col2 in t2.Columns)
                    {
                        insertRow[col2.ColumnName] = fromRow[col2.ColumnName];
                    }
                    result.Rows.Add(insertRow);
                }
            }
            return result;
        }


       public static  System.Collections.ArrayList  ConvertDataSetToArrayList(DataTable dt )
       {


           System.Collections.ArrayList arrlst = new System.Collections.ArrayList();
           foreach (DataRow row in dt.Rows)
           {
               arrlst.Add(row);
           }
           return arrlst;
       }



       public static DataTable FullOuterJoinMethod(DataTable LeftTable, DataTable RightTable,
           String LeftPrimaryColumn, String RightPrimaryColumn)
       {
           //first create the datatable columns 
           DataSet mydataSet = new DataSet();
           mydataSet.Tables.Add("  ");
           DataTable myDataTable = mydataSet.Tables[0];

           //add left table columns 
           DataColumn[] dcLeftTableColumns = new DataColumn[LeftTable.Columns.Count];
           LeftTable.Columns.CopyTo(dcLeftTableColumns, 0);

           foreach (DataColumn LeftTableColumn in dcLeftTableColumns)
           {
               if (!myDataTable.Columns.Contains(LeftTableColumn.ToString()))
                   myDataTable.Columns.Add(LeftTableColumn.ToString());
           }

           //now add right table columns 
           DataColumn[] dcRightTableColumns = new DataColumn[RightTable.Columns.Count];
           RightTable.Columns.CopyTo(dcRightTableColumns, 0);

           foreach (DataColumn RightTableColumn in dcRightTableColumns)
           {
               if (!myDataTable.Columns.Contains(RightTableColumn.ToString()))
               {
                   if (RightTableColumn.ToString() != RightPrimaryColumn)
                       myDataTable.Columns.Add(RightTableColumn.ToString());
               }
           }

           //add left-table data to mytable 
           foreach (DataRow LeftTableDataRows in LeftTable.Rows)
           {
               myDataTable.ImportRow(LeftTableDataRows);
           }

           System.Collections.ArrayList var = new System.Collections.ArrayList(); //this variable holds the id's which have joined 

           System.Collections.ArrayList LeftTableIDs = new System.Collections.ArrayList();
           //LeftTableIDs = DataSetToArrayList(0, LeftTable);
           LeftTableIDs = ConvertDataSetToArrayList( LeftTable);

           //import righttable which having not equal Id's with lefttable 
           foreach (DataRow rightTableDataRows in RightTable.Rows)
           {
               if (LeftTableIDs.Contains(rightTableDataRows[0]))
               {
                   string wherecondition = "[" + myDataTable.Columns[0].ColumnName + "]='"
                           + rightTableDataRows[0].ToString() + "'";
                   DataRow[] dr = myDataTable.Select(wherecondition);
                   int iIndex = myDataTable.Rows.IndexOf(dr[0]);

                   foreach (DataColumn dc in RightTable.Columns)
                   {
                       if (dc.Ordinal != 0)
                           myDataTable.Rows[iIndex][dc.ColumnName.ToString().Trim()] =
                   rightTableDataRows[dc.ColumnName.ToString().Trim()].ToString();
                   }
               }
               else
               {
                   int count = myDataTable.Rows.Count;
                   DataRow row = myDataTable.NewRow();
                   row[0] = rightTableDataRows[0].ToString();
                   myDataTable.Rows.Add(row);
                   foreach (DataColumn dc in RightTable.Columns)
                   {
                       if (dc.Ordinal != 0)
                           myDataTable.Rows[count][dc.ColumnName.ToString().Trim()] =
                   rightTableDataRows[dc.ColumnName.ToString().Trim()].ToString();
                   }
               }
           }

           return myDataTable;
       }



       public static void DeleteRowsFromDataTable(DataTable dataTable, string columnName, string columnValue)
       {
           IEnumerable<DataRow> dataRows = (from t in dataTable.AsEnumerable()
                                            where t.Field<string>(columnName) == columnValue
                                            select t);
           foreach (DataRow row in dataRows)
               dataTable.Rows.Remove(row);
       }


       public static DataTable RemoveAllDuplicates(DataTable dt, string filterColumm)
       {
           DataTable Datatableduplicate = dt.Clone();
           var dupsFromCol = from dr in dt.AsEnumerable()
                             group dr by dr[filterColumm] into groups
                             where groups.Count() > 1
                             select groups;
           foreach (var duplicate in dupsFromCol)
           {
               for (int y = 0; y < duplicate.Count(); y++)
               {
                   DataRow dr = Datatableduplicate.NewRow();
                   dr = duplicate.ElementAt(y);
                   Datatableduplicate.ImportRow(dr);
                   dt.Rows.Remove(dr);
               }

           }

           return dt;

       }


    }
}