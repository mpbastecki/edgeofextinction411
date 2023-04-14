using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Michael Plants
//this script is what makes the action button work
//it has all the function to allow the cards action functions to be called
//when a user presses the action button

public class ActionButton : MonoBehaviour
{
    private Text cardName;

    void Start(){ }

        //this function decides what action function to use
        //we are using a switch cased based off of the cards name to decide on what function to call.
        //where the CardName object
        //PlayerBored Scene >> CardInfoCanvas >> CardInfoPanal >> CardName
        public void WhatActionIsHappening()
    {
        //This is finding the cards name by finding the game object Cardname and using its text component to set it to our variabl.
        CardName = GameObject.Find("CardName").GetComponent<Text>();


        //Using the switch case with the cardname as our condition, using (.text) to derefrence
        //what is inside the CardName variable. If we didn't do that then it will only show
        //the memory address not the name.
        switch (CardName.text)
        {
            case ("Biologist "):
                Biologist();
                break;

            case ("Botanist "):
                Botanist();
                break;

            case ("Ranger "):
                Ranger();
                break;

            case ("Explorer "):
                Explorer();
                break;

            case ("Sisters At Play "):
                TwoSisters();
                break;

        }

        //used to test if we figured out how to find the name of the card or not
        //Debug.Log(CardName.text);
    }

    public void Biologist()
    {
        
    }
    public void Botanist()
    {
    }
    public void Ranger()
    {
    }
    public void Explorer()
    {
    }
    public void TwoSisters()
    {
    }

    //getters and setters
    public Text CardName { get => cardName; set => cardName = value; }

}
