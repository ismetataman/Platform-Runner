using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paint : MonoBehaviour
{
    public Material material;
    public Renderer rend;
    
    private void Start() 
    {
        rend.GetComponent<Renderer>();
        rend.enabled = true;
    }
    void OnMouseOver() 
    {
        if(Input.GetMouseButton(0))
        {
            rend.sharedMaterial = material;
        }
    }
}
