using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Animator camera_animator;
    public static bool gameIsOver = false;

    public void DisplayWinCutscene()
    {
        Debug.Log("YOU GOT THE PRIZE!");
        gameIsOver = true;
        camera_animator.SetBool("getPrize", true);
    }
}
