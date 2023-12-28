using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Text;

namespace Alvisoft.Helpers
    {
 /// <summary>
/// Summary description for ctrlBreadCrumbs.
/// </summary>
public class ctrlBreadCrumbs : System.Web.UI.WebControls.WebControl
{
/// <summary>
/// The 3 variables below, Separator, RootName, directoryNameSpacer can be changed to meet your needs
/// PageTitle is pulled from the CodeBehind of the page
/// </summary>
public string Separator = "> ";
public string RootName = "Home Page";
public char directoryNameSpacer = '_';
private string _PageTitle;
// Above are our variables that we can set:
// Separator is the ‘>’ symbol
// RootName is the homepage anchor text
// directoryNameSpacer is the naming scheme for my directories, for example: search_engine_optimization or website_design (notice the URL in the address bar above)
 
public string PageTitle
{
 get
 {
   return _PageTitle;
 }
 set
 {
  _PageTitle = value;
 }
}
/// <summary>
/// Render this control to the output parameter specified.
/// </summary>
/// <param name="output"> The HTML writer to write out to </param>
protected override void Render(HtmlTextWriter output)
{
  StringBuilder sbResult = new StringBuilder();
  // sbResult StringBuilder will hold the breadcrumb navigation when done
 
  // get the url root, like www.domain.com
  string strDomain = Page.Request.ServerVariables["HTTP_HOST"].ToString();
  strDomain.Trim(); // Trim removes leading and trailing whitespace
  sbResult.Append ( "<a href='http://" + strDomain + "'>" + RootName + "</a>" + Separator );
 
  // gets dir(s), like subdirectory/subsubdirectory/file.aspx
  string scriptName = Page.Request.ServerVariables["SCRIPT_NAME"].ToString();
  // find the last '/' and Remove the text after it as it's the file name
  int lastSlash = scriptName.LastIndexOf('/'); // returns the # of chars. from right to /
  string pathOnly = scriptName.Remove(lastSlash, (scriptName.Length - lastSlash));
 
  // create breadcrumb HTML for the directory name(s)
  // We Remove the first "/" otherwise when you split the string the first item in array is empty
 int nNumDirs=0;
    string[] strDirs=null;


 //******************************************************************************

    if ( pathOnly.Trim ( ) != "" )
      {
      pathOnly = pathOnly.Substring ( 1 );
      strDirs = pathOnly.Split ( '/' );
      nNumDirs = strDirs.Length;
      }
  else
      {

       nNumDirs=0;
        strDirs =null;
      }
 //*******************************************************************
    
    
    // URLs for breadcrumbs
  string strURL = "";
  for (int i=0; i<nNumDirs; i++)
  {
   strURL += "/"+strDirs[i];
 
   // convert underscores to spaces
   strDirs[i] = strDirs[i].Replace(directoryNameSpacer,' ');
 
   int counter = i+1;
   if (counter != nNumDirs)
   {
    sbResult.Append ( "<a href='http://" + strDomain + strURL + "'>" + strDirs[i] + "</a>" + Separator );
   }
   else
   {
    // This is the last directory so don't tack on Separator
    sbResult.Append ( "<a href='http://" + strDomain + strURL + "'>" + strDirs[i] + "</a>" );    }
  }
  // write the PageTitle, pulled from the CodeBehind!
  sbResult.Append ( " : " + this.PageTitle );
 
  output.Write ( sbResult.ToString() );
 
  }

  }


    }

