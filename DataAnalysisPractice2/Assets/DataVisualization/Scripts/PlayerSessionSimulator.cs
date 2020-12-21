using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Events;

public class PlayerSessionSimulator : MonoBehaviour // This class' entire purpose is to "simulate" a player from a plyerbase starting and ending a session in the game
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
        new player_data(3, "Lasse", "Loepfe", 24, "M", "Germany", "B"),
        new player_data(4, "Ena", "Chivers", 35, "F", "France", "A"),
        new player_data(5, "Clarine", "Faubers", 42, "F", "United States", "B"),
        new player_data(6, "Ricard", "Pillosu", 71, "M", "Brazil", "A"),
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

    public void PlayerLogin()   //Called right after all json files are read and added into C# Event Containers in order to append the User Registration to it
    {
        RandomizePlayer();  // Choose a player from the playerbase
        RandomizeSession(); // Create a session event entry
    }

    private void RandomizePlayer()  // Choose a random player from playerbase, if player already played before (present in the register event container), do not add him again, otherwise add a new register event
    {
        bool playerFound = false;
        currentPlayer = playerbase[Random.Range(0, 10)];

        foreach (RegisterEvent registeredPlayers in eventHandler.registerEventContainer.eventList) {
            if (registeredPlayers.player_id == currentPlayer.player_id)
            {
                playerFound = true;
                break;
            }
        }

        if (!playerFound)
            eventHandler.NewRegisterEvent(currentPlayer.player_id, currentPlayer.first_name, currentPlayer.second_name, currentPlayer.age, currentPlayer.gender, currentPlayer.country, currentPlayer.test_group);
    }

    private void RandomizeSession() // Assign the current player to the session and give it a randomized session_id
    {
        currentSession.player_id = currentPlayer.player_id;
        currentSession.session_id = Random.Range(0, 999999999);
        currentSession.start = System.DateTime.Now;
    }

    public void RecordSessionEvent()
    {
        currentSession.end = System.DateTime.Now;
        eventHandler.NewSessionEvent(currentSession.player_id, currentSession.session_id, currentSession.start, currentSession.end);
    }
}
