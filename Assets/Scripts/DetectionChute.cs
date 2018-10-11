using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionChute : MonoBehaviour {
    public ObjetChute objetChute;
    public float minimalForce = 5;

    void OnCollisionEnter(Collision col)
    {
        //Debug.Log(col.relativeVelocity.magnitude);
        if (col.relativeVelocity.magnitude > minimalForce)
        {
            this.GetComponent<Rigidbody>().isKinematic = false;
            chuteDetecte();

            Vector3 impactForce = col.relativeVelocity;
            Debug.Log(impactForce);
            this.gameObject.GetComponent<Rigidbody>().AddForceAtPosition(impactForce, col.transform.position, ForceMode.VelocityChange);

            Destroy(this);
        }
    }

    public void chuteDetecte()
    {
        Debug.Log("ça tombe !");
        objetChute.chute();
    }
}
