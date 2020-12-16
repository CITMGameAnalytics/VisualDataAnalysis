using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Gamekit3D;

namespace Events
{
    // EVENT IDs
    public enum event_types
    {
        EVENT_REGISTER = 1,
        EVENT_SESSION,
        EVENT_WALK_POS,
        EVENT_AIR_POS,
        EVENT_KEY_POS,
        EVENT_JUMP,
        EVENT_ATTACK,
        EVENT_HIT,
        EVENT_DEATH,
        EVENT_SPAWN,
        EVENT_INV_START,
        EVENT_INV_END
    }

    // EVENT GAME DATA STRUCT
    public struct GameEventData
    {
        public GameEventData(int session_id, int entity_id, Transform trs)
        {
            timestamp = System.DateTime.Now;
            this.session_id = session_id;
            this.entity_id = entity_id;
            position = trs.position;
            rotation = trs.rotation.eulerAngles;
        }

        System.DateTime timestamp;  // Moment when event happened
        int session_id;             // Session where event happened
        int entity_id;              // Entity responsible of event
        Vector3 position;           // Entity position
        Vector3 rotation;           // Entity direction
    }

    // EVENT CLASSES
    public class GenericEvent : MonoBehaviour
    {
        protected GenericEvent(event_types event_id)
        {
            this.event_id = event_id;
        }

        public string Serialize(bool pretty = true)
        {
            return JsonUtility.ToJson(this, pretty);
        }

        public event_types event_id;       // Event ID
    }

    // LOGIN EVENTS
    public class RegisterEvent : GenericEvent  // First Time Login Event
    {
        public RegisterEvent(int player_id, int age, string country, string test_group) : base(event_types.EVENT_REGISTER)
        {
            this.player_id = player_id;
            this.age = age;
            this.country = country;
            this.test_group = test_group;
        }

        public static object Deserialize(string json_file)
        {
            return JsonUtility.FromJson<RegisterEvent>(json_file);
        }

        int player_id;
        int age;
        string country;
        string test_group;
    }

    public class SessionEvent : GenericEvent   // Session End Event
    {
        public SessionEvent(int player_id, int session_id, System.DateTime start, System.DateTime end) : base(event_types.EVENT_SESSION)
        {
            this.player_id = player_id;
            this.session_id = session_id;
            this.start = start;
            this.end = end;
        }

        public static object Deserialize(string json_file)
        {
            return JsonUtility.FromJson<SessionEvent>(json_file);
        }

        int player_id;
        int session_id;
        System.DateTime start;
        System.DateTime end;
    }

    // GAME EVENTS
    public class GameEvent : GenericEvent
    {
        public GameEvent(event_types event_id, int session_id, int entity_id, Transform trs) : base(event_id)
        {
            data = new GameEventData(session_id, entity_id, trs);
        }

        public static object Deserialize(string json_file)
        {
            return JsonUtility.FromJson<GameEvent>(json_file);
        }

        public GameEventData data;
    }

    public class HitEvent : GameEvent
    {
        public HitEvent(int session_id, int entity_id, Transform trs, int hitPoints) : base(event_types.EVENT_HIT, session_id, entity_id, trs)
        {
            data = new GameEventData(session_id, entity_id, trs);
            this.hitPoints = hitPoints;
        }

        public static object Deserialize(string json_file)  // This method overriding the one of the parent obj is intended
        {
            return JsonUtility.FromJson<HitEvent>(json_file);
        }

        int hitPoints;
    }
}