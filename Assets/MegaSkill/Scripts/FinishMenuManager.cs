using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MegaSkill.Main
{
    public class FinishMenuManager : MonoBehaviour {
        public static FinishMenuManager main;

        [SerializeField] private string homeScene;
    
        [Space] 
        public TextMeshProUGUI timeText;
        public TextMeshProUGUI attemptsText;
        public TextMeshProUGUI scoreText;

        public static float duration;
        public static int score;
        public static int attempts;

        private void Awake() {
            main = this;
        }

        private void Start() {
            StartCoroutine(_onGameFinish());
        }

        private IEnumerator _onGameFinish() {
            yield return new WaitForSeconds(0.5f);

            int minutes;
            float seconds;

            // Save stats
            DataManager.main.SetCurrentPlayerData(duration, attempts, score);
            if (DataManager.main.IsLastPlayerInTeam()){
                DataManager.main.UpdateTeamData();
                DataManager.main.SaveUserStats();
            }

            // Animation
            for (float i = 0; i < 1; i += Time.deltaTime){
                float cTime = duration * i;
                minutes = (int)(cTime / 60);
                seconds = cTime % 60;
                timeText.text = minutes.ToString() + ":" + seconds.ToString("n2");

                attemptsText.text = ((int)(attempts * i)).ToString();
                scoreText.text = ((int)((float)score * i)).ToString();
                yield return null;
            }

            minutes = (int)(duration / 60);
            seconds = duration % 60;
            timeText.text = minutes.ToString() + ":" + seconds.ToString("n2");
            attemptsText.text = attempts.ToString();
            scoreText.text = score.ToString();

            DataManager.main.ResetStats();
        }
        public void GoHome(){
            SceneManager.LoadScene(homeScene);
        }
    }
}