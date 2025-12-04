using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUNCONTROLLER : MonoBehaviour
{
    public bool is_firing;
    public BULLET1script bullet;
    public float BulletSpeed;
    public float TimeBetweenShots;
    private float ShotCounter;
    public Transform firepoint;

    //UPDATING GUN SYSTEM
    public bool add_spread = true;
    public Vector3 bullet_spread_variance = new Vector3(.1f, .1f, .1f);
    public TrailRenderer bullet_trail;
    public LayerMask mask;

    //EFFECTS
    public ParticleSystem blood_spray;
    public ParticleSystem stone_hit_particles;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (is_firing)
        {
            ShotCounter -= Time.deltaTime;
            if (ShotCounter <= 0 )
            {
                ShotCounter = TimeBetweenShots;
                /*BULLET1script newBullet = Instantiate(bullet, firepoint.position, firepoint.rotation) as BULLET1script;
                newBullet.SPEED = BulletSpeed;*/


                Vector3 direction = Get_direction();

                if (Physics.Raycast(firepoint.position, direction, out RaycastHit hit, float.MaxValue)) 
                {
                    TrailRenderer trail = Instantiate(bullet_trail, firepoint.position, Quaternion.identity);
                    StartCoroutine(Spawntrail(trail, hit));
                    if (hit.collider.isTrigger) 
                    {
                        if(hit.collider.tag == "ZOMBEAN")
                        {
                            hit.collider.gameObject.GetComponent<Zombean_1>().Damage();
                            Instantiate(blood_spray, hit.point, Quaternion.LookRotation(hit.normal));
                        }
                        if (hit.collider.tag == "ZOMBEAN2")
                        {
                            hit.collider.gameObject.GetComponent<Zombean_2>().Damage();
                            Instantiate(blood_spray, hit.point, Quaternion.LookRotation(hit.normal));
                        }

                    }
                    if (hit.collider.tag == "STONE")
                    {
                        print("stone_hit");
                        Instantiate(stone_hit_particles, hit.point, Quaternion.LookRotation(hit.normal));
                    }
                }
                
            }
                
        }else{
            ShotCounter = 0;
        
        }
        
    }

    private Vector3 Get_direction()
    {
        Vector3 direction = transform.forward;
        if (add_spread) 
        {
            direction += new Vector3(
                Random.Range(-bullet_spread_variance.x, bullet_spread_variance.x),
                Random.Range(-bullet_spread_variance.y, bullet_spread_variance.y),
                Random.Range(-bullet_spread_variance.z, bullet_spread_variance.z)
                );

            direction.Normalize();
        }
        return direction;
    }

    private IEnumerator Spawntrail(TrailRenderer trail, RaycastHit hit)
    {
        float time = 0;
        Vector3 startpos = trail.transform.position;

        while (time < 0.01f)
        {
            trail.transform.position = Vector3.Lerp(startpos, hit.point, time);
            time += Time.deltaTime / trail.time;

            yield return null;
        }
        trail.transform.position = hit.point;
        //for impact particles
        //Instantiate(impact_particles, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy( trail.gameObject, trail.time );
    }
}
