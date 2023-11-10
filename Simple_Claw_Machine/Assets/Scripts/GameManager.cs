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
    [SerializeField] private PrizeSpawn prizeSpawner;

    [Header("UI")]
    public static bool gameIsOver = true;
    public WinScreenUI winScreenUI;
    public GameObject rewardPopUpCamera;
    private const float UI_DELAY = 3.5f;
    [SerializeField] private MainMenuUI mainMenuUI;
    [SerializeField] private ClawMovement claw;


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

        DestroyAllPrizes();
    }

    public void DestroyAllPrizes()
    {
        var prizes = GameObject.FindGameObjectsWithTag("Prize");
        if(prizes != null)
        {
            foreach (GameObject p in prizes)
            {
                Destroy(p);
            }
        }

    }

    private void SetWinUIButtonListeners()
    {
        winScreenUI.retryButton.onClick.AddListener(RetryButtonClicked);
        winScreenUI.mainMenuButton.onClick.AddListener(MainMenuButtonClicked);
    }

    private void RetryButtonClicked()
    {
        if (rewardPopUpCamera.activeInHierarchy)
        {
            rewardPopUpCamera.SetActive(false);
        }

        RestartGame();

    }

    public void MainMenuButtonClicked()
    {
        DestroyAllPrizes();
        gameIsOver = true;
        camera_animator.SetBool("gameStop",true);
        winScreenUI.DestroyRewardPreview();
        if (rewardPopUpCamera.activeInHierarchy)
        {
            rewardPopUpCamera.SetActive(false);
        }

        mainMenuUI.EnableMainMenu();
        claw.ResetClawPosition();
    }

    public void RestartGame()
    {
        gameIsOver = false;
        DestroyAllPrizes();
        camera_animator.SetBool("getPrize", false);
        StartCoroutine(prizeSpawner.SpawnPrizes());
        ClawMovement.GameBeginState();
        claw.ResetClawPosition();
    }

    public void ResumeGame()
    {
        gameIsOver = false;
        ClawMovement.GameBeginState();
    }

    public void PauseGame() => gameIsOver = true;
}
