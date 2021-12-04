using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float m_speed = 10;
    [SerializeField]
    private float m_start_threshold = 0.01f;
    [SerializeField]
    private float m_stop_threshold = 0.001f;
    [SerializeField] 
    private GameObject camera_rig;
    [SerializeField]
    private XRController left_hand;
    [SerializeField]
    private XRController right_hand;
    private Rigidbody rigid_body;
    private Vector3 last_difference;

    // Start is called before the first frame update
    void Start()
    {
        rigid_body = GetComponent<Rigidbody>();
        last_difference = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 hand_difference = left_hand.transform.position - right_hand.transform.position;
        Vector3 difference_in_time = hand_difference - last_difference;
        Debug.Log("new difference: " + difference_in_time);

        if ((Mathf.Abs(difference_in_time[0]) > m_start_threshold) || (Mathf.Abs(difference_in_time[1]) > m_start_threshold) || (Mathf.Abs(difference_in_time[2]) > m_start_threshold))
        {
            rigid_body.transform.position += new Vector3(camera_rig.transform.forward.x, 0, camera_rig.transform.forward.z) * Time.deltaTime * m_speed;
            Debug.Log("läuft.");
        }
        else
        {
            if ((Mathf.Abs(rigid_body.velocity.x) > m_stop_threshold || (Mathf.Abs(rigid_body.velocity.y) > m_stop_threshold) || (Mathf.Abs(rigid_body.velocity.z) > m_stop_threshold)))
            {
                Debug.Log("wird langsamer.");
                rigid_body.velocity *= 0.5f;

            }
            else rigid_body.velocity = new Vector3(0, 0, 0); Debug.Log("stoppt.");
        }

        last_difference = hand_difference;
    }
}
