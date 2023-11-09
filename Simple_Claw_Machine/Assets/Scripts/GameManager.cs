using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Animator camera_animator;
    public static bool gameIsOver = false;

    public void DisplayWinCutscene(Prize prize)
    {
        Debug.Log(prize.attribute.name);
        gameIsOver = true;
        camera_animator.SetBool("getPrize", true);
    }
}
