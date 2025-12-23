using Leguar.LowHealth;
using Leguar.LowHealth.Example;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public ExampleScript_DirectAccess l_h_directaccess;
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
        l_h_directaccess.wakingUp2 = amount;
        l_h_directaccess.beingDizzy = amount/2f;
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

            

    }

    public void large_knockback(Vector3 direction)
    {
        MyRigidbody.AddForce(direction * 10, ForceMode.VelocityChange);
        print("hit by large");
    }

    private void FixedUpdate()
    {
        MyRigidbody.linearVelocity = MoveVelocity;
    }
    public void Damage( Vector3 push)
    {
        Health -= 200;

        if (Health <= 0)
            Debug.Log("DEAD");

        MyRigidbody.AddForce(push * Push, ForceMode.Force);
             
    }
}
