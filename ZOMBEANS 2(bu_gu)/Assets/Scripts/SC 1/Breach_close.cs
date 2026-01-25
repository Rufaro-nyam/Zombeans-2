using UnityEngine;
using UnityEngine.UI;

public class Breach_close : MonoBehaviour
{
    public float max_health = 10;
    private float current_health;
    public Image health_sprite;
    public Transform cam;
    public GameObject main_sprite;
    public bool in_range = false;
    public GameObject breach_spawn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        current_health = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);
        health_sprite.fillAmount = current_health / max_health;
        if (Input.GetKey(KeyCode.Q) && in_range)
        {
            current_health += 0.5f * Time.deltaTime;
            if(health_sprite.fillAmount == 1)
            {
                close_breach();
            }
        }

    }

    public void close_breach()
    {
        breach_spawn.SetActive(false);
        Destroy(gameObject);
    }
}
