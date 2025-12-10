using UnityEngine;

public class Flame_particles : MonoBehaviour
{
    private ParticleSystem flame;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        flame = GetComponent<ParticleSystem>();
        ParticleSystem.CollisionModule collision_mod = flame.collision;
        collision_mod.sendCollisionMessages = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        
        if (other.transform.tag == "ZOMBEAN") 
        {
            print("zombean hit by flame");
            other.gameObject.GetComponent<Zombean_flame_reaction>().catch_fire();
        }
    }
}
