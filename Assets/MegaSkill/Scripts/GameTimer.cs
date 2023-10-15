using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MegaSkill.Main
{
    public class GameTimer : MonoBehaviour
    {
        public bool RestartTime = false;
        public TMP_Text timerText;
        public Image timerImage;
        public UnityEvent onFinish;
        public AnimationCurve onStartAnimCurve = AnimationCurve.Linear(0, 1, 1, 1);
        Vector3 startScale;
        void Start(){
            if(RestartTime)DataManager.main.StartTimer();
            startScale = transform.localScale;

            StartCoroutine(_check());
        }

        IEnumerator _check(){
            WaitForSeconds wfs = new(0.3f);
            while (true)
            {
                float time = DataManager.main.GetTime();
                float timerLimitInSec = SaveManager.main.data.timerLimitInMin * 60;
                float timeLeft = timerLimitInSec - time;

                if(timeLeft <= 0)break;
                timeLeft = Mathf.Max(0, timeLeft);
                timerText.text = ((int)(timeLeft / 60)).ToString() + ":" + ((int)(timeLeft % 60)).ToString();
                timerImage.fillAmount = timeLeft / timerLimitInSec;
                yield return wfs;
            }

            DataManager.main.StopTimer();
            onFinish.Invoke();
        }

        public void OnTimeStart() {
            IEnumerator _timeStartAnim() {
                for(float i = 0; i<1; i += Time.deltaTime) {
                    transform.localScale = startScale * onStartAnimCurve.Evaluate(i);
                    yield return null;
                }
                transform.localScale = startScale;
            }

            StartCoroutine(_timeStartAnim());
        }

    }
}
