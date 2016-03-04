using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Span
{
    class Category : JSONCapable
    {
        public Category(string a_name, Color a_color)
        {
            Name = a_name;
            Color = a_color;
            m_num = num++;

            //change this to implement a better hashing function
            m_id = "c" + m_num.ToString("x8");
        }
        public Category()
        {

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


        private Color m_color;
        private uint m_num;
        private string m_name;
        private string m_id;
        private static uint num = 1;
    }
}
