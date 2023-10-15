using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Money : MonoBehaviour
{
    public int money;
    private int totalMoney;
    public int countOfCorrect;
    public int countOfIncorect;
    public int countOfFalled;
    [SerializeField] TextMeshProUGUI textGreen;
    [SerializeField] TextMeshProUGUI textRed;

    [SerializeField] ReadExcel excel;
    [SerializeField] TextMeshProUGUI[] cityTexts;
    [SerializeField] TextMeshProUGUI[] forbidenTexts;
    public Animator forbidenlampAnim;
    [SerializeField] CityGetScript[] cityScr;

    [SerializeField] TextMeshProUGUI[] totaldatatext;
    [SerializeField] TextMeshProUGUI totalMoneytext;
 
    private void Start()
    {
        totalMoney = PlayerPrefs.GetInt("totalMoney");
        for(int i=0; i < 3; i++)
        {
            cityTexts[i].text = excel.cities[i];
            cityScr[i].city_adress = excel.cities[i];
        }
        for (int i = 0; i < 4; i++)
        {
            int a = i + 1;
            forbidenTexts[i].text = a.ToString() + " " + excel.forbiddenProductNameLearning[i];
        }

    }
    
    public void TotalizeMoney()
    {
        totalMoney += money;
        PlayerPrefs.SetInt("totalMoney", totalMoney);
        totalMoneytext.text = totalMoney.ToString() + "$";
    }


    public void AnimsOff()
    {
        StartCoroutine(LampAnimOff());
    }

    IEnumerator LampAnimOff()
    {
        yield return new WaitForSeconds(3);

        forbidenlampAnim.SetBool("lampred", false);
        forbidenlampAnim.SetBool("lampgreen", false);
    }
    void Update()
    {
        if(money>=0)
        {
            textGreen.gameObject.SetActive(true);
            textRed.gameObject.SetActive(false);
            
        }
        else
        {
            textGreen.gameObject.SetActive(false);
            textRed.gameObject.SetActive(true);

        }

        textGreen.text = money.ToString() + "$";
        textRed.text = money.ToString() + "$";

        if(money>=0)
        {
            totaldatatext[0].gameObject.SetActive(true);
            totaldatatext[1].gameObject.SetActive(false);

        }
        else
        {
            totaldatatext[0].gameObject.SetActive(false);
            totaldatatext[1].gameObject.SetActive(true);
        }
        totaldatatext[0].text = money.ToString() + "$";
        totaldatatext[1].text = money.ToString() + "$";
        totaldatatext[2].text = countOfCorrect.ToString();
        totaldatatext[3].text = countOfIncorect.ToString();
        totaldatatext[4].text = countOfFalled.ToString();
    }
}
