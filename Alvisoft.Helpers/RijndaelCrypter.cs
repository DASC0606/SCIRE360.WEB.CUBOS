using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace Alvisoft.Helpers
{
    // <summary>
    // This class uses a symmetric key algorithm (Rijndael/AES) to encrypt and 
    // decrypt data. As long as encryption and decryption routines use the same 
    // parameters to generate the keys, the keys are guaranteed to be the same.
    // The class uses static functions with duplicate code to make it easier to 
    // demonstrate encryption and decryption logic. 
    // </summary>
    public class RijndaelCrypter
    {


        public static string CipherText(string plainText)
        {

                string passPhrase = "1ll&thy@LucR3th&a";        // can be any string
                string saltValue = "@lvI50ft";        // can be any string
                string hashAlgorithm = "SHA1";             // can be "MD5"
                int passwordIterations = 3;                  // can be any number
                string initVector = "!UnPr0gR355ivEs@"; // must be 16 bytes
                int keySize = 256;                // can be 192 or 128


                string cipherText = Encrypt(plainText, 
                                            passPhrase,
                                            saltValue,
                                            hashAlgorithm,
                                            passwordIterations,
                                            initVector,
                                            keySize);

            return cipherText;
        }


        public static string DeCipherText(string cipherText)
        {
            string passPhrase = "1ll&thy@LucR3th&a";        // can be any string
            string saltValue = "@lvI50ft";        // can be any string
            string hashAlgorithm = "SHA1";             // can be "MD5"
            int passwordIterations = 3;                  // can be any number
            string initVector = "!UnPr0gR355ivEs@"; // must be 16 bytes
            int keySize = 256;                // can be 192 or 128

            string plainText = Decrypt(cipherText,
                                       passPhrase,
                                       saltValue,
                                       hashAlgorithm,
                                       passwordIterations,
                                       initVector,
                                       keySize);

            return plainText;
        }



        public static string CipherText(string plainText, string pass)
        {

            string passPhrase = pass;        // can be any string
            string saltValue = "@lvI50ft";        // can be any string
            string hashAlgorithm = "SHA1";             // can be "MD5"
            int passwordIterations = 3;                  // can be any number
            string initVector = "!UnPr0gR355ivEs@"; // must be 16 bytes
            int keySize = 256;                // can be 192 or 128


            string cipherText = Encrypt(plainText,
                                        passPhrase,
                                        saltValue,
                                        hashAlgorithm,
                                        passwordIterations,
                                        initVector,
                                        keySize);

            return cipherText;
        }


        public static string DeCipherText(string cipherText, string pass)
        {
            string passPhrase = pass;        // can be any string
            string saltValue = "@lvI50ft";        // can be any string
            string hashAlgorithm = "SHA1";             // can be "MD5"
            int passwordIterations = 3;                  // can be any number
            string initVector = "!UnPr0gR355ivEs@"; // must be 16 bytes
            int keySize = 256;                // can be 192 or 128

            string plainText = Decrypt(cipherText,
                                       passPhrase,
                                       saltValue,
                                       hashAlgorithm,
                                       passwordIterations,
                                       initVector,
                                       keySize);

            return plainText;
        }

        ///// <summary>
        ///// Illustrates the use of RijndaelSimple class to encrypt and decrypt data.
        ///// </summary>
        //public class RijndaelSimpleTest
        //{
        //    /// <summary>
        //    /// The main entry point for the application.
        //    /// </summary>
        //    [STAThread]
        //    static void Main(string[] args)
        //    {
        //        string plainText = "Hello, World!";    // original plaintext

        //        string passPhrase = "Pas5pr@se";        // can be any string
        //        string saltValue = "s@1tValue";        // can be any string
        //        string hashAlgorithm = "SHA1";             // can be "MD5"
        //        int passwordIterations = 2;                  // can be any number
        //        string initVector = "@1B2c3D4e5F6g7H8"; // must be 16 bytes
        //        int keySize = 256;                // can be 192 or 128

        //        Console.WriteLine(String.Format("Plaintext : {0}", plainText));

        //        string cipherText = RijndaelSimple.Encrypt(plainText,
        //                                                    passPhrase,
        //                                                    saltValue,
        //                                                    hashAlgorithm,
        //                                                    passwordIterations,
        //                                                    initVector,
        //                                                    keySize);

        //        Console.WriteLine(String.Format("Encrypted : {0}", cipherText));

        //        plainText = RijndaelSimple.Decrypt(cipherText,
        //                                                    passPhrase,
        //                                                    saltValue,
        //                                                    hashAlgorithm,
        //                                                    passwordIterations,
        //                                                    initVector,
        //                                                    keySize);

        //        Console.WriteLine(String.Format("Decrypted : {0}", plainText));
        //    }
        //}
        

        // <summary>
        // Encrypts specified plaintext using Rijndael symmetric key algorithm
        // and returns a base64-encoded result.
        // </summary>
        // <param name="plainText">
        // Plaintext value to be encrypted.
        // </param>
        // <param name="passPhrase">
        // Passphrase from which a pseudo-random password will be derived. The 
        // derived password will be used to generate the encryption key. 
        // Passphrase can be any string. In this example we assume that this 
        // passphrase is an ASCII string.
        // </param>
        // <param name="saltValue">
        // Salt value used along with passphrase to generate password. Salt can 
        // be any string. In this example we assume that salt is an ASCII string.
        // </param>
        // <param name="hashAlgorithm">
        // Hash algorithm used to generate password. Allowed values are: "MD5" and
        // "SHA1". SHA1 hashes are a bit slower, but more secure than MD5 hashes.
        // </param>
        // <param name="passwordIterations">
        // Number of iterations used to generate password. One or two iterations
        // should be enough.
        // </param>
        // <param name="initVector">
        // Initialization vector (or IV). This value is required to encrypt the 
        // first block of plaintext data. For RijndaelManaged class IV must be 
        // exactly 16 ASCII characters long.
        // </param>
        // <param name="keySize">
        // Size of encryption key in bits. Allowed values are: 128, 192, and 256. 
        // Longer keys are more secure than shorter keys.
        // </param>
        // <returns>
        // Encrypted value formatted as a base64-encoded string.
        // </returns>
        private static string Encrypt(string plainText, string passPhrase, string saltValue, string hashAlgorithm, int passwordIterations, string initVector, int keySize)
        {

            byte[] initVectorBytes = null;
            byte[] saltValueBytes = null;
            byte[] plainTextBytes = null;
            PasswordDeriveBytes password = null;
            byte[] keyBytes = null;
            RijndaelManaged symmetricKey = null;
            ICryptoTransform encryptor = null;
            MemoryStream memoryStream = null;
            CryptoStream cryptoStream = null;
            byte[] cipherTextBytes = null;
            string cipherText = null;
            string EncryptMSG = null;



            try
            {

                // Convert strings into byte arrays.
                // Let us assume that strings only contain ASCII codes.
                // If strings include Unicode characters, use Unicode, UTF7, or UTF8 
                // encoding.

                initVectorBytes = Encoding.ASCII.GetBytes(initVector);


                saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

                // Convert our plaintext into a byte array.
                // Let us assume that plaintext contains UTF8-encoded characters.

                plainTextBytes = Encoding.UTF8.GetBytes(plainText);

                // First, we must create a password, from which the key will be derived.
                // This password will be generated from the specified passphrase and 
                // salt value. The password will be created using the specified hash 
                // algorithm. Password creation can be done in several iterations.

                password = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);

                // Use the password to generate pseudo-random bytes for the encryption
                // key. Specify the size of the key in bytes (instead of bits).

                keyBytes = password.GetBytes(keySize / 8);


                // Create uninitialized Rijndael encryption object.

                symmetricKey = new RijndaelManaged();

                // It is reasonable to set encryption mode to Cipher Block Chaining
                // (CBC). Use default options for other symmetric key parameters.
                symmetricKey.Mode = CipherMode.CBC;

                // Generate encryptor from the existing key bytes and initialization 
                // vector. Key size will be defined based on the number of the key 
                // bytes.

                encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);

                // Define memory stream which will be used to hold encrypted data.

                memoryStream = new MemoryStream();

                // Define cryptographic stream (always use Write mode for encryption).

                cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
                // Start encrypting.
                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);

                // Finish encrypting.
                cryptoStream.FlushFinalBlock();

                // Convert our encrypted data from a memory stream into a byte array.

                cipherTextBytes = memoryStream.ToArray();

                // Close both streams.
                memoryStream.Close();
                cryptoStream.Close();

                // Convert encrypted data into a base64-encoded string.

                cipherText = Convert.ToBase64String(cipherTextBytes);

                // Return encrypted string.
                EncryptMSG = cipherText;


            }
            catch (Exception ex)
            {
                EncryptMSG = "";


            }

            return EncryptMSG;

        }

        // <summary>
        // Decrypts specified ciphertext using Rijndael symmetric key algorithm.
        // </summary>
        // <param name="cipherText">
        // Base64-formatted ciphertext value.
        // </param>
        // <param name="passPhrase">
        // Passphrase from which a pseudo-random password will be derived. The 
        // derived password will be used to generate the encryption key. 
        // Passphrase can be any string. In this example we assume that this 
        // passphrase is an ASCII string.
        // </param>
        // <param name="saltValue">
        // Salt value used along with passphrase to generate password. Salt can 
        // be any string. In this example we assume that salt is an ASCII string.
        // </param>
        // <param name="hashAlgorithm">
        // Hash algorithm used to generate password. Allowed values are: "MD5" and
        // "SHA1". SHA1 hashes are a bit slower, but more secure than MD5 hashes.
        // </param>
        // <param name="passwordIterations">
        // Number of iterations used to generate password. One or two iterations
        // should be enough.
        // </param>
        // <param name="initVector">
        // Initialization vector (or IV). This value is required to encrypt the 
        // first block of plaintext data. For RijndaelManaged class IV must be 
        // exactly 16 ASCII characters long.
        // </param>
        // <param name="keySize">
        // Size of encryption key in bits. Allowed values are: 128, 192, and 256. 
        // Longer keys are more secure than shorter keys.
        // </param>
        // <returns>
        // Decrypted string value.
        // </returns>
        // <remarks>
        // Most of the logic in this function is similar to the Encrypt 
        // logic. In order for decryption to work, all parameters of this function
        // - except cipherText value - must match the corresponding parameters of 
        // the Encrypt function which was called to generate the 
        // ciphertext.
        // </remarks>
        private static string Decrypt(string cipherText, string passPhrase, string saltValue, string hashAlgorithm, int passwordIterations, string initVector, int keySize)
        {

            // Convert strings defining encryption key characteristics into byte
            // arrays. Let us assume that strings only contain ASCII codes.
            // If strings include Unicode characters, use Unicode, UTF7, or UTF8

            // encoding.
            byte[] initVectorBytes = null;
            byte[] saltValueBytes = null;
            byte[] cipherTextBytes = null;
            PasswordDeriveBytes password = null;
            byte[] keyBytes = null;
            RijndaelManaged symmetricKey = null;
            ICryptoTransform decryptor = null;
            MemoryStream memoryStream = null;
            CryptoStream cryptoStream = null;
            byte[] plainTextBytes = null;
            int decryptedByteCount = 0;
            string plainText = null;
            string DecryptMSG = null;



            try
            {
                initVectorBytes = Encoding.ASCII.GetBytes(initVector);


                saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

                // Convert our ciphertext into a byte array.

                cipherTextBytes = Convert.FromBase64String(cipherText);



                // First, we must create a password, from which the key will be 
                // derived. This password will be generated from the specified 
                // passphrase and salt value. The password will be created using
                // the specified hash algorithm. Password creation can be done in
                // several iterations.

                password = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);

                // Use the password to generate pseudo-random bytes for the encryption
                // key. Specify the size of the key in bytes (instead of bits).

                keyBytes = password.GetBytes(keySize / 8);

                // Create uninitialized Rijndael encryption object.

                symmetricKey = new RijndaelManaged();

                // It is reasonable to set encryption mode to Cipher Block Chaining
                // (CBC). Use default options for other symmetric key parameters.
                symmetricKey.Mode = CipherMode.CBC;

                // Generate decryptor from the existing key bytes and initialization 
                // vector. Key size will be defined based on the number of the key 
                // bytes.

                decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);

                // Define memory stream which will be used to hold encrypted data.

                memoryStream = new MemoryStream(cipherTextBytes);

                // Define memory stream which will be used to hold encrypted data.

                cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);

                // Since at this point we don't know what the size of decrypted data
                // will be, allocate the buffer long enough to hold ciphertext;
                // plaintext is never longer than ciphertext.



                plainTextBytes = new byte[cipherTextBytes.Length + 1];

                // Start decrypting.

                decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);

                // Close both streams.
                memoryStream.Close();
                cryptoStream.Close();

                // Convert decrypted data into a string. 
                // Let us assume that the original plaintext string was UTF8-encoded.

                plainText = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);

                // Return decrypted string.
                DecryptMSG = plainText;



            }
            catch (Exception ex)
            {
                DecryptMSG = "";
            }

            return DecryptMSG;


        }

        public static string GetUniqueKey(int type, int maxSize)
        {
            // Dim maxSize As Integer = 8
            // Dim minSize As Integer = 5
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