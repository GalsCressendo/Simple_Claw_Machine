using System.Collections;
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

    [Header("PIPE")]
    public GameObject pipe1;
    public GameObject pipe2;
    private const float PIPE_FRACTION = 10f;
    private const float PIPE_DURATION = 0.5f;
    private const float PIPE_POSY = -2f;


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

            if (BackFront.transform.position.z + newPos.z < BACK_LIMIT)
            {

                BackFront.transform.position += newPos;
            }

        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            Vector3 newPos = new Vector3(0, 0, -(SPEED * Time.deltaTime));

            if (BackFront.transform.position.z + newPos.z > FRONT_LIMIT)
            {
                BackFront.transform.position += newPos;
            }

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(ClawDownState());
        }

    }

    private IEnumerator ClawDownState()
    {
        for(float t =0; t< PIPE_DURATION; t+=Time.deltaTime)
        {
            pipe1.transform.localPosition = Vector3.Lerp(pipe1.transform.localPosition, new Vector3(pipe1.transform.localPosition.x, PIPE_POSY, pipe1.transform.localPosition.z), t/PIPE_FRACTION);
            yield return null;
        }

        for (float t = 0; t < PIPE_DURATION; t += Time.deltaTime)
        {
            pipe2.transform.localPosition = Vector3.Lerp(pipe2.transform.localPosition, new Vector3(pipe2.transform.localPosition.x, PIPE_POSY, pipe2.transform.localPosition.z), t / PIPE_FRACTION);
            yield return null;
        }


    }
}
