using System.Collections;
using DG.Tweening;
using Interactables;
using UnityEngine;

public class Button : MonoBehaviour
{
    [Header("Functionality")]
    [SerializeField] 
    private bool isActive;
    [SerializeField] 
    private IInteractable[] linkedInteractables;
    [SerializeField] 
    private bool hasTimer;
    [SerializeField] 
    private float time;
    
    [Header("Appearance")]
    [SerializeField]
    private SpriteRenderer buttonSpriteRenderer;
    [SerializeField] 
    private Sprite activeSprite;
    private Sprite _inactiveSprite;

    private void Awake()
    {
        _inactiveSprite = buttonSpriteRenderer.sprite;
    }

    private IEnumerator OnTriggerEnter2D(Collider2D col)
    {
        if (!isActive)
        {
            Activate();

            if (!hasTimer) yield break;
            yield return new WaitForSeconds(time);
            isActive = false;
            AnimateDeactivation();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (hasTimer) return;
        Deactivate();
    }

    private void Activate()
    {
        isActive = true;
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
