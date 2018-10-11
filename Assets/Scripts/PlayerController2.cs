using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController2 : MonoBehaviour
{
    public float speed = 3.0f;
    public float sensitivityX = 3f;
    public float sensitivityY = 3f;

    private PlayerMotor motor;

    private float tmpXRot, tmpYRot;

    void Start(){
        Cursor.visible = false;
        motor = GetComponent<PlayerMotor>();
        tmpXRot = tmpYRot = 0;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //deplacement du personnage
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        //necessaire pour prendre en compte la rotation actuelle du personnage.
        Vector3 horizontalMove = transform.right * x;
        Vector3 verticalMove = transform.forward * z;

        Vector3 velocity = (horizontalMove + verticalMove).normalized * speed;

        motor.Move(velocity);


        //rotation horizontale du personnage
        float yRot = Input.GetAxisRaw("Mouse X");

        Vector3 rotationY = new Vector3(0, yRot, 0) * sensitivityX;
        motor.RotateBody(rotationY);


        //rotation vertical du personnage
        float xRot = Input.GetAxisRaw("Mouse Y") + tmpXRot;

        /* if(xRot * sensitivityX < 1 && xRot * sensitivityX > -1){
            tmpXRot = xRot;
            xRot = 0;
        } else {
            //a refaire pour garder la virgule meme en negatif
            //tmpXRot = xRot - (int)(xRot);
        }*/

        Vector3 rotationX = new Vector3(xRot, 0, 0) * sensitivityY;
        if(xRot != 0)
            Debug.Log("rotation : " + rotationX + " input : " + xRot);
        motor.RoatateCam(rotationX);

        if (Input.GetKeyDown("escape")) {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}