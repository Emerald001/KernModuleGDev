using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidsManager : MonoBehaviour
{
    [SerializeField] private float spawnAmount;
    [SerializeField] private float SeperationAmount = 1;
    [SerializeField] private float Speed = 1;
    [SerializeField] private float Cohesion = 1f;
    [SerializeField] private float BoxSize = 1f;
    [SerializeField] private float BorderJump = 1f;

    public List<GameObject> Boids = new List<GameObject>();
    public GameObject BoidPrefab;

    private void Start()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            var tmp = Instantiate(BoidPrefab, new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), Random.Range(-5f, 5f)), Quaternion.identity);
            Boids.Add(tmp);
        }

        Time.timeScale = .05f;
    }

    private void Update()
    {
        MoveAllBoidsToNewPosition();
    }

    public void MoveAllBoidsToNewPosition()
    {
        Vector3 v1 = Vector3.zero, v2 = Vector3.zero, v3 = Vector3.zero, v4 = Vector3.zero;

        for (int i = 0; i < Boids.Count; i++)
        {
            var tmp = Boids[i].GetComponent<BoidObject>();

            v1 = Rule1(tmp);
            v2 = Rule2(tmp);
            v3 = Rule3(tmp);
            //v4 = Rule4(tmp);

            tmp.velocity += v1 + v2 + v3; 
            var distance = Vector3.Distance(transform.position, tmp.transform.position);
            if (distance > BoxSize)
            {
                tmp.velocity = (transform.position - Boids[i].transform.position).normalized * BorderJump;
            }
            tmp.UpdateBoids();
        }

    }

    public Vector3 Rule1(BoidObject B)
    {
        Vector3 AvPo = Vector3.zero;

        for (int i = 0; i < Boids.Count; i++)
        {
            if(B == Boids[i])
                continue;

            AvPo += Boids[i].transform.position;
        }

        AvPo = AvPo / (Boids.Count - 1);

        return (AvPo - B.transform.position) / Cohesion;
    }

    public Vector3 Rule2(BoidObject B)
    {
        Vector3 c = Vector3.zero;

        for (int i = 0; i < Boids.Count; i++)
        {
            if (B == Boids[i])
                continue;

            if(Vector3.Distance(Boids[i].transform.position, B.transform.position) < SeperationAmount)
            {
                c -= Boids[i].transform.position - B.transform.position;
            }
        }

        return c;
    }

    public Vector3 Rule3(BoidObject B)
    {
        Vector3 Vo = Vector3.zero;

        for (int i = 0; i < Boids.Count; i++)
        {
            if (B == Boids[i])
                continue;

            Vo += Boids[i].GetComponent<BoidObject>().velocity * Time.deltaTime;
        }

        Vo = Vo / (Boids.Count - 1);

        return (Vo - B.GetComponent<BoidObject>().velocity * Time.deltaTime) / Speed;
    }

    public Vector3 Rule4(BoidObject B) { 
        float Xmin = -BoxSize, Xmax = BoxSize, Ymin = -BoxSize, Ymax = BoxSize, Zmin = -BoxSize, Zmax = BoxSize;
        Vector3 v = Vector3.zero;

        if (B.transform.position.x < Xmin)
            v.x = 10;
        else if (B.transform.position.x > Xmax)
            v.x = -10;

        if (B.transform.position.y < Ymin)
            v.y = 10;
        else if (B.transform.position.y > Ymax)
            v.y = -10;

        if (B.transform.position.z < Zmin)
            v.z = 10;
        else if (B.transform.position.z > Zmax)
            v.z = -10;

        return v;
    }
}
