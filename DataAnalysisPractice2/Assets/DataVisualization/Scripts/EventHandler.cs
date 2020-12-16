using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DataVisualizer;
using Gamekit3D;

public class EventHandler : MonoBehaviour
{
    DataSerializer serializer = new DataSerializer();

    private string SerializeNext()
    {
        return ", ";
    }

    private string SerializeEnd()
    {
        return ";\n";
    }

    private string GetTimestamp()
    {
        System.DateTime timestamp = System.DateTime.Now;
        return timestamp.ToString();
    }

    private string SerializeStandardData(Damageable character)
    {
        string event_data = GetTimestamp() + SerializeNext();                      // Timestamp
        //event_data += character.player_id + SerializeNext();                     // TODO: Add a static variable called "player_id" with hardcoded values which identify which player is playing when these events happen
        event_data += character.gameObject.GetInstanceID() + SerializeNext();      // Entity ID
        event_data += character.transform.position.ToString() + SerializeNext();   // Position
        event_data += character.transform.rotation.eulerAngles.ToString();          // Direction

        return event_data;
    }

    // PLAYER EVENTS
    // Login Events
    public void NewRegisterEvent(int player_id, int age, string country, string test_group) // User Register
    {
        string event_data = GetTimestamp() + SerializeNext();
        event_data += player_id.ToString() + SerializeNext();
        event_data += age.ToString() + SerializeNext();
        event_data += country + SerializeNext();
        event_data += test_group;

        DataSerializer.Print(event_data + SerializeEnd(), "Assets/EventRegister/Players.csv");
    }

    public void NewSessionEvent(int player_id, int session_id, System.DateTime start, System.DateTime end) // User Login
    {
        string event_data = GetTimestamp() + SerializeNext();
        event_data += player_id.ToString() + SerializeNext();
        event_data += session_id.ToString() + SerializeNext();
        event_data += start.ToString() + SerializeNext();
        event_data += end.ToString();

        DataSerializer.Print(event_data + SerializeEnd(), "Assets/EventRegister/Sessions.csv");
    }

    // Recurrent events
    // INFO NEEDED: int entity_id, int pos_x, int pos_y, int pos_z, int rot_x, int rot_y, int rot_z
    public void NewWalkingPositionEvent(Damageable character) // Walking on ground (periodical)
    {
        string event_data = SerializeStandardData(character);
        DataSerializer.Print(event_data + SerializeEnd(), "Assets/EventRegister/GroundPositions.csv");
    }

    public void NewAirbornePositionEvent(Damageable character) // Being airborne (periodical)
    {
        string event_data = SerializeStandardData(character);
        DataSerializer.Print(event_data + SerializeEnd(), "Assets/EventRegister/AirbornePositions.csv");
    }

    public void NewKeyPositionEvent(Damageable character) // Position while holding key (periodical)
    {
        string event_data = SerializeStandardData(character);
        DataSerializer.Print(event_data + SerializeEnd(), "Assets/EventRegister/KeyPositions.csv");
    }

    // Trigger events
    public void NewJumpEvent(Damageable character) // Position where jumped
    {
        string event_data = SerializeStandardData(character);
        DataSerializer.Print(event_data + SerializeEnd(), "Assets/EventRegister/Jumps.csv");
    }

    public void NewAttackEvent(Damageable character) // Position where attacked
    {
        string event_data = SerializeStandardData(character);
        DataSerializer.Print(event_data + SerializeEnd(), "Assets/EventRegister/Attacks.csv");
    }

    public void NewHitEvent(Damageable character) // Position where damaged
    {
        string event_data = SerializeStandardData(character) + SerializeNext();
        event_data += character.currentHitPoints.ToString();    // In addition, we add current HP left
        DataSerializer.Print(event_data + SerializeEnd(), "Assets/EventRegister/Hits.csv");
    }

    public void NewDeathEvent(Damageable character) // Position where dead
    {
        string event_data = SerializeStandardData(character);
        DataSerializer.Print(event_data + SerializeEnd(), "Assets/EventRegister/Deaths.csv");
    }

    public void NewSpawnEvent(Damageable character) // Position where spawned or respawned
    {
        string event_data = SerializeStandardData(character);
        DataSerializer.Print(event_data + SerializeEnd(), "Assets/EventRegister/Spawns.csv");
    }

    public void NewInvulnerabilityStartEvent(Damageable character) // Position where started invulnerability
    {
        string event_data = SerializeStandardData(character);
        DataSerializer.Print(event_data + SerializeEnd(), "Assets/EventRegister/InvulnerabilityStarts.csv");
    }

    public void NewInvulnerabilityEndEvent(Damageable character) // Position where ended invulnerability
    {
        string event_data = SerializeStandardData(character);
        serializer._Print(event_data + SerializeEnd(), "Assets/EventRegister/InvulnerabilityEnds.csv");
    }
}
