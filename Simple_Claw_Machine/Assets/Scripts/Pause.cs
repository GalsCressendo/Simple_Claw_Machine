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
    [SerializeField] Button resumeButton;
    [SerializeField] Button restartButton;
    [SerializeField] Button mainMenuButton;

    private void OnEnable()
    {
        RegisterListeners();
    }

    private void RegisterListeners()
    {
        pauseButton.onClick.AddListener(EnablePausePanel);
        resumeButton.onClick.AddListener(ResumeButtonOnClick);
        restartButton.onClick.AddListener(RestartButtonOnClick);
        mainMenuButton.onClick.AddListener(MainMenuButtonOnClick);

    }

    private void EnablePausePanel()
    {
        if(!pausePanel.activeInHierarchy)
        {
            pausePanel.SetActive(true);
        }

        pauseButton.transform.parent.gameObject.SetActive(false);
        gameManager.PauseGame();
    }

    private void DisablePausePanel()
    {
        if (pausePanel.activeInHierarchy)
        {
            pausePanel.SetActive(false);
        }

        pauseButton.transform.parent.gameObject.SetActive(true);
    }

    private void ResumeButtonOnClick()
    {
        gameManager.ResumeGame();
        DisablePausePanel();
    }

    private void RestartButtonOnClick()
    {
        gameManager.RestartGame();
        DisablePausePanel();
    }

    private void MainMenuButtonOnClick()
    {
        gameManager.MainMenuButtonClicked();
        DisablePausePanel();
    }
}
