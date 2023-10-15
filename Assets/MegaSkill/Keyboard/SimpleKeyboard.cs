using UnityEngine;
using UnityEngine.Serialization;

namespace MegaSkill.Keyboard
{
    public class SimpleKeyboard : MonoBehaviour
    {
        public static SimpleKeyboard main;
        public Transform buttonsParent;
        public EditTextField textField;
        public bool shift = false;
        [Space]
        SimpleKeyboardButton[] buttons;

        private void OnEnable() {
            main = this;
        }
        public string text {
            get {
                if (textField == null)
                    return null;
                return textField.GetText();
            }
            set {
                if (textField != null)
                    textField.SetText(value);
            }
        }

        void Awake(){
            buttons = (buttonsParent == null? transform: buttonsParent).GetComponentsInChildren<SimpleKeyboardButton>();
            foreach (var item in buttons)
                item.SetMain(this);
        }


        public void Type(string text){
            this.text += text;
            shift = false;
            UpdateShift();
        }

        public void Delete(){
            if(text.Length > 0){
                text = text.Remove(text.Length - 1, 1);
            }
        }
        public void Clear(){
            text = "";
        }

        public void ToggleShift() {
            shift = !shift;
            UpdateShift();
        }

        void UpdateShift(){
            foreach (var item in buttons)
                item.UpdateText();
        }

        public void SetActiveField(EditTextField editTextField) {
            textField = editTextField;
        }
    }
}
