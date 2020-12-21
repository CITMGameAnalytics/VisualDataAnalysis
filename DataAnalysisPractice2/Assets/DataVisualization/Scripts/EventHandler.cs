using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Events;
using DataVisualizer;
using Gamekit3D;

public class EventHandler : MonoBehaviour
{
    public DataSerializer serializer;
    public PlayerSessionSimulator sessionSimulator;

    string directory = "/DataVisualization/DataFiles/";

    // Event Lists
    EventContainer<RegisterEvent> registerEventContainer = new EventContainer<RegisterEvent>();
    EventContainer<SessionEvent> sessionEventContainer = new EventContainer<SessionEvent>();

    EventContainer<GameEvent> walkEventContainer = new EventContainer<GameEvent>();
    EventContainer<GameEvent> airEventContainer = new EventContainer<GameEvent>();
    EventContainer<GameEvent> keyEventContainer = new EventContainer<GameEvent>();

    EventContainer<GameEvent> jumpEventContainer = new EventContainer<GameEvent>();
    EventContainer<GameEvent> attackEventContainer = new EventContainer<GameEvent>();
    EventContainer<HitEvent> hitEventContainer = new EventContainer<HitEvent>();
    EventContainer<GameEvent> deathEventContainer = new EventContainer<GameEvent>();
    EventContainer<GameEvent> spawnEventContainer = new EventContainer<GameEvent>();

    EventContainer<GameEvent> invStartEventContainer = new EventContainer<GameEvent>();
    EventContainer<GameEvent> invEndEventContainer = new EventContainer<GameEvent>();
    
    void Awake()    //Read all available file data and load it into C# EventContainers to then add the new session events and overwrite the json files in order to maintain a single parsed Class OBJ in the .csv.
    {
        string json_file = "";

        // Players
        DataSerializer.Read(ref json_file, directory + "Players.csv");
        registerEventContainer.DeserializeList(json_file);
        json_file = "";

        // Sessions
        DataSerializer.Read(ref json_file, directory + "Sessions.csv");
        sessionEventContainer.DeserializeList(json_file);
        json_file = "";

        // Walks
        DataSerializer.Read(ref json_file, directory + "GroundPositions.csv");
        walkEventContainer.DeserializeList(json_file);
        json_file = "";

        // Airs
        DataSerializer.Read(ref json_file, directory + "AirbornePositions.csv");
        airEventContainer.DeserializeList(json_file);
        json_file = "";

        // Keys
        DataSerializer.Read(ref json_file, directory + "KeyPositions.csv");
        keyEventContainer.DeserializeList(json_file);
        json_file = "";

        // Jumps
        DataSerializer.Read(ref json_file, directory + "Jumps.csv");
        jumpEventContainer.DeserializeList(json_file);
        json_file = "";

        // Attacks
        DataSerializer.Read(ref json_file, directory + "Attacks.csv");
        attackEventContainer.DeserializeList(json_file);
        json_file = "";

        // Hits
        DataSerializer.Read(ref json_file, directory + "Hits.csv");
        hitEventContainer.DeserializeList(json_file);
        json_file = "";

        // Deaths
        DataSerializer.Read(ref json_file, directory + "Deaths.csv");
        deathEventContainer.DeserializeList(json_file);
        json_file = "";

        // Spawns
        DataSerializer.Read(ref json_file, directory + "Spawns.csv");
        spawnEventContainer.DeserializeList(json_file);
        json_file = "";

        // Invinicibility Starts
        DataSerializer.Read(ref json_file, directory + "InvulnerabilityStarts.csv");
        invStartEventContainer.DeserializeList(json_file);
        json_file = "";

        // Invincibility Ends
        DataSerializer.Read(ref json_file, directory + "InvulnerabilityEnds.csv");
        invEndEventContainer.DeserializeList(json_file);
        json_file = "";
    }

    private bool testSerialization = false;

