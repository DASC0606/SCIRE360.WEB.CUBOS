using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace Alvisoft.Helpers
    {
    public class AuthUtils
        {


        public static string GenerateRandomString ( )
            {
            var builder = new StringBuilder ( );
            var random = new Random ( );
            for ( int i = 0; i < 6; i++ )
                {
                char ch = Convert.ToChar ( Convert.ToInt32 ( Math.Floor ( 25 * random.NextDouble ( ) + 75 ) ) );
                builder.Append ( ch );
                }
            return builder.ToString ( );
            }


        public static class Md5Encrypt
            {
            public static string Md5EncryptPassword ( string data )
                {
                var encoding = new ASCIIEncoding ( );
                var bytes = encoding.GetBytes ( data );
                var hashed = MD5.Create ( ).ComputeHash ( bytes );
                return Encoding.UTF8.GetString ( hashed );
                }
            }
        }
    }