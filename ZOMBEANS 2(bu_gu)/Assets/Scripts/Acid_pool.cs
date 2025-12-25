using UnityEngine;

public class Acid_pool : MonoBehaviour
{
    private ParticleSystem particles;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        particles = GetComponent<ParticleSystem>();
        ParticleSystem.CollisionModule collision_mod = particles.collision;
        collision_mod.sendCollisionMessages = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        print("hacid touched something");
    }
}
