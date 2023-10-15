using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class BoxScript : MonoBehaviour
{
    public string title;
    public string screen_title;
    public string city_adress;
    public string check_name = "none";
    public string check_adress = "none";
    public bool trash = false;
    public bool hasCheck = false;
    public bool forbidden = false;
    public Transform Player;
    [SerializeField] GameObject SmokeEx;
    [SerializeField] GameObject SkullEx;
    [SerializeField] GameObject SkullExTrash;
    [SerializeField] GameObject FireEx;
    [SerializeField] GameObject FireExRed;
    [SerializeField] GameObject GetterEx;
    [SerializeField] Collider QrColider;
    public int deathtype = 1;
    bool dirt = false;
    public bool canbedirt = false;
    public bool tutorialboxx = false;
    float dirtHealth = 100;
    [SerializeField] GameObject dirtObj;
    [SerializeField] GameObject spongeSound;
    public Money moneyScr;

    public TextMeshProUGUI[] texts;

    [SerializeField] SpriteRenderer[] dirtSprites;
    [SerializeField] ParticleSystem SparklesParticle;
    AudioSource cleanAudio;
    [SerializeField] GameObject BallPrefab; 
    private GameObject LastBall;
    public string checkType;
    [SerializeField] bool wrongBoxTutorial = false;
    [SerializeField] LevelManager lm;

    private void Start()
    {
        if(canbedirt ==true)
        {
            int i = Random.Range(0, 10);
            if (i <= 3)
            {
                dirt = true;
            }
            if (dirt == true)
            {
                dirtObj.SetActive(true);
                QrColider.enabled = false;
            }
        }
        cleanAudio = SparklesParticle.gameObject.GetComponent<AudioSource>();

    }
    Coroutine lastRoutine = null;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(tutorialboxx==false)
        {
            if (other.gameObject.tag == "Floor")
            {
                lastRoutine = StartCoroutine(Destroying());
                deathtype = 1;
            }
        }

        if (other.gameObject.tag == "Bake")
        {
            Destroy(gameObject);
            deathtype = 2;
        }
        if (other.gameObject.tag == "Trash")
        {
            Destroy(gameObject);
            deathtype = 3;
        }
        if (other.gameObject.tag != "Sponge")
        {
            spongeSound.SetActive(false);
        }
    }

    IEnumerator Destroying()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Sponge")
        {
            dirtHealth -= 0.5f;

            if(dirtHealth<70)
            {
                for (int i = 0; i < dirtSprites.Length; i++)
                {
                    dirtSprites[i].color = new Color(1f, 1f, 1f, 0.5f);
                }
            }
            if (dirtHealth < 50)
            {
                for (int i = 0; i < dirtSprites.Length; i++)
                {
                    dirtSprites[i].color = new Color(1f, 1f, 1f, 0.4f);
                }
            }
            if(dirtHealth<40)
            {
                for (int i = 0; i < dirtSprites.Length; i++)
                {
                    dirtSprites[i].color = new Color(1f, 1f, 1f, 0.25f);
                }
            }
             if (dirtHealth < 20)
            {
                for (int i = 0; i < dirtSprites.Length; i++)
                {
                    dirtSprites[i].color = new Color(1f, 1f, 1f, 0.1f);
                }
            }


            spongeSound.SetActive(true);
            if (dirtHealth<=0)
            {
                QrColider.enabled = true;
                dirtObj.SetActive(false);
                SparklesParticle.gameObject.SetActive(true);
                cleanAudio.Play();
                if (canbedirt == true && tutorialboxx == true)
                {
                    lm.tutorialSteps += 1;
                    lm.NextTutor();
                    lm.StartGameBut.SetActive(true);

                }


            }
        }
    }
  
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Sponge")
        {
            spongeSound.SetActive(false);
        }

        if (other.gameObject.tag == "Floor")
        {
            StopCoroutine(lastRoutine);
        }
    }
    // Update is called once per frame
    private void OnDestroy()
    {
        if (tutorialboxx == false)
        {
            LastBall = Instantiate(BallPrefab, transform.position, transform.rotation);
            LastBall.transform.LookAt(Player);
        }
      /*  LastBall.transform.position += transform.forward * 0.6f;*/


        if (deathtype==1)
        {
         
            Instantiate(SmokeEx, transform.position, transform.rotation);
            moneyScr.countOfFalled += 1;
            moneyScr.money -= 5;
            LastBall.GetComponent<BallCanvas>().ball = -5;
            LastBall.GetComponent<BallCanvas>().Red.SetActive(true);
        }
        else if(deathtype==2)
        {
            if (tutorialboxx == true && forbidden == false)
            {
                SceneManager.LoadSceneAsync(1);
            }
            if (forbidden==true)
            {
                if (lm != null && tutorialboxx == true)
                {
                    lm.StartGameBut.SetActive(true);
                }
                Instantiate(FireEx, transform.position, transform.rotation);
                moneyScr.money += 35;
                LastBall.GetComponent<BallCanvas>().ball = 35;
                LastBall.GetComponent<BallCanvas>().Green.SetActive(true);
                moneyScr.countOfCorrect +=1;
                moneyScr.AnimsOff();
                moneyScr.forbidenlampAnim.SetBool("lampgreen", true);
        

            }
            else
            {
                if (tutorialboxx == true)
                {
                    SceneManager.LoadSceneAsync(1);
                }
                moneyScr.forbidenlampAnim.SetBool("lampred", true);
                moneyScr.AnimsOff();
                Instantiate(FireExRed, transform.position, transform.rotation);
                LastBall.GetComponent<BallCanvas>().ball = -15;
                LastBall.GetComponent<BallCanvas>().Red.SetActive(true);
                moneyScr.countOfIncorect += 1;
                moneyScr.money -= 15;
            }


        }
        else if(deathtype==7)
        {
            Instantiate(GetterEx, transform.position, transform.rotation);
        }
        else if(deathtype==3)
        {
            if(trash==true && checkType == "NotApproved")
            {
              
                Instantiate(SkullExTrash, transform.position, transform.rotation);
                moneyScr.money += 25;
                LastBall.GetComponent<BallCanvas>().ball = 25;
                
                LastBall.GetComponent<BallCanvas>().Green.SetActive(true);
                moneyScr.countOfCorrect += 1;

                if(wrongBoxTutorial==true)
                {
                    if(lm!=null)
                    {
                        lm.StartButtonActive();
                    }
                    
                }
        /*        lm.StartButtonActive();
                lm.tutorialSteps += 1;
                lm.NextTutor();
                lm.Tutorial = false;*/
            }
            else
            {
                if (tutorialboxx == true)
                {
                    SceneManager.LoadSceneAsync(1);
                }
                Instantiate(SkullEx, transform.position, transform.rotation);
                moneyScr.countOfIncorect += 1;
                moneyScr.money -= 25;
                LastBall.GetComponent<BallCanvas>().ball = -25;
                LastBall.GetComponent<BallCanvas>().Red.SetActive(true);
             
            }
            
        }
        else
        {
            if(tutorialboxx==true)
            {
                SceneManager.LoadSceneAsync(1);
            }
            Instantiate(SkullEx, transform.position, transform.rotation);
        }
        
    }
    
}
