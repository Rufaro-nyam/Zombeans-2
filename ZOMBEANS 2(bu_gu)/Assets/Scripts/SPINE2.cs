using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SPINE2 : MonoBehaviour
{
    public ConfigurableJoint joint;
    public float Push;
    public float Pop = 0;
    private Rigidbody myrigidbody;

    // Start is called before the first frame update
    void Start()
    {
        joint = GetComponent<ConfigurableJoint>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    public void die()
    {
        
    }
}
