using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class PathMovement : MonoBehaviour
{
    public PathCreator pathCreator;
    public float speed = 5f;
    float distanceTravelled;
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            distanceTravelled += speed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled) + new Vector3(0, 4, 0);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("DoughStack"))
        {
            Debug.Log("Hamur stackle");
        }
    }
}
