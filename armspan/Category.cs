/**
 * @file
 * @author Allan Wiener
 * 
 * @section DESCRIPTION
 * 
 * The Category class defines one of several user-defined
 * categories that Events can have.
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Span
{
    class Category : JSONCapable
    {
        /**
         * Creates a new Category from the specified name and color.
         * The category is assigned an Id number so that it can be
         * referenced regardless of name.
         * 
         * @param a_name the name of the category.
         * 
         * @param a_color the color of the category, as a Color
         * struct. If an event sets this Category as its primary
         * category, then the event will appear in this color.
         * 
         * @date March 4, 2016
         */
        public Category(string a_name, Color a_color)
        {
            Name = a_name;
            Color = a_color;
            m_num = num++;

            //change this to implement a better hashing function
            m_id = "c" + m_num.ToString("x8");
            All.Add(m_id, this);
        }

        /**
         * Creates a new Category object without
         * any initialized data.
         * 
         * Please only use this constructor for
         * deserialization.
         * 
         * @date March 26, 2016
         */
        protected Category(){}

        /**
         * Gets or sets the name of the Category.
         * 
         * This can be changed at any time.
         */
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

        /**
         * Gets or sets the color of the category, as a Color
         * struct. If an event sets this Category as its primary
         * category, then the event will appear in this color.
         * 
         * Note: any alpha values will be ignored.
         * 
         */
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

        /**
         * Gets the number representing the position of the
         * Category in the sequence of categories ordered
         * chronologically by their creation.
         * 
         * For example, if four categories were created before
         * this one, this Category would have a number of 5.
         * 
         * This number is displayed to the user instead of the Id.
         */
        public uint Number
        {
            get
            {
                return m_num;
            }
        }

        /**
         * Gets the id of the Category.
         * 
         * The id is written in the form of a string starting
         * with the letter 'c', followed by a 32-bit unsigned
         * integer in hexadecimal (all lowercase). This allows
         * the category to be looked up easily.
         * 
         * Currently, the integer portion of the id is
         * simply equal to the category's Number.
         */
        public string Id
        {
            get
            {
                return m_id;
            }
        }


        /**
         * Generates a Category object from the
         * specified JSON-serialized Category
         * string.
         * 
         * @param json the serialized string
         * representing the object.
         * 
         * @return the object, properly
         * deserialized and initialized.
         * 
         * @date March 26, 2016
         */
        public static Category FromJSON(string json)
        {
            Dictionary<string, object> jsd = JSONDictionary(Category.FromString(json));
            Dictionary<string, object> rawcolor = jss.ConvertToType<Dictionary<string, object>>(jsd["Color"]);
            Color c = Color.FromArgb((int)rawcolor["R"], (int)rawcolor["G"], (int)rawcolor["B"]);
            
            string name = (string)jsd["Name"];
            string id = (string)jsd["Id"];
            uint numId = (uint)(int)jsd["Number"];
            Category loaded = new Category();
            loaded.Color = c;
            loaded.Name = name;
            loaded.m_id = id;
            loaded.m_num = numId;
            if (numId >= num)
            {
                num = numId + 1;
            }
            All.Add(id, loaded);
            return loaded;
        }

        /**
         * Gets a Dictionary containing all Categories by Id.
         */
        public static Dictionary<string, Category> All { get { return all; } }

        /**
         * Gets a list of all Events that are marked under this Category.
         * 
         * @param cat the id of the requested category.
         * @return a list of string ids of the events marked
         * under this category.
         * 
         * @date March 25, 2016
         */
        public static List<string> GetEvents(string cat){
            IEnumerable<KeyValuePair<string, Event>> partial = Event.All.Where(x => x.Value.Categories.Contains(cat));
            return partial.ToDictionary(x => x.Key, x => x.Value).Keys.ToList();
        }

        /**
         * The color of the Category. See also Color.
         */
        private Color m_color;

        /**
         * The number of the Category. See also Number.
         */
        private uint m_num;
        /**
         * The name of the Category. See also Name.
         */
        private string m_name;
        /**
         * The id of the Category. See also Id.
         */
        private string m_id;
        /**
         * This counter is used to give each new Category a distinct Number.
         */
        private static uint num = 1;
        /**
         * Contains all Category objects as values, with their Id strings as keys.
         */
        private static Dictionary<string, Category> all = new Dictionary<string, Category>();
    }
}
