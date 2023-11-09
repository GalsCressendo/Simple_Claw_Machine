using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;
using TMPro;

public class WinScreenUI : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI rewardText;
    [SerializeField]TextMeshProUGUI descriptionText;
    private GameObject rewardPreview;
    private Vector3 rewardPosition = new Vector3 (-1.8f, -2.65f, -6.39f);
    private float rewardUIScale = 0.2f;

    public void DisplayRewardScreen(PrizeAttribute prize)
    {
        ModifyWinningScreenText(prize.prizeName, prize.description);
        SpawnRewardPreview(prize.UI_Gameobject);
    }

    private void ModifyWinningScreenText(string prizeName, string description)
    {
        string rewardString = string.Format("YOU GOT A {0}!", prizeName);
        rewardText.text = rewardString;

        descriptionText.text = description;
    }

    private void SpawnRewardPreview(GameObject rewardUI)
    {
        rewardPreview = Instantiate(rewardUI, rewardPosition, Quaternion.identity);
        rewardPreview.transform.localScale = new Vector3(rewardUIScale, rewardUIScale, rewardUIScale);
    }
}
