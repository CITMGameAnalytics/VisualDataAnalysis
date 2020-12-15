using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private uint[,] grid;

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

        grid = new uint[width,height];

        populateGrid();

        //Print the HeatMap
        displayMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //This function sorts the list and eliminates duplicate cubes (only 1 cube per "grid" position)
    private void populateGrid()
    {
        //positions.Sort();

        foreach (Vector3 vec in positions)
        {
            grid[(int)vec.x - map_start_X, (int)vec.z - map_start_y]++;
        }
    }

    //This function will sapwn all the cubes and place them so they can be rendered
    private void displayMap()
    {
        //foreach(Vector3 vec in positions)
        //{
        //    Instantiate(cube_prefab,vec,Quaternion.identity);
        //}

        for (int i = 0; i < width; ++i)
        {
            for(int j = 0; j < height; ++j)
            {
                if (grid[i, j] > 0)
                {
                    Instantiate(cube_prefab, new Vector3(i + map_start_X, 0.0f, j + map_start_y), Quaternion.identity);
                }
            }
        }
    }
}
