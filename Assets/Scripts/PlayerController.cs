using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    public float speed = 3.0f;

    void Start(){
        Cursor.visible = false;

    }

    void Update()
    {
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * speed;

        //transform.Rotate(0, x, 0);
        transform.Translate(x, 0, z);
    }
}