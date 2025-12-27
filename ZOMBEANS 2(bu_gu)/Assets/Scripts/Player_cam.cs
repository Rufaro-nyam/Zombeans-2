using System.Collections;
using UnityEngine;

public class Player_cam : MonoBehaviour
{
    public Transform target;
    public Vector3 proper_pos;
    public float y_diff;
    public Vector3 cam_rot;

    //SNIPE MODE;
    public float max_fov;
    public float min_fov;
    public float sensitivity = 10f;
    public bool snipe_mode;
    public GameObject snipe_scope;

    //LOOKING
    public Camera cam;

    public float sensx;
    public float sensy;
    public Transform orientation;
    float xrotation;
    float yrotation;

    //EFFECTS
    public ParticleSystem blood_spray;
    public ParticleSystem blood_spray_green;
    public ParticleSystem stone_hit_particles;
    public TrailRenderer bullet_trail;
    public Transform firepoint;

    //CAMSHAKE
    public Camshake camera_shake;
    
    



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        if (snipe_mode)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            snipe_scope.SetActive(true);
        }
        else
        {
            transform.rotation = Quaternion.Euler(cam_rot);
            snipe_scope.SetActive(false);
        }
        
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
        Destroy(trail.gameObject, trail.time);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.LookAt(target);
        
        if (snipe_mode)
        {
           

            float scroll = Input.GetAxis("Mouse ScrollWheel");
            float fov = Camera.main.fieldOfView;
            fov -= scroll * sensitivity;
            fov = Mathf.Clamp(fov, min_fov,max_fov);
            Camera.main.fieldOfView = fov;

            //mouse look
            float mousex = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensx;
            float mousey = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensy;

            yrotation += mousex;
            xrotation -= mousey;
            xrotation = Mathf.Clamp(xrotation, -45f, 45f);
            yrotation = Mathf.Clamp(yrotation, -90f, 120f);
            transform.rotation = Quaternion.Euler(xrotation, yrotation, 0);
            orientation.rotation = Quaternion.Euler(0, yrotation, 0);


            //shooting
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Input.GetMouseButtonDown(0)) 
            {
                
                if (Physics.Raycast(ray, out RaycastHit hitinfo))
                {
                    print(hitinfo.collider.name);
                    Vector3 p_pos = proper_pos;
                    camera_shake.shake(0.2f, p_pos, 1f);
                    TrailRenderer trail = Instantiate(bullet_trail, firepoint.position, Quaternion.identity);
                    StartCoroutine(Spawntrail(trail, hitinfo));
                    if (hitinfo.collider.isTrigger)
                    {
                        
                        if (hitinfo.collider.tag == "ZOMBEAN")
                        {
                            print("sniper hit zombean");
                            hitinfo.collider.gameObject.GetComponent<Zombean_1>().Sniper_Damage(transform);
                            Instantiate(blood_spray, hitinfo.point, Quaternion.LookRotation(hitinfo.normal));
                        }


                    }
                    if (hitinfo.collider.tag == "STONE")
                    {
                        print("stone_hit");
                        //Instantiate(stone_hit_particles, hit.point, Quaternion.LookRotation(hit.normal));
                        ParticleSystem spawned_particles = Instantiate(stone_hit_particles, hitinfo.point, Quaternion.LookRotation(hitinfo.normal));
                        spawned_particles.transform.SetParent(hitinfo.collider.transform);
                        /*if (is_flame_shotgun)
                        {
                            ParticleSystem spawned_particles2 = Instantiate(shotun_flame, hit.point, Quaternion.LookRotation(hit.normal));
                            spawned_particles2.transform.SetParent(hit.collider.transform);
                        }*/
                    }

                }
            }
            

            
          
        }
        else
        {
            proper_pos = transform.position;
            transform.position = new Vector3(target.transform.position.x, target.transform.position.y + y_diff , target.transform.position.z);
            
        }
        
    }




}
