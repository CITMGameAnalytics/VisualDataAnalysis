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

    // EVENT CLASSES
    public class GenericEvent
    {
        protected GenericEvent(event_types event_id)
        {
            this.event_id = event_id;
        }

        public virtual string Serialize(bool pretty)
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

        public override string Serialize(bool pretty)
        {
            return JsonUtility.ToJson(this, pretty);
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
            this.start = start.ToString();
            this.end = end.ToString();
        }

        public override string Serialize(bool pretty)
        {
            return JsonUtility.ToJson(this, pretty);
        }

        public static object Deserialize(string json_file)
        {
            return JsonUtility.FromJson<SessionEvent>(json_file);
        }

        int player_id;
        int session_id;
        string start;
        string end;
    }

    // GAME EVENTS
    public class GameEvent : GenericEvent
    {
        public GameEvent(event_types event_id, int session_id, int entity_id, Transform trs) : base(event_id)
        {
            timestamp = System.DateTime.Now.ToString();
            this.session_id = session_id;
            this.entity_id = entity_id;
            position = trs.position;
            rotation = trs.rotation.eulerAngles;
        }

        public override string Serialize(bool pretty)
        {
            return JsonUtility.ToJson(this, pretty);
        }

        public static object Deserialize(string json_file)
        {
            return JsonUtility.FromJson<GameEvent>(json_file);
        }

        public string timestamp;  // Moment when event happened
        public int session_id;             // Session where event happened
        public int entity_id;              // Entity responsible of event
        public Vector3 position;           // Entity position
        public Vector3 rotation;           // Entity direction
    }

    //[System.Serializable]

    public class HitEvent : GameEvent
    {
        public HitEvent(int session_id, int entity_id, Transform trs, int hitPoints) : base(event_types.EVENT_HIT, session_id, entity_id, trs)
        {
            timestamp = System.DateTime.Now.ToString();
            this.session_id = session_id;
            this.entity_id = entity_id;
            position = trs.position;
            rotation = trs.rotation.eulerAngles;

            this.hitPoints = hitPoints;
        }

        public static object DeserializeHit(string json_file)  // This method overriding the one of the parent obj is intended
        {
            return JsonUtility.FromJson<HitEvent>(json_file);
        }

        int hitPoints;
    }
}