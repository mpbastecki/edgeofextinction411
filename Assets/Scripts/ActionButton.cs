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
        Human humanPerson = GameManager.Instance.Person;
        Computer computerPerson = GameManager.Instance.CP1;

        //Using the switch case with the cardname as our condition, using (.text) to derefrence
        //what is inside the CardName variable. If we didn't do that then it will only show
        //the memory address not the name.
        switch (CardName.text)
        {
            case ("Biologist "):
                Biologist(humanPerson);
                break;

            case ("Botanist "):
                Botanist(humanPerson);
                break;

            case ("Ranger "):
                Ranger(humanPerson);
                break;

            case ("Explorer "):
                Explorer(humanPerson);
                break;

            case ("Two Sisters In The Wild "):
                TwoSisters(humanPerson);
                break;

        }

        //used to test if we figured out how to find the name of the card or not
        //Debug.Log(CardName.text);
    }

    public void Biologist(Human HumanPerson)
    {
        bool foundAnimal = false;
        
        for(int i = 0; i < HumanPerson.Deck.Cards.Count;i++)
        {
            if (HumanPerson.Deck.Cards[i].CardType == "Animal")
            {
                
                HumanPerson.MoveCard(i, HumanPerson.AnimalGameObject, HumanPerson.Deck.Cards, HumanPerson.AnimalPlacement, false);    
                foundAnimal= true;
                break;
            }
        }
        if (foundAnimal)
        {
            for (int i = 0; i < HumanPerson.HumanPlacement.Count; i++)
            {
                if (HumanPerson.HumanPlacement[i].CardName == "Human-Biologist")
                {
                    Destroy(GameObject.Find("Human-Biologist"));
                    HumanPerson.MoveCard(i, HumanPerson.DiscardGameObject, HumanPerson.HumanPlacement, HumanPerson.DiscardPlacement, true);
                }
            }
        }
    }
    public void Botanist(Human HumanPerson)
    {
        bool foundPlant = false;

        for (int i = 0; i < HumanPerson.Deck.Cards.Count; i++)
        {
            if (HumanPerson.Deck.Cards[i].CardType == "Plant")
            {

                HumanPerson.MoveCard(i, HumanPerson.PlantGameObject, HumanPerson.Deck.Cards, HumanPerson.PlantPlacement, false);
                foundPlant = true;
                break;
            }
        }
        if (foundPlant)
        {
            for (int i = 0; i < HumanPerson.HumanPlacement.Count; i++)
            {
                if (HumanPerson.HumanPlacement[i].CardName == "Human-Botanist")
                {
                    Destroy(GameObject.Find("Human-Botanist"));
                    HumanPerson.MoveCard(i, HumanPerson.DiscardGameObject, HumanPerson.HumanPlacement, HumanPerson.DiscardPlacement, true);
                }
            }
        }


    }
    public void Ranger(Human HumanPerson)
    {
    }
    public void Explorer(Human HumanPerson)
    {
        bool foundCondition = false;

        for (int i = 0; i < HumanPerson.Deck.Cards.Count; i++)
        {
            if (HumanPerson.Deck.Cards[i].CardType == "Condition")
            {

                HumanPerson.MoveCard(i, HumanPerson.ConditionGameObject, HumanPerson.Deck.Cards, HumanPerson.ConditionPlacement, false);
                foundCondition = true;
                break;
            }
        }
        if (foundCondition)
        {
            for (int i = 0; i < HumanPerson.HumanPlacement.Count; i++)
            {
                if (HumanPerson.HumanPlacement[i].CardName == "Human-Explorer")
                {
                    Destroy(GameObject.Find("Human-Explorer"));
                    HumanPerson.MoveCard(i, HumanPerson.DiscardGameObject, HumanPerson.HumanPlacement, HumanPerson.DiscardPlacement, true);
                }
            }
        }
    }
    public void TwoSisters(Human HumanPerson)
    {


        HumanPerson.ThreeCardExecuteEffect();
  
        for (int i = 0; i < HumanPerson.HumanPlacement.Count; i++)
        {
            if (HumanPerson.HumanPlacement[i].CardName == "Human-Two-Sisters-In-The-Wild")
            {
                Destroy(GameObject.Find("Human-Two-Sisters-In-The-Wild"));
                HumanPerson.MoveCard(i, HumanPerson.DiscardGameObject, HumanPerson.HumanPlacement, HumanPerson.DiscardPlacement, true);
            }
        }
    
    }

    //getters and setters
    public Text CardName { get => cardName; set => cardName = value; }
    //public Human HumanPerson { get => humanPerson;set => humanPerson = value; }
    //public Computer ComputerPerson { get => computerPerson; set => computerPerson = value; }
}
