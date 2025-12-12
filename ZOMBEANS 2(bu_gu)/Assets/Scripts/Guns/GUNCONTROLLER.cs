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

    public bool is_automatic;
    public bool is_shotgun;
    public bool is_flame_shotgun;
    public bool is_grenade_launcher;
    public bool is_flame_thrower;

    public bool gun1;
    public bool cheese_grater;
    public bool machine_gun;
    public bool shotgun1;
    public bool flame_shotgun;
    

    public ParticleSystem[] flames;
    public GameObject flame_object;
    
    public int[] shotgun_pellets;
    
    private bool manual_can_shoot = true;

    //UPDATING GUN SYSTEM
    public bool add_spread = true;
    public Vector3 bullet_spread_variance = new Vector3(.1f, .1f, .1f);
    public TrailRenderer bullet_trail;
    public LayerMask mask;

    //EFFECTS
    public ParticleSystem blood_spray;
    public ParticleSystem blood_spray_green;
    public ParticleSystem stone_hit_particles;
    public ParticleSystem shotun_flame;

    //CAMSHAKE
    public Camshake Camera_shake;
    public Player_cam player_Cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            is_firing = true;
            if (is_flame_thrower) 
            {
                foreach(ParticleSystem f in flames)
                {
                    f.Play();
                    Vector3 p_pos = player_Cam.proper_pos;
                    Camera_shake.shake(0.2f, p_pos, 4f);
                }
                
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            is_firing= false;
            if (is_flame_thrower)
            {
                foreach (ParticleSystem f in flames)
                {
                    f.Stop();
                }

            }
        }
        if (is_firing )
        {
            if (is_grenade_launcher)
            {
                ShotCounter -= Time.deltaTime;
                if (ShotCounter <= 0)
                {
                    ShotCounter = TimeBetweenShots;
                    BULLET1script newBullet = Instantiate(bullet, firepoint.position, firepoint.rotation) as BULLET1script;
                    //newBullet.SPEED = BulletSpeed;
                }
            }
            if (!is_automatic && manual_can_shoot == true && is_grenade_launcher == false) 
            {
                ShotCounter -= Time.deltaTime;
                if (ShotCounter <= 0)
                {
                    ShotCounter = TimeBetweenShots;

                    //shake region
                    if (gun1)
                    {
                        Vector3 p_pos = player_Cam.proper_pos;
                        Camera_shake.shake(0.2f, p_pos, 2);
                    }
                    if (shotgun1)
                    {
                        Vector3 p_pos = player_Cam.proper_pos;
                        Camera_shake.shake(0.2f, p_pos, 0.25f);
                    }



                    foreach (int p in shotgun_pellets) 
                    {
                        Vector3 direction = Get_direction();
                        if (Physics.Raycast(firepoint.position, direction, out RaycastHit hit, float.MaxValue))
                        {
                            TrailRenderer trail = Instantiate(bullet_trail, firepoint.position, Quaternion.identity);
                            StartCoroutine(Spawntrail(trail, hit));
                            /*if (is_flame_shotgun && hit.collider.isTrigger == false)
                            {
                                ParticleSystem spawned_particles = Instantiate(shotun_flame, hit.point, Quaternion.LookRotation(hit.normal));
                                spawned_particles.transform.SetParent(hit.collider.transform);
                            }*/
                            if (hit.collider.isTrigger)
                            {
                                if (hit.collider.tag == "ZOMBEAN")
                                {
                                    hit.collider.gameObject.GetComponent<Zombean_1>().Damage();
                                    Instantiate(blood_spray, hit.point, Quaternion.LookRotation(hit.normal));
                                    if (is_flame_shotgun)
                                    {
                                        hit.collider.gameObject.GetComponent<Zombean_1>().catch_fire();
                                        
                                    }


                                    
                                }
                                if (hit.collider.tag == "ZOMBEAN2")
                                {
                                    hit.collider.gameObject.GetComponent<Zombean_2>().Damage();
                                    Instantiate(blood_spray, hit.point, Quaternion.LookRotation(hit.normal));
                                    if (is_flame_shotgun)
                                    {
                                        hit.collider.gameObject.GetComponent<Zombean_2>().catch_fire();
                                        
                                    }

                                }
                                if (hit.collider.tag == "ZOMBEAN3")
                                {
                                    hit.collider.gameObject.GetComponent<Zombean_1>().Damage();
                                    Instantiate(blood_spray_green, hit.point, Quaternion.LookRotation(hit.normal));
                                    if (is_flame_shotgun)
                                    {
                                        hit.collider.gameObject.GetComponent<Zombean_1>().catch_fire();
                                        
                                    }

                                }

                            }
                            if (hit.collider.tag == "STONE")
                            {
                                print("stone_hit");
                                //Instantiate(stone_hit_particles, hit.point, Quaternion.LookRotation(hit.normal));
                                ParticleSystem spawned_particles = Instantiate(stone_hit_particles, hit.point, Quaternion.LookRotation(hit.normal));
                                spawned_particles.transform.SetParent(hit.collider.transform);
                                /*if (is_flame_shotgun)
                                {
                                    ParticleSystem spawned_particles2 = Instantiate(shotun_flame, hit.point, Quaternion.LookRotation(hit.normal));
                                    spawned_particles2.transform.SetParent(hit.collider.transform);
                                }*/
                            }
                        }
                    }


                }
                manual_can_shoot = false;
            }

            else if (is_automatic && is_grenade_launcher == false)
            {
                
                ShotCounter -= Time.deltaTime;
                if (ShotCounter <= 0)
                {
                    //shake region
                    if (cheese_grater)
                    {
                        Vector3 p_pos = player_Cam.proper_pos;
                        Camera_shake.shake(0.2f, p_pos, 2);
                    }
                    if (machine_gun)
                    {
                        Vector3 p_pos = player_Cam.proper_pos;
                        Camera_shake.shake(0.2f, p_pos, 1f);
                    }
                    ShotCounter = TimeBetweenShots;
                    /*BULLET1script newBullet = Instantiate(bullet, firepoint.position, firepoint.rotation) as BULLET1script;
                    newBullet.SPEED = BulletSpeed;*/

                    foreach (int p in shotgun_pellets) 
                    {
                        Vector3 direction = Get_direction();

                        if (Physics.Raycast(firepoint.position, direction, out RaycastHit hit, float.MaxValue))
                        {
                            TrailRenderer trail = Instantiate(bullet_trail, firepoint.position, Quaternion.identity);
                            StartCoroutine(Spawntrail(trail, hit));
                            if (hit.collider.isTrigger)
                            {
                                if (hit.collider.tag == "ZOMBEAN")
                                {
                                    hit.collider.gameObject.GetComponent<Zombean_1>().Damage();
                                    Instantiate(blood_spray, hit.point, Quaternion.LookRotation(hit.normal));
                                }
                                if (hit.collider.tag == "ZOMBEAN2")
                                {
                                    hit.collider.gameObject.GetComponent<Zombean_2>().Damage();
                                    Instantiate(blood_spray, hit.point, Quaternion.LookRotation(hit.normal));
                                }
                                if (hit.collider.tag == "ZOMBEAN3")
                                {
                                    hit.collider.gameObject.GetComponent<Zombean_1>().Damage();
                                    Instantiate(blood_spray_green, hit.point, Quaternion.LookRotation(hit.normal));
                                }

                            }
                            if (hit.collider.tag == "STONE")
                            {
                                //print("stone_hit");
                                //Instantiate(stone_hit_particles, hit.point, Quaternion.LookRotation(hit.normal));
                                ParticleSystem spawned_particles = Instantiate(stone_hit_particles, hit.point, Quaternion.LookRotation(hit.normal));
                                spawned_particles.transform.SetParent(hit.collider.transform);
                            }
                        }
                    }


                }
            }


                
        }else{
            ShotCounter = 0;
        
        }
        if (Input.GetMouseButtonUp(0)) 
        {
            manual_can_shoot = true;
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
