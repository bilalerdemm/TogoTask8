using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject customerPre;
    public bool inCustomerSpawner = false;
    public static CustomerSpawner instance;
    private void Awake()
    {
        instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Customer"))
        {
            inCustomerSpawner = true;
            //Debug.Log("CustomerSpawner");
            //Destroy(GameManager.instance.customers[0].gameObject.transform.GetChild(2).gameObject);
            StartCoroutine(CustomerDestroyerSpawner(other));
            //GameManager.instance.customers.Remove(other.gameObject);
            //Destroy(other.gameObject);
            //GameObject newCustomer = Instantiate(customerPre, transform.parent);
            //newCustomer.transform.DOMove(GameManager.instance.customerPoint.transform.position + new Vector3(-20, 0, -4), 1f);
            //GameManager.instance.customers.Add(newCustomer);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        inCustomerSpawner = false;
    }
    IEnumerator CustomerDestroyerSpawner(Collider other)
    {
        yield return new WaitForSeconds(1);
        Destroy(other.gameObject);
        GameObject newCustomer = Instantiate(customerPre);
    }
}