    void Update()   // This serves no purpose other than being an example for how to deserialize .cvs files, specifically for Dídac
    {
        if (testSerialization && Time.time > 3)
        {
            EventContainer<GameEvent> dummyContainer = new EventContainer<GameEvent>();
            string dummyString = "";
            DataSerializer.Read(ref dummyString, directory + "Attacks.csv");
            List<GameEvent> dummyList = dummyContainer.DeserializeList(dummyString);

            testSerialization = false;
        }
    }

    void OnDestroy()    //Since on startup we loaded all events parsed in the json file, we overwrite the entire thing to parse the old and new events in a single C# object class.
    {
        // Players
        if (registerEventContainer.Count > 0)
            DataSerializer.Overwrite(registerEventContainer.SerializeList(), directory + "Players.csv");

        // Sessions
        sessionSimulator.RecordSessionEvent();
        if (sessionEventContainer.Count > 0)
            DataSerializer.Overwrite(sessionEventContainer.SerializeList(), directory + "Sessions.csv");

        // Walks
        if (walkEventContainer.Count > 0)
            DataSerializer.Overwrite(walkEventContainer.SerializeList(false), directory + "GroundPositions.csv");

        // Airs
        if (airEventContainer.Count > 0)
            DataSerializer.Overwrite(airEventContainer.SerializeList(false), directory + "AirbornePositions.csv");

        // Keys
        if (keyEventContainer.Count > 0)
            DataSerializer.Overwrite(keyEventContainer.SerializeList(false), directory + "KeyPositions.csv");

        // Jumps
        if (jumpEventContainer.Count > 0)
            DataSerializer.Overwrite(jumpEventContainer.SerializeList(), directory + "Jumps.csv");

        // Attacks
        if (attackEventContainer.Count > 0)
            DataSerializer.Overwrite(attackEventContainer.SerializeList(), directory + "Attacks.csv");

        // Hits
        if (hitEventContainer.Count > 0)
            DataSerializer.Overwrite(hitEventContainer.SerializeList(), directory + "Hits.csv");

        // Deaths
        if (deathEventContainer.Count > 0)
            DataSerializer.Overwrite(deathEventContainer.SerializeList(), directory + "Deaths.csv");

        // Spawns
        if (spawnEventContainer.Count > 0)
            DataSerializer.Overwrite(spawnEventContainer.SerializeList(), directory + "Spawns.csv");

        // Invinicibility Starts
        if (invStartEventContainer.Count > 0)
            DataSerializer.Overwrite(invStartEventContainer.SerializeList(), directory + "InvulnerabilityStarts.csv");

        // Invincibility Ends
        if (invEndEventContainer.Count > 0)
            DataSerializer.Overwrite(invEndEventContainer.SerializeList(), directory + "InvulnerabilityEnds.csv");
    }

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
    public void NewRegisterEvent(int player_id, string first_name, string second_name, int age, string gender, string country, string test_group) // User Register
    {
        RegisterEvent registerEvent = new RegisterEvent(player_id, first_name, second_name, age, gender, country, test_group);
        registerEventContainer.Add(registerEvent);

        //string event_data = registerEvent.Serialize(true);
        //DataSerializer.Overwrite(event_data, directory + "Players.csv");
    }

    public void NewSessionEvent(int player_id, int session_id, System.DateTime start, System.DateTime end) // User Login
    {
        SessionEvent sessionEvent = new SessionEvent(player_id, session_id, start, end);
        sessionEventContainer.Add(sessionEvent);

        //string event_data = sessionEvent.Serialize(true);
        //DataSerializer.Overwrite(event_data, directory + "Sessions.csv");
    }

    // Game Recurrent Events
    public void NewWalkingPositionEvent(Damageable character) // Walking on ground (periodical)
    {
        GameEvent gameEvent = new GameEvent(event_types.EVENT_WALK_POS, - 1, character.gameObject.GetInstanceID(), character.transform);
        walkEventContainer.Add(gameEvent);

        //string event_data = gameEvent.Serialize(true);
        //DataSerializer.Overwrite(event_data, directory + "GroundPositions.csv");
    }

