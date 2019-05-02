using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bridge1_1 : MonoBehaviour
{
    private Vector3 startpos;
    private Vector3 endpos;
    private bool move_up = false;
    private bool move_down = false;
    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position;
        endpos = startpos + (Vector3.up * 5);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Sphere").GetComponent<Control_a>().switch1)
        {
            move_up = true;
            move_down = false;
            //transform.position = endpos;
        }
        else if (transform.position == endpos)
        {
            move_down = true;
            move_up = false;
            //transform.position = startpos;
        }
        if (move_up && transform.position.y <= endpos.y)
        {
            transform.position = Vector3.Lerp(transform.position, endpos, Time.time / 10);
        }
        else if (transform.position.y >= startpos.y)
        {
            transform.position = Vector3.Lerp(transform.position, startpos, Time.time / 10);
        }
    }
}
