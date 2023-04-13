using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionButton : Player
{

    private Text cardName;
   

    public GameObject CardInfoPanel;
    public GameObject newObject;
    
    public void WhatActionIsHappening()
    {
        
       
        CardName = GameObject.Find("CardName").GetComponent<Text>();

        int i = 2;
        switch (CardName.text)
        {
            case ("Forest"):
                TestFuncOne();
                break;
            case ("Extinction"):
                TestFuncTwo();
                break;
            case (""):
                TestFuncThree();
                break;
        }

        Debug.Log(CardName.text);
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
