using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ignore_collision : MonoBehaviour
{
    [SerializeField]
    Collider this_collider;

    [SerializeField]
    Collider[] colliders_to_ignore;
    // Start is called before the first frame update
    void Start()
    {
        foreach(Collider other in colliders_to_ignore)
        {
            Physics.IgnoreCollision(this_collider, other, true);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
