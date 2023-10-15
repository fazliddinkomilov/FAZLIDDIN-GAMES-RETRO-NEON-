using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustFloorLevel : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public float min;
    public float max;
    Vector3 startpos;
    void Start()
    {
        startpos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var joyAxis = OVRInput.Get(OVRInput.RawAxis2D.RThumbstick, OVRInput.Controller.RTouch);

        if(transform.position.y < min)
        {
            transform.position = startpos;
        }
        else if(transform.position.y > max)
        {
            transform.position = startpos;
        }
        
        transform.position += (transform.up * joyAxis.y + transform.up * joyAxis.y) * Time.deltaTime * speed;
        
      
     }
        
}
