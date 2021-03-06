﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;
using DataVisualizer;

public enum CubeLists
{
    WALK_EVENT = 0,
    AIR_EVENT,
    KEY_EVENT,
    JUMP_EVENT,
    ATTACK_EVENT,
    HIT_EVENT,
    DEATH_EVENT,
    SPAWN_EVENT,
    INV_START_EVENT,
    INV_END_EVENT,
    MAX_EVENTS
}

public class HM_Manager : MonoBehaviour
{

    [Header ("HM Cube Prefab")]
    public GameObject cube_prefab;
    private List<Vector3> positions;

    [Header ("Grid Dimensions")]
    public uint width = 130; //X axis
    public uint height = 90; //Z axis
    public int map_start_X = -35;
    public int map_start_y = -45;
    public float cube_size = 1.0f;

    private float[,] grid;   //This grid will control where each position fits.
    private uint highest_density = 0;
    private uint final_width = 0;
    private uint final_height = 0;

    [Header("Deserialization")]
    public EventHandler ev_handler;

    public List<GameEvent> game_events;

    private Dictionary<int, List<GameObject>> cubes_dictionary= new Dictionary<int, List<GameObject>>();

    //List of game objects
    private List<GameObject> heatMapObjects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        //Intializing list because C# is fun
        positions = new List<Vector3>();

        //Hardcoding some positions
        //positions.Add(new Vector3(0.0f,0.0f,0.0f));
        //positions.Add(new Vector3(1.0f, 0.0f, 0.0f));
        //positions.Add(new Vector3(2.5f, 0.0f, 0.0f));
        //positions.Add(new Vector3(2.3f, 0.0f, 0.0f));
        //positions.Add(new Vector3(2.0f, 0.0f, 0.0f));
        //positions.Add(new Vector3(2.4f, 0.0f, 0.0f));
        //positions.Add(new Vector3(2.4f, 0.0f, 1.0f));
        //positions.Add(new Vector3(2.4f, 0.0f, 2.0f));
        //positions.Add(new Vector3(2.4f, 0.0f, 3.0f));

        //Redimension the array
        final_width = (uint)(width / cube_size);
        final_height = (uint)(height / cube_size);
        grid = new float[final_width,final_height];

        if(ev_handler != null && ev_handler.walkEventContainer != null && ev_handler.walkEventContainer.eventList != null)
        loadEvents(ev_handler.attackEventContainer.eventList);    //Testing with walk events list

        //populateGrid();
        ////Print the HeatMap
        //displayMap();

        //Load all events so they can be displayed
        loadAllEvents();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Function to test and deserialize the events on the 
    private void deserializeEvents()
    {
            //EventContainer<GameEvent> dummyContainer = new EventContainer<GameEvent>();
            //string dummyString = "";
            //DataSerializer.Read(ref dummyString, directory + "Attacks.csv");
            //List<GameEvent> dummyList = dummyContainer.DeserializeList(dummyString);


    }

    //Passes all the positions of the events to the positions list
    private void loadEvents(List<GameEvent> events)
    {
        positions.Clear();
        foreach(GameEvent ge in events)
        {
            positions.Add(ge.position);
        }
    }

    //This function eliminates duplicate cubes (only 1 cube per "grid" position)
    private void populateGrid()
    {
        grid = new float[final_width, final_height];
        highest_density = 0;
        foreach (Vector3 vec in positions)
        {
            int value_1 = (int)(vec.x / cube_size - map_start_X / cube_size);
            int value_2 = (int)(vec.z / cube_size - map_start_y / cube_size);
            grid[(int)(vec.x/cube_size - map_start_X/cube_size), (int)(vec.z/cube_size - map_start_y/cube_size)]++;
        }

        //Now let's check the highest density value!
        for (int i = 0; i < final_width; ++i)
        {
            for (int j = 0; j < final_height; ++j)
            {
                float test = grid[i, j];
                if (grid[i, j] > highest_density)
                    highest_density = (uint)grid[i, j];
            } 
        }
    }

