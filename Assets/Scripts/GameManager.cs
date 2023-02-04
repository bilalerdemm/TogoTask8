using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public List<GameObject> customers;

    public Transform customerPoint,afterBuyPoint,againBuyPoint;

    public int money = 0;
    public TextMeshProUGUI moneyText;
    public GameObject moneyObject;

    private void Awake() => instance = this;



}
