using System.Collections;
using System.Collections.Generic;
using MegaSkill.Main;
using UnityEngine;
using UnityEngine.SceneManagement;
using MegaSkill.Utils;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

namespace MegaSkill.VRIntroduction
{
    public class VRIntroduction : MonoBehaviour
    {
        // public static VRIntroduction main;
        public bool autoStart = true;
        List<VRInputTutorial> tutorials;
        int currentTutID;

        private void Awake() {
            // main = this;
            tutorials = new(transform.GetComponentsInChildren<VRInputTutorial>());
            foreach (var item in tutorials)
                item.main = this;
        }

        private void Start() {
            if(autoStart)
                StartQuests();
        }

        public void StartQuests(){
            foreach (var tutorial in tutorials){
                foreach (var item in tutorial.specificObjects)
                    if(item!=null)item.SetActive(false);
            }
            currentTutID = -1;
            NextTutorial();
        }
        
        [ContextMenu("Finish")]
        void FinishQuests(){
            SceneManager.LoadScene(DataManager.main.GetCurrentLevel());
        }

        [ContextMenu("Next")]
        public void NextTutorial(){
            if(tutorials.IsRange(currentTutID)){
                foreach (var item in tutorials[currentTutID].specificObjects)
                    item.SetActive(false);
            }
            currentTutID++;
            if(currentTutID >= tutorials.Count){
                FinishQuests();
                return;
            }

            foreach (var item in tutorials[currentTutID].specificObjects)
                item.SetActive(true);
            tutorials[currentTutID].StartTask();
        }
    }
}
