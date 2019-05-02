using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lift_1 : MonoBehaviour
{
    private Vector3 start_tf;
    [SerializeField] private Vector3 end_tf;
    [SerializeField] [Range(0f, 60f)] private float delayy = 1f;
    // Start is called before the first frame update
    void Start()
    {
        start_tf = transform.position;
        end_tf = start_tf + new Vector3(0, 70, 0);
    }
    // Update is called once per frame
    void Update()
    {
            transform.position = Vector3.Lerp(start_tf, end_tf,
                                                 Mathf.SmoothStep(0f, 1f,
                                                 Mathf.PingPong((Time.time-delayy) / 14f, 1f)
                                                 ));
    }
}
