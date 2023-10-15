using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakeOpenAnim : MonoBehaviour
{
    bool closed = true;
    Animator anim;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }
    public void Switch()
    {
        if(closed==true)
        {
            anim.SetBool("closed", false);
            closed = false;
        }
        else
        {
            anim.SetBool("closed", true);
            closed = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
    
    }
}
