using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Animator camera_animator;
    [SerializeField] private PrizeSpawn prizeSpawner;
    [SerializeField] private Audio audioManager;

    [Header("UI")]
    public static bool gameIsOver = true;
    public WinScreenUI winScreenUI;
    public GameObject rewardPopUpCamera;
    private const float UI_DELAY = 3.5f;
    [SerializeField] private MainMenuUI mainMenuUI;
    [SerializeField] private ClawMovement claw;
    [SerializeField] private Button pauseButton;

    private void Start()
    {
        SetWinUIButtonListeners();
    }

    public void DisplayWinCutscene(Prize prize)
    {
        Debug.Log(prize.attribute.prizeName);
        gameIsOver = true;
        camera_animator.SetBool("getPrize", true);
        DisablePauseButton();

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

        audioManager.Win();
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
        winScreenUI.retryButton.onClick.AddListener(RetryButtonOnRewardUIClicked);
        winScreenUI.mainMenuButton.onClick.AddListener(MainMenuButtonClicked);
    }

    private void RetryButtonOnRewardUIClicked()
    {
        if (rewardPopUpCamera.activeInHierarchy)
        {
            rewardPopUpCamera.SetActive(false);
        }

        EnablePauseButton();
        RestartGame();
        audioManager.PlayResetButtonClicked();

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

        DisablePauseButton();
        mainMenuUI.EnableMainMenu();
        claw.ResetClawPosition();
        audioManager.ButtonsClicked();
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

    public void PauseGame() => gameIsOver = true;

    public void DisablePauseButton() => pauseButton.transform.parent.gameObject.SetActive(false);
    public void EnablePauseButton() => pauseButton.transform.parent.gameObject.SetActive(true);
}
