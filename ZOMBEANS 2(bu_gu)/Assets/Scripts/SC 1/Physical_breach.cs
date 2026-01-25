using UnityEngine;

public class Physical_breach : MonoBehaviour
{
    public Breach_close breach_Close;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }




    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            breach_Close.in_range = true;
            print("player entered");
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            breach_Close.in_range = false;
            print("player exit");
        }
    }
}
