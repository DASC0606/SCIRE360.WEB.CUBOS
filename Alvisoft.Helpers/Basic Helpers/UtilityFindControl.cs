using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Alvisoft.Helpers
{
    public class UtilityFindControl
    {
        /*********************  Helper for search controls in Container Control *******************/
        //usage: var listOfControls = UtilityFindControl.FindControlsOfType<Label>(grv);
        public static List<T> FindControlsOfType<T>(Control ctlRoot)
        {
            List<T> controlsFound = new List<T>();

            if (typeof(T).IsInstanceOfType(ctlRoot))
                controlsFound.Add((T)(object)ctlRoot);

            foreach (Control ctlTemp in ctlRoot.Controls)
            {
                controlsFound.AddRange(FindControlsOfType<T>(ctlTemp));
            }

            return controlsFound;
        }


        // Usage: Control myControl =  UtilityFindControl.FindControlRecursive(PlaceHolder1, "myControl");

        public static Control FindControlRecursive(Control root, string id)
        {
            if (root.ID == id)
                return root;

            foreach (Control c in root.Controls)
            {
                Control t = FindControlRecursive(c, id);
                if (t != null)
                    return t;
            }

            return null;
        }

        public static Control FindControlIterative ( Control root, string id )
            {
            //

            Control ctl = root;
            LinkedList<Control> ctls = new LinkedList<Control> ( );

            while ( ( ctl != null ) )
                {
                if ( ctl.ID == id )
                    {
                    return ctl;
                    }
                foreach ( Control child in ctl.Controls )
                    {
                    if ( child.ID == id )
                        {
                        return child;
                        }
                    if ( child.HasControls ( ) )
                        {
                        ctls.AddLast ( child );
                        }
                    }
                ctl = ctls.First.Value;
                ctls.Remove ( ctl );
                }

            return null;

            }


        
    }
}