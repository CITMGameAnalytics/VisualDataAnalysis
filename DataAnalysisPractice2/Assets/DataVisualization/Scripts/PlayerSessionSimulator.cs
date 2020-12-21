using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Events;

public class PlayerSessionSimulator : MonoBehaviour
{
    public EventHandler eventHandler;

    public struct player_data
    {
        public player_data(int player_id, string first_name, string second_name, int age, string gender, string country, string test_group)
        {
            this.player_id = player_id;
            this.first_name = first_name;
            this.second_name = second_name;
            this.age = age;
            this.gender = gender;
            this.country = country;
            this.test_group = test_group;
        }

        public int player_id;
        public string gender;
        public string first_name;
        public string second_name;
        public int age;
        public string country;
        public string test_group;
    }
    public player_data currentPlayer = new player_data();

    public player_data[] playerbase = new player_data[10] {
        new player_data(1, "Agustin", "Sarin", 21, "M", "United Kingdom", "A"),
        new player_data(2, "Sol", "Coolbaugh", 14, "M", "India", "A"),
        new player_data(3, "Johnny", "Trumbo", 24, "M", "Germany", "B"),
        new player_data(4, "Ena", "Chivers", 35, "F", "France", "A"),
        new player_data(5, "Clarine", "Faubers", 42, "F", "United States", "B"),
        new player_data(6, "Hoyt", "Bewley", 71, "M", "Brazil", "A"),
        new player_data(7, "Lakenya", "Bushmaker", 57, "F", "China", "B"),
        new player_data(8, "Tyree", "Gustiuts", 12, "M", "Japan", "B"),
        new player_data(9, "Esteban", "Dacenzo", 17, "M", "Norway", "B"),
        new player_data(10, "Qiana", "Callejo", 66, "F", "Rusia", "A")
    };

    public struct session_data
    {
        public int player_id;
        public int session_id;
        public System.DateTime start;
        public System.DateTime end;
    }
    public session_data currentSession = new session_data();

    // Start is called before the first frame update
    void Awake()
    {
        RandomizePlayer();  // Choose a player from the playerbase
        RandomizeSession(); // Create a session event entry
    }

    public void RecordSessionEvent()
    {
        currentSession.end = System.DateTime.Now;
        eventHandler.NewSessionEvent(currentSession.player_id, currentSession.session_id, currentSession.start, currentSession.end);
    }

    private void RandomizePlayer()
    {
        currentPlayer = playerbase[Random.Range(1, 11)];
        eventHandler.NewRegisterEvent(currentPlayer.player_id, currentPlayer.first_name, currentPlayer.second_name, currentPlayer.age, currentPlayer.gender, currentPlayer.country, currentPlayer.test_group);
    }

    private void RandomizeSession()
    {
        currentSession.player_id = currentPlayer.player_id;
        currentSession.session_id = Random.Range(0, 999999999);
        currentSession.start = System.DateTime.Now;
    }
}
