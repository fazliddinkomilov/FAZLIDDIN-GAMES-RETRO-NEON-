using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace MegaSkill.Keyboard
{
    public class EditTextField : MonoBehaviour
    {
        public TMP_Text textTMP;
        public string text;
        public GameObject requiredIcon;
        public int maxChars = 8;
        public bool hideText = false;
        public char hideChar = '*';

        private void Start() {
            UpdateText();
        }
        public void Activate() {
            SimpleKeyboard.main.SetActiveField(this);
        }

        public string GetText() {
            return text;
        }
        public void SetText(string text) {
            this.text = text;
            UpdateText();
        }

        public bool isEmpty(){
            return text == "";
        }

        public void SetIsRequired(bool isRequired){
            if(requiredIcon == null) return;
            requiredIcon.SetActive(isRequired);
        }

        private void OnValidate() {
            UpdateText();
        }
        public void UpdateText() {
            if (text.Length > maxChars) {
                text = text.Remove(maxChars, text.Length - maxChars);
            }

            if (hideText){
                textTMP.text = new string(hideChar, text.Length);
            }else{
                textTMP.text = text;
            }
        }
    }
}