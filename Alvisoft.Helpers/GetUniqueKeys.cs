using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace Alvisoft.Helpers
{
    public class GetUniqueKeys
    {
        /// <summary>
        /// Type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="maxSize"></param>
        /// <returns></returns>

        public static string UniqueString(int type, int maxSize)
        {
            
            char[] charsAlfa = new char[25];
            char[] charsAlfaNum = new char[34];
            char[] charsNum = new char[11];
            string a = null;
            a = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            string bb = null;
            bb = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string cc = null;
            cc = "1234567890";
            StringBuilder results = default(StringBuilder);

            if (type == 1)
            {
                charsAlfaNum = a.ToCharArray();
                int size = maxSize;
                byte[] data = new byte[1];
                RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
                crypto.GetNonZeroBytes(data);
                size = maxSize;
                data = new byte[size];
                crypto.GetNonZeroBytes(data);
                StringBuilder result = new StringBuilder(size);
                foreach (byte b in data)
                {
                    results = result.Append(charsAlfaNum[b % (charsAlfaNum.Length - 1)]);
                }
            }
            else if (type == 2)
            {
                charsAlfa = bb.ToCharArray();
                int size = maxSize;
                byte[] data = new byte[1];
                RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
                crypto.GetNonZeroBytes(data);
                size = maxSize;
                data = new byte[size];
                crypto.GetNonZeroBytes(data);
                StringBuilder result = new StringBuilder(size);
                foreach (byte b in data)
                {
                    results = result.Append(charsAlfa[b % (charsAlfa.Length - 1)]);
                }
            }
            else if (type == 3)
            {
                charsNum = cc.ToCharArray();
                int size = maxSize;
                byte[] data = new byte[1];
                RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
                crypto.GetNonZeroBytes(data);
                size = maxSize;
                data = new byte[size];
                crypto.GetNonZeroBytes(data);
                StringBuilder result = new StringBuilder(size);
                foreach (byte b in data)
                {
                    results = result.Append(charsNum[b % (charsNum.Length - 1)]);
                }
            }

            return results.ToString();

        }
    }
}