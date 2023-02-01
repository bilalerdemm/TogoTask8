using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public List<GameObject> customers;

    public Transform customerPoint,afterBuyPoint,againBuyPoint;

    private void Awake() => instance = this;



}
