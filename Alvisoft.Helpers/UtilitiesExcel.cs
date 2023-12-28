using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Xml;
using System.Web;
using Excel;
using System.Globalization;
using System.Security;
using OfficeOpenXml;
using OfficeOpenXml.Style;


namespace Alvisoft.Helpers
{

    
    public class UtilitiesExcel
    {

        public static DataSet ImportExcelXLS(HttpPostedFile file, bool hasHeaders)
        {
            string fileName = Path.GetTempFileName();
            file.SaveAs(fileName);

            return ImportExcelXLS(fileName, hasHeaders);
        }

        public static DataSet ImportExcelUpload(HttpPostedFile file)
        {
            IExcelDataReader iExcelDataReader = null;

            if (null != file)
            {
                string fileExtension = Path.GetExtension(file.FileName);

                switch (fileExtension)
                {
                    case ".xls":
                        iExcelDataReader = ExcelReaderFactory.CreateBinaryReader(file.InputStream);
                        break;
                    case ".xlsx":
                        iExcelDataReader = ExcelReaderFactory.CreateOpenXmlReader(file.InputStream);
                        break;
                    default:
                        iExcelDataReader = null;
                        break;
                }
            }

            iExcelDataReader.IsFirstRowAsColumnNames = true;
            DataSet dsUnUpdated = new DataSet();
            dsUnUpdated = iExcelDataReader.AsDataSet();
            iExcelDataReader.Close();


            return dsUnUpdated;
        }

            public static byte[] GetByteFromPostedFile(System.Web.HttpPostedFile FileToUpload) 
            {
                if (FileToUpload != null)
                {
              
                    try
                    {
                        
                        HttpPostedFile MyFile;
                        int FileLen;
                        System.IO.Stream MyStream;
                       
                        MyFile = FileToUpload;

                        FileLen = MyFile.ContentLength;
                 
                        // Initialize the stream.
                        MyStream = MyFile.InputStream;

                        byte[] fileData = new byte[FileToUpload.InputStream.Length];
                        FileToUpload.InputStream.Seek(0, SeekOrigin.Begin);
                        FileToUpload.InputStream.Read(fileData, 0, fileData.Length);
     
                        return fileData;
                    }
                    catch
                    {
                        return null;
                    }
                }
                else
                {
                    return null;


                }

        } 

        //public static DataSet ImportExcelUpload(HttpPostedFile file)
        //{
        //    IExcelDataReader iExcelDataReader = null;

        //    if (null != file)
        //    {
        //        string fileExtension = Path.GetExtension(file.FileName);

        //        switch (fileExtension)
        //        {
        //            case ".xls":
        //                iExcelDataReader = ExcelReaderFactory.CreateBinaryReader(file.InputStream);
        //                break;
        //            case ".xlsx":
        //                iExcelDataReader = ExcelReaderFactory.CreateOpenXmlReader(file.InputStream);
        //                break;
        //            default:
        //                iExcelDataReader = null;
        //                break;
        //        }
        //    }

        //    iExcelDataReader.IsFirstRowAsColumnNames = true;
        //    DataSet dsUnUpdated = new DataSet();
        //    dsUnUpdated = iExcelDataReader.AsDataSet();
        //    iExcelDataReader.Close();


        //    return dsUnUpdated;
        //}


        public static DataSet ImportExcelFromStream(Stream st, string ext)
        {
            IExcelDataReader iExcelDataReader = null;

            if (null != st)
            {
                string fileExtension = ext;

                switch (fileExtension)
                {
                    case ".xls":
                        iExcelDataReader = ExcelReaderFactory.CreateBinaryReader(st);
                        break;
                    case ".xlsx":
                        iExcelDataReader = ExcelReaderFactory.CreateOpenXmlReader(st);
                        break;
                    default:
                        iExcelDataReader = null;
                        break;
                }
            }

            iExcelDataReader.IsFirstRowAsColumnNames = true;
            DataSet dsUnUpdated = new DataSet();
            dsUnUpdated = iExcelDataReader.AsDataSet();
            iExcelDataReader.Close();
            
            return dsUnUpdated;
        }


