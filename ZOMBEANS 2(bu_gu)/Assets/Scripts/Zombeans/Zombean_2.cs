using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class Zombean_2 : MonoBehaviour
{

    public int Health;
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
    public LayerMask ground_layer;

    public GameObject model_body;

    private bool can_attack = true;

    public GameObject flames;
    public bool onfire;
    private float fire_time = 35;
    // Start is called before the first frame update
    void Start()
    {
        Current_Health = Health;
        Anim = GetComponentInParent<Animator>();
        nav = GetComponent<NavMeshAgent>();
       // UnityEditor.EditorWindow.focusedWindow.maximized = !UnityEditor.EditorWindow.focusedWindow.maximized;
        B_collider = GetComponent<BoxCollider>();
        player = GameObject.FindGameObjectWithTag("Player").transform;


    }

    // Update is called once per frame
    void Update()
    {
        if (onfire)
        {
            fire_damage(1 * Time.deltaTime);
        }
        if (!dead)
        {
            model_body.transform.position = new Vector3(transform.position.x, model_body.transform.position.y, transform.position.z);
        }
        if (!dead) 
        {
            float dist = (Vector3.Distance(transform.position, player.transform.position));
            if (dist < 2.5f)
            {
                nav.speed = 0;
            }
            else
            {
                nav.speed = 7f;
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

    public void catch_fire()
    {
        flames.SetActive(true);
        onfire = true;
        fire_damage(0.01f);
    }
    private IEnumerator reset_attack()
    {
        yield return new WaitForSeconds(0.5f);
        can_attack = true;
    }

    public IEnumerator zombean_on_fire()
    {
        yield return new WaitForSeconds(10);
        flames.SetActive(false);

    }
    public void fire_damage(float damage)
    {
        if (Current_Health > 0)
        {
            Current_Health -= damage;
            //print(Current_Health);
        }





        //blood_spray.Play();
        //spawning_blood
        Vector3 rayorigin = transform.position;
        Vector3 raydirection = Vector3.down;
        RaycastHit hit;
        if (Physics.Raycast(rayorigin, raydirection, out hit, 10f, ground_layer))
        {


        }






        if (Current_Health <= 0 && dead == false)
        {
            StartCoroutine(zombean_on_fire());

           
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

                // Destroy(gameObject);


        }
        else
        {

        }
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
            print(hit.collider.name);
            int numb = Random.Range(0, 7);
            Instantiate(splatter[numb], new Vector3(hit.point.x, hit.point.y + 0.01f, hit.point.z), hit.transform.rotation);

        }


        
        
        

        if (Current_Health <= 0 && dead == false)
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
            Vector3 directiontoplayer = player.transform.position - transform.position;
            Vector3 oppositedirection = -directiontoplayer.normalized;
            Head.AddForce(oppositedirection * 30, ForceMode.Impulse);
            Spine1.AddForce(oppositedirection * 30, ForceMode.Impulse);
            Spine2.AddForce(oppositedirection * 30, ForceMode.Impulse);

            
            // Destroy(gameObject);
        }
        else
        {
            Vector3 directiontoplayer2 = player.transform.position - transform.position;
            Vector3 oppositedirection2 = -directiontoplayer2.normalized;
            Head.AddForce(oppositedirection2 * 5, ForceMode.Impulse);
            Spine1.AddForce(oppositedirection2 * 5, ForceMode.Impulse);
        }

    }
}

