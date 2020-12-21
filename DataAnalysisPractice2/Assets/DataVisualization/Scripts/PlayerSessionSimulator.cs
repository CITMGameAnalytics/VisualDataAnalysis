using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Events;

public class PlayerSessionSimulator : MonoBehaviour
{
    public EventHandler eventHandler;

    public struct player_data
    {
        public int player_id;
        public string first_name;
        public string second_name;
        public int age;
        public string country;
        string test_group;
    }

    public struct session_data
    {
        public int session_id;
        public string start;
        public string end;
    }

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //player_data RandomizePlayer()
    //{

    //}

    //session_data RandomizeSession()
    //{

    //}
}
