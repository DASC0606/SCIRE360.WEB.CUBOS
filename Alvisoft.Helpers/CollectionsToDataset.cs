using System;
using System.Collections;
using System.Collections.Generic;

using System.Reflection;
using System.Linq;
using System.Data;
using System.Web;

namespace Alvisoft.Helpers
    {
    /// <summary>
    /// Convert Generic List to Dataset. This will take anything that implements the ICollection interface and convert
    /// it to a DataSet.
    /// </summary>
    /// <example>
    /// CollectiontoDataSet converter = new CollectionToDataSet<Letters[]>(letters);
    /// DataSet ds = converter.CreateDataSet();
    /// </example>
    /// <typeparam name=”T”></typeparam>

    public static class CollectionsToDataSet<T> where T : System.Collections.ICollection
        {
        T _collection;
        public CollectionsToDataSet ( T list )
            {
            _collection = list;
            }

        private PropertyInfo[] _propertyCollection = null;

        private PropertyInfo [ ] PropertyCollection
            {
            get
                {
                if ( _propertyCollection == null )
                    {
                    _propertyCollection = GetPropertyCollection ( );
                    }
                return _propertyCollection;
                }
            }

        private PropertyInfo [ ] GetPropertyCollection ( )
            {
            if ( _collection.Count > 0 )
                {
                IEnumerator enumerator = _collection.GetEnumerator ( );
                enumerator.MoveNext ( );
                return enumerator.Current.GetType ( ).GetProperties ( );
                }
            return null;
            }

        public DataSet CreateDataSet ( )
            {
            DataSet ds = new DataSet ( "AlvisoftGridDataSet" );
            ds.Tables.Add ( FillDataTable ( ) );
            return ds;
            }

        private DataTable FillDataTable ( )
            {
            IEnumerator enumerator = _collection.GetEnumerator ( );
            DataTable dt = CreateDataTable ( );
            while ( enumerator.MoveNext ( ) )
                {
                dt.Rows.Add ( FillDataRow ( dt.NewRow ( ), enumerator.Current ) );
                }
            return dt;
            }

        private DataRow FillDataRow ( DataRow dataRow, object p )
            {
            foreach ( PropertyInfo property in PropertyCollection )
                {
                dataRow [ property.Name.ToString ( ) ] = property.GetValue ( p, null );
                }
            return dataRow;
            }

        private DataTable CreateDataTable ( )
            {
            DataTable dt = new DataTable ( "AlvisoftGridDataTable" );
            foreach ( PropertyInfo property in PropertyCollection )
                {
                dt.Columns.Add ( property.Name.ToString ( ) );
                }
            return dt;
            }
        }

       
    }