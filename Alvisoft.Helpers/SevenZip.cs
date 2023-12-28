using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SevenZip;
//using Ionic.Zip;


namespace Alvisoft.Helpers
{
    public class SevenZip
    {


        public static void Compress(string SevenZipLibrary, string source, string output, string password)
        {

            try
            {
                string fileName = source;
                //string outputDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                //SevenZipBase.SetLibraryPath(SevenZipLibrary);
                SevenZipCompressor.SetLibraryPath(SevenZipLibrary);
                SevenZipCompressor compressor = new SevenZipCompressor();

                compressor.CompressionMethod = CompressionMethod.Deflate;
                compressor.CompressionLevel = CompressionLevel.High;
                compressor.ZipEncryptionMethod = ZipEncryptionMethod.Aes256;
                compressor.ArchiveFormat = OutArchiveFormat.SevenZip;
                compressor.CompressionMode = CompressionMode.Create;
                compressor.TempFolderPath = System.IO.Path.GetTempPath();
                compressor.EventSynchronization = EventSynchronizationStrategy.AlwaysAsynchronous;
                compressor.FastCompression = false;
                compressor.EncryptHeaders = true;
                compressor.ScanOnlyWritable = true;
                compressor.CompressFilesEncrypted(output, password, source);

            }
            catch (Exception)
            {

                throw;
            }


        }



        //public static string ZipCompress(string zipPath, string source, string output, string password)
        //{

        //    string ZIPfileToCreate = output + System.DateTime.Now.ToString("u").Replace(":","").Replace(" ","").Replace("-","") + ".zip";

        //    using (ZipFile zip = new ZipFile())
        //    {
        //        zip.Password = password;
        //        zip.Encryption = EncryptionAlgorithm.WinZipAes256;
        //        zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;
        //        System.DateTime Timestamp = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 0, 0);
        //        String[] filenames = { source };
        //        foreach (String f in filenames)
        //        {
        //            ZipEntry en = zip.AddFile(f, "");
        //            en.LastModified = Timestamp;
        //        }
        //        zip.Comment = "Este comprimido zip fue creado el " + System.DateTime.Now.ToString("G");
        //        zip.Save(zipPath + "\\" + ZIPfileToCreate);

        //        return zipPath + "\\" + ZIPfileToCreate;


        //    }


        //}


    }


}

           

      




        
   