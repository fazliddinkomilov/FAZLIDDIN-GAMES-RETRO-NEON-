using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MegaSkill.Keyboard
{
    public class SimpleKeyboardButton : MonoBehaviour
    {
        public enum ButtonType{
            Character, Shift, Capslock, Delete, Clear
        }
        public ButtonType type;
        public string text;
        public TMP_Text UIText;
        public Button button;
        SimpleKeyboard main;

        private void Awake() {
            button.onClick.AddListener(OnPress);
        }

        private void Start() {
            UpdateText();
        }

        private void OnValidate() {
            UIText.text = text;
            gameObject.name = "Key_" + text;
        }

        public void UpdateText(){
            UIText.text = GetModifiedText();
        }

        string GetModifiedText(){
            return (type == ButtonType.Character && main.shift) ? text.ToUpper() : text.ToLower();
        }
    
        public void SetMain(SimpleKeyboard keyboard){
            main = keyboard;
        }

        public void OnPress(){
            if(main == null)return;
            switch (type){
                case ButtonType.Character:
                    main.Type(GetModifiedText());
                    break;
                case ButtonType.Shift:
                    break;
                case ButtonType.Capslock:
                    break;
                case ButtonType.Delete:
                    main.Delete();
                    break;
                case ButtonType.Clear:
                    main.Clear();
                    break;
            }
        }
    }
}
