using UnityEngine;
using UnityEngine.UI;

public class Player_damage_script : MonoBehaviour
{
    public RawImage blood_droplets;
    public RawImage red_tint;

    public RawImage[] acid_drops;
    public RawImage green_tint;
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
            //red_tint.color = droplets_color;
        }

        if(red_tint.color.a > 0)
        {
            Color tint_color = red_tint.color;
            tint_color.a -= Time.deltaTime * 0.1f;
            red_tint.color = tint_color;
        }

        foreach(RawImage r in acid_drops)
        {
            if(r.color.a > 0)
            {
                Color droplets_color = r.color;
                droplets_color.a -= Time.deltaTime * 0.1f;
                r.color = droplets_color;
            }
        }

        if(green_tint.color.a > 0)
        {
            Color green_tint_color = blood_droplets.color;
            green_tint_color.a -= Time.deltaTime * 0.1f;
            green_tint.color = green_tint_color;
        }
    }

    public void normal_damage()
    {
        Color droplets_color = blood_droplets.color;
        droplets_color.a += 0.1f;
        blood_droplets.color = droplets_color;
        //red_tint.color = droplets_color;

        Color tint_color = red_tint.color;
        tint_color.a += 0.1f;
        red_tint.color = tint_color;

    }

    public void acid_damage()
    {
        Color droplets_color = blood_droplets.color;
        droplets_color.a += 0.01f;
        blood_droplets.color = droplets_color;
        //red_tint.color = droplets_color;

        Color tint_color = red_tint.color;
        tint_color.a += 0.01f;
        red_tint.color = tint_color;
    }
}
