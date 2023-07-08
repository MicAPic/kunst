using System;
using System.Collections;
using UI;
using UnityEngine;

namespace Interactables
{
    public abstract class IInteractable : MonoBehaviour
    {
        public bool isEnabled;
        public Timer timer;
        private RectTransform _timerRect;

        public virtual void Awake()
        {
            _timerRect = timer.transform.GetChild(0).GetComponent<RectTransform>();
        }

        public virtual void Update()
        {
            RecenterTimer();
        }

        public abstract void Enable();

        public abstract void Disable();

        public abstract IEnumerator EnableWithTimer(float time);

        private void RecenterTimer()
        {
            var viewportPosition = GameManager.Instance.mainCamera.WorldToViewportPoint(transform.position);

            _timerRect.anchorMin = viewportPosition;  
            _timerRect.anchorMax = viewportPosition;
        }
    }
}
