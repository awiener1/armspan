using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Span
{
    class JSONCapable
    {
        public static JavaScriptSerializer jss = new JavaScriptSerializer();

        //public static Object FromString(string json, Type type)
        //{
        //    Object obj = jss.Deserialize(json, type);
        //    return obj;
        //}

        public override string ToString()
        {
            return jss.Serialize(this);
        }
    }
}
