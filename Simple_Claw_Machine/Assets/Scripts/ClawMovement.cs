using UnityEngine;

public class ClawMovement : MonoBehaviour
{
    public GameObject LeftRight;
    public GameObject BackFront;

    private const float SPEED = 2f;

    private const float LEFT_LIMIT = -1.6f;
    private const float RIGHT_LIMIT = 0.6f;
    private const float BACK_LIMIT = -1.6f;
    private const float FRONT_LIMIT = -4.1f;

    private void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Vector3 newPos = new Vector3(SPEED * Time.deltaTime, 0, 0);

            if (LeftRight.transform.position.x + newPos.x < RIGHT_LIMIT)
            {
                LeftRight.transform.position += newPos;
            }

        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Vector3 newPos = new Vector3(-(SPEED * Time.deltaTime), 0, 0);

            if (LeftRight.transform.position.x + newPos.x > LEFT_LIMIT)
            {
                LeftRight.transform.position += newPos;
            }
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            Vector3 newPos = new Vector3(0, 0, SPEED * Time.deltaTime);

            if(BackFront.transform.position.z + newPos.z < BACK_LIMIT)
            {

                BackFront.transform.position += newPos;
            }

        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            Vector3 newPos = new Vector3(0, 0, -(SPEED * Time.deltaTime));

            Debug.Log(BackFront.transform.position.z);

            if (BackFront.transform.position.z + newPos.z > FRONT_LIMIT)
            {
                BackFront.transform.position += newPos;
            }

        }


    }
}
