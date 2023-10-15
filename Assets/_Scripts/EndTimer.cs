using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTimer : MonoBehaviour
{
    private GameObject[] boxes;
    private GameObject[] bags;

    Timer timerScr;
    public GameObject tiktaksound;
    private void Start()
    {
        timerScr = gameObject.GetComponent<Timer>();
    }
    private void Update()
    {
        if(timerScr.timeRemaining<10)
        {
            tiktaksound.SetActive(true);
        }
    }
    public void CleanBoxes()
    {
        
        boxes = GameObject.FindGameObjectsWithTag("Box");
        bags = GameObject.FindGameObjectsWithTag("Bag");

        foreach (GameObject boxes in boxes)
        {
            boxes.SetActive(false);
        }
        foreach (GameObject bags in bags)
        {
            bags.SetActive(false);
        }
    }
}
