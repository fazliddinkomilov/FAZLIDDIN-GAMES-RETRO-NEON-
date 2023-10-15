using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

namespace MegaSkill.VRIntroduction
{
    public class VRInputTutorial : MonoBehaviour{
        [System.Serializable]
        public class InputTask{
            public bool enabled = true;
            public InputActionReference inputAction;
            public GameObject[] specificObjects;
            public UnityEvent onComplete;
            [HideInInspector] public bool completed = false;
            [HideInInspector] public VRInputTutorial main;

            public void Start(){
                completed = false;
                inputAction.action.performed += OnComplete;
            }
            void OnComplete(InputAction.CallbackContext c){
                if(completed)return;
                completed = true;
                inputAction.action.performed -= OnComplete;
                onComplete.Invoke();
                main.OnComplete(this);
            }
        }
        public VRIntroduction main;
        public UnityEvent onComplete;
        public InputTask[] tasks;
        public GameObject[] specificObjects;
        bool completed = false;

        public void StartTask(){
            foreach (var item in tasks){
                item.main = this;
                item.Start();
            }
        }

        void OnComplete(InputTask task){
            if(completed)return;
            foreach (var item in tasks){
                if(!item.enabled)continue;
                if(!item.completed)return;
            }

            onComplete.Invoke();
            main.NextTutorial();
            completed = true;
        }
    }
}
