/****************** Copyright Notice *****************
 

   
*******************************************************/
using System;
namespace Alvisoft.Helpers
{
   public class BusinessLayerHelper
    {
        public static void ThrowErrorForInvalidDataKey(string dataFieldName)
        {
        throw new InvalidDataKeyException ( "El campo no es una clave de datos válido. Verificar el campo: " + dataFieldName );
                            //El campo no es una clave de datos válido. Verificar el campo:
                            //Data field is not a valid data key. Data field name:
        }

        public static void ThrowErrorForEmptyValue ( string dataFieldName )
            {
                //throw new EmptyValueNotAllowedException ( "El campo no está permitido referenciar el valor vacío. Verificar el campo: " + dataFieldName );
                //El campo no está permitido referenciar el valor vacío. Verificar el campo:
                //Data field is not allowed to be empty. Data field name:
            }

        public static void ThrowErrorForNullValue(string dataFieldName)
        {
        throw new NullValueNotAllowedException ( "El campo no está permitido referenciar valores NULL. Verificar el campo:: " + dataFieldName );
                        //El campo no está permitido referenciar valores NULL. Verificar el campo:
                        //Data field is not allowed to be null. Data field name:
        }
    }
}
