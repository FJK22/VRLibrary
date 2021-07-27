using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDoor : MonoBehaviour
{
    [SerializeField] Transform door;
    bool isRotating = false;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            isRotating = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            isRotating = false;
        }
    }
    private void Update()
    {
        if(isRotating)
            door.Rotate(Vector3.down);
    }
}
