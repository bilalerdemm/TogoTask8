using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

using DG.Tweening;
using UnityEngine.UI;

public class PathMovement : MonoBehaviour
{
    public PathCreator pathCreator;
    public float speed = 5f;
    float distanceTravelled;

    public bool giveDough;
    public bool isPlayerTakeDough = false;
    public bool giveBread;
    public Transform doughMachine;
    public Transform  doughStackPoint;
    public Vector3 firstDoughStackPoint;
    public GameObject breadDough;
    public List<GameObject> breadDoughList;
    public List<GameObject> bakedBreadList;

    public Image fillImage1;
    public GameObject breadDoughPoint;
    public GameObject bakedBreadPoint;


    public float stackTimer = 0f;
    public float breadTimer = 2f;

    private void Awake()
    {
        firstDoughStackPoint = doughStackPoint.localPosition;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            distanceTravelled += speed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled) + new Vector3(0, 4, 0);
            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);
        }
        if (stackTimer < 1)
        {
            stackTimer += Time.deltaTime;
        }
        if (stackTimer >= 1)
        {
            giveDough = true;

        }
        if (isPlayerTakeDough)
        {
            fillImage1.fillAmount = stackTimer;
            if (breadDoughList.Count >= 5)
            {
                giveDough = false;
                fillImage1.fillAmount = 0;
            }
        }
        #region
        if (breadTimer > 0)
        {
            breadTimer -= Time.deltaTime;
        }
        if (breadTimer <= 0)
        {
            giveBread = true;
        }
        #endregion
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("DoughStack") && breadDoughList.Count <= 4 && giveDough)
        {
            //HAMUR STACKLEDIGIMIZ YER.
            DoughStackMechanic();

        }

        if (other.gameObject.CompareTag("GiveDough"))
        {
            doughStackPoint.localPosition = firstDoughStackPoint;
            //HAMURLARI FIRINA VERDIGIMIZ YER 
            GiveDoughMechanic();
            //InvokeRepeating("BakingBread", 2f, .05f);
            BakingBread();
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("DoughStack"))
        {
            isPlayerTakeDough = false;
            breadTimer = 0;
            fillImage1.fillAmount = 0;
            giveDough = true;
        }
    }
    void DoughStackMechanic()
    {

        Debug.Log("Hamur stackle");
        isPlayerTakeDough = true;
        giveDough = false;
        stackTimer = 0f;
        if (fillImage1.fillAmount >= 1f)
        {
            if (isPlayerTakeDough)
            {
                Debug.Log("If icine girildi.");
                GameObject newBreadDough = Instantiate(breadDough, doughMachine.transform.parent);
                //newBreadDough.transform.parent = doughStackPoint.transform;
                newBreadDough.transform.DOMove(doughStackPoint.transform.position, .03f);
                doughStackPoint.position += new Vector3(0, .5f, 0);
                newBreadDough.transform.parent = transform;
                //newBreadDough.transform.position = doughStackPoint.position;
                breadDoughList.Add(newBreadDough);
            }
        }
    }
    #region
    void GiveDoughMechanic()
    {
        if (breadDoughList.Count > 0)
        {
            for (int i = 0; i < breadDoughList.Count; i++)
            {
                Debug.Log("Hamurlari ver ");
                breadDoughList[breadDoughList.Count - 1].gameObject.transform.parent = breadDoughPoint.transform;
                breadDoughList[breadDoughList.Count - 1].gameObject.transform.position = breadDoughPoint.transform.position;

                bakedBreadList.Add(breadDoughList[breadDoughList.Count - 1].gameObject);
                breadDoughList.Remove(breadDoughList[breadDoughList.Count - 1].gameObject);
            }
        }

    }
    void BakingBread()
    {
        if (breadTimer <= 0)
        {
            int currentIndex = bakedBreadList.Count - 1;
            for (int i = 0; i < bakedBreadList.Count; i++)
            {
                giveBread = false;
                breadTimer = 2f;
                bakedBreadList[currentIndex].gameObject.transform.DOMove(bakedBreadPoint.transform.position, 1f);
                currentIndex--;

            }

        }
        //giveBread = false;
        //breadTimer = 2f;



        /*
        if (breadDoughList.Count > 0)
        {
            giveBread = false;
            breadTimer = 2f;
            Debug.Log("Ekmek gitmeye hazir");
            //bakedBreadList[bakedBreadList.Count - 1].gameObject.transform.DOMove(bakedBreadPoint.transform.position, 2f);
            //bakedBreadPoint.transform.position = bakedBreadPoint.transform.position - new Vector3(0, 0, - 1.5f);
        }
        */
    }
    #endregion
}
