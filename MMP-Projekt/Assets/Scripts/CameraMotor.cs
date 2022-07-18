using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraMotor : MonoBehaviour
{

    public GameObject target;
    public Vector4 border = new Vector4(10, 10, -10, -10); // right - top - left - down
    private float x;
    private float y;

    void LateUpdate()
    {
        if (target)
        {
            x = target.transform.position.x;
            y = target.transform.position.y;

            if (x > border.x) x = border.x;
            else if (x < border.z) x = border.z;

            if (y > border.y) y = border.y;
            else if (y < border.w) y = border.w;

            transform.position = new Vector3(x, y, -10);
        }
    }


}