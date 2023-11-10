using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Animator camera_animator;
    public static bool gameIsOver = true;
    public WinScreenUI winScreenUI;
    public GameObject rewardPopUpCamera;
    private const float UI_DELAY = 3.5f;
    [SerializeField] private PrizeSpawn prizeSpawner;

    private void Start()
    {
        SetWinUIButtonListeners();
    }

    public void DisplayWinCutscene(Prize prize)
    {
        Debug.Log(prize.attribute.prizeName);
        gameIsOver = true;
        camera_animator.SetBool("getPrize", true);

        StartCoroutine(DisplayWinUI(prize));
    }

    public IEnumerator DisplayWinUI(Prize prize)
    {
        winScreenUI.DisplayRewardScreen(prize.attribute);

        yield return new WaitForSeconds(UI_DELAY);
        if(!rewardPopUpCamera.activeInHierarchy)
        {
            rewardPopUpCamera.SetActive(true);
        }

        var prizes = GameObject.FindGameObjectsWithTag("Prize");
        foreach(GameObject p in prizes)
        {
            Destroy(p);
        }
    }

    private void SetWinUIButtonListeners()
    {
        winScreenUI.retryButton.onClick.AddListener(RetryButtonClicked);
    }

    private void RetryButtonClicked()
    {
        if (rewardPopUpCamera.activeInHierarchy)
        {
            rewardPopUpCamera.SetActive(false);
        }

        winScreenUI.DestroyRewardPreview();

        RestartGame();

    }

    public void RestartGame()
    {
        gameIsOver = false;
        camera_animator.SetBool("getPrize", false);
        StartCoroutine(prizeSpawner.SpawnPrizes());
        ClawMovement.GameBeginState();
    }
}
