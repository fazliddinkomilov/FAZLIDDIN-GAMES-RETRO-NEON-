using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedButtonsDelay : MonoBehaviour
{
    [SerializeField] GameObject Buttons;
    [SerializeField] GameObject Grey;
    public void ReactiveButtons()
    {
        StartCoroutine(Reactive());
    }

    IEnumerator Reactive()
    {
      
        yield return new WaitForSeconds(2);
        Buttons.SetActive(true);
        Grey.SetActive(false);
    }
}

