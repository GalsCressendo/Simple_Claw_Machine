using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public Button StartButton;
    public GameObject Panel;
    public PrizeSpawn prizeSpawner;
    public Animator camera_anim;


    private void Start()
    {
        GameManager.gameIsOver = true;
        StartButton.onClick.AddListener(GameStart);
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
    }

    IEnumerator GameBeginSequence()
    {
        camera_anim.SetTrigger("gameBegin");
        camera_anim.SetBool("getPrize", false);
        DisableMainMenu();

        StartCoroutine(prizeSpawner.SpawnPrizes());

        yield return new WaitForSeconds(0.1f);
        GameManager.gameIsOver = false;
        ClawMovement.GameBeginState();
        
    }
}
