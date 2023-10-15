using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadPosition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PosCorrect(1));
        
    }

    // Update is called once per frame
    IEnumerator PosCorrect(int i)
    {
        yield return new WaitForSeconds(i);
        transform.localPosition = new Vector3(transform.localPosition.x, 0.71f, transform.localPosition.z);
    }
}
