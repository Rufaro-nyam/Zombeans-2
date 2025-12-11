using UnityEngine;

public class Player_cam : MonoBehaviour
{
    public Transform target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.LookAt(target);
        transform.position = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
    }
}
