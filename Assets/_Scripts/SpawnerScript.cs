using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    [SerializeField] GameObject box;
    [SerializeField] GameObject BAG;
/*    [SerializeField] string[] cities;
    [SerializeField] string[] product_name;
    [SerializeField] string[] product_name_screen;

    [SerializeField] string[] forbidden_product_name;
    [SerializeField] string[] forbidden_product_name_screen;*/

    [SerializeField] List<string> product_name = new List<string>();
    [SerializeField] List<string> product_name_screen = new List<string>();
    [SerializeField] List<string> forbidden_product_name = new List<string>();
    [SerializeField] List<string> forbidden_product_name_screen = new List<string>();
    [SerializeField] List<string> cities = new List<string>();

    ReadExcel excel;

    [SerializeField] Transform SpawnPos;
    [SerializeField] Transform SpawnPos1;
    [SerializeField] Transform Player;

    [SerializeField] Money MoneyScr;
    public float delay = 20f;

    public bool forbiden = false;
    public bool dirty = false;
    public bool bags = false;
    public bool trash = true;

    private void Start()
    {
        InvokeRepeating("Spawning", 0.0f, delay);
        excel = gameObject.GetComponent<ReadExcel>();

        product_name.Clear();
        product_name_screen.Clear();
        forbidden_product_name.Clear();
        forbidden_product_name_screen.Clear();
        cities.Clear();

        product_name = excel.productNameNative;
        product_name_screen = excel.productNameLearning;
        forbidden_product_name = excel.forbiddenProductNameNative;
        forbidden_product_name_screen = excel.forbiddenProductNameLearning;
        cities = excel.cities;
    }

   

    public void TurnMode(bool b)
    {
        if(b==false)
        {
            CancelInvoke("Spawning");
        }
        else
        {
            InvokeRepeating("Spawning", 0.0f, delay);
        }
    }
    // Update is called once per frame
    void Spawning()
    {
        int randomSpawn;
        if (bags==true)
        {
            randomSpawn = Random.Range(0, 3);
        }
        else
        {
            randomSpawn = 3;
        }
       

        if (randomSpawn == 0)
        {
            BlackBag bag = Instantiate(BAG, SpawnPos1.position, SpawnPos1.rotation).GetComponent<BlackBag>();
            bag.product_name = product_name;
            bag.product_name_screen = product_name_screen;
            bag.forbidden_product_name = forbidden_product_name;
            bag.forbidden_product_name_screen = forbidden_product_name_screen;
            bag.cities = cities;
            bag.Player = Player;
            bag.MoneyScr = MoneyScr;
            if(dirty==true)
            {
                bag.dirty = true;
            }
            if(forbiden==true)
            {
                bag.forbiden = true;
            }

        }
        else
        {
            int forbidenCheck;

            if (forbiden==true)
            {
                forbidenCheck = Random.Range(0, 10);
            }
            else
            {
                forbidenCheck = 10;
            }
            int lastIndex = 0;
           

            if(forbidenCheck <=3)
            {
                BoxScript boxscript = Instantiate(box, SpawnPos.position, SpawnPos.rotation).GetComponent<BoxScript>();
                int index = Random.Range(0, forbidden_product_name.Count);
                if(index==lastIndex)
                {
                    index = Random.Range(0, forbidden_product_name.Count);
                }
                lastIndex = index;
                boxscript.title = forbidden_product_name[index];
                boxscript.texts[0].text = forbidden_product_name[index];
                boxscript.texts[1].text = forbidden_product_name[index];
                boxscript.screen_title = forbidden_product_name_screen[index];
                boxscript.city_adress = cities[Random.Range(0, cities.Count)];
                boxscript.forbidden = true;
                boxscript.moneyScr = MoneyScr;
                if(dirty==true)
                {
                    boxscript.canbedirt = true;
                }
            }
            else
            {
                   BoxScript boxscript = Instantiate(box, SpawnPos.position, SpawnPos.rotation).GetComponent<BoxScript>();
                    int index = Random.Range(0, product_name.Count);
                    if (index == lastIndex)
                    {
                        index = Random.Range(0, product_name.Count);
                    }
                    lastIndex = index;
                    boxscript.title = product_name[index];
                    boxscript.texts[0].text = product_name[index];
                    boxscript.texts[1].text = product_name[index];
                    if (dirty == true)
                    {
                        boxscript.canbedirt = true;
                    }

              
                    boxscript.moneyScr = MoneyScr;


                int i = Random.Range(0, 3);

                if (trash==false)
                {

                    i = 1;
                }



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

                    boxscript.city_adress = cities[Random.Range(0, cities.Count)];
                    boxscript.Player = Player;

            }

 

        }







    }
}
