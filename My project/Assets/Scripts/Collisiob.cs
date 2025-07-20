using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisiob : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("player collides with " + collision.gameObject.name);
    }
    void OnCollisionExit(Collision collision)
    {
        Debug.Log("player colliding ends with " + collision.gameObject.name);
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player entered the trigger zone!");

    }
    void OnTriggerStay(Collider other)
    {

        Debug.Log("Player is still in the trigger zone!");
    }
    void OnTriggerExit(Collider other)
    {

        Debug.Log("Player left the trigger zone!");
    }
}
