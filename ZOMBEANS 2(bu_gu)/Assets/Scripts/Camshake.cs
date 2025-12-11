using System.Collections;
using UnityEngine;

public class Camshake : MonoBehaviour
{
    public float duration = 1f;
    public bool start;
    public AnimationCurve curve;    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            start = false;
            //StartCoroutine(shaking());
        }
    }

    public void shake(float duration, Vector3 proper_pos)
    {
        StartCoroutine(shaking(duration, proper_pos));
    }
     IEnumerator shaking(float duration, Vector3 proper_pos)
    {
        Vector3 startpos = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration) 
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime/duration);
            transform.position = startpos + Random.insideUnitSphere * strength;
            yield return null;
            transform.position = startpos;
        }
        transform.position = startpos;

    }
}
