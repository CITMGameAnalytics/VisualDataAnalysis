using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

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

    public List<GameEvent> game_events;

    //List of game objects
    private List<GameObject> heatMapObjects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        //Intializing list because C# is fun
        positions = new List<Vector3>();

        //Hardcoding some positions
        positions.Add(new Vector3(0.0f,0.0f,0.0f));
        positions.Add(new Vector3(1.0f, 0.0f, 0.0f));
        positions.Add(new Vector3(2.5f, 0.0f, 0.0f));
        positions.Add(new Vector3(2.3f, 0.0f, 0.0f));
        positions.Add(new Vector3(2.0f, 0.0f, 0.0f));
        positions.Add(new Vector3(2.4f, 0.0f, 0.0f));
        positions.Add(new Vector3(2.4f, 0.0f, 1.0f));
        positions.Add(new Vector3(2.4f, 0.0f, 2.0f));
        positions.Add(new Vector3(2.4f, 0.0f, 3.0f));

        //Redimension the array
        final_width = (uint)(width / cube_size);
        final_height = (uint)(height / cube_size);
        grid = new float[final_width,final_height];

        deserializeEvents();
        populateGrid();

        //Print the HeatMap
        displayMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Function to test and deserialize the events on the 
    private void deserializeEvents()
    {

    }

    //This function eliminates duplicate cubes (only 1 cube per "grid" position)
    private void populateGrid()
    {
        foreach (Vector3 vec in positions)
        {
            //int value_1 = (int)(vec.x / cube_size - map_start_X / cube_size);
            //int value_2 = (int)(vec.z / cube_size - map_start_y / cube_size);
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
                    GameObject go = Instantiate(cube_prefab, new Vector3(i * cube_size + map_start_X, 0.0f, j * cube_size + map_start_y), Quaternion.identity);
                    HM_Script script = go.GetComponent<HM_Script>();
                    script.setColor(grid[i, j]/highest_density);
                    heatMapObjects.Add(go);
                    go.SetActive(false);
                }
            }
        }
    }

    public void drawMap()
    {
        for (int i = 0; i < heatMapObjects.Count; i++)
            heatMapObjects[i].SetActive(true);
    }

    public void hideMap()
    {
        for (int i = 0; i < heatMapObjects.Count; i++)
            heatMapObjects[i].SetActive(false);
    }

    public void hideAllMaps()
    {
        hideMap();
    }

}
