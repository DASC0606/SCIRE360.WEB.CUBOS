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
using System.Security.Cryptography;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;
using System.Net;
using System.Globalization;




namespace Alvisoft.Helpers
{
    public  class StringHelper
    {



        /// <summary>
        /// method for removing all whitespace from a given string
        /// </summary>
        /// <param name="str">string to strip</param>
        /// <returns></returns>
        public static string RemoveAllWhitespace(string str)
        {
            try
            {
                Regex reg = new Regex(@"\s*");
                str = reg.Replace(str, "");
                return str;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        // Function to test for Positive Integers.
        public bool IsNaturalNumber(String strNumber)
        {
            Regex objNotNaturalPattern = new Regex("[^0-9]");
            Regex objNaturalPattern = new Regex("0*[1-9][0-9]*");
            return !objNotNaturalPattern.IsMatch(strNumber) && objNaturalPattern.IsMatch(strNumber);
        }
        // Function to test for Positive Integers with zero inclusive
        public bool IsWholeNumber(String strNumber)
        {
            Regex objNotWholePattern = new Regex("[^0-9]");
            return !objNotWholePattern.IsMatch(strNumber);
        }
        // Function to Test for Integers both Positive & Negative
        public static bool IsInteger(String strNumber)
        {
            Regex objNotIntPattern = new Regex("[^0-9-]");
            Regex objIntPattern = new Regex("^-[0-9]+$|^[0-9]+$");
            return !objNotIntPattern.IsMatch(strNumber) &&
            objIntPattern.IsMatch(strNumber);
        }
        // Function to Test for Positive Number both Integer & Real
        public static bool IsPositiveNumber(String strNumber)
        {
            Regex objNotPositivePattern = new Regex("[^0-9.]");
            Regex objPositivePattern = new Regex("^[.][0-9]+$|[0-9]*[.]*[0-9]+$");
            Regex objTwoDotPattern = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
            return !objNotPositivePattern.IsMatch(strNumber) &&
            objPositivePattern.IsMatch(strNumber) && !objTwoDotPattern.IsMatch(strNumber);
        }
        // Function to test whether the string is valid number or not
        public static bool IsNumber(String strNumber)
        {
            Regex objNotNumberPattern = new Regex("[^0-9.-]");
            Regex objTwoDotPattern = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
            Regex objTwoMinusPattern = new Regex("[0-9]*[-][0-9]*[-][0-9]*");
            String strValidRealPattern = "^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$";
            String strValidIntegerPattern = "^([-]|[0-9])[0-9]*$";
            Regex objNumberPattern = new Regex("(" + strValidRealPattern + ")|(" + strValidIntegerPattern + ")");
            return !objNotNumberPattern.IsMatch(strNumber) && !objTwoDotPattern.IsMatch(strNumber) && !objTwoMinusPattern.IsMatch(strNumber) && objNumberPattern.IsMatch(strNumber);
        }
        // Function To test for Alphabets.
        public static bool IsAlpha(String strToCheck)
        {
            Regex objAlphaPattern = new Regex("[^a-zA-Z]");
            return !objAlphaPattern.IsMatch(strToCheck);
        }
        // Function to Check for AlphaNumeric.
        public static bool IsAlphaNumeric(String strToCheck)
        {
            Regex objAlphaNumericPattern = new Regex("[^a-zA-Z0-9]");
            return !objAlphaNumericPattern.IsMatch(strToCheck);
        }

        static Regex ValidEmailRegex = CreateValidEmailRegex();

        private static Regex CreateValidEmailRegex()
        {
            string validEmailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

            return new Regex(validEmailPattern, RegexOptions.IgnoreCase);
        }


        public static bool EmailIsValid(string emailAddress)
        {
            bool isValid = ValidEmailRegex.IsMatch(emailAddress);

            return isValid;
        }


        public static double CalculateRating(int raters, double rating, double newRating)
        {
            double calcRating = ((rating * raters) + newRating) / (raters + 1);
            calcRating = Math.Floor((calcRating * 2) + .5) / 2;
            return calcRating;
        }

        public static string GetMD5Hash(string input)
        {
            if (String.IsNullOrWhiteSpace(input))
                return String.Empty;

            // step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            return sb.ToString();
        }

     
        public static string GetSHA256Hash(string input)
        {
            byte[] data = Encoding.UTF8.GetBytes(input);
            using (HashAlgorithm sha = new SHA256Managed())
            {
                byte[] encryptedBytes = sha.TransformFinalBlock(data, 0, data.Length);
                return Convert.ToBase64String(sha.Hash);
            }
        }

        /// <summary>
        /// method for removing all break line from a given string
        /// </summary>
        /// <param name="str">string to strip</param>
        /// <returns></returns>
        public static string ReplaceLineBreaks(string lines, string replacement)
        {
            string var = lines.Replace("\r\n", replacement)
                        .Replace("\r", replacement)
                        .Replace("\n", replacement)
                        .Replace("'", " ");
            return var;
        }

        public static string RemoveIllegalCharacters(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            text = text.Replace(":", string.Empty);
            text = text.Replace("/", string.Empty);
            text = text.Replace("?", string.Empty);
            text = text.Replace("#", string.Empty);
            text = text.Replace("[", string.Empty);
            text = text.Replace("]", string.Empty);
            text = text.Replace("@", string.Empty);
            text = text.Replace("*", string.Empty);
            text = text.Replace(".", string.Empty);
            text = text.Replace(",", string.Empty);
            text = text.Replace("\"", string.Empty);
            text = text.Replace("&", string.Empty);
            text = text.Replace("'", string.Empty);
            text = text.Replace(" ", "-");
            text = RemoveDiacritics(text);
            text = RemoveExtraHyphen(text);

            return HttpUtility.UrlEncode(text).Replace("%", string.Empty);
        }

        private static string RemoveExtraHyphen(string text)
        {
            if (text.Contains("--"))
            {
                text = text.Replace("--", "-");
                return RemoveExtraHyphen(text);
            }

            return text;
        }

        private static String RemoveDiacritics(string text)
        {
            String normalized = text.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < normalized.Length; i++)
            {
                Char c = normalized[i];
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            }

            return sb.ToString();
        }

        private static Regex _Regex = new Regex("<[^>]*>", RegexOptions.Compiled);
        /// <summary>
        /// Removes all HTML tags from a given string
        /// </summary>
        public static string StripHtml(string html)
        {
            if (string.IsNullOrEmpty(html))
                return string.Empty;

            return _Regex.Replace(html, string.Empty);
        }
       
            public  string StripPunctuation(string s)
            {
                var sb = new StringBuilder();
                foreach (char c in s)
                {
                    if (!char.IsPunctuation(c))
                        sb.Append(c);
                }
                return sb.ToString();
            }
    
        


        /// <summary>
        /// cleans string for disallowed non-alphanumeric characters
        /// </summary>
        /// <param name="strIn"></param>
        /// <returns></returns>
        public string CleanString(string strIn)
        {
            // Replace invalid characters with empty strings.
            return Regex.Replace(strIn, @"[^\w\.@-]", "");
        }

     


        public  string RemoveSpecialCharacters(string s)
{
   // return Regex.Replace(s, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled);
    return Regex.Replace(s, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled);
}
        
        
        public  string RemoveCharacters(string s, string removeChars)
        {
            int i = 0, j = 0;

            int lengthC = removeChars.Length;
            int lengthS = s.Length;
            int[] intCollection = new int[256];
            char[] s2 = new char[lengthS];

            for (i = 0; i < lengthC; i++)
            {
                intCollection[removeChars[i]] = 1;
            }

            i = j = 0;
            for (i = 0; i < lengthS; i++)
            {
                if (intCollection[s[i]] != 1)
                {
                    s2[j] = s[i];
                    j++;
                }
            }

            return new string(s2);

        }


        /// <summary>
        /// Amended date of birth cannot be greater than or equal to one month either side of original date of birth.
        /// </summary>
        /// <param name="dateOfBirth">Date of birth user could have amended.</param>
        /// <param name="originalDateOfBirth">Original date of birth to compare against.</param>
        /// <returns></returns>
        public static string ValidateDateOfBirth(string dateOfBirth, string originalDateOfBirth)
        {
            DateTime dob, originalDob;
            bool isValid = false;

            if (DateTime.TryParse(dateOfBirth, out dob) && DateTime.TryParse(originalDateOfBirth, out originalDob))
            {
                int diff = ((dob.Month - originalDob.Month) + 12 * (dob.Year - originalDob.Year));

                switch (diff)
                {
                    case 0:
                        // We're on the same month, so ok.
                        isValid = true;
                        break;
                    case -1:
                        // The month is the previous month, so check if the date makes it a calendar month out.
                        isValid = (dob.Day > originalDob.Day);
                        break;
                    case 1:
                        // The month is the next month, so check if the date makes it a calendar month out.
                        isValid = (dob.Day < originalDob.Day);
                        break;
                    default:
                        // Either zero or greater than 1 month difference, so not ok.
                        isValid = false;
                        break;
                }

                if (!isValid)
                    return "Date of Birth cannot be greater than one month either side of the date we hold.";
            }
            else
            {
                return "Date of Birth is invalid.";
            }

            return "OK"; 
        }

    }
}