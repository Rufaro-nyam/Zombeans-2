using System.Collections;
using UnityEngine;

public class Hitflash : MonoBehaviour
{
    public Renderer[] rend;
    public Color flash_color = Color.white ;
    public float flash_duration = 0.1f;

    private Color original_colour;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        original_colour = rend[0].material.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator DOflash()
    {

        rend[0].material.color = flash_color;
        rend[0].material.EnableKeyword("_EMISSION");
        rend[1].material.color = flash_color;
        rend[1].material.EnableKeyword("_EMISSION");
        yield return new WaitForSeconds(flash_duration);
        rend[0].material.color = original_colour;
        rend[0].material.DisableKeyword("_EMISSION");
        rend[1].material.color = original_colour;
        rend[1].material.DisableKeyword("_EMISSION");


    }

    public void flash()
    {
        StopAllCoroutines();
        StartCoroutine(DOflash());
    }
}
