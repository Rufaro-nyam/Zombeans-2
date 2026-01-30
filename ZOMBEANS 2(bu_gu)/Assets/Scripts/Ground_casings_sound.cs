using UnityEngine;

public class Ground_casings_sound : MonoBehaviour
{
    public AudioSource casing_drop;
    private float master_volume = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        
        if(other.gameObject.name == "Bullet_cases")
        {
            
            print(other.gameObject.name);
            play_sound();
        }
        
    }

    private void play_sound()
    {
        if(casing_drop.isPlaying == false)
        {
            casing_drop.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
            casing_drop.Play();
            master_volume -= 0.1f;
            casing_drop.volume = master_volume;
        }
        if(master_volume<= 0)
        {
            master_volume = 1;
        }
        
    }
}
