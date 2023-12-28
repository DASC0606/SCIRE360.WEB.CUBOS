

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Alvisoft.Helpers
    {

    /*
* Description: convenient static method to make database connection call
*/
    public sealed class DBHelper
        {
            // Get DataTable set of data for passin query
            public static DataTable ExecuteDataTable(string query, CommandType queryType, SqlParameter[] parameters, string connectionStringValue, int indexTable)
            {
                DataTable dt;
                
                using (SqlConnection connection = new SqlConnection(connectionStringValue))
                {
                    connection.Open();
                    SqlCommand command = configureSqlCommand(query, queryType, parameters, connection);

                    try
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataSet ds = new DataSet();
                            adapter.Fill(ds);
                            dt = new DataTable { Locale = new System.Globalization.CultureInfo("en-us") };
                            dt = ds.Tables[indexTable];

                        }
                    }
                    finally { connection.Close(); }
                }
                return dt;
            }
        
        // Get DataTable set of data for passin query
        public static DataTable ExecuteDataTable ( string query, CommandType queryType, SqlParameter [ ] parameters, string connectionStringValue )
            {
            DataTable dt;
            using ( SqlConnection connection = new SqlConnection ( connectionStringValue ) )
                {
                connection.Open ( );
                SqlCommand command = configureSqlCommand ( query, queryType, parameters, connection );

                try
                    {
                    using ( SqlDataAdapter adapter = new SqlDataAdapter ( command ) )
                        {
                        adapter.Fill ( dt = new DataTable { Locale = new System.Globalization.CultureInfo ( "en-us" ) } );
                        }
                    }
                finally { connection.Close ( ); }
                }
            return dt;
            }

        // Get execute query using ExecuteNonQuery
        public static int ExecuteNonQuery ( string query, CommandType queryType, SqlParameter [ ] parameters, string connectionStringValue )
            {
            int result = 0;
            using ( SqlConnection connection = new SqlConnection ( connectionStringValue ) )
                {
                connection.Open ( );
                SqlCommand command = configureSqlCommand ( query, queryType, parameters, connection );

                try
                    {
                    result = command.ExecuteNonQuery ( );
                    }
                finally
                    {
                    connection.Close ( );
                    }
                }
            return result;
            }

        // Get execute query using ExecuteScalar
        public static object ExecuteScalar ( string query, CommandType queryType, SqlParameter [ ] parameters, string connectionStringValue )
            {
            object result = 0;
            using ( SqlConnection connection = new SqlConnection ( connectionStringValue ) )
                {
                connection.Open ( );
                SqlCommand command = configureSqlCommand ( query, queryType, parameters, connection );

                try
                    {
                    result = command.ExecuteScalar ( );
                    }
                finally
                    {
                    connection.Close ( );
                    }
                }
            return result;
            }

        private static SqlCommand configureSqlCommand ( string query, CommandType queryType, SqlParameter [ ] parameters, SqlConnection connection )
            {
            SqlCommand command = connection.CreateCommand ( );
            command.CommandType = queryType;
            command.CommandText = query;
            command.CommandTimeout = 60;

            // add parameter collection to command
            if ( parameters != null )
                command.Parameters.AddRange ( parameters );

            return command;
            }
        }

   


    }