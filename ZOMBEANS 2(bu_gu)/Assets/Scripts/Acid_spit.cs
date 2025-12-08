using UnityEngine;

public class Acid_spit : MonoBehaviour
{
    private Transform target;
    public bool is_mouth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        transform.LookAt(target);
    }

    // Update is called once per frame
    void Update()
    {
        if (is_mouth) { transform.LookAt(target); }
    }
}