    public void NewAirbornePositionEvent(Damageable character) // Being airborne (periodical)
    {
        GameEvent gameEvent = new GameEvent(event_types.EVENT_AIR_POS, -1, character.gameObject.GetInstanceID(), character.transform);
        airEventContainer.Add(gameEvent);

        //string event_data = gameEvent.Serialize(true);
        //DataSerializer.Overwrite(event_data, directory + "AirbornePositions.csv");
    }

    public void NewKeyPositionEvent(Damageable character) // Position while holding key (periodical)
    {
        GameEvent gameEvent = new GameEvent(event_types.EVENT_KEY_POS, -1, character.gameObject.GetInstanceID(), character.transform);
        keyEventContainer.Add(gameEvent);

        //string event_data = gameEvent.Serialize(true);
        //DataSerializer.Overwrite(event_data, directory + "KeyPositions.csv");
    }

    // Game Trigger Events
    public void NewJumpEvent(Damageable character) // Position where jumped
    {
        GameEvent gameEvent = new GameEvent(event_types.EVENT_JUMP, -1, character.gameObject.GetInstanceID(), character.transform);
        jumpEventContainer.Add(gameEvent);

        //string event_data = gameEvent.Serialize(true);
        //DataSerializer.Overwrite(event_data, directory + "Jumps.csv");
    }

    public void NewAttackEvent(Damageable character) // Position where attacked
    {
        GameEvent gameEvent = new GameEvent(event_types.EVENT_ATTACK, -1, character.gameObject.GetInstanceID(), character.transform);
        attackEventContainer.Add(gameEvent);

        //string event_data = gameEvent.Serialize(true);
        //DataSerializer.Overwrite(event_data, directory + "Attacks.csv");
    }

    public void NewHitEvent(Damageable character) // Position where damaged
    {
        HitEvent gameEvent = new HitEvent(-1, character.gameObject.GetInstanceID(), character.transform, character.currentHitPoints);
        hitEventContainer.Add(gameEvent);

        //string event_data = gameEvent.Serialize(true);
        //DataSerializer.Overwrite(event_data, directory + "Hits.csv");
    }

    public void NewDeathEvent(Damageable character) // Position where dead
    {
        GameEvent gameEvent = new GameEvent(event_types.EVENT_DEATH, -1, character.gameObject.GetInstanceID(), character.transform);
        deathEventContainer.Add(gameEvent);

        //string event_data = gameEvent.Serialize(true);
        //DataSerializer.Overwrite(event_data, directory + "Deaths.csv");
    }

    public void NewSpawnEvent(Damageable character) // Position where spawned or respawned
    {
        GameEvent gameEvent = new GameEvent(event_types.EVENT_SPAWN, -1, character.gameObject.GetInstanceID(), character.transform);
        spawnEventContainer.Add(gameEvent);

        //string event_data = gameEvent.Serialize(true);
        //DataSerializer.Overwrite(event_data, directory + "Spawns.csv");
    }

    public void NewInvulnerabilityStartEvent(Damageable character) // Position where started invulnerability
    {
        GameEvent gameEvent = new GameEvent(event_types.EVENT_INV_START, -1, character.gameObject.GetInstanceID(), character.transform);
        invStartEventContainer.Add(gameEvent);

        //string event_data = gameEvent.Serialize(true);
        //DataSerializer.Overwrite(event_data, directory + "InvulnerabilityStarts.csv");
    }

    public void NewInvulnerabilityEndEvent(Damageable character) // Position where ended invulnerability
    {
        GameEvent gameEvent = new GameEvent(event_types.EVENT_INV_END, -1, character.gameObject.GetInstanceID(), character.transform);
        invEndEventContainer.Add(gameEvent);

        //string event_data = gameEvent.Serialize(true);
        //DataSerializer.Overwrite(event_data, directory + "InvulnerabilityEnds.csv");
    }
}
