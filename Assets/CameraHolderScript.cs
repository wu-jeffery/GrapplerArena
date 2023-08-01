using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHolderScript : MonoBehaviour
{
    // Start is called before the first frame upda
    public Transform cameraPosition;

    // Update is called once per frame
    void Update()
    {
        transform.position = cameraPosition.position;
    }
}
