using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text coinText;
    public Text buySpoolCoinText;
    GameManager gameManager;
    GridController gridController; 

    public Button buySpoolButton;

    public static event Action OnBuySpoolAction; 

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.gameManager;
        gridController = GridController.gridController;

        GridController.OnSpoolSpawned += OnSpoolSpawned; 

        UpdateCoinText(); 
        ActivateButtons(); 
    }



    // Update is called once per frame
    void Update()
    {
        
    } 

    public void BuyNewSpool()
    {
        //check if there's enough money 
        //check if slots are free 
    }
    //nail down  

    public void ActivateButtons()
    {
        buySpoolButton.interactable = gameManager.currentCoinAmount >= gameManager.coinsPerSpool ? true : false; 
    } 

    public void OnBuySpoolClick()
    {
        Debug.Log("Event Buy Spool UI");

        OnBuySpoolAction?.Invoke();

    }

    private void OnSpoolSpawned()
    {
        gameManager.OnBuySpool(); 
        UpdateCoinText();
        ActivateButtons();
    }

    public void UpdateCoinText()
    {
        coinText.text = "$" + gameManager.currentCoinAmount.ToString();
        buySpoolCoinText.text = "$" + gameManager.coinsPerSpool.ToString();
    }
}
