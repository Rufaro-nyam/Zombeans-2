using UnityEngine;

public class Zombean_flame_reaction : MonoBehaviour
{
    public Zombean_1 zmb1;
    public Zombean_2 zmb2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void catch_fire()
    {
        if(zmb1 != null)
        {
            zmb1.catch_fire();
        }
        if(zmb2 != null)
        {
            zmb2.catch_fire();
        }
        
    }
}
