using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerMove : MonoBehaviour
{
    public float running_Speed = 10f;
    public float side_speed = 5f;
    int currunt_pos = 1; // 0 = Left, 1 = Center, 2 = Right

    public Transform left_pos;
    public Transform center_pos;
    public Transform right_pos;

    void Update()
    {
        // ✅ Forward movement
        transform.Translate(Vector3.forward * running_Speed * Time.deltaTime);

        // ✅ Left & Right
        if (Input.GetKeyDown(KeyCode.LeftArrow) && currunt_pos > 0) currunt_pos--;
        else if (Input.GetKeyDown(KeyCode.RightArrow) && currunt_pos < 2) currunt_pos++;

        Vector3 targetPosition = center_pos.position;
        if (currunt_pos == 0) targetPosition = left_pos.position;
        else if (currunt_pos == 2) targetPosition = right_pos.position;

        transform.position = Vector3.MoveTowards(
            transform.position,
            new Vector3(targetPosition.x, transform.position.y, transform.position.z),
            side_speed * Time.deltaTime
        );
    }
}
