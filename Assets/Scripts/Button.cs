using System;
using System.Collections;
using System.Collections.Generic;
using Interactables;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] 
    private bool isActive;
    [SerializeField] 
    private IInteractable[] linkedInteractables;
    [SerializeField] 
    private bool hasTimer;
    [SerializeField] 
    private float time;
    
    // Start is called before the first frame update
    // void Start()
    // {
    //     
    // }

    // Update is called once per frame
    // void Update()
    // {
    //     
    // }
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
        else
        {
            if (hasTimer) yield break;
            Deactivate();
        }
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
        //TODO: swap sprites
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
    }
    
    private void AnimateDeactivation()
    {
        //TODO: swap sprites
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
