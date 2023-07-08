using System;
using DG.Tweening;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Animation")] 
    [SerializeField] 
    private float shakeDuration = 0.485f;
    [SerializeField] 
    private float shakeStrength = 0.004341f;
    [SerializeField] 
    private int shakeVibratio;
    [SerializeField] 
    private float shakeRandomness;
    [Space]
    [SerializeField] 
    private Sprite deathSprite;
    private Collider2D _collider;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Thorn"))
        {
            Die();
        }
    }

    public void Die()
    {
        GameManager.Instance.canPause = false;
        _collider.enabled = false;
        Time.timeScale = 0.0f;

        _spriteRenderer.sprite = deathSprite;
        
        transform.DOShakePosition(
            shakeDuration,
            shakeStrength,
            shakeVibratio,
            shakeRandomness, 
            false, 
            true, 
            ShakeRandomnessMode.Harmonic
            ).SetUpdate(true)
             .OnComplete(() =>
             {
                 // Time.timeScale = 1.0f;
                 InGameUI.Instance.AnimateTransition(SceneManager.GetActiveScene().name);
             });
    }
}
