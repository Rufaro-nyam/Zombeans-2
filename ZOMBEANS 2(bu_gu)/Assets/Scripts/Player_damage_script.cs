using UnityEngine;
using UnityEngine.UI;

public class Player_damage_script : MonoBehaviour
{
    public RawImage blood_droplets;
    public RawImage red_tint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(blood_droplets.color.a > 0)
        {
            Color droplets_color = blood_droplets.color;
            droplets_color.a -= Time.deltaTime * 0.1f;
            blood_droplets.color = droplets_color;
            red_tint.color = droplets_color;
        }
    }

    public void normal_damage()
    {
        Color droplets_color = blood_droplets.color;
        droplets_color.a += 0.1f;
        blood_droplets.color = droplets_color;
        red_tint.color = droplets_color;
    }
}
