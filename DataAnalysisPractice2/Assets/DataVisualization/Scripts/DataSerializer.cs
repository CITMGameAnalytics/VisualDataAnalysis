using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        public static void Print(string s)
        {
            dataSerializer._Print(s);
        }

        public void _Print(string s)
        {
            // write json
        }

    }
}