        public static DataSet ImportExcel(string filePath, string file)
        {
            IExcelDataReader iExcelDataReader = null;

            //FileInfo fileInfo = new FileInfo(fileUp.FileName);
            //string file = fileInfo.Name;
            FileStream oStream = File.Open(filePath, FileMode.Open, FileAccess.Read);

            if (file.Split('.')[1].Equals("xls"))
            {
                iExcelDataReader = ExcelReaderFactory.CreateBinaryReader(oStream);
            }
            else if (file.Split('.')[1].Equals("xlsx"))
            {
                iExcelDataReader = ExcelReaderFactory.CreateOpenXmlReader(oStream);
            }

            iExcelDataReader.IsFirstRowAsColumnNames = true;
            DataSet dsUnUpdated = new DataSet();
            dsUnUpdated = iExcelDataReader.AsDataSet();
            iExcelDataReader.Close();


            return dsUnUpdated;
        }

        public static DataSet ImportFileExcelXLS(string fileName, bool hasHeaders)
        {
            //string fileName = Path.GetTempFileName();
            //file.SaveAs(fileName);

            return ImportExcelXLS(fileName, hasHeaders);
        }


        private static DataSet ImportExcelXLS(string FileName, bool hasHeaders)
        {
            string HDR = hasHeaders ? "Yes" : "No";
            string strConn;

            if (FileName.Substring(FileName.LastIndexOf('.')).ToLower() == ".xlsx")
                strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileName + ";Extended Properties=\"Excel 12.0;HDR=" + HDR + ";IMEX=0\"";
            else
                strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FileName + ";Extended Properties=\"Excel 8.0;HDR=" + HDR + ";IMEX=0\"";



            DataSet output = new DataSet();

            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                DataTable dt = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

                foreach (DataRow row in dt.Rows)
                {
                    string sheet = row["TABLE_NAME"].ToString();

                    OleDbCommand cmd = new OleDbCommand("SELECT * FROM [" + sheet + "]", conn);
                    cmd.CommandType = CommandType.Text;

                    DataTable outputTable = new DataTable(sheet);
                    output.Tables.Add(outputTable);
                    new OleDbDataAdapter(cmd).Fill(outputTable);
                }
            }
            return output;
        }

        struct ColumnType
        {
            public Type type;
            private string name;
            public ColumnType(Type type) { this.type = type; this.name = type.ToString().ToLower(); }
            public object ParseString(string input)
            {
                if (String.IsNullOrEmpty(input))
                    return DBNull.Value;
                switch (type.ToString())
                {
                    case "system.datetime":
                        return DateTime.Parse(input);
                    case "system.decimal":
                        return decimal.Parse(input);
                    case "system.boolean":
                        return bool.Parse(input);
                    default:
                        return input;
                }
            }
        }
        
        public static DataSet ImportExcelXML(HttpPostedFile file, bool hasHeaders, bool autoDetectColumnType)
        {
            return ImportExcelXML(file.InputStream, hasHeaders, autoDetectColumnType);
        }
        
