using System.Collections;
using UnityEngine;

public class Camshake : MonoBehaviour
{
    public float duration = 1f;
    public bool start;
    public AnimationCurve curve;
    public Transform target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 targetpos = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetpos, 0.1f);
    }

    public void shake(float duration, Vector3 proper_pos, float shake_division)
    {
        StartCoroutine(shaking(duration, proper_pos, shake_division));
    }
     IEnumerator shaking(float duration, Vector3 proper_pos, float strength_division)
    {
        Vector3 startpos = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration) 
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime/duration);
            transform.position = transform.position + Random.insideUnitSphere * strength/strength_division;
            yield return null;
            //transform.position = startpos;
        }
        //stransform.position = startpos;

    }

    public void force_reset_cam()
    {

    }
}
