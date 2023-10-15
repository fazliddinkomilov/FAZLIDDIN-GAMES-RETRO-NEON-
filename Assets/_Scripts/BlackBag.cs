using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlackBag : MonoBehaviour
{
    [SerializeField] GameObject box;
    /*    public string[] cities;
        public string[] product_name;
        public string[] product_name_screen;
        public string[] forbidden_product_name;
        public string[] forbidden_product_name_screen;*/

    public List<string> cities = new List<string>();
    public List<string> product_name = new List<string>();
    public List<string> product_name_screen = new List<string>();
    public List<string> forbidden_product_name = new List<string>();
    public List<string> forbidden_product_name_screen = new List<string>();

    // Start is called before the first frame update
    public Transform Player;
    [SerializeField] GameObject SmokeEx;
    [SerializeField] Transform SpawnPoint;
    [SerializeField] Transform SpawnPoint2;
    [SerializeField] Transform Cutted;
    bool cuttedbool = false;
    public Money MoneyScr;

    public bool forbiden = false;
    public bool dirty = false;

    Coroutine lastRoutine = null;
    bool destroyBag = false;
    [SerializeField] GameObject BallPrefab;
    private GameObject LastBall;
    public bool tutorailbag = false;
    [SerializeField] LevelManager lm;
    [SerializeField] GameObject OverButtons; 


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Blade")
        {
            cuttedbool = true;
            Destroy(gameObject);

        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            lastRoutine = StartCoroutine(Destroying());

        }
        if (collision.gameObject.tag == "Trash")
        {
            Destroy(gameObject);

        }
    }

    private void OnCollisionExit(Collision collision)
    {
      
        if (collision.gameObject.tag == "Floor")
        {
            StopCoroutine(lastRoutine);
        }
    }

    IEnumerator Destroying()
    {
        yield return new WaitForSeconds(5);
        destroyBag = true;
        Destroy(gameObject);
    }

    // Update is called once per frame
    void OnDestroy()
    {
        LastBall = Instantiate(BallPrefab, transform.position, transform.rotation);
        LastBall.transform.LookAt(Player);
        if (destroyBag==false)
        {
            if (cuttedbool == true)
            {
                Cutted.gameObject.SetActive(true);
                Cutted.transform.parent = transform.parent;

                if (tutorailbag == true)
                {
                    lm.tutorialSteps += 1;
                    lm.NextTutor();
                    OverButtons.SetActive(true);
                }

                int forbidenCheck;

         /*       if (forbiden == true)
                {
                    forbidenCheck = Random.Range(0, 10);
                }
                else
                {*/
                    forbidenCheck = 10;
               /* }*/

                int forbidenCheck2;

           /*     if (forbiden == true)
                {
                    forbidenCheck2 = Random.Range(0, 10);
                }
                else
                {*/
                    forbidenCheck2 = 10;
             /*   }*/

                if (forbidenCheck <= 3)
                {
                    BoxScript boxscript2 = Instantiate(box, SpawnPoint.position, SpawnPoint.rotation).GetComponent<BoxScript>();
                    int index2 = Random.Range(0, product_name.Count);
                    boxscript2.title = forbidden_product_name[index2];
                    boxscript2.texts[0].text = forbidden_product_name[index2];
                    boxscript2.texts[1].text = forbidden_product_name[index2];
                    boxscript2.screen_title = forbidden_product_name_screen[index2];
                    boxscript2.city_adress = cities[Random.Range(0, cities.Count)];
                    boxscript2.forbidden = true;
                    boxscript2.moneyScr = MoneyScr;
                    if (dirty == true)
                    {
                        boxscript2.canbedirt = true;
                    }
                }
                else
                {
                    BoxScript boxscript2 = Instantiate(box, SpawnPoint.position, SpawnPoint.rotation).GetComponent<BoxScript>();
                    int index2 = Random.Range(0, product_name.Count);
                    boxscript2.title = product_name[index2];
                    boxscript2.texts[0].text = product_name[index2];
                    boxscript2.texts[1].text = product_name[index2];
                    if (dirty == true)
                    {
                        boxscript2.canbedirt = true;
                    }
                    int i2 = Random.Range(0, 3);
                    if (i2 < 2)
                    {
                        boxscript2.screen_title = product_name_screen[index2];
                    }
                    else
                    {
                        int s;


                        s = Random.Range(0, product_name_screen.Count);
                        if (s == index2)
                        {
                            s = Random.Range(0, product_name_screen.Count);
                            boxscript2.screen_title = product_name_screen[s];
                        }
                        else
                        {
                            boxscript2.screen_title = product_name_screen[s];
                        }



                        boxscript2.trash = true;

                    }
                    boxscript2.moneyScr = MoneyScr;
                    boxscript2.city_adress = cities[Random.Range(0, cities.Count)];
                    boxscript2.Player = Player;
                }



                if (forbidenCheck2 <= 3)
                {
                    BoxScript boxscript = Instantiate(box, SpawnPoint2.position, SpawnPoint2.rotation).GetComponent<BoxScript>();
                    int index = Random.Range(0, forbidden_product_name.Count);
                    boxscript.title = forbidden_product_name[index];
                    boxscript.texts[0].text = forbidden_product_name[index];
                    boxscript.texts[1].text = forbidden_product_name[index];
                    boxscript.screen_title = forbidden_product_name_screen[index];
                    boxscript.city_adress = cities[Random.Range(0, cities.Count)];
                    boxscript.forbidden = true;
                    boxscript.moneyScr = MoneyScr;
                    if (dirty == true)
                    {
                        boxscript.canbedirt = true;
                    }
                }
                else
                {
                    BoxScript boxscript = Instantiate(box, SpawnPoint2.position, SpawnPoint2.rotation).GetComponent<BoxScript>();
                    int index = Random.Range(0, product_name.Count);
                    boxscript.title = product_name[index];
                    boxscript.texts[0].text = product_name[index];
                    boxscript.texts[1].text = product_name[index];
                    if (dirty == true)
                    {
                        boxscript.canbedirt = true;
                    }
                    int i = Random.Range(0, 3);
                    if (i < 2)
                    {
                        boxscript.screen_title = product_name_screen[index];
                    }
                    else
                    {
                        int s;


                        s = Random.Range(0, product_name_screen.Count);
                        if (s == index)
                        {
                            s = Random.Range(0, product_name_screen.Count);
                            boxscript.screen_title = product_name_screen[s];
                        }
                        else
                        {
                            boxscript.screen_title = product_name_screen[s];
                        }



                        boxscript.trash = true;

                    }
                    boxscript.moneyScr = MoneyScr;
                    boxscript.city_adress = cities[Random.Range(0, cities.Count)];
                    boxscript.Player = Player;
                }



           
            }
        }
        else
        {
            if (tutorailbag == true)
            {
                SceneManager.LoadSceneAsync(1);
            }
            MoneyScr.countOfFalled += 1;
            MoneyScr.money -= 30;
            LastBall.GetComponent<BallCanvas>().ball = -30;
            LastBall.GetComponent<BallCanvas>().Red.SetActive(true);
            Instantiate(SmokeEx, SpawnPoint.position, SpawnPoint.rotation);
        }

        
    }
}