        public static DataSet ImportExcelXML(Stream inputFileStream, bool hasHeaders, bool autoDetectColumnType)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(new XmlTextReader(inputFileStream));
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);

            nsmgr.AddNamespace("o", "urn:schemas-microsoft-com:office:office");
            nsmgr.AddNamespace("x", "urn:schemas-microsoft-com:office:excel");
            nsmgr.AddNamespace("ss", "urn:schemas-microsoft-com:office:spreadsheet");

            DataSet ds = new DataSet();

            foreach (XmlNode node in doc.DocumentElement.SelectNodes("//ss:Worksheet", nsmgr))
            {
                DataTable dt = new DataTable(node.Attributes["ss:Name"].Value);
                ds.Tables.Add(dt);
                XmlNodeList rows = node.SelectNodes("ss:Table/ss:Row", nsmgr);
                if (rows.Count > 0)
                {
                    List<ColumnType> columns = new List<ColumnType>();
                    int startIndex = 0;
                    if (hasHeaders)
                    {
                        foreach (XmlNode data in rows[0].SelectNodes("ss:Cell/ss:Data", nsmgr))
                        {
                            columns.Add(new ColumnType(typeof(string)));//default to text
                            dt.Columns.Add(data.InnerText, typeof(string));
                        }
                        startIndex++;
                    }
                    if (autoDetectColumnType && rows.Count > 0)
                    {
                        XmlNodeList cells = rows[startIndex].SelectNodes("ss:Cell", nsmgr);
                        int actualCellIndex = 0;
                        for (int cellIndex = 0; cellIndex < cells.Count; cellIndex++)
                        {
                            XmlNode cell = cells[cellIndex];
                            if (cell.Attributes["ss:Index"] != null)
                                actualCellIndex = int.Parse(cell.Attributes["ss:Index"].Value) - 1;

                            ColumnType autoDetectType = getType(cell.SelectSingleNode("ss:Data", nsmgr));

                            if (actualCellIndex >= dt.Columns.Count)
                            {
                                dt.Columns.Add("Column" + actualCellIndex.ToString(), autoDetectType.type);
                                columns.Add(autoDetectType);
                            }
                            else
                            {
                                dt.Columns[actualCellIndex].DataType = autoDetectType.type;
                                columns[actualCellIndex] = autoDetectType;
                            }

                            actualCellIndex++;
                        }
                    }
                    for (int i = startIndex; i < rows.Count; i++)
                    {
                        DataRow row = dt.NewRow();
                        XmlNodeList cells = rows[i].SelectNodes("ss:Cell", nsmgr);
                        int actualCellIndex = 0;
                        for (int cellIndex = 0; cellIndex < cells.Count; cellIndex++)
                        {
                            XmlNode cell = cells[cellIndex];
                            if (cell.Attributes["ss:Index"] != null)
                                actualCellIndex = int.Parse(cell.Attributes["ss:Index"].Value) - 1;

                            XmlNode data = cell.SelectSingleNode("ss:Data", nsmgr);

                            if (actualCellIndex >= dt.Columns.Count)
                            {
                                for (int j = dt.Columns.Count; j < actualCellIndex; j++)
                                {
                                    dt.Columns.Add("Column" + actualCellIndex.ToString(), typeof(string));
                                    columns.Add(getDefaultType());
                                }
                                ColumnType autoDetectType = getType(cell.SelectSingleNode("ss:Data", nsmgr));
                                dt.Columns.Add("Column" + actualCellIndex.ToString(), typeof(string));
                                columns.Add(autoDetectType);
                            }
                            if (data != null)
                                row[actualCellIndex] = data.InnerText;

                            actualCellIndex++;
                        }

                        dt.Rows.Add(row);
                    }
                }
            }
            return ds;

            //<?xml version="1.0"?>
            //<?mso-application progid="Excel.Sheet"?>
            //<Workbook>
            // <Worksheet ss:Name="Sheet1">
            //  <Table>
            //   <Row>
            //    <Cell><Data ss:Type="String">Item Number</Data></Cell>
            //    <Cell><Data ss:Type="String">Description</Data></Cell>
            //    <Cell ss:StyleID="s21"><Data ss:Type="String">Item Barcode</Data></Cell>
            //   </Row>
            // </Worksheet>
            //</Workbook>
        }

        private static ColumnType getDefaultType()
        {
            return new ColumnType(typeof(String));
        }

        private static ColumnType getType(XmlNode data)
        {
            string type = null;
            if (data.Attributes["ss:Type"] == null || data.Attributes["ss:Type"].Value == null)
                type = "";
            else
                type = data.Attributes["ss:Type"].Value;

            switch (type)
            {
                case "DateTime":
                    return new ColumnType(typeof(DateTime));
                case "Boolean":
                    return new ColumnType(typeof(Boolean));
                case "Number":
                    return new ColumnType(typeof(Decimal));
                case "":
                    decimal test2;
                    if (data == null || String.IsNullOrEmpty(data.InnerText) || decimal.TryParse(data.InnerText, out test2))
                    {
                        return new ColumnType(typeof(Decimal));
                    }
                    else
                    {
                        return new ColumnType(typeof(String));
                    }
                default://"String"
                    return new ColumnType(typeof(String));
            }
        }


        #region Generating an Excel File

        /// <summary>
        /// Converts the DataTable into the excel format and stores at the target location (also takes care of the date values)
        /// </summary>
        /// <param name="sourceTable">Source DataTable</param>
        /// <param name="targetFilePath">Target File Path</param>
        /// <param name="dateColumnsInDataTable">List of Columns with Date datatype in the DataTable</param>
    
        //public static void ConvertDataTableToExcel(DataTable sourceTable, string targetFilePath, List<string> dateColumnsInDataTable)
        //{
        //    if (File.Exists(targetFilePath))
        //        File.Delete(targetFilePath);

        //    SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(targetFilePath, SpreadsheetDocumentType.Workbook);

        //    spreadsheetDocument.AddWorkbookPart();
        //    spreadsheetDocument.Close();

        //    InsertWorksheet(targetFilePath);

        //    using (SpreadsheetDocument myWorkbook = SpreadsheetDocument.Open(targetFilePath, true))
        //    {
        //        var wrkSheet = myWorkbook.WorkbookPart.WorksheetParts.First().Worksheet;
        //        var sheetData = wrkSheet.GetFirstChild<SheetData>();

        //        var colValueList = new List<KeyValuePair<string, string>>();

        //        //Add Rows to the Sheet                                
        //        for (int j = 0; j < sourceTable.Columns.Count; j++)
        //        {
        //            colValueList.Add(new KeyValuePair<string, string>(FindDestinationColLetter("A", j), sourceTable.Columns[j].ColumnName));
        //        }

        //        var columnRow = CreateContentRow(1, colValueList);
        //        sheetData.AppendChild(columnRow);

        //        for (int i = 0; i < sourceTable.Rows.Count; i++)
        //        {
        //            var rowValList = new List<KeyValuePair<string, string>>();

        //            for (int j = 0; j < sourceTable.Columns.Count; j++)
        //            {
        //                string rowVal;
        //                if (dateColumnsInDataTable.Contains(sourceTable.Columns[j].ColumnName))
        //                {
        //                    //Checks if the date is in date format or Julian format (that excel returns) and stores it accordingly
        //                    var dateVal = sourceTable.Rows[i][j].ToString();
        //                    if (IsDateFormat(dateVal))
        //                    {
        //                        rowVal = dateVal;
        //                    }
        //                    else
        //                    {
        //                        int no;
        //                        rowVal = int.TryParse(dateVal, out no)
        //                                          ? ExcelSerialDateToDMY(no)
        //                                          : dateVal;
        //                    }
        //                }
        //                else
        //                    rowVal = sourceTable.Rows[i][j].ToString();

        //                rowValList.Add(new KeyValuePair<string, string>(FindDestinationColLetter("A", j), rowVal));
        //            }

        //            var row = CreateContentRow(i + 2, rowValList);
        //            sheetData.AppendChild(row);
        //        }
        //        wrkSheet.Save();
        //    }
        //}



        /// <summary>
        /// Checks if the give string is a date
        /// </summary>
        /// <param name="dateStr">String</param>
        /// <returns></returns>
        public static bool IsDateFormat(string dateStr)
        {
            //Remove the trailing timestamp
            dateStr = dateStr.Split(' ')[0];

            DateTime date;
            if (DateTime.TryParse(dateStr, out date))
                return true;

            if (DateTime.TryParse(dateStr, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out date))
            {
                return true;
            }

            if (DateTime.TryParse(dateStr, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AllowWhiteSpaces, out date))
            {
                return true;
            }

            var dateArr = dateStr.Split('-');
            if (dateArr.Length == 3)
            {
                int no;
                return int.TryParse(dateArr[0], out no) && int.TryParse(dateArr[1], out no) && int.TryParse(dateArr[2], out no);
            }

            return false;
        }

        /// <summary>
        /// Converts the given Excel Julian date to the Date-Month-Year format
        /// </summary>
        /// <param name="nSerialDate">Date in the Excel Julian Date Format</param>
        /// <returns></returns>
        public static string ExcelSerialDateToDMY(int nSerialDate)
        {
            // Excel/Lotus 123 have a problem with 29-02-1900. 1900 is not a leap year, but Excel/Lotus 123 think it is...

            int nDay;
            int nMonth;
            int nYear;

            if (nSerialDate == 60)
            {
                nDay = 29;
                nMonth = 2;
                nYear = 1900;

                return nDay + "-" + nMonth + "-" + nYear;
            }

            if (nSerialDate < 60)
            {
                // Because of the 29-02-1900 problem, any serial date under 60 is one off... Compensate.
                nSerialDate++;
            }

            // Modified Julian to DMY calculation with an addition of 2415019
            var l = nSerialDate + 68569 + 2415019;
            var n = (4 * l) / 146097;
            l = l - (146097 * n + 3) / 4;
            var i = (4000 * (l + 1)) / 1461001;
            l = l - (1461 * i) / 4 + 31;
            var j = (80 * l) / 2447;
            nDay = l - (2447 * j) / 80;
            l = j / 11;
            nMonth = j + 2 - (12 * l);
            nYear = 100 * (n - 49) + i + l;

            return nDay + "-" + nMonth + "-" + nYear;
        }

        /// <summary>
        /// Inserts a worksheet inside the given excel path
        /// </summary>
        /// <param name="excelFilePath"></param>
    
        //private static void InsertWorksheet(string excelFilePath)
        //{
        //    // Open the document for editing.
        //    using (SpreadsheetDocument spreadSheet = SpreadsheetDocument.Open(excelFilePath, true))
        //    {
        //        // Add a blank WorksheetPart.
        //        spreadSheet.WorkbookPart.Workbook = new Workbook();
        //        var newWorksheetPart = spreadSheet.WorkbookPart.AddNewPart<WorksheetPart>();
        //        newWorksheetPart.Worksheet = new Worksheet(new SheetData());
        //        newWorksheetPart.Worksheet.Save();

        //        spreadSheet.WorkbookPart.Workbook.AppendChild(new Sheets());
        //        var sheets = spreadSheet.WorkbookPart.Workbook.GetFirstChild<Sheets>();
        //        string relationshipId = spreadSheet.WorkbookPart.GetIdOfPart(newWorksheetPart);

        //        // Get a unique ID for the new worksheet.
        //        uint sheetId = 1;
        //        if (sheets.Elements<Sheet>().Count() > 0)
        //        {
        //            sheetId = sheets.Elements<Sheet>().Select(s => s.SheetId.Value).Max() + 1;
        //        }

        //        // Give the new worksheet a name.
        //        string sheetName = "Sheet" + sheetId;

        //        // Append the new worksheet and associate it with the workbook.
        //        var sheet = new Sheet { Id = relationshipId, SheetId = sheetId, Name = sheetName };
        //        sheets.Append(sheet);
        //        spreadSheet.WorkbookPart.Workbook.Save();
        //    }
        //}

        /// <summary>
        /// Inserts the give row data in the excel file
        /// </summary>
        /// <param name="rowIndex">The index at which the row has to be inserted</param>
        /// <param name="valueList">The Row content</param>
        /// <returns></returns>
      
        //private static Row CreateContentRow(int rowIndex, IEnumerable<KeyValuePair<string, string>> valueList)
        //{
        //    //Create new row
        //    var row = new Row { RowIndex = (UInt32)rowIndex };

        //    foreach (var keyValuePair in valueList)
        //    {
        //        row.AppendChild(CreateTextCell(keyValuePair.Key, keyValuePair.Value, rowIndex));
        //    }

        //    return row;

        //}

        /// <summary>
        /// Create a text cell that can be inserted in the excel row
        /// </summary>
        /// <param name="headerCellName">Header cell name</param>
        /// <param name="cellText">Cell Text</param>
        /// <param name="cellRowIndex">Row Index</param>
        /// <returns></returns>
       
        //private static Cell CreateTextCell(string headerCellName, string cellText, int cellRowIndex)
        //{
        //    int intText;
        //    var cell = new Cell { CellReference = headerCellName + cellRowIndex };
        //    if (int.TryParse(cellText, out intText))
        //    {
        //        var cellValue = new CellValue { Text = cellText };
        //        cell.AppendChild(cellValue);
        //        return cell;
        //    }

        //    //Create new inline string cell
        //    cell.DataType = CellValues.InlineString;

        //    //Add text to text cell
        //    var inlineString = new InlineString();
        //    if (string.IsNullOrWhiteSpace(cellText))
        //    {
        //        cellText = string.Empty;
        //    }

        //    var t = new Text { Text = SecurityElement.Escape(cellText) };
        //    inlineString.AppendChild(t);

        //    cell.AppendChild(inlineString);

        //    return cell;
        //}

        /// <summary>
        /// Finds the Column to be worked on, based on the first column letter and target Column Index
        /// </summary>
        /// <param name="startingColumnName">Header name</param>
        /// <param name="targetColumnIndex">Index of the target column starting from the header</param>
        /// <returns></returns>
      
        private static string FindDestinationColLetter(string startingColumnName, int targetColumnIndex)
        {
            var columnLetters = startingColumnName.ToCharArray();

            for (var i = 1; i <= targetColumnIndex; i++)
            {
                if (columnLetters[columnLetters.Length - 1] == 'Z')
                    columnLetters = ChangeColumnLetter(columnLetters, columnLetters.Length - 1);
                else
                    columnLetters[columnLetters.Length - 1]++;
            }
            return new string(columnLetters);
        }

        private static char[] ChangeColumnLetter(char[] columnLetters, int columnIndex)
        {
            if (columnIndex == -1)
            {
                var newCharArray = new List<char> { 'A' };
                newCharArray.AddRange(columnLetters);
                columnLetters = newCharArray.ToArray();
            }
            else if (columnLetters[columnIndex] == 'Z')
            {
                columnLetters[columnIndex] = 'A';
                columnLetters = ChangeColumnLetter(columnLetters, --columnIndex);
            }
            else
            {
                columnLetters[columnIndex]++;
            }

            return columnIndex != -2 ? columnLetters : null;
        }

        #endregion




    }


    ///// <summary>
    ///// Export to File Excel
    ///// </summary>
    //public class Excel2007
    //{
    //    public Excel2007()
    //    {
    //        //
    //        // TODO: Add constructor logic here
    //        //
    //    }

    //    public byte[] Export(DataSet DsData)
    //    {
    //        int i = 1;
    //        ExcelPackage xlApp = new ExcelPackage();

    //        foreach (DataTable DtData in DsData.Tables)
    //        {
    //            AddSheet(xlApp, DtData, i);
    //            i++;
    //        }

    //        return xlApp.GetAsByteArray();
    //    }

    //    public byte[] Export(DataTable DtData)
    //    {
    //        ExcelPackage xlApp = new ExcelPackage();

    //        AddSheet(xlApp, DtData);

    //        return xlApp.GetAsByteArray();
    //    }

    //    public byte[] Export(DataSet DsData, string TableName)
    //    {
    //        return Export(DsData.Tables[TableName]);
    //    }

    //    protected void AddSheet(ExcelPackage xlApp, DataTable DtData)
    //    {
    //        AddSheet(xlApp, DtData, 1);
    //    }

    //    protected void AddSheet(ExcelPackage xlApp, DataTable DtData, int Index)
    //    {
    //        ExcelWorksheet xlSheet = xlApp.Workbook.Worksheets.Add((DtData.TableName != string.Empty) ? DtData.TableName : string.Format("Sheet{0}", Index.ToString()));
    //        xlSheet.Cells["A1"].LoadFromDataTable(DtData, true);

    //        int rowCount = DtData.Rows.Count;
    //        IEnumerable<int> dateColumns = (from DataColumn d in DtData.Columns
    //                                       where d.DataType == typeof(DateTime) || d.ColumnName.Contains("Date")
    //                                       select d.Ordinal + 1);

    //        foreach (int dc in dateColumns)
    //        {
    //            xlSheet.Cells[2, dc, rowCount + 1, dc].Style.Numberformat.Format = "dd/MM/yyyy";
    //        }

    //        (from DataColumn d in DtData.Columns select d.Ordinal + 1).ToList().ForEach(dc =>
    //        {
    //            //background color
    //            xlSheet.Cells[1, 1, 1, dc].Style.Fill.PatternType = ExcelFillStyle.Solid;
    //            xlSheet.Cells[1, 1, 1, dc].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightYellow);

    //            //border
    //            xlSheet.Cells[1, dc, rowCount + 1, dc].Style.Border.Top.Style = ExcelBorderStyle.Thin;
    //            xlSheet.Cells[1, dc, rowCount + 1, dc].Style.Border.Right.Style = ExcelBorderStyle.Thin;
    //            xlSheet.Cells[1, dc, rowCount + 1, dc].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
    //            xlSheet.Cells[1, dc, rowCount + 1, dc].Style.Border.Left.Style = ExcelBorderStyle.Thin;
    //            xlSheet.Cells[1, dc, rowCount + 1, dc].Style.Border.Top.Color.SetColor(System.Drawing.Color.LightGray);
    //            xlSheet.Cells[1, dc, rowCount + 1, dc].Style.Border.Right.Color.SetColor(System.Drawing.Color.LightGray);
    //            xlSheet.Cells[1, dc, rowCount + 1, dc].Style.Border.Bottom.Color.SetColor(System.Drawing.Color.LightGray);
    //            xlSheet.Cells[1, dc, rowCount + 1, dc].Style.Border.Left.Color.SetColor(System.Drawing.Color.LightGray);
    //        });
    //    }
    //}


}