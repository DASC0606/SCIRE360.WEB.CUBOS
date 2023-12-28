using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace Alvisoft.Helpers
    {
    public partial class DataSetHelper
        {


        public static DataSet ds;


        public  DataSetHelper ( ref DataSet DataSet )

            {

            ds = DataSet;

            }

        public DataSetHelper ( )

            {

            ds = null;

            }


        private static bool ColumnEqual ( object A, object B )
            {

            // Compares two values equals to DBNULL.Value.
            
            if ( A == DBNull.Value && B == DBNull.Value ) 
                return true;
            if ( A == DBNull.Value || B == DBNull.Value ) 
                return false;
            return ( A.Equals ( B ) );  
            }

        /// <summary>
        /// Important/Importante:
        /// Return WebForm Name. 
        /// If the key does not exists then returns the default value.
        /// </summary>
        /// <param name="separator">Array strings character separator.</param>
        /// <param name="applicationPath">AppRelativeVirtualPath variable .</param>
        /// <returns></returns>
        public static DataTable SelectDistinct ( string TableName, DataTable SourceTable, string FieldName )
            {
            DataTable dt = new DataTable ( TableName );
            dt.Columns.Add ( FieldName, SourceTable.Columns [ FieldName ].DataType );

            object LastValue = null;
            foreach ( DataRow dr in SourceTable.Select ( "", FieldName ) )
                {
                if ( LastValue == null || !( ColumnEqual ( LastValue, dr [ FieldName ] ) ) )
                    {
                    LastValue = dr [ FieldName ];
                    dt.Rows.Add ( new object [ ] { LastValue } );
                    }
                }
            if ( ds != null )
                ds.Tables.Add ( dt );
            return dt;
            }


        /// <summary>
        /// Important/Importante:
        /// Return WebForm Name. 
        /// If the key does not exists then returns the default value.
        /// </summary>
        /// <param name="separator">Array strings character separator.</param>
        /// <param name="applicationPath">AppRelativeVirtualPath variable .</param>
        /// <returns></returns>
        public static DataTable SelectDistinct ( string TableName, DataTable SourceTable, params string [ ] Columns )
            {

            DataTable Result = new DataTable ( TableName );

            if ( SourceTable != null )
                {

                DataView DView = SourceTable.DefaultView;

                    Result = DView.ToTable ( true, Columns );

                }

            return Result;

            }



  
        /// <summary>
        /// Important/Importante:
        /// Return  SqlDataReader to Datatable . 
        /// </summary>
        /// <param name="_reader">Result  SqlDataReader variable .</param>
        /// <returns></returns>
   
        //System.Data.DataSet ds = new System.Data.DataSet();
        //ds.Tables.Add(this.GetTable(_SqlDataReader));
        public DataTable GetTable ( SqlDataReader _reader )
            {
            DataTable _table = _reader.GetSchemaTable ( );
            DataTable _dt = new DataTable ( );
            DataColumn _dc;
            DataRow _row;
            System.Collections.ArrayList _al = new System.Collections.ArrayList ( );

            for ( int i = 0; i < _table.Rows.Count; i++ )
                {
                _dc = new DataColumn ( );

                if ( !_dt.Columns.Contains ( _table.Rows [ i ] [ "ColumnName" ].ToString ( ) ) )
                    {

                    _dc.ColumnName = _table.Rows [ i ] [ "ColumnName" ].ToString ( );
                    _dc.Unique = Convert.ToBoolean ( _table.Rows [ i ] [ "IsUnique" ] );
                    _dc.AllowDBNull = Convert.ToBoolean ( _table.Rows [ i ] [ "AllowDBNull" ] );
                    _dc.ReadOnly = Convert.ToBoolean ( _table.Rows [ i ] [ "IsReadOnly" ] );
                    _al.Add ( _dc.ColumnName );
                    _dt.Columns.Add ( _dc );

                    }

                }

            while ( _reader.Read ( ) )
                {

                _row = _dt.NewRow ( );

                for ( int i = 0; i < _al.Count; i++ )
                    {

                    _row [ ( ( System.String ) _al [ i ] ) ] = _reader [ ( System.String ) _al [ i ] ];

                    }

                _dt.Rows.Add ( _row );

                }

            return _dt;

            }


        public static string InsertDataSetBatch(DataSet ds, string TableNameInDatabase, string cnString)
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


        }




    }