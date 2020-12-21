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

        private void Awake()
        {
            if (dataSerializer == null)
                dataSerializer = this;
            else
                Destroy(gameObject);
        }

        public static void Print(string s, string filename)
        {
            dataSerializer._Print(ref s, filename);
        }

        public static void Read(ref string s, string filename)
        {
            dataSerializer._Read(ref s, filename);
        }

        public void _Print(ref string s, string filename)
        {
            string destination = Application.dataPath + "/" + filename;

            // Note we are currently overwriting the file if it exists, this is intentional
             System.IO.File.WriteAllText(destination, s);
            
        }

        public void _Read(ref string s, string filename)
        {
            string destination = Application.dataPath + "/" + filename;

            if (File.Exists(destination))
                s = System.IO.File.ReadAllText(destination);
        }

    }
}

