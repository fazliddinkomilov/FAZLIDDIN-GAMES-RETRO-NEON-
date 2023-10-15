using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CityGetScript : MonoBehaviour
{

    public string city_adress;
    BoxScript selectedBox;
    [SerializeField] Money moneyScript;
    [SerializeField] Animator lampAnim;
    [SerializeField] Animator lampAnim2;
    [SerializeField] AudioSource soundCorrect;
    [SerializeField] AudioSource incorrect;
    [SerializeField] bool needCheck = false;

    [SerializeField] GameObject BallPrefab;
    private GameObject LastBall;
    [SerializeField] Transform Player;
    [SerializeField] LevelManager lm;
    [SerializeField] Scaner scanerScript;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Box")
        {
            LastBall = Instantiate(BallPrefab, transform.position, transform.rotation);
            LastBall.transform.LookAt(Player);
            LastBall.transform.position += transform.forward * 0.6f;

       

            selectedBox = other.gameObject.GetComponent<BoxScript>();
            if(selectedBox.tutorialboxx == false)
            {
                   if (selectedBox.city_adress == city_adress && selectedBox.forbidden == false )
                            {
                               
                                    if(selectedBox.hasCheck==true && selectedBox.checkType == "Approved")
                                    {
                                        moneyScript.money += 20;
                                        LastBall.GetComponent<BallCanvas>().ball = 20;
                                        LastBall.GetComponent<BallCanvas>().Green.SetActive(true);
                                        lampAnim.SetBool("lampgreen", true);
                                        lampAnim.SetBool("lampred", false);
                                        lampAnim2.SetBool("lampred", false);
                                        moneyScript.countOfCorrect += 1;
                                        soundCorrect.Play();
                                    }
                                    else
                                    {
                                        moneyScript.money -= 20;
                                        LastBall.GetComponent<BallCanvas>().ball = -20;
                                        LastBall.GetComponent<BallCanvas>().Red.SetActive(true);
                                        lampAnim.SetBool("lampred", true);
                                        lampAnim.SetBool("lampgreen", false);
                                        moneyScript.countOfIncorect += 1;
                                        if (selectedBox.hasCheck == false)
                                        {
                                            lampAnim2.SetBool("lampred", true);
                                        }

                                        incorrect.Play();
                                    }
                         

                
                            }
                            else
                            {
                                moneyScript.money -= 20;
                                LastBall.GetComponent<BallCanvas>().ball = -20;
                                LastBall.GetComponent<BallCanvas>().Red.SetActive(true);
                                lampAnim.SetBool("lampred", true);
                                moneyScript.countOfIncorect += 1;
                                lampAnim.SetBool("lampgreen", false);
                                if(selectedBox.hasCheck==false)
                                {
                                    lampAnim2.SetBool("lampred", true);
                                }
                
                                incorrect.Play();
                            }
            }
            else
            {
                if (selectedBox.hasCheck == true)
                {
                    moneyScript.money += 20;
                    LastBall.GetComponent<BallCanvas>().ball = 20;
                    LastBall.GetComponent<BallCanvas>().Green.SetActive(true);
                    lampAnim.SetBool("lampgreen", true);
                    lampAnim.SetBool("lampred", false);
                    lampAnim2.SetBool("lampred", false);
                    moneyScript.countOfCorrect += 1;
                    soundCorrect.Play();
                }
                else
                {
                   
                    
                    
                    moneyScript.money -= 20;
                    LastBall.GetComponent<BallCanvas>().ball = -20;
                    LastBall.GetComponent<BallCanvas>().Red.SetActive(true);
                    lampAnim.SetBool("lampred", true);
                    lampAnim.SetBool("lampgreen", false);
                    moneyScript.countOfIncorect += 1;
                    if (selectedBox.hasCheck == false)
                    {
                        lampAnim2.SetBool("lampred", true);
                    }

                    incorrect.Play();
                    StartCoroutine(ReloadTutorial());
                   
                }
            }
         

            if (lm.Tutorial == true)
            {
                /*   lm.StartButtonActive();
                   lm.tutorialSteps += 1;
                   lm.NextTutor();
                   lm.Tutorial = false;*/
                lm.tutorialSteps += 1;
                lm.NextTutor();
                scanerScript.tutor = true;
              /*  StartCoroutine(lm.NextTutorCorutine());*/

            }
            StartCoroutine(AnimOff());
            selectedBox.deathtype = 7;
            Destroy(other.gameObject);
        }
    }

  
    IEnumerator ReloadTutorial()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadSceneAsync(1);
    }
    // Update is called once per frame
    IEnumerator AnimOff()
    {
        yield return new WaitForSeconds(4);
        lampAnim.SetBool("lampred", false);
        lampAnim2.SetBool("lampred", false);
        lampAnim.SetBool("lampgreen", false);
        lampAnim2.SetBool("lampgreen", false);


    }
}
