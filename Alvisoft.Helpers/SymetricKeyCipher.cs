using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alvisoft.Helpers
    {
    public partial class SymetricKeyCipher

        {
        /// <summary> 
        /// Return  Cipher/ClearText Strings . 
        /// </summary> 
        /// <param name="string">Ckey= String to Crypt/Decrypt</param> 
        /// <param name="bool">tNormal= Flag to Crypt/Decrypt according to sended string variable Ckey</param> 
        /// Example Crypt:  string  strKey = Pase ( myString, false ); return Crypt string 
        /// Example Decrypt:  string  strKey = Pase ( myCryptString, true ); return ClearText string 
        /// <returns>string</returns> 

        public static string Pase ( string cKey, bool tNormal )
            {
            string[,] mClave=new string [ 38, 2 ];
            mClave [ 0, 0 ] = "a";
            mClave [ 0, 1 ] = "#";
            mClave [ 1, 0 ] = "b";
            mClave [ 1, 1 ] = "$";
            mClave [ 2, 0 ] = "c";
            mClave [ 2, 1 ] = "%";
            mClave [ 3, 0 ] = "d";
            mClave [ 3, 1 ] = "!";
            mClave [ 4, 0 ] = "e";
            mClave [ 4, 1 ] = ",";
            mClave [ 5, 0 ] = "f";
            mClave [ 5, 1 ] = "\\";
            mClave [ 6, 0 ] = "g";
            mClave [ 6, 1 ] = "/";
            mClave [ 7, 0 ] = "h";
            mClave [ 7, 1 ] = "(";
            mClave [ 8, 0 ] = "i";
            mClave [ 8, 1 ] = ")";
            mClave [ 9, 0 ] = "j";
            mClave [ 9, 1 ] = "@";
            mClave [ 10, 0 ] = "k";
            mClave [ 10, 1 ] = "d";
            mClave [ 11, 0 ] = "l";
            mClave [ 11, 1 ] = "+";
            mClave [ 12, 0 ] = "m";
            mClave [ 12, 1 ] = "[";
            mClave [ 13, 0 ] = "n";
            mClave [ 13, 1 ] = "«";
            mClave [ 14, 0 ] = "ñ";
            mClave [ 14, 1 ] = "»";
            mClave [ 15, 0 ] = "o";
            mClave [ 15, 1 ] = "a";
            mClave [ 16, 0 ] = "p";
            mClave [ 16, 1 ] = "b";
            mClave [ 17, 0 ] = "q";
            mClave [ 17, 1 ] = "c";
            mClave [ 18, 0 ] = "r";
            mClave [ 18, 1 ] = "©";
            mClave [ 19, 0 ] = "s";
            mClave [ 19, 1 ] = "î";
            mClave [ 20, 0 ] = "t";
            mClave [ 20, 1 ] = "Ä";
            mClave [ 21, 0 ] = "u";
            mClave [ 21, 1 ] = "}";
            mClave [ 22, 0 ] = "v";
            mClave [ 22, 1 ] = "{";
            mClave [ 23, 0 ] = "w";
            mClave [ 23, 1 ] = "=";
            mClave [ 24, 0 ] = "x";
            mClave [ 24, 1 ] = "¥";
            mClave [ 25, 0 ] = "y";
            mClave [ 25, 1 ] = "¢";
            mClave [ 26, 0 ] = "z";
            mClave [ 26, 1 ] = "-";
            mClave [ 27, 0 ] = "_";
            mClave [ 27, 1 ] = "e";
            mClave [ 28, 0 ] = "0";
            mClave [ 28, 1 ] = "û";
            mClave [ 29, 0 ] = "1";
            mClave [ 29, 1 ] = "ù";
            mClave [ 30, 0 ] = "2";
            mClave [ 30, 1 ] = "ÿ";
            mClave [ 31, 0 ] = "3";
            mClave [ 31, 1 ] = "Ö";
            mClave [ 32, 0 ] = "4";
            mClave [ 32, 1 ] = "Ü";
            mClave [ 33, 0 ] = "5";
            mClave [ 33, 1 ] = "ø";
            mClave [ 34, 0 ] = "6";
            mClave [ 34, 1 ] = "£";
            mClave [ 35, 0 ] = "7";
            mClave [ 35, 1 ] = "|";
            mClave [ 36, 0 ] = "8";
            mClave [ 36, 1 ] = "x";
            mClave [ 37, 0 ] = "9";
            mClave [ 37, 1 ] = "ƒ";

            string ckey1="";
            string cClave="";

            // Si va a devolve un valor en simbolos
            if ( !tNormal )
                {
                for ( int T = 0; T <= cKey.Trim ( ).Length - 1; T++ )
                    {
                    ckey1 = cKey.Substring ( T, 1 );
                    for ( int h = 0; h < 38; h++ )
                        {
                        if ( ckey1.ToLower ( ) == mClave [ h, 0 ].ToLower ( ) )
                            {
                            cClave = cClave + mClave [ h, 1 ];
                            break;
                            }

                        }
                    }
                }
            // Aqui devuelve el valor que conoce el usuario

            else
                {
                for ( int T = 0; T <= cKey.Trim ( ).Length - 1; T++ )
                    {
                    ckey1 = cKey.Substring ( T, 1 );
                    for ( int h = 0; h < 38; h++ )
                        {
                        if ( ckey1.ToLower ( ) == mClave [ h, 1 ].ToLower ( ) )
                            {
                            cClave = cClave + mClave [ h, 0 ];
                            break;
                            }
                        }
                    }
                }
            return cClave;
            }

        
        }
    }