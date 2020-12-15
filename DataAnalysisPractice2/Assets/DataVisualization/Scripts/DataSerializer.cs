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

            //dataSerializer._Print("test", "test.csv");
            //string t = "";
            //dataSerializer._Read(ref t, "test.csv");
        }

        public static void Print(string s, string filename)
        {
            dataSerializer._Print(s, filename);
        }

        public static void Read(ref string s, string filename)
        {
            dataSerializer._Read(ref s, filename);
        }

        public void _Print(string s, string filename)
        {
            // write csv
            string destination = Application.dataPath + filename;
            FileStream file;

            if (File.Exists(destination))
                file = File.Open(destination, FileMode.Append);
            else
                file = File.Create(destination);

            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(file, s);

            file.Close();
        }

        public void _Read(ref string s, string filename)
        {
            // write csv
            string destination = Application.dataPath + "/" + filename;
            FileStream file;

            if (File.Exists(destination))
                file = File.OpenRead(destination);
            else
            {
                Debug.LogError("File not found");
                return;
            }

            BinaryFormatter bf = new BinaryFormatter();
            s = bf.Deserialize(file).ToString();

            file.Close();
        }

    }
}

