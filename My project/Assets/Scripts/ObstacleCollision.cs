using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Player_controller player = collision.gameObject.GetComponent<Player_controller>();
            if (player != null)
            {
                player.GameOver();
            }
        }
    }
}
