using System.Collections;
using UnityEngine;

public class ClawMovement : MonoBehaviour
{
    enum ClawState
    {
        None = 0,
        Grab = 1,
        Off = 2,
    }

    private static ClawState CLAW_STATE;

    public GameObject LeftRight;
    public GameObject BackFront;

    private const float SPEED = 0.8f;
    private Vector3 DROPBOX_POS;

    Coroutine clawMoves;
    [SerializeField] Audio audioManager;

    [Header("MOTOR")]
    private const float LEFT_LIMIT = -1.6f;
    private const float RIGHT_LIMIT = 0.6f;
    private const float BACK_LIMIT = -1.6f;
    private const float FRONT_LIMIT = -4.1f;
    private const float MOTOR_DURATION = 3f;
    private const float MOTOR_SPEED = 0.7f;

    [Header("PIPE")]
    public GameObject pipe1;
    public GameObject pipe2;
    private const float PIPE_SPEED = 0.7f;
    private const float PIPE_DURATION = 2.5f;
    private const float PIPE_POS_Y = -2f;

    [Header("HANDS")]
    public GameObject rightHand;
    public GameObject leftHand;
    public GameObject backHand;
    public GameObject frontHand;
    private const float CLOSE_HAND_SPEED = 0.3f;
    private const float OPEN_HAND_SPEED = 1.5f;
    private const float CLOSE_HAND_TIME = 5f;
    private const float OPEN_HAND_TIME = 3f;
    private Quaternion right_initialRotation;
    private Quaternion left_initialRotation;
    private Quaternion back_initialRotation;
    private Quaternion front_initialRotation;
    private const float angle_z = 70f;

    [Header("Colliders")]
    [SerializeField] private Collider[] colliders;

    private void Start()
    {
        DROPBOX_POS = new Vector3(LeftRight.transform.position.x, 0, BackFront.transform.position.z);
        right_initialRotation = rightHand.transform.localRotation;
        left_initialRotation = leftHand.transform.localRotation;
        front_initialRotation = frontHand.transform.localRotation;
        back_initialRotation = backHand.transform.localRotation;
    }

    public static void GameBeginState()
    {
        CLAW_STATE = ClawState.None;
    }

    public void ResetClawPosition()
    {
        rightHand.transform.localRotation = right_initialRotation;
        leftHand.transform.localRotation = left_initialRotation;
        frontHand.transform.localRotation = front_initialRotation;
        backHand.transform.localRotation = back_initialRotation;
        LeftRight.transform.position = new Vector3(DROPBOX_POS.x, LeftRight.transform.position.y, LeftRight.transform.position.z);
        BackFront.transform.position = new Vector3(BackFront.transform.position.x, BackFront.transform.position.y, DROPBOX_POS.z);
        pipe1.transform.localPosition = Vector3.zero;
        pipe2.transform.localPosition = Vector3.zero;
        EnableColliders();
    }

    private void Update()
    {
        if (GameManager.gameIsOver)
        {
            CLAW_STATE = ClawState.Off;
            if (clawMoves != null)
            {
                StopCoroutine(clawMoves);

            }
        }

        if (CLAW_STATE == ClawState.None)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                Vector3 newPos = new Vector3(SPEED * Time.deltaTime, 0, 0);

                if (LeftRight.transform.position.x + newPos.x < RIGHT_LIMIT)
                {
                    LeftRight.transform.position += newPos;
                }

                audioManager.ClawMove1();

            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                Vector3 newPos = new Vector3(-(SPEED * Time.deltaTime), 0, 0);

                if (LeftRight.transform.position.x + newPos.x > LEFT_LIMIT)
                {
                    LeftRight.transform.position += newPos;
                }

                audioManager.ClawMove1();
            }


            if (Input.GetKey(KeyCode.UpArrow))
            {
                Vector3 newPos = new Vector3(0, 0, SPEED * Time.deltaTime);

                if (BackFront.transform.position.z + newPos.z < BACK_LIMIT)
                {

                    BackFront.transform.position += newPos;
                }

                audioManager.ClawMove1();

            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                Vector3 newPos = new Vector3(0, 0, -(SPEED * Time.deltaTime));

                if (BackFront.transform.position.z + newPos.z > FRONT_LIMIT)
                {
                    BackFront.transform.position += newPos;
                }

                audioManager.ClawMove1();

            }

