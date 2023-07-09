using System.Collections;
using Audio;
using DG.Tweening;
using UnityEngine;

namespace Interactables
{
    public class Door : IInteractable
    {
        [Header("Animation")]
        [SerializeField] 
        private float animationDuration = 1.0f;
        
        [Header("Sound")]
        [SerializeField] 
        private AudioClip openSfx;
        [SerializeField] 
        private AudioClip closeSfx;
        
        [Header("Mechanics")]
        [SerializeField] 
        private Transform[] doorParts;
        [SerializeField] 
        private Vector2[] doorEndPositions;
        private Vector2[] _doorStartPositions;

        void Start()
        {
            if (doorParts.Length != doorEndPositions.Length)
            {
                Debug.LogWarning("You done goofed! Arrays mismatch");
            }
            
            _doorStartPositions = new Vector2[doorEndPositions.Length];
            for (var index = 0; index < doorParts.Length; index++)
            {
                _doorStartPositions[index] = doorParts[index].localPosition;
            }
        }

        public override void Update()
        {
            base.Update();
            
            if (Input.GetKeyDown(KeyCode.O))
            {
                Enable();
            }
            else if (Input.GetKeyDown(KeyCode.P))
            {
                Disable();
            }
            else if (Input.GetKeyDown(KeyCode.I))
            {
                StartCoroutine(EnableWithTimer(5.0f));
            }
        }

        public override void Enable()
        {
            isEnabled = true;
            AudioManager.Instance.sfxSource.PlayOneShot(openSfx);
            for (var index = 0; index < doorParts.Length; index++)
            {
                doorParts[index].DOLocalMove(doorEndPositions[index], animationDuration);
            }
        }

        public override void Disable()
        {
            isEnabled = false;
            AudioManager.Instance.sfxSource.PlayOneShot(closeSfx);
            for (var index = 0; index < doorParts.Length; index++)
            {
                doorParts[index].DOLocalMove(_doorStartPositions[index], animationDuration);
            }
        }

        public override IEnumerator EnableWithTimer(float time)
        {
            isEnabled = true;
            AudioManager.Instance.sfxSource.PlayOneShot(openSfx);
            for (var index = 0; index < doorParts.Length; index++)
            {
                doorParts[index].DOLocalMove(doorEndPositions[index], animationDuration);
            }
            timer.gameObject.SetActive(true);
            StartCoroutine(timer.Countdown(time, this));

            yield return null;
        }
    }
}