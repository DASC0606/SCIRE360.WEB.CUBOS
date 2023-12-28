using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace Alvisoft.Helpers
    {
    public class DBUtil
        {
        // Methods
        public static object ConvertTo<T> ( T? parameter ) where T : struct
            {
            if ( parameter.HasValue )
                {
                return parameter.Value;
                }
            return DBNull.Value;
            }

        public static object ConvertTo<T> ( T parameter ) where T : class
            {
            if ( parameter == null )
                {
                return DBNull.Value;
                }
            return parameter;
            }

        public static T? Get<T> ( IDataReader reader, string FielName ) where T : struct
            {
            int ordinal = reader.GetOrdinal ( FielName );
            if ( !reader.IsDBNull ( ordinal ) )
                {
                return new T? ( ( T ) reader.GetValue ( ordinal ) );
                }
            return null;
            }

        public static bool GetAsBoolean ( IDataReader reader, string sField )
            {
            int ordinal = reader.GetOrdinal ( sField );
            return ( !reader.IsDBNull ( ordinal ) && reader.GetBoolean ( ordinal ) );
            }

        public static byte [ ] GetAsBytes ( IDataReader reader, string sField )
            {
            int ordinal = reader.GetOrdinal ( sField );
            if ( !reader.IsDBNull ( ordinal ) )
                {
                return ( byte [ ] ) reader.GetValue ( ordinal );
                }
            return null;
            }

        public static DateTime GetAsDateTime ( IDataReader reader, string sField )
            {
            int ordinal = reader.GetOrdinal ( sField );
            if ( !reader.IsDBNull ( ordinal ) )
                {
                return reader.GetDateTime ( ordinal );
                }
            return DateTime.MinValue;
            }

        public static decimal GetAsDecimal ( IDataReader reader, string sField )
            {
            int ordinal = reader.GetOrdinal ( sField );
            if ( !reader.IsDBNull ( ordinal ) )
                {
                return reader.GetDecimal ( ordinal );
                }
            return 0.0M;
            }

        public static int GetAsInteger ( IDataReader reader, string sField )
            {
            int ordinal = reader.GetOrdinal ( sField );
            if ( !reader.IsDBNull ( ordinal ) )
                {
                return Convert.ToInt32 ( reader.GetValue ( ordinal ) );
                }
            return 0;
            }

        public static string GetAsString ( IDataReader reader, string sField )
            {
            int ordinal = reader.GetOrdinal ( sField );
            if ( !reader.IsDBNull ( ordinal ) )
                {
                return reader.GetString ( ordinal );
                }
            return "";
            }


        public static DataTable DataViewToDataTable ( DataView obDataView )
            {
            if ( null == obDataView )
                {
                throw new ArgumentNullException
                ( "DataView", "Invalid DataView object specified" );
                }

            DataTable obNewDt = obDataView.Table.Clone ( );
            int idx = 0;
            string [] strColNames = new string [ obNewDt.Columns.Count ];
            foreach ( DataColumn col in obNewDt.Columns )
                {
                strColNames [ idx++ ] = col.ColumnName;
                }

            IEnumerator viewEnumerator = obDataView.GetEnumerator ( );
            while ( viewEnumerator.MoveNext ( ) )
                {
                DataRowView drv = ( DataRowView ) viewEnumerator.Current;
                DataRow dr = obNewDt.NewRow ( );
                try
                    {
                    foreach ( string strName in strColNames )
                        {
                        dr [ strName ] = drv [ strName ];
                        }
                    }
                catch ( Exception ex )
                    {
                    ExceptionManager.DoLogAndGetFriendlyMessageForException ( ex );
                    }
                obNewDt.Rows.Add ( dr );
                }

            return obNewDt;
            }	

        }
    }
