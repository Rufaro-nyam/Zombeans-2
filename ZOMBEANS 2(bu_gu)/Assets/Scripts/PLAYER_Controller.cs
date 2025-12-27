//using Leguar.LowHealth;
//using Leguar.LowHealth.Example;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PLAYER : MonoBehaviour{


    public float MoveSpeed;
    public float Push;
    private Rigidbody MyRigidbody;
    private Vector3 MoveInput;
    private Vector3 MoveVelocity;
    private Camera MainCamera;

    private int Health;

    //GUNS
    public GUNCONTROLLER TheGun;
    public Cheese_grater_gun Gun2;
    //GFX
    public GameObject model;
    //EFFECTS
   // public ExampleScript_DirectAccess l_h_directaccess;
    private GameObject[] muffle_audios;
    public AudioSource earsting;
    public RawImage[] acid_ui;
    private float acid_dmg_amount;
    //CAMSHAKE
    public Camshake camera_shake;
    public Vector3 proper_pos;


    // Start is called before the first frame update
    void Start()
    {
        MyRigidbody = GetComponent<Rigidbody>();
        MainCamera = FindObjectOfType<Camera>();
        Health = 100;
    
        
        
    }

    public void explosion_blur(float amount)
    {
       // l_h_directaccess.wakingUp2 += amount;
       // l_h_directaccess.beingDizzy += amount/2f;
        Vector3 p_pos = proper_pos;
        camera_shake.shake_exp(0.9f, p_pos, 0.1f);
        AudioLowPassFilter[] filters = FindObjectsOfType<AudioLowPassFilter>();
        foreach(AudioLowPassFilter l in filters)
        {
            float dist = 2.5f * amount;
            if(dist > 0.35f) 
            {
                l.cutoffFrequency -= (amount * 3f) * 22000;
                earsting.Play();
                earsting.volume += amount; ;
            }
            
        }
        
        //print(amount/2);
    }

    public void acid_explosion_damage(float amount)
    {
        acid_dmg_amount += amount;
    }

    public void wall_collision_shake()
    {
        Vector3 p_pos = proper_pos;
        camera_shake.shake_exp(0.9f, p_pos, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        //model.transform.position = new Vector3(transform.position.x, model.transform.position.y, transform.position.z);

        MoveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        MoveVelocity = MoveInput * MoveSpeed;
        
        Ray cameraRay = MainCamera.ScreenPointToRay(Input.mousePosition);
        Plane GroundPlane = new Plane(Vector3.up, Vector3.zero);
        float RayLength;

        if (GroundPlane.Raycast(cameraRay, out RayLength))
        {
            Vector3 PointToLook = cameraRay.GetPoint(RayLength);
            Debug.DrawLine(cameraRay.origin, PointToLook, Color.blue);

            transform.LookAt( new Vector3( PointToLook.x, transform.position.y, PointToLook.z) );
        }

        //AUDIO MUFFLE
        if(earsting.volume > 0)
        {
            earsting.volume -= Time.deltaTime * 0.05f;
            print(earsting.volume);
        }

        //ACID
        acid_dmg_amount = Mathf.Clamp(acid_dmg_amount, 0f, 0.9f);
        foreach(RawImage r in acid_ui)
        {
            float newalpha = Mathf.Clamp01(acid_dmg_amount);
            Color current_alpha = r.color;
            current_alpha.a = newalpha;
            r.color = current_alpha;
        }

        if(acid_dmg_amount > 0)
        {
            acid_dmg_amount -= Time.deltaTime * 0.05f;
        }
            

    }

    public void large_knockback(Vector3 direction)
    {
        MyRigidbody.AddForce(direction * 10, ForceMode.VelocityChange);
        print("hit by large");
       // l_h_directaccess.takingDamage += 0.5f;
        //APPLY DAMAGE HERE
    }

    private void FixedUpdate()
    {
        MyRigidbody.linearVelocity = MoveVelocity;
    }
    public void Damage( Vector3 push)
    {
        Health -= 200;
        //l_h_directaccess.takingDamage += 0.1f;
        if (Health <= 0)
            Debug.Log("DEAD");

        //MyRigidbody.AddForce(push * Push, ForceMode.Force);
             
    }

    public void acid_damage()
    {
        acid_dmg_amount += 0.01f;
    }

    private void OnParticleCollision(GameObject other)
    {
        print("hacid touched something");
    }
}
