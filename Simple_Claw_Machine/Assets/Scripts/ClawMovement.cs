using System.Collections;
using UnityEngine;

public class ClawMovement : MonoBehaviour
{
    enum ClawState
    {
        None = 0,
        Grab = 1,
    }

    private ClawState CLAW_STATE;

    public GameObject LeftRight;
    public GameObject BackFront;

    private const float SPEED = 2f;

    [Header("MOTOR")]
    private const float LEFT_LIMIT = -1.6f;
    private const float RIGHT_LIMIT = 0.6f;
    private const float BACK_LIMIT = -1.6f;
    private const float FRONT_LIMIT = -4.1f;

    [Header("PIPE")]
    public GameObject pipe1;
    public GameObject pipe2;
    private const float PIPE_FRACTION = 10f;
    private const float PIPE_DURATION = 0.5f;
    private const float PIPE_POS_Y = -2f;

    [Header("HAND")]
    public GameObject rightHand;
    public GameObject leftHand;
    private const float HAND_POS_Z = 0.09f;
    private const float HAND_DURATION = 1.5f;
    private const float HAND_FRACTION = 10f;


    private void Update()
    {
        if(CLAW_STATE == ClawState.None)
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
        }
       

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CLAW_STATE = ClawState.Grab;
            StartCoroutine(ClawGrabState());
        }

    }

    private IEnumerator ClawGrabState()
    {
        //claw descending
        for(float t =0; t< PIPE_DURATION; t+=Time.deltaTime)
        {
            pipe1.transform.localPosition = Vector3.Lerp(pipe1.transform.localPosition, new Vector3(pipe1.transform.localPosition.x, PIPE_POS_Y, pipe1.transform.localPosition.z), t/PIPE_FRACTION);
            yield return null;
        }

        for (float t = 0; t < PIPE_DURATION; t += Time.deltaTime)
        {
            pipe2.transform.localPosition = Vector3.Lerp(pipe2.transform.localPosition, new Vector3(pipe2.transform.localPosition.x, PIPE_POS_Y, pipe2.transform.localPosition.z), t / PIPE_FRACTION);
            yield return null;
        }

        //close state
        for (float t = 0; t < HAND_DURATION; t += Time.deltaTime)
        {
            rightHand.transform.localPosition = Vector3.Lerp(rightHand.transform.localPosition, new Vector3(rightHand.transform.localPosition.x, rightHand.transform.localPosition.y, -(HAND_POS_Z)), t / HAND_FRACTION);
            leftHand.transform.localPosition = Vector3.Lerp(leftHand.transform.localPosition, new Vector3(leftHand.transform.localPosition.x, leftHand.transform.localPosition.y, HAND_POS_Z), t / HAND_FRACTION);
            yield return null;
        }

    }
}
