using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidsManager : MonoBehaviour
{
    [SerializeField] private float spawnAmount;

    public List<GameObject> Boids = new List<GameObject>();
    public GameObject BoidPrefab;

    private void Start()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            var tmp = Instantiate(BoidPrefab);
            var tmpComponents = tmp.GetComponent<BoidObject>();
            tmpComponents.position = Vector3.zero;
            tmpComponents.velocity = Vector3.zero;
            tmpComponents.direction = Vector3.zero;
            Boids.Add(tmp);
        }

        Debug.Log(Boids.Count);
    }

    public void MoveAllBoidsToNewPosition()
    {
        Vector3 v1 = Vector3.zero, v2 = Vector3.zero, v3 = Vector3.zero;

        for (int i = 0; i < Boids.Count; i++)
        {
            var tmp = Boids[i].GetComponent<BoidObject>();

            v1 = Rule1(tmp);
            v2 = Rule2(tmp);
            v3 = Rule3(tmp);

            tmp.velocity += v1 + v2 + v3;
            tmp.position += tmp.velocity;
        }
    }

    public Vector3 Rule1(BoidObject B)
    {
        Vector3 AvPo = Vector3.zero;

        for (int i = 0; i < Boids.Count; i++)
        {
            if(B == Boids[i])
                continue;

            AvPo += Boids[i].GetComponent<BoidObject>().position;
        }

        AvPo /= Boids.Count - 1;

        return (AvPo - B.position) / 100;
    }

    public Vector3 Rule2(BoidObject B)
    {
        Vector3 c = Vector3.zero;

        for (int i = 0; i < Boids.Count; i++)
        {
            if (B == Boids[i])
                continue;

            c -= B.position - Boids[i].GetComponent<BoidObject>().position;
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

            Vo += Boids[i].GetComponent<BoidObject>().velocity;
        }

        Vo /= Boids.Count - 1;

        return Vo;
    }
}
