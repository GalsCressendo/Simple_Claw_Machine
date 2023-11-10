using UnityEngine;

public class Gift : MonoBehaviour
{
    GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Body")
        {
            Prize prize = other.transform.root.transform.GetComponent<Prize>();
            gameManager.DisplayWinCutscene(prize);
        }
    }

}
