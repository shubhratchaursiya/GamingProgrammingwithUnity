using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float rotationSpeed = 100f;

    void Update()
    {
        // ✅ Rotate continuously on Y-axis
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f, Space.World);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player_controller player = other.GetComponent<Player_controller>();
            if (player != null)
            {
                player.AddCoin();
            }

            Destroy(gameObject); // ✅ Remove coin after collection
        }
    }
}
