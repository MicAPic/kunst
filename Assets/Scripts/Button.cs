using System.Collections;
using Audio;
using DG.Tweening;
using Interactables;
using UnityEngine;

public class Button : MonoBehaviour
{
    [Header("Sound")]
    [SerializeField] 
    private AudioClip pressSfx;
    [SerializeField] 
    private AudioClip timerSfx;
    [SerializeField] 
    private AudioClip timerEndSfx;
    
    [Header("Functionality")]
    [SerializeField] 
    private bool isActive;
    [SerializeField] 
    private IInteractable[] linkedInteractables;
    [SerializeField] 
    private bool hasTimer;
    [SerializeField] 
    private float time;

    private int _contactCount;
    
    [Header("Appearance")]
    [SerializeField]
    private GameObject timerIcon;
    [SerializeField]
    private SpriteRenderer buttonSpriteRenderer;
    [SerializeField] 
    private Sprite activeSprite;
    private Sprite _inactiveSprite;

    private void Awake()
    {
        _inactiveSprite = buttonSpriteRenderer.sprite;
        if (hasTimer)
        {
            timerIcon.SetActive(true);
        }
    }

    private IEnumerator OnTriggerEnter2D(Collider2D col)
    {
        _contactCount++;
        
        if (isActive) yield break;
        Activate();

        if (!hasTimer) yield break;
        AudioManager.Instance.sfxSource.clip = timerSfx;
        AudioManager.Instance.sfxSource.Play();
        
        yield return new WaitForSeconds(time);
        
        isActive = false;
        AudioManager.Instance.sfxSource.PlayOneShot(timerEndSfx);
        if (_contactCount > 0)
        {
            yield break;
        }
        AnimateDeactivation();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _contactCount--;
        
        if ((hasTimer && isActive) || _contactCount > 0) return;
        Deactivate();
    }

    private void Activate()
    {
        isActive = true;
        AudioManager.Instance.sfxSource.PlayOneShot(pressSfx, 0.1f);
        foreach (var interactable in linkedInteractables)
        {
            if (hasTimer)
                StartCoroutine(interactable.EnableWithTimer(time));
            else
                interactable.Enable();
        }
        
        AnimateActivation();
    }

    private void Deactivate()
    {
        isActive = false;
        foreach (var interactable in linkedInteractables)
        {
            interactable.Disable();
        }
        
        AnimateDeactivation();
    }

    private void AnimateActivation()
    {
        buttonSpriteRenderer.sprite = activeSprite;
        buttonSpriteRenderer.DOColor(new Color(1, 1, 1, 0.5f), 0.2f);
    }
    
    private void AnimateDeactivation()
    {
        buttonSpriteRenderer.sprite = _inactiveSprite;
        buttonSpriteRenderer.DOColor(Color.white, 0.2f);
    }
}
