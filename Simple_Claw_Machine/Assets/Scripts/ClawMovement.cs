using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawMovement : MonoBehaviour
{
    public GameObject LeftRight;
    public GameObject BackFront;

    private const float SPEED = 5f;

    private void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Vector3 newPos = new Vector3(SPEED * Time.deltaTime, 0, 0);
            LeftRight.transform.position += newPos;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Vector3 newPos = new Vector3(-(SPEED * Time.deltaTime), 0, 0);
            LeftRight.transform.position += newPos;
        }

        if(Input.GetKey(KeyCode.UpArrow))
        {
            Vector3 newPos = new Vector3(0, 0, SPEED * Time.deltaTime);
            BackFront.transform.position += newPos;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            Vector3 newPos = new Vector3(0, 0, -(SPEED * Time.deltaTime));
            BackFront.transform.position += newPos;
        }


    }
}
