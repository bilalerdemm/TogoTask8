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
            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled) + new Vector3 (0,2,0);

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Musteri geldi");

        if (other.gameObject.CompareTag("SellArea") && PathMovement.instance.finalBreadList.Count > 0)
        {
            PathMovement.instance.finalBreadList[PathMovement.instance.finalBreadList.Count - 1].gameObject.transform.DOLocalMove(new Vector3(0, 5, 1), .5f);
            PathMovement.instance.finalBreadList[PathMovement.instance.finalBreadList.Count - 1].gameObject.transform.parent = transform;
            PathMovement.instance.finalBreadList.RemoveAt(PathMovement.instance.finalBreadList.Count - 1);
            GameManager.instance.money += 5;
            GameManager.instance.moneyText.text = GameManager.instance.money.ToString();
            GameManager.instance.moneyObject.transform.DOScale(new Vector3(1.35f, 1.35f, 1.35f), .5f).OnComplete(() =>
            {
                GameManager.instance.moneyObject.transform.DOScale(new Vector3(1f, 1f, 1f), .5f);
            });
            transform.DOLocalRotate(new Vector3(transform.rotation.x,180,transform.rotation.z),1f);
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

}
