using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetChute : MonoBehaviour {
    public Rigidbody[] enfants;

    private bool aChu = false;

    public void chute()
    {
        if (!aChu)
        {
            foreach (Rigidbody rb in enfants)
            {
                rb.isKinematic = false;
                Destroy(rb.gameObject.GetComponent<DetectionChute>());
            }

            aChu = true;
        }
    }
	
}
