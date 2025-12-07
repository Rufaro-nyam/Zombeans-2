using UnityEngine;

public class RGBD_ZMBNLRG_REACTION : MonoBehaviour
{
    private Rigidbody rb;
    public Zombean_1 zmb1;
    public bool is_zmb_1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void throwback(Vector3 direction)
    {
        
        if (is_zmb_1) 
        {
            zmb1.charge_death(direction);
        }
        else
        {
            rb.AddForce(direction * 1.5f, ForceMode.Impulse);
        }
    }
}
