using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]int timetoDestroy = 5;
    void Start()
    {
        StartCoroutine(SelfDes());
    }

    // Update is called once per frame
    IEnumerator SelfDes()
    {
        yield return new WaitForSeconds(timetoDestroy);
        Destroy(gameObject);
    }
}
