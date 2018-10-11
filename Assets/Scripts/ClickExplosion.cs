using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickExplosion : MonoBehaviour {
    public Camera cam;
    public float powerExplosion = 5f;
    public float radiusExplosion = 5f;

    public GameObject prefabProjectile;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {

            RaycastHit hit;
            //Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            Ray ray = cam.ScreenPointToRay(new Vector3(cam.pixelWidth/2,cam.pixelHeight/2,0));

            GameObject.Instantiate(prefabProjectile, cam.transform.position, Quaternion.LookRotation(ray.direction));

            if (Physics.Raycast(ray, out hit, 100f))
            {
                Collider[] colliders = Physics.OverlapSphere(hit.point, radiusExplosion);

                foreach (Collider col in colliders)
                {
                    Rigidbody rb = col.GetComponent<Rigidbody>();

                    DetectionChute chute = col.GetComponent<DetectionChute>();

                    if (chute != null)
                        chute.chuteDetecte();

                    if(rb != null)
                        rb.AddExplosionForce(powerExplosion, hit.point, radiusExplosion);
                }

            }

        }

    }
}
