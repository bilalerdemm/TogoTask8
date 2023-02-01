using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerPath : MonoBehaviour
{
    public PathCreator pathCreator;
    public float speed = 5f;
    float distanceTravelled;
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            distanceTravelled += speed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
        }
    }
}
