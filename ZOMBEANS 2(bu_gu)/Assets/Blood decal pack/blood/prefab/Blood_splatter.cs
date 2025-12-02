using UnityEngine;

public class Blood_splatter : MonoBehaviour
{
    public float x_scale;
    public float y_scale;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LeanTween.scaleX(gameObject, x_scale, 0.1f);
        LeanTween.scaleZ(gameObject, y_scale, 0.1f);
        float randomy = Random.Range(1f, 360f);
        Quaternion newrot = Quaternion.Euler(transform.rotation.x, randomy, transform.rotation.z);
        transform.rotation = newrot;


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
