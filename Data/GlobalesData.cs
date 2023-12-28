using System;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Configuration;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;


namespace Alvisoft.DataAccessLayer
{
    public static class GlobalesData
    {
        #region "Variables privadas"
        private static Database m_database;
        private static Database m_database_digemin;
        private static Database m_database_RQ;
        private static Database m_database_SerWeb;
        private static Database m_database_ConWeb;
        private static Database m_database_Produccion; 
        #endregion

        #region "Variables publicas"
        public static string ServidorDB;
        public static string UsuarioDB;
        public static string PasswordDB;
        public static string NombreDB;

        public static string ServidorDBDigemin;
        public static string UsuarioDBDigemin;
        public static string PasswordDBDigemin;
        public static string NombreDBDigemin;

        public static string ServidorDBAccesoWeb;
        public static string UsuarioDBAccesoWeb;
        public static string PasswordDBAccesoWeb;
        public static string NombreDBAccesoWeb;

        public static string ServidorDBConsultaWeb;
        public static string UsuarioDBConsultaWeb;
        public static string PasswordDBConsultaWeb;
        public static string NombreDBConsultaWeb;

        public static string ServidorDBinner;
        public static string UsuarioDBinner;
        public static string PasswordDBinner;
        public static string NombreDBinner;
        #endregion

        public static Database CreateDatabase()
        {
            if (m_database == null)
            {
                CargarConfig();
                m_database = new SqlDatabase("Database=" + NombreDB + ";Server=" + ServidorDB + ";user=" + UsuarioDB + ";password=" + PasswordDB + ";");

            }
            return m_database;
        }
        
        // public static string DefaultAPI = ConfigurationSettings.AppSettings["apiRest"].ToString();


        internal static void CargarConfig()
        {
            NombreDB = ConfigurationSettings.AppSettings["NombreDB"].ToString();
            ServidorDB = ConfigurationSettings.AppSettings["ServidorDB"].ToString();
            UsuarioDB = ConfigurationSettings.AppSettings["UsuarioDB"].ToString();
            PasswordDB = EncDec.Decrypt(ConfigurationSettings.AppSettings["PasswordDB"].ToString(), "SergiusMaximus");
        }

        #region Query Dynamic

        public static string CSql { get; set; }
        public static string CQuery { get; set; }
        public static CommandType CType { get; set; }
        public static SqlParameter[] Parametros { get; set; }
        public static string orderBy { get; set; }

        static string conex = String.Empty;

        
        #endregion




        

 
        
    }
}
