using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DataVisualizer
{
    public class DataSerializer : MonoBehaviour
    {
        public static DataSerializer dataSerializer;

        //[System.Serializable]
        //private class Wrapper<T>
        //{
        //    public T[] Items;
        //}

        private void Awake()
        {
            if (dataSerializer == null)
                dataSerializer = this;
            else
                Destroy(gameObject);

            string t = "HEY";


            //dataSerializer._Read(ref t, "DataVisualization/DataFiles/InvulnerabilityStarts.csv");

            //// --- Serialize ---
            //List<Events.HitEvent> hitEvents = new List<Events.HitEvent>();
            //hitEvents.Add(new Events.HitEvent(0, 0, gameObject.transform, 0));
            //hitEvents.Add(new Events.HitEvent(0, 0, gameObject.transform, 0));

            //Wrapper<Events.HitEvent> wrapper = new Wrapper<Events.HitEvent>();
            //wrapper.Items = hitEvents.ToArray();
            //t = JsonUtility.ToJson(wrapper, true);

            //dataSerializer._Print(ref t, "test.csv");

            //// --- Deserialize ---
            //dataSerializer._Read(ref t, "test.csv");

            //Wrapper<Events.HitEvent> Dewrapper = JsonUtility.FromJson<Wrapper<Events.HitEvent>>(t);
            //hitEvents.AddRange(Dewrapper.Items);

        }

        public static void Overwrite(string s, string filename)
        {
            dataSerializer._Overwrite(ref s, filename);
        }

        public static void Print(string s, string filename)
        {
            dataSerializer._Print(ref s, filename);
        }

        public static void Read(ref string s, string filename)
        {
            dataSerializer._Read(ref s, filename);
        }

        public void _Overwrite(ref string s, string filename)
        {
            string destination = Application.dataPath + "/" + filename;
            System.IO.File.WriteAllText(destination, s);
        }

        public void _Print(ref string s, string filename)
        {
            string destination = Application.dataPath + "/" + filename;

            if (File.Exists(destination))
                System.IO.File.AppendAllText(destination, s);
            else
            {
                File.Create(destination);
                System.IO.File.WriteAllText(destination, s);
            }
        }

        public void _Read(ref string s, string filename)
        {
            string destination = Application.dataPath + "/" + filename;

            if (File.Exists(destination))
                s = System.IO.File.ReadAllText(destination);
        }

    }
}

