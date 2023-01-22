using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

using DG.Tweening;
using UnityEngine.UI;

public class PathMovement : MonoBehaviour
{
    public static PathMovement instance;

    public PathCreator pathCreator;
    public float speed = 5f;
    float distanceTravelled;

    public bool giveDough;
    public bool isPlayerTakeDough = false;
    public bool giveBread;
    public bool isFrontBake = false;

    public Transform doughMachine;
    public Transform doughStackPoint;
    public Transform movingBreadPoint;
    public Vector3 firstDoughStackPoint;
    
    public List<GameObject> breadDoughList;
    public List<GameObject> bakedBreadList;
    public List<GameObject> movingBreadList;
    
    public GameObject breadDough;
    public GameObject breadDoughPoint;
    public GameObject bakedBreadPoint;

    public float stackTimer = 0f;
    public float breadTimer = 0f;

    public Animator playerAnim;

    public Material breadMat;

    public Image fillImage1, fillImage2;

    private void Awake()
    {
        firstDoughStackPoint = doughStackPoint.localPosition;
        instance = this;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            playerAnim.SetBool("isRunning", true);
            distanceTravelled += speed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled) + new Vector3(0, 4, 0);
            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);
        }
        if (Input.GetMouseButtonUp(0))
        {

            playerAnim.SetBool("isRunning", false);
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

        if (isFrontBake)
        {
            #region  BreadTimer
            if (breadTimer < 1)
            {
                breadTimer += Time.deltaTime;
            }
        }
        #endregion
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("TakeBread"))
        {
            Debug.Log("Ekmekleri al usta");
            //for (int i = 0; i < bakedBreadList.Count; i++)
            //{
            //    bakedBreadList[i].gameObject.transform.parent = movingBreadPoint.transform;
            //    bakedBreadList[i].gameObject.transform.localPosition = Vector3.zero;
            //    //bakedBreadList[i].gameObject.transform.DOMove(movingBreadPoint.transform.position, .1f);
            //    movingBreadList.Add(bakedBreadList[i].gameObject);
            //}

            foreach (var item in bakedBreadList)
            {
                movingBreadList.Add(item.gameObject);
                item.transform.SetParent(transform.root);
            }
            bakedBreadList.Clear();
            foreach (var item in movingBreadList)
            {
                item.transform.DOLocalMove(movingBreadPoint.transform.localPosition, .25f);
            }
        }
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
            isFrontBake = true;
            //Hamur stackledigim pointi en bastaki pozisyonuna cekiyorum
            doughStackPoint.localPosition = firstDoughStackPoint;

            fillImage2.fillAmount = breadTimer;

            //HAMURLARI FIRINA VERDIGIMIZ YER 
            GiveDoughMechanic();

            StartCoroutine(BakingBread());
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("DoughStack"))
        {
            isPlayerTakeDough = false;
            fillImage1.fillAmount = 0;
            giveDough = true;
        }
        if (other.gameObject.CompareTag("GiveDough"))
        {
            isFrontBake = false;
            fillImage2.fillAmount = 0;
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
                newBreadDough.transform.DOMove(doughStackPoint.transform.position, .03f);
                doughStackPoint.position += new Vector3(0, .5f, 0);
                newBreadDough.transform.parent = transform;
                breadDoughList.Add(newBreadDough);
            }
        }
    }
    void GiveDoughMechanic()
    {
        if (fillImage2.fillAmount >= 1)
        {
            if (breadDoughList.Count > 0)
            {
                for (int i = 0; i < breadDoughList.Count; i++)
                {
                    Debug.Log("Hamurlari ver ");
                    breadDoughList[i].gameObject.transform.parent = breadDoughPoint.transform;

                    breadDoughList[i].gameObject.transform.position = breadDoughPoint.transform.position;

                    bakedBreadList.Add(breadDoughList[i].gameObject);
                }
                breadDoughList.Clear();
            }
        }

    }
    IEnumerator BakingBread()
    {
        for (int i = 0; i < bakedBreadList.Count; i++)
        {
            bakedBreadList[i].gameObject.transform.DOMove(bakedBreadPoint.transform.position, .1f);
            var renderer = bakedBreadList[i].gameObject.GetComponent<SkinnedMeshRenderer>();
            renderer.material = breadMat;
            yield return new WaitForSeconds(.1f);
        }
    }
}
