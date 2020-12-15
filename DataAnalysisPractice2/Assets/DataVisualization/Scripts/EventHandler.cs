using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    // PLAYER EVENTS
    // Login Events
    public void AddNewPlayerEvent(int player_id, string country, string test_group) // User Register
    {
        //string event_data = GetTimestamp();
    }

    public void AddNewSessionEvent(int player_id, int session_id, string start, string end) // User Login
    {
        string event_data = GetTimestamp();
    }

    // Recurrent events
    public void AddNewWalkingPositionEvent(int entity_id, int pos_x, int pos_y, int pos_z, int rot_x, int rot_y, int rot_z) // Walking on ground (periodical)
    {
        string event_data = GetTimestamp();
    }

    public void AddNewAirbornePositionEvent(int entity_id, int pos_x, int pos_y, int pos_z, int rot_x, int rot_y, int rot_z) // Being airborne (periodical)
    {
        string event_data = GetTimestamp();
    }

    public void AddNewKeyPositionEvent(int entity_id, int pos_x, int pos_y, int pos_z, int rot_x, int rot_y, int rot_z) // Position while holding key (periodical)
    {
        string event_data = GetTimestamp();
    }

    // Trigger events
    public void AddNewJumpEvent(int entity_id, int pos_x, int pos_y, int pos_z, int rot_x, int rot_y, int rot_z) // Position where jumped
    {
        string event_data = GetTimestamp();
    }

    public void AddNewAttackEvent(int entity_id, int pos_x, int pos_y, int pos_z, int rot_x, int rot_y, int rot_z) // Position where attacked
    {
        string event_data = GetTimestamp();
    }

    public void AddNewDamageEvent(int entity_id, int pos_x, int pos_y, int pos_z, int rot_x, int rot_y, int rot_z) // Position where damaged
    {
        string event_data = GetTimestamp();
    }

    public void AddNewDeathEvent(int entity_id, int pos_x, int pos_y, int pos_z, int rot_x, int rot_y, int rot_z) // Position where dead
    {
        string event_data = GetTimestamp();
    }

    public void AddNewSpawnEvent(int entity_id, int pos_x, int pos_y, int pos_z, int rot_x, int rot_y, int rot_z) // Position where spawned or respawned
    {
        string event_data = GetTimestamp();
    }

    public void AddNewInvulnerabilityStartEvent(int entity_id, int pos_x, int pos_y, int pos_z, int rot_x, int rot_y, int rot_z) // Position where started invulnerability
    {
        string event_data = GetTimestamp();
    }

    public void AddNewInvulnerabilityEndEvent(int entity_id, int pos_x, int pos_y, int pos_z, int rot_x, int rot_y, int rot_z) // Position where ended invulnerability
    {
        string event_data = GetTimestamp();
    }

    public string GetTimestamp()
    {
        System.DateTime timestamp = System.DateTime.Now;
        return timestamp.ToString();
    }
}
