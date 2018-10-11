using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecompositionToucheSol : MonoBehaviour
{
    public GameObject CubeDecompose;


    // Use this for initialization
    void Start()
    {

    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag.Equals("Sol"))
        {
            Instantiate(CubeDecompose, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
        }
        
    }
}