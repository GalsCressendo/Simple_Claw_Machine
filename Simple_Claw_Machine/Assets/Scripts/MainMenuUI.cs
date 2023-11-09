using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public Button StartButton;
    public GameObject Panel;
    public GameObject spawnObject;
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

    IEnumerator GameBeginSequence()
    {
        camera_anim.SetTrigger("gameBegin");
        DisableMainMenu();
        spawnObject.SetActive(true);

        yield return new WaitForSeconds(0.1f);
        GameManager.gameIsOver = false;
        ClawMovement.GameBeginState();
        
    }
}
