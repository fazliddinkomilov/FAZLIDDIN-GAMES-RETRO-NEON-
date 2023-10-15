using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace MegaSkill.Main
{
    public class GameSceneManager : MonoBehaviour{
        public static GameSceneManager main;
        public bool autoStartTime = false;
        [Space]
        public InputAction finishGameInput;
        [Space]
        public GameTimer timer;

        private void Awake() {
            main = this;
        }

        private void Start() {
            if (!SaveManager.main.isDemoMode) {
                timer.gameObject.SetActive(false);
            }
            if(autoStartTime)
                StartTimer();
        }

        private void OnEnable() {
            finishGameInput.performed += ForceGameFinish;
            finishGameInput.Enable();
        }

        private void OnDisable() {
            finishGameInput.performed -= ForceGameFinish;
            finishGameInput.Disable();
        }
    
        [ContextMenu("Force New Fake Attempt")]
        public void OnNewAttempt(){
            DataManager.main.attempts++;
        }
        public void OnSuccess(int amount){
            DataManager.main.successCount+=amount;
        }
        public void OnFail(int amount){
            DataManager.main.failCount+=amount;
        }
        [ContextMenu("Force Time Out")]
        public void ForceTimeOut(){
            DataManager.main.StopTimer();
            OnGameFinish();
        }

        public void OnTimeOut(){
            if (!SaveManager.main.isDemoMode) return;
            DataManager.main.StopTimer();
            OnGameFinish();
        }

        IEnumerator _LoadSceneDelayed(string level) {
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene(level);
        }
        //------------------------------------------------------------------------------------
        private void ForceGameFinish(InputAction.CallbackContext context) {
            ForceGameFinish();
        }
        [ContextMenu("Force Game Finish")]
        public void ForceGameFinish(){
            OnGameFinish();
        }
        [ContextMenu("Force Level Finish")]
        public void ForceLevelFinish() {
            OnLevelFinish();
        }

        bool finished = false;
        [ContextMenu("Force Level Finish")]
        private void OnLevelFinish(){
            if(finished)return;
            if(DataManager.main.IsLastLevel()){
                OnGameFinish();
            }else{
                finished = true;
                StartCoroutine(_LoadSceneDelayed(DataManager.main.GetNextLevel()));
            }
        }
        private void OnGameFinish(){
            if (finished) return;
            finished = true;
            if(SaveManager.main.isDemoMode)
                SaveManager.main.data.demoSessionCnt++;
            else
                SaveManager.main.data.normalSessionCnt++;
            SaveManager.main.Save();

            DataManager.main.StopTimer();
            
            FinishMenuManager.duration = DataManager.main.GetTime();
            FinishMenuManager.attempts = DataManager.main.attempts;
            FinishMenuManager.score = ScoreCalculator.Calculate();
        
            StartCoroutine(_LoadSceneDelayed(MegaSkillData.main.FinishScene));
        }

        //------------------------------------------------------------------------------------

        [ContextMenu("Force Timer Start")]
        public void StartTimer() {
            if(DataManager.main.isTimerRunning())return;
            DataManager.main.StartTimer();
            if (!SaveManager.main.isDemoMode) return;
            timer.OnTimeStart();
        }

        //------------------------------------------------------------------------------------
        // public void NextLevel(){
        //     SceneManager.LoadScene(nextLevel);
        // }
        // public void GoHome(){
        //     SceneManager.LoadScene(homeScene);
        // }
    }
}
