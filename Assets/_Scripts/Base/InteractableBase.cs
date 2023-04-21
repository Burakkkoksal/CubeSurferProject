using UnityEngine;

namespace Game.Base
{
    /// <summary>
    /// Used by objects that is interactable such as diamond and cube
    /// </summary>
    public abstract class InteractableBase<T> : MonoBehaviour
    {
        private bool _isInteracted;
        private Collider _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (_isInteracted || other == null || !other.TryGetComponent<T>(out var t)) return;
            Interact(t);
        }

        protected virtual void Interact(T from)
        {
            _isInteracted = true;
            _collider.enabled = false;
        }
    }
}
