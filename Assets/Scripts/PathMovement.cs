using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

using DG.Tweening;
using UnityEngine.UI;
using Unity.VisualScripting;

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
    public Transform sellBreadPoint;
    public Vector3 firstDoughStackPoint;

    public List<GameObject> breadDoughList;
    public List<GameObject> bakedBreadList;
    public List<GameObject> movingBreadList;
    public List<GameObject> sellBreadList;

    public GameObject breadDough;
    public GameObject breadDoughPoint;
    public GameObject bakedBreadPoint;
    public GameObject sellStand;

    public float stackTimer = 0f;
    public float breadTimer = 0f;

    public int bakedBreadCount = 0;

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
        bakedBreadCount = bakedBreadList.Count;

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
            TakeBreadMechanic();
        }
        if (other.gameObject.CompareTag("Sell"))
        {
            SellBreadMechanic();
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
            //HAMURLARIN PISIP EKMEK OLDUGU YER

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
        if (other.gameObject.CompareTag("TakeBread"))
        {
            bakedBreadPoint.transform.localPosition = new Vector3(6, 6, 3);
        }
    }
    void DoughStackMechanic()
    {
        isPlayerTakeDough = true;
        giveDough = false;
        stackTimer = 0f;
        if (fillImage1.fillAmount >= 1f)
        {
            if (isPlayerTakeDough)
            {
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
                    bakedBreadList.Add(breadDoughList[i].gameObject);
                    StartCoroutine(BakingBread());
                    breadDoughList[i].gameObject.transform.parent = breadDoughPoint.transform;
                    breadDoughList[i].gameObject.transform.position = breadDoughPoint.transform.position;
                    breadDoughList.RemoveAt(i);
                }
            }
        }
    }
    IEnumerator BakingBread()
    {
        for (int i = 0; i < bakedBreadList.Count; i++)
        {
            bakedBreadList[i].gameObject.transform.DOMove(bakedBreadPoint.transform.position, .3f);
            var renderer = bakedBreadList[i].gameObject.GetComponent<SkinnedMeshRenderer>();
            renderer.material = breadMat;
            bakedBreadPoint.transform.localPosition += new Vector3(0, .125f, 0);
            yield return new WaitForSeconds(2f);
        }
    }
    public void TakeBreadMechanic()
    {
        int bakedBreadListCount = bakedBreadList.Count;
        for (int i = 0; i < bakedBreadListCount; i++)
        {
            movingBreadList.Add(bakedBreadList[0].gameObject);
            bakedBreadList[0].gameObject.transform.parent = movingBreadPoint.transform;
            bakedBreadList[0].gameObject.transform.DOMove(movingBreadPoint.transform.position + new Vector3(0, i, 0), /*y ekle i kullanarak */ .1f);
            bakedBreadList.RemoveAt(0);
        }
    }
    public void SellBreadMechanic()
    {
        Debug.Log("Sell  Girildi");
        int movingBreadCount = movingBreadList.Count;
        for (int i = 0; i <= movingBreadCount; i++)
        {
            int sellBreadCount = sellBreadList.Count;
            for (int k = 0; k <= sellBreadCount; k++)
            {
                movingBreadList[0].gameObject.transform.DOMove(sellBreadList[k].transform.position, .3f);
                movingBreadList[0].gameObject.transform.parent = sellStand.transform;
                movingBreadList.RemoveAt(0);
            }
        }
    }
}
