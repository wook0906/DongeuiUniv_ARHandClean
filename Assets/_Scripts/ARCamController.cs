using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARCamController : MonoBehaviour
{
    public static ARCamController S;
    //public float speed;
    //public VariableJoystick variableJoystick;
    public VariableJoystick horizontal;
    public VariableJoystick vertical;

    void Awake()
    {
        S = this;
    }

    // Update is called once per .0frame
    void FixedUpdate()
    {
        
        //Debug.Log(transform.position);
        //Vector3 direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
        //if (direction.magnitude > 0f)
        //{
        //    transform.position += direction * Time.deltaTime * speed;
        //}
    }

}
