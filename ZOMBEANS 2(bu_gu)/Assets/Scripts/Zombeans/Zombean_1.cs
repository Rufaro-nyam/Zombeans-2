using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class Zombean_1 : MonoBehaviour
{
    public bool is_explosive;
    public float explosion_force, explosion_radius;
    public GameObject explosion;
    public GameObject exp_mist;

    public bool is_large_zombean;
    public LARGE_ZOMBEAN_NAVIGATION large_zomb_nav;
    private bool is_carging = false;
    public GameObject charge_target;
    public LayerMask wall_layer;

    public bool is_spitter;
    public Transform spitpos;
    public GameObject acid_spit;
    private bool can_spit = true;
    public ParticleSystem[] spit;


    public float regular_speed;
    public float large_zomb_speed;


    public float Health;
    private float Current_Health;
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
    public GameObject acid;
    public GameObject explosion_splatter;
    public LayerMask ground_layer;


    public GameObject model_body;

    private bool can_attack = true;

    public GameObject flames;

    public bool onfire;
    private float fire_time = 35;

    public ParticleSystem wallblood;

    // Start is called before the first frame update
    void Start()
    {
        Current_Health = Health;
        Anim = GetComponentInParent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        UnityEditor.EditorWindow.focusedWindow.maximized = !UnityEditor.EditorWindow.focusedWindow.maximized;
        B_collider = GetComponent<BoxCollider>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        charge_target.transform.position = player.transform.position;


    }

    // Update is called once per frame
    void Update()
    {
        if (onfire)
        {
            fire_damage(1*Time.deltaTime);
        }
        if (is_large_zombean && !dead)
        {
            transform.rotation = new Quaternion(0,0,0,0);
            if (is_carging) 
            {
                /*RaycastHit hit;
                Vector3 direction = (charge_target.transform.position - transform.position).normalized;
                Ray ray = new Ray(transform.position, direction);
                if (Physics.Raycast(ray, out hit, 1f, wall_layer))
                {
                    print("hit wall raycast");
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    charge_target.transform.position = player.transform.position;
                }
                Debug.DrawRay(transform.position, direction * 10f, Color.yellow);*/
            }

        }
        if(!dead && is_spitter)
        {
            float dist = (Vector3.Distance(transform.position, player.transform.position));
            if (dist < 8f)
            {

                nav.speed = 0;
                if (can_spit)
                {
                    Instantiate(acid_spit, spitpos.position, Quaternion.identity);
                    acid_spit.transform.LookAt(player.transform.position);
                    Vector3 directiontoplayer2 = player.transform.position - transform.position;
                    Vector3 oppositedirection2 = directiontoplayer2.normalized;
                    Head.AddForce(oppositedirection2 * 40, ForceMode.Impulse);
                    StartCoroutine(spit_attack());
                    can_spit = false;
                }
                
                
            }
            else
            {
                
                nav.speed = 3.5f;
            }
        }

        {
            
        }
        if (!dead)
        {
            model_body.transform.position = new Vector3(transform.position.x, model_body.transform.position.y, transform.position.z);
        }
        if (!dead && !is_large_zombean && !is_spitter) 
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
        if (collision.transform.tag == "STONE")
        {
            print("stunned");
        }
    

    }

    public void catch_fire()
    {
        flames.SetActive(true);
        onfire = true;
        fire_damage(0.01f);
        //StartCoroutine(zombean_on_fire());
    }

    public IEnumerator zombean_on_fire()
    {
        yield return new WaitForSeconds(10);
        flames.SetActive(false);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (is_large_zombean)
        {
            if (other.transform.name == "WALL")
            {
                print("stunned");
                StartCoroutine(stunned());

            }
            PLAYER p = other.transform.GetComponent<PLAYER>(); 
            if (p)
            {
                
                Vector3 knockbac_dir = charge_target.transform.position - transform.position;
                p.large_knockback(knockbac_dir);
            }
            RGBD_ZMBNLRG_REACTION rgbd = other.transform.GetComponent<RGBD_ZMBNLRG_REACTION>();
            if (rgbd)
            {
                Vector3 knockbac_dir = charge_target.transform.position - transform.position;
                rgbd.throwback(knockbac_dir);
            }
            Zombean_1 zm1 = other.transform.GetComponent<Zombean_1>();
            if (zm1)
            {
                Vector3 knockbac_dir = charge_target.transform.position - transform.position;
                zm1.charge_death(knockbac_dir);
            }
            /*Rigidbody r = other.transform.GetComponent<Rigidbody>();
            if (r && r.transform.tag != "ZOMBEAN")
            {
                Vector3 knockbac_dir = charge_target.transform.position - transform.position;
                r.AddForce(knockbac_dir * 30, ForceMode.Impulse);

            }*/
        }


    }

    public void charge_death(Vector3 direction)
    {
        if (!is_large_zombean)
        {
            plain_death();
            Head.AddForce(direction * 1, ForceMode.Impulse);
            Spine1.AddForce(direction * 1, ForceMode.Impulse);
        }

    }
    private IEnumerator reset_attack()
    {
        yield return new WaitForSeconds(1);
        can_attack = true;
    }

    private IEnumerator spit_attack()
    {

            yield return new WaitForSeconds(3);
            can_spit = true;


    }

    public void Damage()
    {
        Current_Health -= 1;
        blood_mist.Play();

        
        
        //blood_spray.Play();
        //spawning_blood
        Vector3 rayorigin = transform.position;
        Vector3 raydirection = Vector3.down;
        Vector3 wall_ray_origin = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        Vector3 Wall_ray_direction = transform.position - player.transform.position;
        RaycastHit wall_hit;
        RaycastHit hit;
        if(Physics.Raycast(rayorigin, raydirection, out hit, 10f, ground_layer))
        {
            if (!is_explosive)
            {
                //print(hit.collider.name);
                int numb = Random.Range(0, 7);
                Instantiate(splatter[numb], hit.point, Quaternion.LookRotation(hit.normal));
            }
            else
            {
                //print(hit.collider.name);
                int numb = Random.Range(0, 6);
                Instantiate(splatter[numb], hit.point, Quaternion.LookRotation(hit.normal));
            }


        }
        /*if (Physics.Raycast(wall_ray_origin, Wall_ray_direction, out wall_hit, 100f, wall_layer))
        {
            print("wall detected");
            if (!is_explosive)
            {
                //print(wall_hit.collider.name);
                int numb = Random.Range(0, 7);
                Instantiate(splatter[numb], wall_hit.point, Quaternion.LookRotation(-wall_hit.normal));
                print("blood spawned on wall");
            }
            else
            {
               //print(wall_hit.collider.name);
                int numb = Random.Range(0, 6);
                Instantiate(splatter[numb], wall_hit.point, Quaternion.LookRotation(-wall_hit.normal));
                
            }


        }*/






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

    public void fire_damage(float damage)
    {
        if (Current_Health > 0) 
        {
            Current_Health -= damage;
            print(Current_Health);
        }
        
       



        //blood_spray.Play();
        //spawning_blood
        Vector3 rayorigin = transform.position;
        Vector3 raydirection = Vector3.down;
        RaycastHit hit;
        if (Physics.Raycast(rayorigin, raydirection, out hit, 10f, ground_layer))
        {
            if (!is_explosive)
            {
                
            }
            else
            {
               
            }


        }






        if (Current_Health <= 0 && dead == false)
        {
            StartCoroutine(zombean_on_fire());
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

                }





                // Destroy(gameObject);
            }

        }
        else
        {
            
        }
    }

    public IEnumerator stunned()
    {
        if (is_large_zombean && dead == false) 
        {
            //print("stopped");
            is_carging = false;
            nav.speed = 0;
            yield return new WaitForSeconds(3);
            if(!dead) 
            {
                charge_target.transform.position = player.transform.position;
                is_carging = true;
                nav.speed = 30;
                //print("started");
                yield return new WaitForSeconds(0.5f);
                charge_target.transform.position = player.transform.position;
            }

        }
        else
        {
            yield return null;
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
        if (is_explosive)
        {
            Damage();
        }
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
                    if(rb.TryGetComponent<Zombean_1>(out Zombean_1 zombean))
                    {
                        zombean.plain_death();
                    }
                    
                }
                if (rb.tag == "ZOMBEAN2")
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

