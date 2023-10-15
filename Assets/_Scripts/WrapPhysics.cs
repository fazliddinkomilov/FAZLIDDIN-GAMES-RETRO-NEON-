using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrapPhysics : MonoBehaviour
{
    [SerializeField] Cloth[] wraps;
    SphereCollider selectedCol;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Box")
        {
            for(int i = 0; i<wraps.Length; i++)
            {
                selectedCol = other.gameObject.GetComponent<SphereCollider>();
                var ClothColliders = new ClothSphereColliderPair[1];
                ClothColliders[0] = new ClothSphereColliderPair(selectedCol);
 

                wraps[i].sphereColliders = ClothColliders;
            }
           
        }
    }
}
