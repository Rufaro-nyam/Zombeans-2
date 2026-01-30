using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Spawn_system : MonoBehaviour
{
    public Transform[] spawn_positions;
    public GameObject[] zombeans;
    public Transform player;
    public Transform test_pos;
    private int spawn_amount = 0;
    private int target_spawn_amount = 100;
    private int killed_amount = 0;
    private int wave = 0;
    private int zombean_range = 0;

    public int max_to_spawn;
    public float time_between_spawns;


    private bool added_zmb2 = false;
    private bool added_spitter;
    private bool added_expzmb;


    public Transform backup_spawn;

    private bool corpse_wipeout = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(start_spawn());
        }
    }

    public IEnumerator start_spawn()
    {

        while(spawn_amount < target_spawn_amount)
        {
            int num = Random.Range(0, max_to_spawn);
            float dist = Vector3.Distance(spawn_positions[num].position, player.position);
            int random_bean = Random.Range(0, zombean_range);
            if (dist > 30f )
            {
                if( spawn_positions[num].gameObject.active == true)
                {
                    Instantiate(zombeans[random_bean], spawn_positions[num].transform.position, Quaternion.identity);
                    print("zombean_spawned");
                    spawn_amount += 1;
                    yield return new WaitForSeconds(time_between_spawns);
                    //StartCoroutine(start_spawn());
                }
                else
                {
                    GameObject rand_bean = Instantiate(zombeans[random_bean], backup_spawn.transform.position, Quaternion.identity);
                    print("zombean_spawned");
                    spawn_amount += 1;
                    yield return new WaitForSeconds(0.1f);
                    Destroy(rand_bean);
                    print("rand bean destroyed");
                    //StartCoroutine(start_spawn());
                }

            }
            else
            {
                //StartCoroutine(start_spawn());
                //StopAllCoroutines();
            }
            
        }
        print("spawning completed");
        



    }
    void wipeout()
    {

            Collider[] colliders = Physics.OverlapSphere(transform.position, 1000);
            foreach (Collider nearby in colliders)
            {
                if (nearby.tag == "ZOMBEAN")
                {
                    Destroy(nearby.gameObject);
                }
                if (nearby.tag == "ZOMBEAN2")
                {
                    Destroy(nearby.gameObject);
                }
                if (nearby.tag == "ZOMBEAN3")
                {
                    Destroy(nearby.gameObject);
                }
                if (nearby.tag == "ZOMBEAN4")
                {
                    Destroy(nearby.gameObject);
                }
        }
            corpse_wipeout = false;
        
    }

    public void add_zombean_death()
    {
        print("added to kill count");
        killed_amount += 1;
        if(killed_amount == target_spawn_amount)
        {
            print("all zombeans killed");
            killed_amount = 0;
            spawn_amount = 0;
            target_spawn_amount += 10;
            wipeout();
            StartCoroutine(start_spawn());
            wave += 1;
        }
        if(wave == 2 && added_zmb2 == false)
        {
            zombean_range = 2;
            print("zombean range is" + zombean_range);
            added_zmb2 = true;
        }
        if (wave == 4 && added_spitter == false)
        {
            zombean_range = 3;
            print("zombean range is" + zombean_range);
            added_spitter = true;
        }
        if (wave == 6 && added_expzmb == false)
        {
            zombean_range = 4;
            print("zombean range is" + zombean_range);
            added_expzmb = true;
        }
    }


}
