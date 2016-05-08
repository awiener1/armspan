/**
 * @file
 * @author Allan Wiener
 * 
 * @section DESCRIPTION
 * 
 * The JSONCapable class allows any of its subclasses
 * to be serialized to and deserialized from a JSON string. It also
 * provides serialization capability for the entire program state.
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Span
{
    public class JSONCapable
    {
        /**
         * Provides serialization functionality.
         */
        public static JavaScriptSerializer jss = new JavaScriptSerializer();

        /**
         * Creates an object from the specified JSON-serialized
         * string.
         * 
         * @param json the JSON string.
         * 
         * @return the equivalent object. This object is of
         * a non-usable type. Using JSONDictionary will
         * convert the object to a usable type.
         * 
         * @date March 4, 2016
         */
        public static Object FromString(string json)
        {
            Object obj = jss.DeserializeObject(json);
            return obj;
        }

        /**
         * Converts the specified JSON object into a
         * Dictionary.
         * 
         * @param JSONObject the non-usable JSON object.
         * 
         * @return a Dictionary with string keys and
         * object values equivalent to those found in
         * the JSON object.
         * 
         * @date March 17, 2016
         */
        public static Dictionary<string, object> JSONDictionary(object JSONObject)
        {
            Dictionary<string, object> outputter = jss.ConvertToType<Dictionary<string, object>>(JSONObject);
            return outputter;
        }

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

        /**
         * Saves the program state to a
         * JSON-serialized string.
         * 
         * @return a serialized string
         * representing the program's
         * current state (the first line
         * is a date and time stamp.)
         * 
         * @date March 26, 2016
         */
        public static string SaveState(){
            Dictionary<string, object> state = new Dictionary<string, object>();
            foreach (Event e in Event.All.Values)
            {
                
                KeyValuePair<DateTime, Occurrence> occpair = TimeKeeper.Alarms.FirstOrDefault(x=>x.Value.Id.Equals(e.Alarms.ParentId));
                e.Alarms.HasNextAlarm = (occpair.Value != null ? e.Id : "");
                if (e.Alarms.HasNextAlarm.Length > 0)
                {
                    e.Alarms.NextAlarmTimeToSave = occpair.Value.StartActual.ToUniversalTime();
                }
                
            }
            state.Add("Categories", Category.All);
            state.Add("Events", Event.All);
            string header = DateTime.Now.ToString() + "\n";
            return header + jss.Serialize(state);
        }

        /**
         * Loads the saved state from the
         * specified JSON-serialized
         * string.
         * 
         * @param json the serialized string
         * representing the object (the first line
         * must be a date and time stamp.)
         * 
         * @date March 26, 2016
         */
        public static void LoadState(string json)
        {
            Dictionary<string, object> jsd = JSONDictionary(Event.FromString(json.Substring(json.IndexOf("\n") + 1)));
            object jsdcat = jsd["Categories"];
          
            Dictionary<string, object> catsraw = jss.ConvertToType<Dictionary<string, object>>(jsd["Categories"]);
            foreach (Dictionary<string, object> catraw in catsraw.Values)
            {
                Category cat = Category.FromJSON(jss.Serialize(catraw));
                
            }

            Dictionary<string, object> evtsraw = jss.ConvertToType<Dictionary<string, object>>(jsd["Events"]);
            foreach (Dictionary<string, object> evtraw in evtsraw.Values)
            {
                Event evt = Event.FromJSON(jss.Serialize(evtraw));

            }
            //redefine all non-manual occurrences
            foreach (Event evt in Event.All.Values)
            {
                foreach (Period pd in evt.Rules)
                {
                    pd.Occurrences();
                }
            }
            
        }
    }
}
