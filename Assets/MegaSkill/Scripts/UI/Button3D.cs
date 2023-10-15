using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

// [RequireComponent(typeof(XRBaseInteractable))]
namespace MegaSkill.Main
{
    public class Button3D : MonoBehaviour
    {
        public UnityEvent onPress;
        public GameObject hoverObject;
        public bool pressableByRobot = true;
        public bool pressableByPlayer = true;
        XRBaseInteractable interactable;
        bool active = false;

        public virtual void Awake()
        {
            interactable = GetComponent<XRBaseInteractable>();
            if (hoverObject)
                hoverObject.SetActive(false);
        }

        private void OnEnable() {
            active = false;
            if (interactable){
                interactable.activated.AddListener(pressByPlayer);
                interactable.selectEntered.AddListener(pressByPlayer);
                interactable.hoverEntered.AddListener(onHoverEnter);
                interactable.hoverExited.AddListener(onHoverExit);
            }
            StartCoroutine(_activate());
        }

        IEnumerator _activate(){
            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();
            active = true;
        }

        private void OnDisable() {
            if (interactable){
                interactable.activated.RemoveListener(pressByPlayer);
                interactable.selectEntered.RemoveListener(pressByPlayer);
                interactable.hoverEntered.RemoveListener(onHoverEnter);
                interactable.hoverExited.RemoveListener(onHoverExit);
            }
            active = false;
        }

        void onHoverEnter(HoverEnterEventArgs args){
            if (hoverObject)
                hoverObject.SetActive(true);
        }
        void onHoverExit(HoverExitEventArgs args){
            if (hoverObject)
                hoverObject.SetActive(false);
        }

        void pressByPlayer(SelectEnterEventArgs args){
            if(pressableByPlayer)
                Press();
        }
        void pressByPlayer(ActivateEventArgs args){
            if(pressableByPlayer)
                Press();
        }

        public void PressByRobot(){
            if(pressableByRobot)
                Press();
        }

        public void Press(){
            if (!active) return; // Prevent invoking when enabled
            onPress.Invoke();
        }
    }
}
