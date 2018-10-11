using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {
	public Camera cam;
	private Vector3 velocity, rotation, cameraRotation;
	private Rigidbody body;
	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody>();
	}
	
	public void Move(Vector3 velocity){
		this.velocity = velocity;
	}

	public void RotateBody(Vector3 rotation){
		this.rotation = rotation;
	}

	public void RoatateCam(Vector3 rotation){
		this.cameraRotation = rotation;
	}

	void FixedUpdate(){
		PerformMovement();
		PerformRotationBody();
		PerformRotationCam();
	}

	private void PerformMovement(){
		if(velocity != Vector3.zero){
			body.MovePosition(body.position + velocity * Time.fixedDeltaTime);
		}
	}

	private void PerformRotationBody(){
		if(rotation != Vector3.zero){
			body.MoveRotation(body.rotation * Quaternion.Euler(rotation));
		}
	}

	private void PerformRotationCam(){
		if(rotation != Vector3.zero){
			cam.transform.Rotate(-cameraRotation);
		}
	}

}
