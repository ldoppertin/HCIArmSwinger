using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float m_speed = 1;
    [SerializeField]
    private float m_threshold = 1;

    [SerializeField]
    private XRController left_hand;
    [SerializeField]
    private XRController right_hand;

    private Rigidbody rigid_body;

    private float last_timestep;
    private Vector3 last_difference;

    // Start is called before the first frame update
    void Start()
    {
        last_timestep = 0f;
        rigid_body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 hand_difference = left_hand.transform.position - right_hand.transform.position;
        Debug.Log("hand difference: " + hand_difference);

        Vector3 new_difference = hand_difference - last_difference;

        if ((Mathf.Abs(new_difference[0]) > m_threshold) || (Mathf.Abs(new_difference[1]) > m_threshold) || (Mathf.Abs(new_difference[2]) > m_threshold))
        {
            rigid_body.velocity = transform.forward * m_speed; // forward of headset?
        }

        last_timestep = Time.deltaTime;
        last_difference = new_difference;
    }
}
