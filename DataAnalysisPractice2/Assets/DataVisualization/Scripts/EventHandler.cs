using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DataVisualizer;
using Gamekit3D;

public class EventHandler : MonoBehaviour
{
    private string GetTimestamp()
    {
        System.DateTime timestamp = System.DateTime.Now;
        return timestamp.ToString();
    }

    private string SerializeStandardData(Damageable character)
    {
        string event_data = GetTimestamp() + ElementEnd();                      // Timestamp
        //event_data += character.player_id + ElementEnd();                     // TODO: Add a static variable called "player_id" with hardcoded values which identify which player is playing when these events happen
        event_data += character.gameObject.GetInstanceID() + ElementEnd();      // Entity ID
        event_data += character.transform.position.ToString() + ElementEnd();   // Position
        event_data += character.transform.rotation.eulerAngles.ToString();      // Direction

        return event_data;
    }

    private string ElementEnd()
    {
        return ", ";
    }

    private string LineEnd()
    {
        return ";\n";
    }

    // PLAYER EVENTS
    // Login Events
    public void AddNewRegisterEvent(int player_id, int age, string country, string test_group) // User Register
    {
        string event_data = GetTimestamp() + ElementEnd();
        event_data += player_id.ToString() + ElementEnd();
        event_data += age.ToString() + ElementEnd();
        event_data += country + ElementEnd();
        event_data += test_group;

        DataSerializer.Print(event_data + LineEnd(), "Assets/EventRegister/Players.csv");
    }

    public void AddNewSessionEvent(int player_id, int session_id, System.DateTime start, System.DateTime end) // User Login
    {
        string event_data = GetTimestamp() + ElementEnd();
        event_data += player_id.ToString() + ElementEnd();
        event_data += session_id.ToString() + ElementEnd();
        event_data += start.ToString() + ElementEnd();
        event_data += end.ToString();

        DataSerializer.Print(event_data + LineEnd(), "Assets/EventRegister/Sessions.csv");
    }

    // Recurrent events
    // INFO NEEDED: int entity_id, int pos_x, int pos_y, int pos_z, int rot_x, int rot_y, int rot_z
    public void AddNewWalkingPositionEvent(Damageable character) // Walking on ground (periodical)
    {
        string event_data = SerializeStandardData(character);
        DataSerializer.Print(event_data + LineEnd(), "Assets/EventRegister/GroundPositions.csv");
    }

    public void AddNewAirbornePositionEvent(Damageable character) // Being airborne (periodical)
    {
        string event_data = SerializeStandardData(character);
        DataSerializer.Print(event_data + LineEnd(), "Assets/EventRegister/AirbornePositions.csv");
    }

    public void AddNewKeyPositionEvent(Damageable character) // Position while holding key (periodical)
    {
        string event_data = SerializeStandardData(character);
        DataSerializer.Print(event_data + LineEnd(), "Assets/EventRegister/KeyPositions.csv");
    }

    // Trigger events
    public void AddNewJumpEvent(Damageable character) // Position where jumped
    {
        string event_data = SerializeStandardData(character);
        DataSerializer.Print(event_data + LineEnd(), "Assets/EventRegister/Jumps.csv");
    }

    public void AddNewAttackEvent(Damageable character) // Position where attacked
    {
        string event_data = SerializeStandardData(character);
        DataSerializer.Print(event_data + LineEnd(), "Assets/EventRegister/Attacks.csv");
    }

    public void AddNewHitEvent(Damageable character) // Position where damaged
    {
        string event_data = SerializeStandardData(character) + ElementEnd();
        event_data += character.currentHitPoints.ToString();    // In addition, we add current HP left
        DataSerializer.Print(event_data + LineEnd(), "Assets/EventRegister/Hits.csv");
    }

    public void AddNewDeathEvent(Damageable character) // Position where dead
    {
        string event_data = SerializeStandardData(character);
        DataSerializer.Print(event_data + LineEnd(), "Assets/EventRegister/Deaths.csv");
    }

    public void AddNewSpawnEvent(Damageable character) // Position where spawned or respawned
    {
        string event_data = SerializeStandardData(character);
        DataSerializer.Print(event_data + LineEnd(), "Assets/EventRegister/Spawns.csv");
    }

    public void AddNewInvulnerabilityStartEvent(Damageable character) // Position where started invulnerability
    {
        string event_data = SerializeStandardData(character);
        DataSerializer.Print(event_data + LineEnd(), "Assets/EventRegister/InvulnerabilityStarts.csv");
    }

    public void AddNewInvulnerabilityEndEvent(Damageable character) // Position where ended invulnerability
    {
        string event_data = SerializeStandardData(character);
        DataSerializer.Print(event_data + LineEnd(), "Assets/EventRegister/InvulnerabilityEnds.csv");
    }
}
