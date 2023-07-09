using System.Collections;
using Audio;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    [SerializeField] 
    private AudioClip goalSfx;
    [SerializeField] 
    private string levelToLoad;
    
    private IEnumerator OnTriggerEnter2D(Collider2D col)
    {
        if (!col.gameObject.CompareTag("Player")) yield break;

        GameManager.Instance.canPause = false;
        
        AudioManager.Instance.sfxSource.PlayOneShot(goalSfx);

        // un-DontDestroyOnLoad the Audio Manager:
        SceneManager.MoveGameObjectToScene(AudioManager.Instance.gameObject, SceneManager.GetActiveScene());
        AudioManager.Instance.FadeOutAll(2f);

        yield return new WaitForSeconds(1.0f);
        
        InGameUI.Instance.AnimateTransition(levelToLoad);
    }
}
