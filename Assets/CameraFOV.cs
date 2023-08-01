using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFOV : MonoBehaviour
{

    [SerializeField] public float mouseSensitivity;
    public Transform playerBody;
    public Transform orientation;

    float xRotation = 0f;
    float yRotation = 0f;

    // Start is called before the first frame update
    void Start() 
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        yRotation += mouseX;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f); //update rotation up and down
        orientation.rotation = Quaternion.Euler(0,yRotation,0f ); //update rotate camera around y axis
        playerBody.Rotate(new Vector3(0, 1, 0) * mouseX); //rotation around  y-axis 
    }
}
