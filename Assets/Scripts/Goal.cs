using System.Collections;
using Audio;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class Goal : MonoBehaviour
{
    [SerializeField] 
    private AudioClip goalSfx;
    [SerializeField] 
    private string levelToLoad;
    [SerializeField]
    private Color nextLevelColor;

    private IEnumerator OnTriggerEnter2D(Collider2D col)
    {
        if (!col.gameObject.CompareTag("Player")) yield break;

        GameManager.Instance.canPause = false;
        
        AudioManager.Instance.sfxSource.PlayOneShot(goalSfx);

        foreach (var paintBrush in InGameUI.Instance.paintBrushes)
        {
            paintBrush.GetComponent<Image>().color = nextLevelColor;
        }

        // un-DontDestroyOnLoad the Audio Manager:
        AudioManager.Instance.Reset(2.0f);

        yield return new WaitForSeconds(1.0f);
        
        InGameUI.Instance.AnimateTransition(levelToLoad);
    }
}
