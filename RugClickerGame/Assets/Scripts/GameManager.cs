using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager; 

    public int currentCoinAmount;
    public int coinsPerSew;
    public int coinsPerSpool;
    public float offset = 1.5f; 

    private void Awake()
    {
        gameManager = this; 
    }



    void Start()
    {
        //on sew animation ended update coins 
    }

    public void OnBuySpool()
    {
        currentCoinAmount -= coinsPerSpool;
        coinsPerSpool = (int) (coinsPerSpool* offset); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
