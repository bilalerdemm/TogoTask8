using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using UnityEngine.UIElements;

public class PathMovement : MonoBehaviour
{
    public PathCreator pathCreator;
    public float speed = 5f;
    float distanceTravelled;

    public int currentDough;
    public bool giveDough;
    public Transform doughStackPoint;
    public GameObject breadDough;
    public List<GameObject> breadDoughList;


    public float timer = 1f;
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            distanceTravelled += speed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled) + new Vector3(0, 4, 0);
        }
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if (timer <= 0)
        {
            giveDough = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("DoughStack") && currentDough <= 4 && giveDough)
        {
            DoughStackMechanic();
        }
    }
    void DoughStackMechanic()
    {
        giveDough = false;
        timer = 1f;
        currentDough++;
        doughStackPoint.position += new Vector3(0, .5f, 0);
        GameObject newBreadDough = Instantiate(breadDough);
        newBreadDough.transform.transform.parent = transform;
        newBreadDough.transform.position = doughStackPoint.position;
        breadDoughList.Add(newBreadDough);


        //breadDough.transform.SetParent(transform, true);
        Debug.Log("Hamur stackle");
        //StartCoroutine(DoughStackMechanic());
    }
}
