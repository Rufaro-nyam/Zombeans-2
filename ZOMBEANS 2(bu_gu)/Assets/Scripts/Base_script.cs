using UnityEngine;

public class Base_script : MonoBehaviour
{
    public float current_ealth;
    private float max_health = 100f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        current_ealth = max_health;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void take_damage(float damage_amount)
    {
        current_ealth -= damage_amount;
    }
}
