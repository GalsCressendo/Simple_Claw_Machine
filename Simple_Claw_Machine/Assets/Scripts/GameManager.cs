using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Animator camera_animator;
    public static bool gameIsOver = false;
    public WinScreenUI winScreenUI;
    public GameObject rewardPopUpCamera;
    private const float UI_DELAY = 4f;

    public void DisplayWinCutscene(Prize prize)
    {
        Debug.Log(prize.attribute.prizeName);
        gameIsOver = true;
        camera_animator.SetBool("getPrize", true);

        StartCoroutine(DisplayWinUI(prize));
    }

    public IEnumerator DisplayWinUI(Prize prize)
    {
        winScreenUI.DisplayRewardScreen(prize.attribute.prizeName, prize.attribute.description);

        yield return new WaitForSeconds(UI_DELAY);
        if(!rewardPopUpCamera.activeInHierarchy)
        {
            rewardPopUpCamera.SetActive(true);
        }

        if (!winScreenUI.transform.gameObject.activeInHierarchy)
        {
            winScreenUI.transform.gameObject.SetActive(true);
        }
    }
}
