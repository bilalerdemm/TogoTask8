using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customers : MonoBehaviour
{
    void Update()
    {
        StartCoroutine(CustomersMoveToBuy());
    }

    IEnumerator CustomersMoveToBuy()
    {
        for (int i = 0; i <GameManager.instance.customers.Count; i++)
        {
            GameManager.instance.customers[i].gameObject.transform.DOMove(PathMovement.instance.sellBreadPoint.transform.position, 1f);
            yield return new WaitForSeconds(2);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SellArea"))
        {

        }
    }
}
