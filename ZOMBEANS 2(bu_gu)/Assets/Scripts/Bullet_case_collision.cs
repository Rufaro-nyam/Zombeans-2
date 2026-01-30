using UnityEngine;

public class Bullet_case_collision : MonoBehaviour
{
    public AudioSource case_drop;
    private ParticleSystem casings;
    private bool can_play = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        casings = GetComponent<ParticleSystem>();
        ParticleSystem.CollisionModule collision_mod = casings.collision;
        collision_mod.sendCollisionMessages = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        if (can_play)
        {
            print("case dropped");
            //case_drop.Play();
            can_play = false;
        }
        
    }

    

    
}
