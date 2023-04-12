using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionButton : Player
{

    private Text cardName;
   

    public GameObject CardInfoPanel;
    public GameObject newObject;
    

    //This finction closes the cardinfo panel to go back to the game board when you click the action button
    //that is in the cardinfo canvise 
    public void CloseInfoPanal()
    {
       // CardName = GameObject.Find("CardName").GetComponent<Text>();

        if (CardInfoPanel != null)
        {
            CardInfoPanel.SetActive(false);
        }

    }

    //This function figures out what card affects what function needs to be called when you 
    //click the action button on the card info panel
    public void WhatActionIsHappening()
    {
        
       
        CardName = GameObject.Find("CardName").GetComponent<Text>();

        int i = 2;
        switch (i)
        {
            case (0):
                TestFuncOne();
                break;
            case 1:
                TestFuncTwo();
                break;
            case 2:
                TestFuncThree();
                break;
        }

        Debug.Log(CardName);
    }


    public void TestFuncOne()
    {
        Debug.Log("First test func worked");
    }

    public void TestFuncTwo()
    {
        Debug.Log("Second test func worked");
    }

    public void TestFuncThree()
    {
        Debug.Log("Third test func worked");

    }

    public Text CardName { get => cardName; set => cardName = value; }

}
