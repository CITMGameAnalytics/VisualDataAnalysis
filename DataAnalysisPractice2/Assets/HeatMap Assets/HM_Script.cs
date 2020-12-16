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
        Debug.Log(gradient.Evaluate(0.25f));
        renderer = GetComponent<Renderer>();
        cube_mat = renderer.material;
        cube_mat.color = gradient.Evaluate(0.25f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
