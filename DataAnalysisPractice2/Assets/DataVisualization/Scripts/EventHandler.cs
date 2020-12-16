using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Events;
using DataVisualizer;
using Gamekit3D;

public class EventHandler : MonoBehaviour
{
    public DataSerializer serializer;
    string directory = "/DataVisualization/DataFiles/";

    // -------------------------------------- CSV LEGACY CODE --------------------------------------

    //enum event_types
    //{
    //    EVENT_REGISTER = 1,
    //    EVENT_SESSION,
    //    EVENT_WALK_POS,
    //    EVENT_AIR_POS,
    //    EVENT_KEY_POS,
    //    EVENT_JUMP,
    //    EVENT_ATTACK,
    //    EVENT_HIT,
    //    EVENT_DEATH,
    //    EVENT_SPAWN,
    //    EVENT_INV_START,
    //    EVENT_INV_END
    //}

    //private string SerializeNext()
    //{
    //    return ",";
    //}

    //private string SerializeEnd()
    //{
    //    return "\n";
    //}

    //private string GetTimestamp()
    //{
    //    System.DateTime timestamp = System.DateTime.Now;
    //    return timestamp.ToString();
    //}

    //private string SerializeStandardData(Damageable character, event_types event_id)
    //{
    //    //string event_data = event_id.ToString() + SerializeNext();               // TODO: Check if necessary
    //    string event_data = GetTimestamp() + SerializeNext();                      // Timestamp
    //    //event_data += character.player_id + SerializeNext();                     // TODO: Add a static variable called "player_id" with hardcoded values which identify which player is playing when these events happen
    //    event_data += character.gameObject.GetInstanceID() + SerializeNext();      // Entity ID
    //    event_data += character.transform.position.ToString() + SerializeNext();   // Position
    //    event_data += character.transform.rotation.eulerAngles.ToString();         // Direction

    //    return event_data;
    //}

    // -------------------------------------- CSV LEGACY CODE --------------------------------------

    // Session Events
    public void NewRegisterEvent(int player_id, int age, string country, string test_group) // User Register
    {
        RegisterEvent registerEvent = new RegisterEvent(player_id, age, country, test_group);
        string event_data = registerEvent.Serialize(true);
        
        DataSerializer.Print(event_data, directory + "Players.csv");
    }

    public void NewSessionEvent(int player_id, int session_id, System.DateTime start, System.DateTime end) // User Login
    {
        SessionEvent sessionEvent = new SessionEvent(player_id, session_id, start, end);
        string event_data = sessionEvent.Serialize(true);

        DataSerializer.Print(event_data, directory + "Sessions.csv");
    }

    // Game Recurrent Events
    public void NewWalkingPositionEvent(Damageable character) // Walking on ground (periodical)
    {
        GameEvent gameEvent = new GameEvent(Events.event_types.EVENT_WALK_POS, - 1, character.gameObject.GetInstanceID(), character.transform);
        string event_data = gameEvent.Serialize(false);

        DataSerializer.Print(event_data, directory + "GroundPositions.csv");
    }

    public void NewAirbornePositionEvent(Damageable character) // Being airborne (periodical)
    {
        GameEvent gameEvent = new GameEvent(Events.event_types.EVENT_AIR_POS, -1, character.gameObject.GetInstanceID(), character.transform);
        string event_data = gameEvent.Serialize(false);

        DataSerializer.Print(event_data, directory + "AirbornePositions.csv");
    }

    public void NewKeyPositionEvent(Damageable character) // Position while holding key (periodical)
    {
        GameEvent gameEvent = new GameEvent(Events.event_types.EVENT_KEY_POS, -1, character.gameObject.GetInstanceID(), character.transform);
        string event_data = gameEvent.Serialize(false);

        DataSerializer.Print(event_data, directory + "KeyPositions.csv");
    }

    // Game Trigger Events
    public void NewJumpEvent(Damageable character) // Position where jumped
    {
        GameEvent gameEvent = new GameEvent(Events.event_types.EVENT_JUMP, -1, character.gameObject.GetInstanceID(), character.transform);
        string event_data = gameEvent.Serialize(true);

        DataSerializer.Print(event_data, directory + "Jumps.csv");
    }

    public void NewAttackEvent(Damageable character) // Position where attacked
    {
        GameEvent gameEvent = new GameEvent(Events.event_types.EVENT_ATTACK, -1, character.gameObject.GetInstanceID(), character.transform);
        string event_data = gameEvent.Serialize(true);

        DataSerializer.Print(event_data, directory + "Attacks.csv");
    }

    public void NewHitEvent(Damageable character) // Position where damaged
    {
        HitEvent gameEvent = new HitEvent(-1, character.gameObject.GetInstanceID(), character.transform, character.currentHitPoints);
        string event_data = gameEvent.Serialize(true);

        DataSerializer.Print(event_data, directory + "Hits.csv");
    }

    public void NewDeathEvent(Damageable character) // Position where dead
    {
        GameEvent gameEvent = new GameEvent(Events.event_types.EVENT_DEATH, -1, character.gameObject.GetInstanceID(), character.transform);
        string event_data = gameEvent.Serialize(true);

        DataSerializer.Print(event_data, directory + "Deaths.csv");
    }

    public void NewSpawnEvent(Damageable character) // Position where spawned or respawned
    {
        GameEvent gameEvent = new GameEvent(Events.event_types.EVENT_SPAWN, -1, character.gameObject.GetInstanceID(), character.transform);
        string event_data = gameEvent.Serialize(true);

        DataSerializer.Print(event_data, directory + "Spawns.csv");
    }

    public void NewInvulnerabilityStartEvent(Damageable character) // Position where started invulnerability
    {
        GameEvent gameEvent = new GameEvent(Events.event_types.EVENT_INV_START, -1, character.gameObject.GetInstanceID(), character.transform);
        string event_data = gameEvent.Serialize(true);

        DataSerializer.Print(event_data, directory + "InvulnerabilityStarts.csv");
    }

    public void NewInvulnerabilityEndEvent(Damageable character) // Position where ended invulnerability
    {
        GameEvent gameEvent = new GameEvent(Events.event_types.EVENT_INV_END, -1, character.gameObject.GetInstanceID(), character.transform);
        string event_data = gameEvent.Serialize(true);

        DataSerializer.Print(event_data, directory + "InvulnerabilityEnds.csv");
    }
}
