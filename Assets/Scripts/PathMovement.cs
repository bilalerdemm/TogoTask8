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

    public GameObject breadDoughPoint;


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
        if (other.gameObject.CompareTag("GiveDough"))
        {
            //Debug.Log("Hmaurlari ver ");
            GiveDoughMechanic();
        }
    }
    void DoughStackMechanic()
    {
        Debug.Log("Hamur stackle");
        giveDough = false;
        timer = 1f;
        currentDough++;
        doughStackPoint.position += new Vector3(0, .5f, 0);
        GameObject newBreadDough = Instantiate(breadDough);
        newBreadDough.transform.transform.parent = transform;
        newBreadDough.transform.position = doughStackPoint.position;
        breadDoughList.Add(newBreadDough);
    }
    void GiveDoughMechanic()
    {
        if (breadDoughList.Count > 0)
        {
            for (int i = 0; i < breadDoughList.Count; i++)
            {
                Debug.Log("Hamurlari ver ");
                breadDoughList[breadDoughList.Count - 1].gameObject.transform.parent = breadDoughPoint.transform;
                breadDoughList[breadDoughList.Count - 1].gameObject.transform.position = breadDoughPoint.transform.position;
                breadDoughList.Remove(breadDoughList[breadDoughList.Count - 1].gameObject);
            }
        }
    }
}
