using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngineInternal;

public class Pause : MonoBehaviour
{
    public GameObject pausePanel;
    [SerializeField] private Button pauseButton;

    private void OnEnable()
    {
       pauseButton.onClick.AddListener(EnablePausePanel);
    }

    private void EnablePausePanel()
    {
        if(!pausePanel.activeInHierarchy)
        {
            pausePanel.SetActive(true);
        }

        pauseButton.onClick.RemoveListener(EnablePausePanel);
        pauseButton.transform.parent.gameObject.SetActive(false);
    }
}
