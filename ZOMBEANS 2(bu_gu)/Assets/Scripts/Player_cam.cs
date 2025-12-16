using UnityEngine;

public class Player_cam : MonoBehaviour
{
    public Transform target;
    public Vector3 proper_pos;
    private Camera cam;

    //SNIPE MODE;
    public float max_fov;
    public float min_fov;
    public float sensitivity = 10f;
    public bool snipe_mode;

    public float mouse_sensitivity = 100f;
    private float x_rotation = 0f;

    public GameObject snipe_aim;

    public float sensx;
    public float sensy;
    float xrotation;
    float yrotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.LookAt(target);
        
        if (snipe_mode)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            float fov = Camera.main.fieldOfView;
            fov -= scroll * sensitivity;
            fov = Mathf.Clamp(fov, min_fov,max_fov);
            Camera.main.fieldOfView = fov;

            float mousex = Input.GetAxis("Mouse X") * mouse_sensitivity * Time.deltaTime;
            float mousey = Input.GetAxis("Mouse Y") * mouse_sensitivity * Time.deltaTime;

            xrotation -= mousey;
            xrotation = Mathf.Clamp(xrotation, -90f, 90f);
            transform.localRotation = Quaternion.Euler(xrotation, 0, 0);
            transform.Rotate(Vector3.down * mousex);

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out RaycastHit hitinfo))
            {
                print("cam hit");

            }

            
          
        }
        else
        {
            proper_pos = transform.position;
            transform.position = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
        }
        
    }
}
