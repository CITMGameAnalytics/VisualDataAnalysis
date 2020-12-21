using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

    // EVENT CONTAINER
    public class EventContainer<ContainerEventType>  // Has a propper list and a wrapper object class to serialize it
    {
        [System.Serializable]
        public class EventWrapper<WrapperEventType>   // Object class which only purpose is to contain an array to de/serialize
        {
            public int Length
            {
                get { return events.Length; }
            }

            public WrapperEventType[] events;
        }

        public void Add(ContainerEventType eventEntry)
        {
            eventList.Add(eventEntry);
        }

        public string SerializeList(bool pretty = true)
        {
            eventWrapper.events = eventList.ToArray();
            string s = JsonUtility.ToJson(eventWrapper, pretty);
            return s;
        }

        public List<ContainerEventType> DeserializeList(string json_file)
        {
            if (json_file != "")
            {
                eventWrapper = JsonUtility.FromJson<EventWrapper<ContainerEventType>>(json_file);
                eventList = eventWrapper.events.ToList();
                return eventList;
            }
            else
                return null;
        }

        public int Count
        {
            get { return eventList.Count; }
        }

        public int Length
        {
            get { return eventWrapper.events.Length; }
        }

        public List<ContainerEventType> eventList = new List<ContainerEventType>();
        public EventWrapper<ContainerEventType> eventWrapper = new EventWrapper<ContainerEventType>();
    }

    // EVENT CLASSES
    [System.Serializable]
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
    [System.Serializable]
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

        int player_id;
        int age;
        string country;
        string test_group;
    }

    [System.Serializable]
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

        int player_id;
        int session_id;
        string start;
        string end;
    }

    // GAME EVENTS
    [System.Serializable]
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

        public string timestamp;  // Moment when event happened
        public int session_id;             // Session where event happened
        public int entity_id;              // Entity responsible of event
        public Vector3 position;           // Entity position
        public Vector3 rotation;           // Entity direction
    }

    [System.Serializable]
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

        public int hitPoints;
    }
}
