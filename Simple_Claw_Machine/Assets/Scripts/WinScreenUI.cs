using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;
using TMPro;

public class WinScreenUI : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI rewardText;
    [SerializeField]TextMeshProUGUI descriptionText;

    public void DisplayRewardScreen(string prizeName, string description)
    {
        ModifyWinningScreenText(prizeName, description);
    }

    private void ModifyWinningScreenText(string prizeName, string description)
    {
        string rewardString = string.Format("YOU GOT A {0}!", prizeName);
        rewardText.text = rewardString;

        descriptionText.text = description;
    }
}
