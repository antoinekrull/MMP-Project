using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraMotor : MonoBehaviour
{

    public GameObject target; // = player
    [SerializeField] Vector4 border; // right - top - left - down (player position)
    private float x, y;

    void LateUpdate()
    {         
        if (target)
        {
            x = target.transform.position.x; // get pos of player
            y = target.transform.position.y;

            if (x > border.x) x = border.x; // if pos is greater than border, use border
            else if (x < border.z) x = border.z;

            if (y > border.y) y = border.y;
            else if (y < border.w) y = border.w;

            transform.position = new Vector3(x, y, -10); // set cameras new pos
        }
    }


}