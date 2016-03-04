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

        public override string ToString()
        {
            return jss.Serialize(this);
        }
    }
}
