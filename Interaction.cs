
using UnityEngine;

namespace TauntGames
{


    public class Interaction : MonoBehaviour
    {
        public Collider interactable;
        private Interactable interactWith;

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            if (interactable)
            {
                interactWith.CustomUpdate();

                if (Input.GetButtonDown("Interact"))
                {
                    if (interactWith.canInteract)
                    {
                        interactWith.Interact();
                    }
                }
            }
        }

        public void SetInteractable(Interactable _interactable)
        {
            interactWith = _interactable;
        }
    }
}