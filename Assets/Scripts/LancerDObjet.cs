using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LancerDObjet : MonoBehaviour {
    public Camera cam;
    public GameObject aLancer;
    public float power = 5;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(1))
        {
            GameObject lance = GameObject.Instantiate(aLancer, cam.transform.position, cam.transform.rotation);
            Rigidbody rb = lance.GetComponent<Rigidbody>();
            Debug.Log("force lancé" + cam.transform.forward * power);
            rb.AddForce(cam.transform.forward * power, ForceMode.Impulse);

        }
    }
}
