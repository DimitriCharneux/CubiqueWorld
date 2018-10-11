using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveObject : MonoBehaviour {
	public Camera cam;

	[Header("UI")]

	public Text nameObject;
	public GameObject powerBar;
	public RectTransform powerBarRect;
	private float powerBarRectInitialHeight;
	
	[Space]
	
	[Header("Object Movement")]
	public float distanceToSelectAnObject = 3;
	[SerializeField]
	private float distanceFromSelectedObject;
	public float scroolWheelsStep = 0.5f;
	public float minimalDistance = 1f, maximalDistance = 5f;
	private GameObject selectedObject;
	private bool objectIsSelected = false;
	[Space]

	[Header("Object Launch")]

	public float power = 5, maxPower = 50, powerStep = 2;
	public GameObject aLancer;
	[Space]
	public GameObject[] desactivOnSelected;

	

	void Start(){
		powerBarRectInitialHeight = powerBarRect.rect.height;
	}

	// Update is called once per frame
	void Update () {

		interactButtonManagement();

		powerInputManagement();

		if(objectIsSelected){
			setActiveObject(false);

			if(!Input.GetButton("PowerGestion"))
				objectDistanceManagement();

			Debug.Log("distance : " + distanceFromSelectedObject);
			moveObject();

			if(Input.GetButtonDown("Fire1")){
				throwObject();
			}
			if(Input.GetButtonDown("Fire2")){
				dropObject();
			}

		} else {
			setActiveObject(true);

			if (Input.GetMouseButtonDown(0)){
            	throwPrefab();
       		}

			showObjectName();
		}
		
	}

	private void showObjectName(){
		RaycastHit hit;

		Ray ray = cam.ScreenPointToRay(new Vector3(cam.pixelWidth/2,cam.pixelHeight/2,0));

		if (Physics.Raycast(ray, out hit, 100f)){
			nameObject.text = hit.collider.gameObject.gameObject.name;
        }
	}

	
	private void selectObject(){
		RaycastHit hit;
		
		Ray ray = cam.ScreenPointToRay(new Vector3(cam.pixelWidth/2,cam.pixelHeight/2,0));

		if (Physics.Raycast(ray, out hit, distanceToSelectAnObject)){
			if(hit.collider.gameObject.tag == "Sol") return;
			if(hit.collider.gameObject.GetComponent<Rigidbody>() == null) return;
			if(hit.collider.gameObject.GetComponent<Rigidbody>().isKinematic) return;

			selectedObject = hit.collider.gameObject;
			selectedObject.GetComponent<Rigidbody>().isKinematic = true;
			distanceFromSelectedObject = hit.distance;
			nameObject.text = selectedObject.name + " selectionné (" + distanceFromSelectedObject + ")";
			objectIsSelected = true;
        }
	}

	private void setActiveObject(bool active){
		foreach(GameObject obj in desactivOnSelected){
			obj.SetActive(active);
		}
	}

	private void dropObject(){
		Debug.Log("on lache " + selectedObject.name);
		selectedObject.GetComponent<Rigidbody>().isKinematic = false;
		nameObject.text = "";
		objectIsSelected = false;
		selectedObject = null;
		distanceFromSelectedObject = 0;
	}

	private void throwObject(){
		Debug.Log("on lance " + selectedObject.name);
		Rigidbody rb = selectedObject.GetComponent<Rigidbody>();
		if(rb == null)
			return;
			
		dropObject();
		Debug.Log("force lancé " + cam.transform.forward * power);
		rb.AddForce(cam.transform.forward * power, ForceMode.Impulse);

	}

	private void moveObject(){
		Ray ray = cam.ScreenPointToRay(new Vector3(cam.pixelWidth/2,cam.pixelHeight/2,0));

		Vector3 newPos = ray.GetPoint(distanceFromSelectedObject);
		//Debug.Log(newPos - selectedObject.transform.position);

		selectedObject.transform.position = newPos;
		//selectedObject.transform.Translate(newPos - selectedObject.transform.position);

		selectedObject.transform.LookAt(this.transform);
	}

	private void interactButtonManagement(){
		if(Input.GetButtonDown("Interact")){
			if(objectIsSelected){
				dropObject();
			} else {
				selectObject();
			}
		}
	}

	private void objectDistanceManagement(){
		if(Input.GetAxis("Mouse ScrollWheel") > 0f){
			distanceFromSelectedObject += scroolWheelsStep;

			if(distanceFromSelectedObject > maximalDistance){
				distanceFromSelectedObject = maximalDistance;
			}

		} else if(Input.GetAxis("Mouse ScrollWheel") < 0f){
			distanceFromSelectedObject -= scroolWheelsStep;

			if(distanceFromSelectedObject < minimalDistance){
				distanceFromSelectedObject = minimalDistance;
			}
		}
	}


	private void powerInputManagement(){
		if(Input.GetButton("PowerGestion")){
			powerBar.SetActive(true);
			powerManagement();
		} else {
			powerBar.SetActive(false);
		}
	}
	private void powerManagement(){
		if(Input.GetAxis("Mouse ScrollWheel") > 0f){
			power += powerStep;

			if(power > maxPower){
				power = maxPower;
			}

		} else if(Input.GetAxis("Mouse ScrollWheel") < 0f){
			power -= powerStep;

			if(power < 0){
				power = 0;
			}
		}

		//gestion taille barre de puissance
		float newHeight = (power/maxPower) * powerBarRectInitialHeight;
		Debug.Log("new power bar height : " + newHeight);
		powerBarRect.sizeDelta = new Vector2(powerBarRect.sizeDelta.x, newHeight);
	}

	private void throwPrefab(){
		GameObject lance = GameObject.Instantiate(aLancer, cam.transform.position, cam.transform.rotation);
        Rigidbody rb = lance.GetComponent<Rigidbody>();
        Debug.Log("force lancé" + cam.transform.forward * power);
        rb.AddForce(cam.transform.forward * power, ForceMode.Impulse);
	}
}
