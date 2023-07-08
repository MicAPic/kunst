using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    [SerializeField] 
    private string levelToLoad;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.gameObject.CompareTag("Player")) return;

        InGameUI.Instance.AnimateTransition(levelToLoad);
    }
}
