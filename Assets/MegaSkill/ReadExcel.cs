using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadExcel : MonoBehaviour
{
    public TextAsset russian;
    public TextAsset english;
    public TextAsset uzbek;
    public TextAsset firstLanguage;
    public TextAsset secondLanguage;
    public List<string> cities = new List<string>();
    public List<string> productNameNative = new List<string>();
    public List<string> productNameLearning = new List<string>();
    public List<string> forbiddenProductNameNative = new List<string>();
    public List<string> forbiddenProductNameLearning = new List<string>();


    void Awake()
    {
        firstLanguage = uzbek;
        secondLanguage = russian;

        switch (PlayerPrefs.GetString("first language"))
        {
            case "russian":
                firstLanguage = russian;
                break;
            case "english":
                firstLanguage = english;
                break;
            case "uzbek":
                firstLanguage = uzbek;
                break;
        }

        switch (PlayerPrefs.GetString("second language"))
        {
            case "russian":
                secondLanguage = russian;
                break;
            case "english":
                secondLanguage = english;
                break;
            case "uzbek":
                secondLanguage = uzbek;
                break;
        }


        string[] dataNative = firstLanguage.text.Split(new string[] { ";", "\n" }, System.StringSplitOptions.None);
        string[] dataLearning = secondLanguage.text.Split(new string[] { ";", "\n" }, System.StringSplitOptions.None);

        for (int i = 0; i < dataNative.Length; i++)
        {
            string[] rowNative = dataNative[i + 1].Split(new char[] { ',' });
            string[] rowLearning = dataLearning[i + 1].Split(new char[] { ',' });

            productNameNative.Add(rowNative[0]);
            productNameLearning.Add(rowLearning[0]);


            if (forbiddenProductNameNative.Count < 4)
            {
                forbiddenProductNameNative.Add(rowNative[1]);
            }
            if (forbiddenProductNameLearning.Count < 4)
            {
                forbiddenProductNameLearning.Add(rowLearning[1]);
            }
            if (cities.Count < 3)
            {
                cities.Add(rowNative[2]);
            }








        }








    }

}