            if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
            {
                audioManager.StopClawMove1();
            }
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(CLAW_STATE == ClawState.None)
            {
                CLAW_STATE = ClawState.Grab;
                audioManager.ButtonsClicked();
                clawMoves = StartCoroutine(ClawGrabState());
            }

        }

        if (CLAW_STATE == ClawState.Grab)
        {
            audioManager.ClawMove2();
        }

    }

    private IEnumerator ClawGrabState()
    {
        EnableColliders();
        //claw descending
        for (float t = 0; t < PIPE_DURATION; t += Time.deltaTime)
        {
            pipe1.transform.localPosition = Vector3.Lerp(pipe1.transform.localPosition, new Vector3(pipe1.transform.localPosition.x, PIPE_POS_Y, pipe1.transform.localPosition.z), PIPE_SPEED * Time.deltaTime);
            yield return null;
        }

        for (float t = 0; t < PIPE_DURATION; t += Time.deltaTime)
        {
            pipe2.transform.localPosition = Vector3.Lerp(pipe2.transform.localPosition, new Vector3(pipe2.transform.localPosition.x, PIPE_POS_Y, pipe2.transform.localPosition.z), PIPE_SPEED * Time.deltaTime);
            yield return null;
        }

        //close state
        for (float t = 0; t < CLOSE_HAND_TIME; t += Time.deltaTime)
        {
            rightHand.transform.localRotation = Quaternion.Slerp(rightHand.transform.localRotation, Quaternion.Euler(rightHand.transform.localRotation.eulerAngles.x, rightHand.transform.localRotation.eulerAngles.y, angle_z), CLOSE_HAND_SPEED * Time.deltaTime);
            leftHand.transform.localRotation = Quaternion.Slerp(leftHand.transform.localRotation, Quaternion.Euler(leftHand.transform.localRotation.eulerAngles.x, leftHand.transform.localRotation.eulerAngles.y, angle_z), CLOSE_HAND_SPEED * Time.deltaTime);
            backHand.transform.localRotation = Quaternion.Slerp(backHand.transform.localRotation, Quaternion.Euler(backHand.transform.localRotation.eulerAngles.x, backHand.transform.localRotation.eulerAngles.y, angle_z), CLOSE_HAND_SPEED * Time.deltaTime);
            frontHand.transform.localRotation = Quaternion.Slerp(frontHand.transform.localRotation, Quaternion.Euler(frontHand.transform.localRotation.eulerAngles.x, frontHand.transform.localRotation.eulerAngles.y, angle_z), CLOSE_HAND_SPEED * Time.deltaTime);


            yield return null;
        }


        //rising state
        for (float t = 0; t < PIPE_DURATION; t += Time.deltaTime)
        {
            pipe2.transform.localPosition = Vector3.Lerp(pipe2.transform.localPosition, Vector3.zero, PIPE_SPEED * Time.deltaTime);
            yield return null;
        }

        for (float t = 0; t < PIPE_DURATION; t += Time.deltaTime)
        {
            pipe1.transform.localPosition = Vector3.Lerp(pipe1.transform.localPosition, Vector3.zero, PIPE_SPEED * Time.deltaTime);
            yield return null;
        }

        //Claw move to the dropbox
        for (float t = 0; t < MOTOR_DURATION; t += Time.deltaTime)
        {
            BackFront.transform.position = Vector3.Lerp(BackFront.transform.position, new Vector3(BackFront.transform.position.x, BackFront.transform.position.y, DROPBOX_POS.z), MOTOR_SPEED * Time.deltaTime);
            yield return null;
        }

        for (float t = 0; t < MOTOR_DURATION; t += Time.deltaTime)
        {
            LeftRight.transform.position = Vector3.Lerp(LeftRight.transform.position, new Vector3(DROPBOX_POS.x, LeftRight.transform.position.y, LeftRight.transform.position.z), MOTOR_SPEED * Time.deltaTime);
            yield return null;
        }

        //Claw reopened
        for (float t = 0; t < OPEN_HAND_TIME; t += Time.deltaTime)
        {
            rightHand.transform.localRotation = Quaternion.Slerp(rightHand.transform.localRotation, right_initialRotation, OPEN_HAND_SPEED * Time.deltaTime);
            leftHand.transform.localRotation = Quaternion.Slerp(leftHand.transform.localRotation, left_initialRotation, OPEN_HAND_SPEED * Time.deltaTime);
            backHand.transform.localRotation = Quaternion.Slerp(backHand.transform.localRotation, back_initialRotation, OPEN_HAND_SPEED * Time.deltaTime);
            frontHand.transform.localRotation = Quaternion.Slerp(frontHand.transform.localRotation, front_initialRotation,OPEN_HAND_SPEED * Time.deltaTime);
            yield return null;
        }

        DisableColliders();
        audioManager.StopClawMove2();

        if (!GameManager.gameIsOver)
        {
            CLAW_STATE = ClawState.None;
        }

    }

    private void DisableColliders()
    {
        foreach (Collider col in colliders)
        {
            col.enabled = false;
        }
    }

    private void EnableColliders()
    {
        foreach (Collider col in colliders)
        {
            col.enabled = true;
        }
    }
}
