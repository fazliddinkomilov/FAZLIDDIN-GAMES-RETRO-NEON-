using MegaSkill.Keyboard;
using UnityEngine;
using UnityEngine.Events;

namespace MegaSkill.Main
{
    public class PasswordManager : MonoBehaviour
    {
        public string password;
        public SimpleKeyboard passwordKeyboard;
        public UnityEvent onPass;

        public void Enter(){
            if(passwordKeyboard.text == password){
                onPass.Invoke();
                passwordKeyboard.Clear();
            }
        }
    }
}
