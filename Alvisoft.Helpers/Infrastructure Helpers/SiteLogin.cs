/****************** Copyright Notice ******************
 
 * Falta complementar con el propio sistema de perfiles, roles y accesos de usuario

   
*******************************************************/
using System.Web.Security;
using System.Web.Routing;




namespace Alvisoft.Helpers
{
    
    /// <summary>
    /// Design and Architecture: Anatoly A. Pedemonte Ku 
    /// </summary>
    public sealed class SiteLogin
    {
        public static void PerformAuthentication(string userName, bool remember)
        {
            FormsAuthentication.RedirectFromLoginPage(userName, remember);

            if ( System.Web.HttpContext.Current.Request.QueryString [ "ReturnUrl" ] == null )
                  RedirectToDefaultPage();
              //  RedirectToDefaultPageUser ( defaultPageAccess );

            else
                System.Web.HttpContext.Current.Response.Redirect ( System.Web.HttpContext.Current.Request.QueryString [ "ReturnUrl" ].ToString ( ) );

        }

        public static void PerformAdminAuthentication(string userName, bool remember)
        {
            FormsAuthentication.RedirectFromLoginPage(userName, remember);

            if (System.Web.HttpContext.Current.Request.QueryString["ReturnUrl"] == null)
                RedirectToAdminDefaultPage();
            else
                System.Web.HttpContext.Current.Response.Redirect(System.Web.HttpContext.Current.Request.QueryString["ReturnUrl"].ToString());

        }

        /// <summary>
        /// Redirects the current user based on role
        /// </summary>
        public static void RedirectToDefaultPage()
        {

           // System.Web.HttpContext.Current.Response.Redirect("~/index.aspx");

            System.Web.HttpContext.Current.Response.RedirectToRoute ( "Default" );

        //if ( System.Web.HttpContext.Current.Request.QueryString [ "ReturnUrl" ] == null )
        //    System.Web.HttpContext.Current.Response.Redirect ( "~/index.aspx" );
        //else
        //    System.Web.HttpContext.Current.Response.Redirect ( "~"+ System.Web.HttpContext.Current.Request.QueryString [ "ReturnUrl" ].ToString ( ),true );


        }

        public static void RedirectToDefaultPageUser ( string pageRedirect)
            {
        

            // System.Web.HttpContext.Current.Response.Redirect("~/index.aspx");

            System.Web.HttpContext.Current.Response.RedirectToRoute ( pageRedirect );

          

            }

        public static void RedirectToAdminDefaultPage()
        {
           
            System.Web.HttpContext.Current.Response.Redirect("~/secured/admin/admin-default.aspx");
        }

        public static void LogOff()
        {
            // Put user code to initialize the page here
            FormsAuthentication.SignOut();

            //// Invalidate roles token
            //Response.Cookies[Globals.UserRoles].Value = "";
            //Response.Cookies[Globals.UserRoles].Path = "/";
            //Response.Cookies[Globals.UserRoles].Expires = new System.DateTime(1999, 10, 12);

            //Set the current user as null
            System.Web.HttpContext.Current.User = null;
            System.Web.HttpContext.Current.Response.RedirectToRoute ( "Index" );
        }
    }
}