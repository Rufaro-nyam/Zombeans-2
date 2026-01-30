using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DecalDestroyer : MonoBehaviour {

	public float lifeTime = 5.0f;
	public ParticleSystem particles;
	public bool blood = false;
	public AudioSource hit_effect;
	public bool is_blood;
	
	private IEnumerator Start()
	{
		if (is_blood)
		{
            hit_effect.pitch = UnityEngine.Random.Range(1f, 1.5f);
            hit_effect.Play();
        }


        yield return new WaitForSeconds(lifeTime);
		Destroy(particles);
		if (blood)
		{
			Destroy(gameObject);
			print("blood destroyed");
		}

	}
}
