using TMPro;
using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    public int gamelevel = 0;

    [SerializeField] GameObject[] level2Objs;
    [SerializeField] GameObject[] level3Objs;
    [SerializeField] GameObject[] level4Objs;
    [SerializeField] GameObject[] level5Objs;
    [SerializeField] Timer timerScr;
    int needMoneyToWin = 300;
    SpawnerScript spawner;
    [SerializeField] Money money;
    string levelname = "LEVEL 1";
    [SerializeField] TextMeshProUGUI[] levelnameTexts;
    [SerializeField] GameObject[] Totals;
    [SerializeField] GameObject timer;
    [SerializeField] GameObject robotCamera;
    [SerializeField] GameObject[] tutorialCanvas;
    [SerializeField] GameObject tutorialFirstBox;
    [SerializeField] TutorialArrow tutorialArrow;
    [SerializeField] GameObject BGMusic;

    public bool Tutorial = false;
    public int tutorialSteps = 0;
    [SerializeField] GameObject CountDownGo;
    public GameObject StartGameBut;
    [SerializeField] GameObject RicardoScreen;
 
    void Start()
    {
        gamelevel = PlayerPrefs.GetInt("gamelevel");
        if(gamelevel>=5)
        {
            gamelevel = 4;
        }

        spawner = gameObject.GetComponent<SpawnerScript>();
        /*        if (currentLevel <= 1)
                {
                    Tutorial = true;
                }*/

        if (gamelevel == 0 || gamelevel == 1 || gamelevel == 2 || gamelevel == 3 || gamelevel == 4)
        {
            Tutorial = true;
        }
        else
        {
            Tutorial = false;
        }
        if (Tutorial == false)
        {
            StartGameCor();
            tutorialCanvas[gamelevel].SetActive(false);
            tutorialFirstBox.SetActive(false);
            tutorialArrow.gameObject.SetActive(false);
        }
        else
        {
            tutorialCanvas[gamelevel].SetActive(true);
            if(gamelevel==0)
            {
                tutorialFirstBox.SetActive(true);
            }

            tutorialArrow.gameObject.SetActive(true);
        }



        for (int i = 0; i < level2Objs.Length; i++)
        {
            level2Objs[i].SetActive(false);
        }
        for (int i = 0; i < level3Objs.Length; i++)
        {
            level3Objs[i].SetActive(false);
        }
        for (int i = 0; i < level4Objs.Length; i++)
        {
            level4Objs[i].SetActive(false);
        }

        if (gamelevel >= 1)
        {
            spawner.trash = true;
            spawner.forbiden = false;
            for (int i = 0; i < level2Objs.Length; i++)
            {
                level2Objs[i].SetActive(true);
            }
        }
        if (gamelevel >= 2)
        {
            spawner.forbiden = true;
            for (int i = 0; i < level3Objs.Length; i++)
            {
                level3Objs[i].SetActive(true);
            }
        }
        if (gamelevel >= 3)
        {
            spawner.bags = true;
            for (int i = 0; i < level4Objs.Length; i++)
            {
                level4Objs[i].SetActive(true);
            }
        }
        if (gamelevel >= 4)
        {
            spawner.dirty = true;
            for (int i = 0; i < level4Objs.Length; i++)
            {
                level5Objs[i].SetActive(true);
            }
        }
        spawner.trash = true;
    }

    public void NextTutor()
    {
        switch (tutorialSteps)
        {
            case 0:
                tutorialArrow.TutorialScreens[0].SetActive(true);
                tutorialArrow.TutorialScreens2[0].SetActive(true);
                tutorialArrow.TutorialScreens3[0].SetActive(true);
                tutorialArrow.TutorialScreens4[0].SetActive(true);
                tutorialArrow.TutorialScreens5[0].SetActive(true);

                break;
            case 1:
                tutorialArrow.TutorialScreens[0].SetActive(false);
                tutorialArrow.TutorialScreens[1].SetActive(true);

                tutorialArrow.TutorialScreens2[0].SetActive(false);
                tutorialArrow.TutorialScreens2[1].SetActive(true);

                tutorialArrow.TutorialScreens3[0].SetActive(false);
                tutorialArrow.TutorialScreens3[1].SetActive(true);

                tutorialArrow.TutorialScreens4[0].SetActive(false);
                tutorialArrow.TutorialScreens4[1].SetActive(true);

                tutorialArrow.TutorialScreens5[0].SetActive(false);
                tutorialArrow.TutorialScreens5[1].SetActive(true);
                break;
            case 2:
                tutorialArrow.TutorialScreens[1].SetActive(false);
                tutorialArrow.TutorialScreens[0].SetActive(false);
                tutorialArrow.TutorialScreens[2].SetActive(true);

                tutorialArrow.TutorialScreens2[1].SetActive(false);
                tutorialArrow.TutorialScreens2[0].SetActive(false);
                tutorialArrow.TutorialScreens2[2].SetActive(true);

                tutorialArrow.TutorialScreens3[1].SetActive(false);
                tutorialArrow.TutorialScreens3[0].SetActive(false);
                tutorialArrow.TutorialScreens3[2].SetActive(true);

                tutorialArrow.TutorialScreens4[1].SetActive(false);
                tutorialArrow.TutorialScreens4[0].SetActive(false);
                tutorialArrow.TutorialScreens4[2].SetActive(true);

                tutorialArrow.TutorialScreens5[1].SetActive(false);
                tutorialArrow.TutorialScreens5[0].SetActive(false);
                tutorialArrow.TutorialScreens5[2].SetActive(true);
                break;
            case 3:
                tutorialArrow.TutorialScreens[2].SetActive(false);
                tutorialArrow.TutorialScreens[0].SetActive(false);
                tutorialArrow.TutorialScreens[1].SetActive(false);
                tutorialArrow.TutorialScreens[3].SetActive(true);

                tutorialArrow.TutorialScreens2[2].SetActive(false);
                tutorialArrow.TutorialScreens2[3].SetActive(true);


                tutorialArrow.TutorialScreens3[2].SetActive(false);
                tutorialArrow.TutorialScreens3[3].SetActive(true);


                tutorialArrow.TutorialScreens4[2].SetActive(false);
                tutorialArrow.TutorialScreens4[3].SetActive(true);


                tutorialArrow.TutorialScreens5[2].SetActive(false);
                tutorialArrow.TutorialScreens5[3].SetActive(true);
                break;
            case 4:
                tutorialArrow.TutorialScreens[3].SetActive(false);
                tutorialArrow.TutorialScreens[4].SetActive(true);

                tutorialArrow.TutorialScreens2[3].SetActive(false);
                tutorialArrow.TutorialScreens2[4].SetActive(true);

                tutorialArrow.TutorialScreens3[3].SetActive(false);
                tutorialArrow.TutorialScreens3[4].SetActive(true);

                tutorialArrow.TutorialScreens4[3].SetActive(false);
                tutorialArrow.TutorialScreens4[4].SetActive(true);

                tutorialArrow.TutorialScreens5[3].SetActive(false);
                tutorialArrow.TutorialScreens5[4].SetActive(true);
                break;
            case 5:
                tutorialArrow.TutorialScreens[4].SetActive(false);
                tutorialArrow.TutorialScreens[5].SetActive(true);
                break;
            case 6:
                tutorialArrow.TutorialScreens[5].SetActive(false);
                tutorialArrow.TutorialScreens[6].SetActive(true);
                break;
            case 7:
                tutorialArrow.TutorialScreens[6].SetActive(false);
                tutorialArrow.TutorialScreens[7].SetActive(true);
                break;
            case 8:
                tutorialArrow.TutorialScreens[7].SetActive(false);
                tutorialArrow.TutorialScreens[8].SetActive(true);
                break;
            case 9:
                tutorialArrow.TutorialScreens[8].SetActive(false);
                tutorialArrow.TutorialScreens[9].SetActive(true);
                break;

            default:
                tutorialArrow.TutorialScreens[2].SetActive(false);
                tutorialArrow.TutorialScreens[1].SetActive(false);
                tutorialArrow.TutorialScreens[3].SetActive(false);
                break;


        }


    }

    public void StartGameCor()
    {
/*        NextTutor();*/
        CountDownGo.SetActive(true);
        BGMusic.SetActive(true);
        Tutorial = false;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Box");
        for (int i = 0; i < enemies.Length; i++)
        {
            Destroy(enemies[i]);
        }
       
        StartGame();
    }
  /*  IEnumerator StartingGame()
    {
        CountDownGo.SetActive(true);
        BGMusic.SetActive(true);
        Destroy(CountDownGo);
        StartGame();
    }*/

    public void StartButtonActive()
    {
        StartGameBut.SetActive(true);
    }

    public IEnumerator NextTutorCorutine()
    {
        yield return new WaitForSeconds(7);
        tutorialSteps += 1;
        NextTutor();
    }
     private void StartGame()
    {
        /*
                tutorialCanvas.SetActive(false);*/
        Tutorial = false;

        /*gameObject.GetComponent<SpawnerScript>().StartSpawning();*/
        tutorialArrow.gameObject.SetActive(false);
        StartGameBut.SetActive(false);
        RicardoScreen.SetActive(false);
        spawner.enabled = true;
        timer.SetActive(true);
        robotCamera.SetActive(true);
        BGMusic.SetActive(true);

        switch (gamelevel)
        {
            case 0:
                needMoneyToWin = 300;
                timerScr.minutes = 3;
                levelname = "LEVEL 1";
                break;
            case 1:
                needMoneyToWin = 350;
                timerScr.minutes = 3;
                levelname = "LEVEL 2";
                break;
            case 2:
                needMoneyToWin = 400;
                timerScr.minutes = 4;
                levelname = "LEVEL 3";
                break;
            case 3:
                needMoneyToWin = 450;
                timerScr.minutes = 4;
                levelname = "LEVEL 4";
                break;
            case 4:
                needMoneyToWin = 500;
                timerScr.minutes = 5;
                levelname = "LEVEL 5";
                Totals[0].SetActive(false);
                Totals[1].SetActive(true);
               
                break;

        }

        for (int i = 0; i < levelnameTexts.Length; i++)
        {
            levelnameTexts[i].text = levelname;
        }





    }

    public void TimeEnd()
    {
        gamelevel += 1;
        PlayerPrefs.SetInt("gamelevel", gamelevel);
    }

    // Update is called once per frame
    void Update()
    {
        if(money.money >= needMoneyToWin)
        {
            timerScr.timeRemaining = 0;
        }
 
    }
}
