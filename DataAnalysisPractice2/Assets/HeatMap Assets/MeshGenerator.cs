using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{

    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;

    public int xSize = 20;
    public int zSize = 20;

    // Start is called before the first frame update
    void Start()
    {
        //Create the vertices
        //newVertices = new Vector3[100*100];
        //for (int i = 0; i < 100; ++i)
        //{
        //    for (int j = 0; j < 100; ++j)
        //    {
        //        newVertices[i] = new Vector3(i,0.0f,j);
        //    }
        //}

        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateShape();
        UpdateMesh();

        transform.position = new Vector3(0.0f,0.5f,0.0f);
    }


    void CreateShape()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= zSize; x++)
            {

                // Check the position of the ground with a raycast
                // This would cast rays only against colliders in layer 22.
                int layerMask = 1 << 22;
                layerMask = ~layerMask;
                float y_position = 0.0f;

                RaycastHit hit;
                Vector3 point_pos = new Vector3(x + transform.position.x, 30.0f, z + transform.position.z);
                // Does the ray intersect any objects in layer 22
                if (Physics.Raycast(point_pos, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layerMask))
                {
                    y_position = hit.transform.position.y;
                }
                else
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 1000, Color.white);
                    Debug.Log("Did not Hit");
                }
                vertices[i] = new Vector3(x + transform.position.x, y_position, z + transform.position.z);                //X and Z should be the position where the point is located in the real world
                i++;
            }
        }

        triangles = new int[xSize * zSize * 6];
        int vert = 0, tris = 0;

        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