    //This function will spawn all the cubes and place them so they can be rendered
    private void displayMap()
    {
        for (int i = 0; i < final_width; ++i)
        {
            for(int j = 0; j < final_height; ++j)
            {
                if (grid[i, j] > 0)
                {
                    GameObject go = Instantiate(cube_prefab, new Vector3(i * cube_size + map_start_X, 30.0f, j * cube_size + map_start_y), Quaternion.identity);
                    
                    // Check the position of the ground with a raycast
                // This would cast rays only against colliders in layer 22.
                int layerMask = 1 << 16;
                //layerMask = ~layerMask;
                float y_position = 0.0f;

                RaycastHit hit;
                Vector3 point_pos = go.transform.position;
                // Does the ray intersect any objects in layer 22
                if (Physics.Raycast(go.transform.position, go.transform.TransformDirection(Vector3.down), out hit, 100.0f,layerMask))
                {
                    y_position = go.transform.position.y - hit.distance;
                }
                else
                {
                    Debug.Log("Did not Hit");
                }
                    go.transform.position =new Vector3(go.transform.position.x, y_position + cube_size/2, go.transform.position.z);
                    HM_Script script = go.GetComponent<HM_Script>();
                    script.setColor(grid[i, j]/highest_density);
                    heatMapObjects.Add(go);
                    go.SetActive(false);
                }
            }
        }
    }

    //Sets all cubes from a dictionary list as active
    public void drawMap(int cube_list)
    {
        hideAllMaps();
        if (cubes_dictionary.ContainsKey((cube_list)))
        {
            List<GameObject> gos = cubes_dictionary[cube_list];

            for (int i = 0; i < gos.Count; i++)
                gos[i].SetActive(true);
        }
    }

    public void drawMap(CubeLists cube_list)
    {
        hideAllMaps();
        if (cubes_dictionary.ContainsKey((int)cube_list))
        {
            List<GameObject> gos = cubes_dictionary[(int)cube_list];

            for (int i = 0; i < gos.Count; i++)
                gos[i].SetActive(true);
        }
    }

    public void hideMap(CubeLists cube_list)
    {
        if (cubes_dictionary.ContainsKey((int)cube_list))
        {
            List<GameObject> gos = cubes_dictionary[(int)cube_list];

            for (int i = 0; i < gos.Count; i++)
                gos[i].SetActive(false);
        }
    }

    public void hideAllMaps()
    {
        for (int i = 0; i < (int)CubeLists.MAX_EVENTS; ++i)
        {
            hideMap((CubeLists)i);
        }
    }

    //This function loads ALL lists cubes into a Dictionary 
    private void loadAllEvents()
    {
        for (int i = 0; i < (int)CubeLists.MAX_EVENTS; ++i)
        {
            //First load the events info
            switch (i)
                {
                case (int)CubeLists.WALK_EVENT:
                    loadEvents(ev_handler.walkEventContainer.eventList);
                    break;
                case (int)CubeLists.AIR_EVENT:
                    loadEvents(ev_handler.airEventContainer.eventList);
                    break;
                case (int)CubeLists.KEY_EVENT:
                    loadEvents(ev_handler.keyEventContainer.eventList);
                    break;
                case (int)CubeLists.JUMP_EVENT:
                    loadEvents(ev_handler.jumpEventContainer.eventList);
                    break;
                case (int)CubeLists.ATTACK_EVENT:
                    loadEvents(ev_handler.attackEventContainer.eventList);
                    break;
                case (int)CubeLists.HIT_EVENT:
                    //loadEvents(ev_handler.hitEventContainer.eventList);
                    break;
                case (int)CubeLists.DEATH_EVENT:
                    loadEvents(ev_handler.deathEventContainer.eventList);
                    break;
                case (int)CubeLists.SPAWN_EVENT:
                    loadEvents(ev_handler.spawnEventContainer.eventList);
                    break;
                case (int)CubeLists.INV_START_EVENT:
                    loadEvents(ev_handler.invStartEventContainer.eventList);
                    break;
                case (int)CubeLists.INV_END_EVENT:
                    loadEvents(ev_handler.invEndEventContainer.eventList);
                    break;
            }

            //Now populate the grid & the map 
            populateGrid();
            displayMap();

            List<GameObject> gos = new List<GameObject>();
            //Copy all elements from HeatMap gos to our own list of gos in the dictionary
            foreach (GameObject go in heatMapObjects)
            {
                gos.Add(go);//Copy each gameobject
            }
            heatMapObjects.Clear(); //Clear placeholder
            cubes_dictionary.Add(i, gos);   //Add the list to our dictionary
        }
        int test = cubes_dictionary.Count;

    }
}
