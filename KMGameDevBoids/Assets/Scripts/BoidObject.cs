using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidObject : MonoBehaviour
{
    [SerializeField] private float speed = 6;
    
    public Vector3 velocity = Vector3.zero, direction;

    private void Start()
    {
        velocity = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    }

    public void UpdateBoids()
    {
        var dir = velocity - transform.position;

        transform.rotation = Quaternion.LookRotation(dir);

        transform.position += velocity * Time.deltaTime;
    }
}
