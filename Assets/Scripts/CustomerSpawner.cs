using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject customerPre;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Customer"))
        {
            GameObject newCustomer = Instantiate(customerPre, transform.parent);
            newCustomer.transform.DOMove(GameManager.instance.customerPoint.transform.position + new Vector3(-20, 0, -4), 1f);
            GameManager.instance.customers.Add(newCustomer);
            GameManager.instance.customers.Remove(other.gameObject);
            Destroy(other.gameObject);
        }
    }

}
