using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLanguage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void changeFirstLanguage(string language)
    {
        PlayerPrefs.SetString("first language", language);    
    }

    public void changeSecondLanguage(string language)
    {
        PlayerPrefs.SetString("second language", language);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
