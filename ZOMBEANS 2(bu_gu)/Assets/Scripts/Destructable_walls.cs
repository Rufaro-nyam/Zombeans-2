using UnityEngine;

public class Destructable_walls : MonoBehaviour
{

    public GameObject mesh;

    float cubewidth;
    float cubeheight;
    float cubedepth;

    public float cubescale = 0.3f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        cubewidth = transform.localScale.z;
        cubeheight = transform.localScale.y;
        cubedepth = transform.localScale.x;

        //gameObject.GetComponent<MeshRenderer>().enabled = false;
        mesh.gameObject.GetComponent<Transform>().localScale = new Vector3(cubescale, cubescale, cubescale);
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        //CreateCube();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            CreateCube();
        }
    }

    void CreateCube()
    {
        for(float x = 0; x < cubewidth; x += cubescale)
        {
            for (float y = 0; y < cubeheight; y += cubescale)
            {
                for (float z = 0; z < cubedepth; z += cubescale)
                {
                    Vector3 vec = transform.position;
                    GameObject cubes = (GameObject)Instantiate(mesh, vec + new Vector3(x, y, z), transform.rotation);
                    cubes.gameObject.GetComponent<MeshRenderer>().material = gameObject.GetComponent<MeshRenderer>().material;
                }
            }
        }
    }
}
