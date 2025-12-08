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
    public GUNCONTROLLER TheGun;
    private int Health;

    //GFX
    public GameObject model;


    // Start is called before the first frame update
    void Start()
    {
        MyRigidbody = GetComponent<Rigidbody>();
        MainCamera = FindObjectOfType<Camera>();
        Health = 100;
        
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
        if (Input.GetMouseButtonDown(0))
            TheGun.is_firing = true;
        

        if (Input.GetMouseButtonUp(0))
            TheGun.is_firing = false; 

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
