/**
 * @file
 * @author Allan Wiener
 * 
 * @section DESCRIPTION
 * 
 * The JSONCapable class allows any of its subclasses
 * to be serialized to and deserialized from a JSON string.
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Span
{
    class JSONCapable
    {
        /**
         * Provides serialization functionality.
         */
        public static JavaScriptSerializer jss = new JavaScriptSerializer();

        //public static Object FromString(string json, Type type)
        //{
        //    Object obj = jss.Deserialize(json, type);
        //    return obj;
        //}

        /**
         * Serializes the object to a JSON string.
         * 
         * @return a string in JSON format representing this object.
         * 
         * @date March 4, 2016
         */
        public override string ToString()
        {
            return jss.Serialize(this);
        }
    }
}
