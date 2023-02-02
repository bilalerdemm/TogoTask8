using DG.Tweening;
using PathCreation;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Customers : MonoBehaviour
{
    public PathCreator pathCreator;
    public float speed = 5f;
    float distanceTravelled;

    public Animator customerAnim;



    private void Update()
    {
        // Debug.Log("ekmek: " + PathMovement.instance.finalBreadList.Count);

        if (PathMovement.instance.finalBreadList.Count > 0 || transform.childCount > 2)
        {
            customerAnim.SetBool("isRunning", true);
            distanceTravelled += speed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
            //if (CustomerSpawner.instance.inCustomerSpawner)
            //{
            //    Destroy(GameManager.instance.customers[0].gameObject.transform.GetChild(2).gameObject);
            //}
        }
    }

    public void CustomerBuy()
    {
        //StartCoroutine(CustomersMoveToBuy());
    }
    /*
    public IEnumerator CustomersMoveToBuy()
    {
        int cust = GameManager.instance.customers.Count;
        for (int i = 0; i < cust; i++)
        {
            Debug.Log("CustomersBuy");
            GameManager.instance.customers[0].gameObject.transform.DOMove(GameManager.instance.customerPoint.transform.position, 1f).
                OnComplete(() =>
                {
                    GameManager.instance.customers[0].gameObject.transform.DOMove(GameManager.instance.afterBuyPoint.transform.position, 3f);
                });
            //OnComplete(() =>
            //{
            //    GameManager.instance.customers[0].gameObject.transform.DOMove(GameManager.instance.againBuyPoint.transform.position, 1f);
            //    Destroy(GameManager.instance.customers[0].gameObject.transform.GetChild(2).gameObject);
            //});
            //OnComplete(() => 
            //{
            //    GameManager.instance.customers[0].gameObject.transform.DOMove(GameManager.instance.customerPoint.transform.position, 1f);
            //});
            yield return new WaitForSeconds(2f);
        }
    }
    */
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Musteri geldi");

        if (other.gameObject.CompareTag("SellArea") && PathMovement.instance.finalBreadList.Count > 0)
        {
            PathMovement.instance.finalBreadList[PathMovement.instance.finalBreadList.Count - 1].gameObject.transform.DOLocalMove(new Vector3(0, 5, 1), .5f);
            PathMovement.instance.finalBreadList[PathMovement.instance.finalBreadList.Count - 1].gameObject.transform.parent = transform;
            PathMovement.instance.finalBreadList.RemoveAt(PathMovement.instance.finalBreadList.Count - 1);
        }
    }
}
