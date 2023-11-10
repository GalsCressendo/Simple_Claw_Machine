using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngineInternal;

public class Pause : MonoBehaviour
{
    public GameObject pausePanel;
    [SerializeField] private Button pauseButton;
    public GameManager gameManager;

    [Header("In Game Menu Buttons")]
    [SerializeField] Button restartButton;
    [SerializeField] Button mainMenuButton;
    [SerializeField] Audio audioManager;

    private void OnEnable()
    {
        RegisterListeners();
    }

    private void RegisterListeners()
    {
        pauseButton.onClick.AddListener(PauseButtonOnClick);
        restartButton.onClick.AddListener(RestartButtonOnClick);
        mainMenuButton.onClick.AddListener(MainMenuButtonOnClick);

    }

    private void EnablePausePanel()
    {
        if(!pausePanel.activeInHierarchy)
        {
            pausePanel.SetActive(true);
        }
    }

    private void DisablePausePanel()
    {
        if (pausePanel.activeInHierarchy)
        {
            pausePanel.SetActive(false);
        }
    }

    private void PauseButtonOnClick()
    {
        audioManager.ButtonsClicked();
        gameManager.PauseGame();
        EnablePausePanel();
        gameManager.DisablePauseButton();
    }

    private void RestartButtonOnClick()
    {
        audioManager.PlayResetButtonClicked();
        gameManager.RestartGame();
        DisablePausePanel();
        gameManager.EnablePauseButton();
    }

    private void MainMenuButtonOnClick()
    {
        audioManager.ButtonsClicked();
        gameManager.MainMenuButtonClicked();
        DisablePausePanel();
    }
}
