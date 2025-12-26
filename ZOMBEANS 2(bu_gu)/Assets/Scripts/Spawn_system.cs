using System.Collections;
using UnityEngine;

public class Spawn_system : MonoBehaviour
{
    public Transform[] spawn_positions;
    public GameObject zombean_1;
    public Transform player;
    public Transform test_pos;
    private int spawn_amount = 0;
    private int target_spawn_amount = 400;
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
            int num = Random.Range(0, 100);
            float dist = Vector3.Distance(spawn_positions[num].position, player.position);
            if (dist > 30f)
            {
                Instantiate(zombean_1, spawn_positions[num].transform.position, Quaternion.identity);
                print("zombean_spawned");
                spawn_amount += 1;
                yield return new WaitForSeconds(1);
                //StartCoroutine(start_spawn());
            }
            else
            {
                //StartCoroutine(start_spawn());
            }
            
        }
        print("spawning completed");
        



    }
}
