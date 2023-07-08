using UI;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] 
    private string levelToLoad;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.gameObject.CompareTag("Player")) return;

        GameManager.Instance.canPause = false;
        InGameUI.Instance.AnimateTransition(levelToLoad);
    }
}
