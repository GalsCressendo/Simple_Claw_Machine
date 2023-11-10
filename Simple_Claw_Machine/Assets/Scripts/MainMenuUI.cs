using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public GameObject Panel;
    public PrizeSpawn prizeSpawner;
    public Animator camera_anim;
    public GameManager gameManager;
    [SerializeField] Audio audioManager;

    [Header("GUIDE UI")]
    [SerializeField] Button backButton;
    [SerializeField] GameObject GuideContainer;

    [Header("Buttons")]
    public Button StartButton;
    public Button GuideButton;
    public Button ExitButton;

    private void Start()
    {
        GameManager.gameIsOver = true;
        gameManager.DisablePauseButton();
        StartButton.onClick.AddListener(GameStart);
        GuideButton.onClick.AddListener(ShowTutorial);
        GuideContainer.SetActive(false);
    }

    private void HideMainButtons()
    {
        StartButton.gameObject.SetActive(false);
        GuideButton.gameObject.SetActive(false);
        ExitButton.gameObject.SetActive(false);
    }

    private void ShowTutorial()
    {
        audioManager.ButtonsClicked();
        HideMainButtons();
        GuideContainer.SetActive(true);
    }

    private void GameStart()
    {
        StartCoroutine(GameBeginSequence());
    }

    private void DisableMainMenu()
    {
        if(Panel.activeInHierarchy)
        {
            Panel.SetActive(false);
        }
    }

    public void EnableMainMenu()
    {
        if (!Panel.activeInHierarchy)
        {
            Panel.SetActive(true);
        }
        audioManager.MainMenuAudio();
    }

    IEnumerator GameBeginSequence()
    {
        audioManager.PlayResetButtonClicked();
        gameManager.EnablePauseButton();
        camera_anim.SetTrigger("gameBegin");
        camera_anim.SetBool("getPrize", false);
        camera_anim.SetBool("gameStop", false);
        DisableMainMenu();

        StartCoroutine(prizeSpawner.SpawnPrizes());

        yield return new WaitForSeconds(0.1f);
        GameManager.gameIsOver = false;
        ClawMovement.GameBeginState();
        
    }
}
