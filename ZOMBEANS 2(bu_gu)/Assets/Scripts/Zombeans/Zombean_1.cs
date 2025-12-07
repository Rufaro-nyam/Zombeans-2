using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class Zombean_1 : MonoBehaviour
{
    public bool is_explosive;
    public float explosion_force, explosion_radius;
    public GameObject explosion;
    public GameObject exp_mist;

    public bool is_large_zombean;

    public float regular_speed;
    public float large_zomb_speed;


    public int Health;
    private int Current_Health;
    public Rigidbody Head;
    public Rigidbody Spine1;
    public Rigidbody Spine2;
    public Animator Anim;
    public NavMeshAgent nav;

    private bool dead = false;
    private  Transform player; 

    public ConfigurableJoint[] joints;
    public ConfigurableJoint joint;
    private BoxCollider B_collider;

    public ParticleSystem blood_mist;
    public ParticleSystem blood_spray;

    public GameObject[] splatter;
    public GameObject explosion_splatter;
    public LayerMask ground_layer;

    public GameObject model_body;

    private bool can_attack = true;

    // Start is called before the first frame update
    void Start()
    {
        Current_Health = Health;
        Anim = GetComponentInParent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        UnityEditor.EditorWindow.focusedWindow.maximized = !UnityEditor.EditorWindow.focusedWindow.maximized;
        B_collider = GetComponent<BoxCollider>();
        player = GameObject.FindGameObjectWithTag("Player").transform;


    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            model_body.transform.position = new Vector3(transform.position.x, model_body.transform.position.y, transform.position.z);
        }
        if (!dead) 
        {
            float dist = (Vector3.Distance(transform.position, player.transform.position));
            if (dist < 2f)
            {
                nav.speed = 0;
            }
            else
            {
                nav.speed = 3.5f;
            }
            if (can_attack && dist < 2f)
            {
                Vector3 directiontoplayer2 = player.transform.position - transform.position;
                Vector3 oppositedirection2 = directiontoplayer2.normalized;
                Head.AddForce(oppositedirection2 * 40, ForceMode.Impulse);
                StartCoroutine(reset_attack());
                can_attack = false;
            }
        }


        
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        PLAYER p = collision.transform.GetComponent<PLAYER>();
        if (p)
            p.Damage(-collision.GetContact(0).normal);
    }

    private IEnumerator reset_attack()
    {
        yield return new WaitForSeconds(1);
        can_attack = true;
    }

    public void Damage()
    {
        Current_Health -= 1;
        blood_mist.Play();

        
        
        //blood_spray.Play();
        //spawning_blood
        Vector3 rayorigin = transform.position;
        Vector3 raydirection = Vector3.down;
        RaycastHit hit;
        if(Physics.Raycast(rayorigin, raydirection, out hit, 10f, ground_layer))
        {
            if (!is_explosive)
            {
                print(hit.collider.name);
                int numb = Random.Range(0, 7);
                Instantiate(splatter[numb], hit.point, hit.transform.rotation);
            }
            else
            {
                print(hit.collider.name);
                int numb = Random.Range(0, 6);
                Instantiate(splatter[numb], hit.point, hit.transform.rotation);
            }


        }


        
        
        

        if (Current_Health <= 0 && dead == false)
        {
            if (is_explosive) 
            {
                
                explosion_knockback();
                Destroy(gameObject);
                dead = true;
                nav.speed = 0;
                if (Physics.Raycast(rayorigin, raydirection, out hit, 10f, ground_layer))
                {
                    print(hit.collider.name);
                    Instantiate(explosion_splatter, hit.point, hit.transform.rotation);

                }
            }
            else 
            {
                //Destroy(joint);
                //Anim.Play("Zombean_1_death");
                nav.speed = 0;
                dead = true;
                foreach (ConfigurableJoint joint in joints)
                {
                    Destroy(joint);

                    JointDrive drive = joint.slerpDrive;
                    drive.positionSpring = 0;
                    joint.slerpDrive = drive;
                    //print("dead zombean");
                    B_collider.enabled = false;
                }
                if (!is_large_zombean) 
                {
                    Vector3 directiontoplayer = player.transform.position - transform.position;
                    Vector3 oppositedirection = -directiontoplayer.normalized;
                    Head.AddForce(oppositedirection * 30, ForceMode.Impulse);
                    Spine1.AddForce(oppositedirection * 30, ForceMode.Impulse);
                    Spine2.AddForce(oppositedirection * 30, ForceMode.Impulse);
                }





                // Destroy(gameObject);
            }

        }
        else
        {
            Vector3 directiontoplayer2 = player.transform.position - transform.position;
            Vector3 oppositedirection2 = -directiontoplayer2.normalized;
            Head.AddForce(oppositedirection2 * 5, ForceMode.Impulse);
            Spine1.AddForce(oppositedirection2 * 5, ForceMode.Impulse);
        }

    }

    public void plain_death()
    {
        if (!dead)
        {
            nav.speed = 0;
            dead = true;
            foreach (ConfigurableJoint joint in joints)
            {
                Destroy(joint);

                JointDrive drive = joint.slerpDrive;
                drive.positionSpring = 0;
                joint.slerpDrive = drive;
                //print("dead zombean");
                B_collider.enabled = false;
            }
        }
        dead = true;
    }

    public void explosion_knockback()
    {
        print("explosion");
        Instantiate(explosion, transform.position, Quaternion.identity);
        Instantiate(exp_mist, transform.position, Quaternion.identity);
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosion_radius);

        foreach (Collider nearby in colliders)
        {
           Rigidbody rb = nearby.GetComponent<Rigidbody>();
            if(rb != null)
            {
                if(rb.tag == "ZOMBEAN")
                {
                    rb.TryGetComponent<Zombean_1>(out Zombean_1 zombean);
                    zombean.plain_death();
                }
                if (rb.tag == "ZOMBEAN2")
                {
                    rb.TryGetComponent<Zombean_2>(out Zombean_2 zombean);
                    zombean.plain_death();
                }
                rb.AddExplosionForce(explosion_force, transform.position, explosion_radius);
            }
        }
    }
}

