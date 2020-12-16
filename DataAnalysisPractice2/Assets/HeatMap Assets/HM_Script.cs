using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HM_Script : MonoBehaviour
{
    public Gradient gradient;
    private Material cube_mat;
    private Renderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        cube_mat = renderer.material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setColor(float value)
    {
        if (value <= 1.0f && value >= 0.0f)
        {
            //If the gameobject hasn't yet started but the function is called make sure we assign the material
            if(cube_mat == null)
            {
                renderer = GetComponent<Renderer>();
                cube_mat = renderer.material;
                cube_mat.color = gradient.Evaluate(value);
            }
                else
            cube_mat.color = gradient.Evaluate(value);
        }
        else
            Debug.Log("Value was not between 0.0f & 1.0f Value: " + value);
    }
}
