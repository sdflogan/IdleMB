
using TinyBytes.Idle.Production.Machines;
using UnityEngine;

namespace TinyBytes.Idle.Interactable
{
    public class InteractableMachine : MonoBehaviour, IInteractable
    {
        #region Inspector properties

        [SerializeField] private ResourceMachine _machine;

        #endregion

        public void Interact()
        {
            throw new System.NotImplementedException();
        }
    }
}