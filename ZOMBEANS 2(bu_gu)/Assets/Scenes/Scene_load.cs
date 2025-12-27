using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_load : MonoBehaviour
{
    float time;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time > 2)
        {
            SceneManager.LoadScene(1);
        }
    }
}
