using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    public float rotationSpeed;

    float rotationX;
    float rotationY;

    Vector3 initPosition;

    void Start()
    {
        initPosition = GetComponent<Transform>().position;
    }

    void Update()
    {
        rotationY += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        rotationX += Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(-rotationX, rotationY, 0.0f);
    }


    // void OnTriggerEnter(Collider other)
    // {
    //     if (other.gameObject.tag == "bullet")
    //     {

    //         for (int i = 0; i < 5; i++)
    //         {
    //             float rx = Random.RandomRange(-1.5f, 1.5f);
    //             float ry = Random.RandomRange(-1.5f, 1.5f);
    //             transform.localPosition = new Vector3(initPosition.x + rx, initPosition.y + ry, initPosition.z);
    //             Invoke("basingPosition", 2.0f);
    //         }

    //     }
    // }

    void basingPosition()
    {
        transform.localPosition = initPosition;
    }
}
