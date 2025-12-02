using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecalDestroyer : MonoBehaviour {

	public float lifeTime = 5.0f;
	public ParticleSystem particles;
	public bool blood = false;
	private IEnumerator Start()
	{
		yield return new WaitForSeconds(lifeTime);
		Destroy(particles);
		if (blood)
		{
			Destroy(gameObject);
		}

	}
}
