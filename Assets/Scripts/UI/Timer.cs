using System.Collections;
using Interactables;
using UnityEngine;

namespace UI
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] 
        private GameObject[] ticks;

        public IEnumerator Countdown(float time, IInteractable trigger)
        {
            foreach (var tick in ticks)
            {
                tick.SetActive(true);
            }
        
            var interval = time / ticks.Length;
            foreach (var tick in ticks)
            {
                yield return new WaitForSeconds(interval);
                tick.SetActive(false);
            }
            
            trigger.Disable();
            gameObject.SetActive(false);
        }
    }
}
