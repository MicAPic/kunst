using UnityEngine;

namespace Interactables
{
    public abstract class IInteractable : MonoBehaviour
    {
        public bool isEnabled;
        
        public abstract void Enable();

        public abstract void Disable();
    }
}
