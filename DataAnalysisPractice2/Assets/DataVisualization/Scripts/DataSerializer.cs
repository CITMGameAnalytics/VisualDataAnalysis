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

            string t = "HEY";
            //t = JsonUtility.ToJson(t);
            dataSerializer._Print(ref t, "test.csv");
            
            //dataSerializer._Read(ref t, "test.csv");
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
            // write csv
            string destination = Application.dataPath + filename;
            //FileStream file;

            //if (File.Exists(destination))
            //    file = File.Open(destination, FileMode.Open);
            //else
            //    file = File.Create(destination);

            //System.IO.StringWriter stringWriter;

            //BinaryFormatter bf = new BinaryFormatter();
            //bf.Serialize(file, s);

            //file.Write(s.t, (int)file.Length, s.Length);
            if (File.Exists(destination))
                System.IO.File.AppendAllText(destination, s);
            else
            {
                File.Create(destination);
                System.IO.File.WriteAllText(destination, s);
            }

            //file.Close();
        }

        public void _Read(ref string s, string filename)
        {
            // write csv
            string destination = Application.dataPath + "/" + filename;
            //FileStream file;

            //if (File.Exists(destination))
            //    file = File.OpenRead(destination);
            //else
            //{
            //    Debug.LogError("File not found");
            //    return;
            //}

            if (File.Exists(destination))
                s = System.IO.File.ReadAllText(destination);

            //file.Close();
        }

    }
}

