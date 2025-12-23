using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BULLET1script : MonoBehaviour
{
    public float SPEED;
    public float LIFETIME;
    public Transform ray_point;
    private Rigidbody rb;
    public float explosion_force, explosion_radius;
    public GameObject explosion;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Vector3 foward_direction = transform.forward * 30;
        Vector3 upward_direction = transform.up * 5;
        rb.AddForce(foward_direction, ForceMode.Impulse);
        rb.AddForce(upward_direction, ForceMode.Impulse);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        /*transform.Translate(Vector3.forward * SPEED * Time.deltaTime);
        LIFETIME -= Time.deltaTime;
        if (LIFETIME < 0 )
            Destroy(gameObject);*/
        
    }

    private void OnCollisionEnter(Collision other)
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        explosion_knockback();
        
        Destroy(gameObject);


    }

    private void OnTriggerEnter(Collider other)
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        explosion_knockback();
        
        Destroy(gameObject);
    }

    public void explosion_knockback()
    {
        print("explosion");
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosion_radius);
        float dist = Vector3.Distance(transform.position, player.transform.position);
        float blur_num = 2.5f/dist;
        print(blur_num);
        player.TryGetComponent<PLAYER>(out PLAYER player_script);
        if (player_script)
        {
            player_script.explosion_blur(blur_num);
        }
        
        foreach (Collider nearby in colliders)
        {
            Rigidbody rb = nearby.GetComponent<Rigidbody>();
            if (rb != null)
            {
                if (rb.tag == "ZOMBEAN" || rb.tag == "ZOMBEAN3")
                {
                    if(rb.TryGetComponent<Zombean_1>(out Zombean_1 zombean))
                    {
                        zombean.plain_death();
                    }
                    
                }
                if (rb.tag == "ZOMBEAN2" || rb.tag == "ZOMBEAN3")
                {
                    if(rb.TryGetComponent<Zombean_2>(out Zombean_2 zombean))
                    {
                        zombean.plain_death();
                    }
                    
                }
                rb.AddExplosionForce(explosion_force, transform.position, explosion_radius);
            }
        }
    }
}
