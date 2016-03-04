using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Web.Script.Serialization;

namespace Span
{
    class Category
    {
        public Category(string a_name, Color a_color)
        {
            Name = a_name;
            Color = a_color;
            m_num = num++;

            //change this to implement a better hashing function
            m_id = "c" + m_num.ToString("x8");
        }

        public string Name
        {
            get
            {
                return m_name;
            }
            set
            {
                m_name = value;
            }
        }

        public Color Color
        {
            get
            {
                return m_color;
            }
            set
            {
                //ignore transparency
                m_color = Color.FromArgb(value.R, value.G, value.B);
            }
        }

        public uint Number
        {
            get
            {
                return m_num;
            }
        }

        public string Id
        {
            get
            {
                return m_id;
            }
        }

        public override string ToString()
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Serialize(this);
        }


        private Color m_color;
        private uint m_num;
        private string m_name;
        private string m_id;
        private static uint num = 1;
    }
}
