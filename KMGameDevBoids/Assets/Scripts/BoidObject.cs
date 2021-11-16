using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidObject : MonoBehaviour
{
    public Vector3 position, velocity, direction;


    private void Update()
    {
        transform.position = position;


    }
}
