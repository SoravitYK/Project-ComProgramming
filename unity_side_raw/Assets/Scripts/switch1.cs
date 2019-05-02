using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switch1 : MonoBehaviour
{
    private Vector3 startpos;
    private Vector3 endpos;
    private int counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position;
        endpos = startpos + (Vector3.down * 0.005f);
    }

    // Update is called once per frame
    void Update()
    {
        if (counter >= 1) {
            if(GameObject.Find("Sphere").GetComponent<Control_a>().switch1 == false) {
                GameObject.Find("Sphere").GetComponent<Control_a>().switch1 = true;
            }
        }
        else if(GameObject.Find("Sphere").GetComponent<Control_a>().switch1 == true)
        {
            GameObject.Find("Sphere").GetComponent<Control_a>().switch1 = false;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "key_box1")
        {
            counter++;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "key_box1")
        {
            counter--;
        }
    }

}
