using UnityEngine;

public class Cam_follow1 : MonoBehaviour
{
    [SerializeField]private Vector3 cam_offset = new Vector3(0, 10, 2);
    //[SerializeField]private Transform target;
    [SerializeField]private float zoom_out = 2f;
    //[SerializeField]private float smoothSpeed = 0.2f;
    [SerializeField]private GameObject player_1;

    //[SerializeField]private Space offsetPositionSpace = Space.Self;
    //[SerializeField]private bool lookAt = true;


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void LateUpdate()
    {
        //GameObject player1 = GameObject.Find("Sphere");
        Control_a control_a = player_1.GetComponent<Control_a>();
        //cam_offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * 4, Vector3.up) * Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * 4, Vector3.right) * cam_offset;
        //cam_offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * 4, Vector3.up) * cam_offset;

        //cam_offset = Quaternion.AngleAxis(control_a.get_input[1, 2] *-2, Vector3.up) * cam_offset;
        //transform.position = target.position + cam_offset;
        Vector3 wantedPostion = player_1.transform.position + cam_offset * zoom_out;
        //Vector3 smoothPostion = Vector3.Lerp(transform.position, wantedPostion, smoothSpeed);
        transform.position = wantedPostion;

        //transform.LookAt(target.position);
        //Debug.Log(Input.GetAxis("Mouse X").ToString()+ "   "+ Input.GetAxis("Mouse Y").ToString());

    }

}
