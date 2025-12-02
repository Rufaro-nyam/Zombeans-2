using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BULLET1script : MonoBehaviour
{
    public float SPEED;
    public float LIFETIME;
    public Transform ray_point;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * SPEED * Time.deltaTime);
        LIFETIME -= Time.deltaTime;
        if (LIFETIME < 0 )
            Destroy(gameObject);
        
    }

    private void OnCollisionEnter(Collision other)
    {
        
        if (other.gameObject.tag == "ZOMBEAN")
        {
            
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ZOMBEAN")
        {
            Debug.Log("collision");
            other.gameObject.GetComponent<Zombean_1>().Damage();
        }
        Destroy(gameObject);
    }
}
