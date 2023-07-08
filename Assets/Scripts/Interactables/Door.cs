using System;
using DG.Tweening;
using UnityEngine;

namespace Interactables
{
    public class Door : IInteractable
    {
        [Header("Animation")]
        [SerializeField] 
        private float animationDuration = 1.0f;
        
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

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                Enable();
            }
            else if (Input.GetKeyDown(KeyCode.P))
            {
                Disable();
            }
        }

        public override void Enable()
        {
            isEnabled = true;
            for (var index = 0; index < doorParts.Length; index++)
            {
                doorParts[index].DOLocalMove(doorEndPositions[index], animationDuration);
            }
        }

        public override void Disable()
        {
            isEnabled = false;
            for (var index = 0; index < doorParts.Length; index++)
            {
                doorParts[index].DOLocalMove(_doorStartPositions[index], animationDuration);
            }
        }
    }
}