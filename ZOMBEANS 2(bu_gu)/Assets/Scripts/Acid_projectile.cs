using JetBrains.Annotations;
using System.Collections;
using UnityEngine;

public class Acid_projectile : MonoBehaviour
{
    public float movespeed = 10f;
    private Rigidbody rb;
    private Transform target;
    private Transform  current_target;
    public Vector2 minnoise = new Vector2(-3f, -0.25f);
    public Vector2 maxnoise = new Vector2(3f, 1f);

    public AnimationCurve pos_curve;
    public AnimationCurve noise_curve;
    public float y_offset = 1f;
    bool can_track = true;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        current_target = GameObject.FindGameObjectWithTag("Player").transform;
        target = current_target.transform;
        rb = GetComponent<Rigidbody>();
        StartCoroutine(find_target());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawn(Vector3 foward, Transform target)
    {
        this.target = target;
        rb.AddForce(foward * movespeed, ForceMode.VelocityChange);
    }

    public IEnumerator find_target()
    {
        Vector3 startpos = transform.position;

        Vector2 noise = new Vector2(Random.Range(minnoise.x, maxnoise.x), Random.Range(minnoise.y, maxnoise.y));
        Vector3 bulletdirectionvector = new Vector3( target.position.x, target.position.y, target.position.z) - startpos;
        Vector3 HorizontalNoiseVector = Vector3.Cross(bulletdirectionvector, Vector3.up).normalized;
        float noiseposition = 0;
        float time = 0;
        while (time < 1 ) 
        {
            noiseposition = noise_curve.Evaluate(time);
            transform.position = Vector3.Lerp(startpos, target.position, time) + new Vector3(HorizontalNoiseVector.x * noiseposition*noise.x, noiseposition * noise.y, noiseposition * HorizontalNoiseVector.z * noise.x);
            transform.LookAt(target.position);
            time += Time.deltaTime * movespeed;
            yield return null;

        }


    }
}
