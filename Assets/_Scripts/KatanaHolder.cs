using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatanaHolder : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject KatanaPrefab;
    [SerializeField] Transform SpawnPos;
    [SerializeField] LevelManager lm;
    [SerializeField] GameObject bag;

    private void Start()
    {
        SpawnKatana();
    }

    public void TutorialNext()
    {
        if(lm.Tutorial==true)
        {
            lm.tutorialSteps = 1;
            lm.NextTutor();

            if (lm.gamelevel == 3)
            {
                bag.SetActive(true);
            }
        }
    }
    public void SpawnKatana()
    {
        Instantiate(KatanaPrefab, SpawnPos.position, SpawnPos.rotation).GetComponent<Katana>().holder = gameObject.GetComponent<KatanaHolder>();
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag=="Katana")
        {
            Destroy(col.gameObject);
            
        }
    }
}
