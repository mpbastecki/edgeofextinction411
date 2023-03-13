﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardDoubleClickInfo : MonoBehaviour, IPointerClickHandler
{
    private Text nameOfCard;
    private Image imageOfCard;
    private Text descriptionOfCard;
    private Text actions;
    private Button buttonActionEnabler;
    private Card cardHolder;
    private Player thePlayer; //Used to hold card info for the player

    //Multiplayer Card Functionality Variables:
    //Used to find which computer the humans multiplayer card effects
    private string multiplayerComputer;
    //Multiplayer Buttons
    private Button buttonMultiComputerOne;
    private Button buttonMultiComputerTwo;
    private Button buttonMultiComputerThree;
    private Computer comp;

    // Script Initialization:
    void Start()
    {
        //Player Initialization:
        if (GameObject.Find("CardInfoPanel/MultiComputerOneButton").GetComponent<Button>() == null)
        {
            Debug.Log("null");
        }
        ButtonMultiComputerOne = GameObject.Find("CardInfoPanel/MultiComputerOneButton").GetComponent<Button>();
        ButtonMultiComputerTwo = GameObject.Find("CardInfoPanel/MultiComputerTwoButton").GetComponent<Button>();
        ButtonMultiComputerThree = GameObject.Find("CardInfoPanel/MultiComputerThreeButton").GetComponent<Button>();

        //Multiplayer Button Listeners:
        ButtonMultiComputerOne.onClick.AddListener(ButtonOneClick);
        ButtonMultiComputerTwo.onClick.AddListener(ButtonTwoClick);
        ButtonMultiComputerThree.onClick.AddListener(ButtonThreeClick);
    }

    //Create a 'Player' object
    public void CreatePlayer(Player pPlayer)
    {
        ThePlayer = pPlayer;
        //Instantiate the CastPlayer() function
        CastPlayer();
    }

    //Determines - Player or Computer
    public void CastPlayer()
    {
        //If 'Player' object is type 'Human'
        if (ThePlayer.GetType() == typeof(Human))
        {
            //Set the 'Player' object to 'Human'
            ThePlayer = (Human)ThePlayer;
        }
        //If 'Player' object is not type 'Human'
        else
        {
            //Set the 'Player' object to 'Computer'
            ThePlayer = (Computer)ThePlayer;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }


    //Multi-Player Button Effect Section:
    //*******************************************************************************************//

    //ButtonOneClick Function:
    void ButtonOneClick()
    {
        //Checks to see if button to use effect on 'ComputerOne' is clicked
        MultiplayerButtonClick("ComputerOne");
    }
    //ButtonTwoClick Function:
    void ButtonTwoClick()
    {
        //Checks to see if button to use effect on 'ComputerTwo' is clicked
        MultiplayerButtonClick("ComputerTwo");
    }
    //ButtonThreeClick Function:
    void ButtonThreeClick()
    {
        //Checks to see if button to use effect on 'ComputerThree' is clicked
        MultiplayerButtonClick("ComputerThree");
    }

    //Function to display Multiplayer Buttons:
    public void ShowMultiplayerButtons()
    {
        //Allow Buttons 1-3 to be interactable
        ButtonMultiComputerOne.interactable = true;
        ButtonMultiComputerTwo.interactable = true;
        ButtonMultiComputerThree.interactable = true;
    }

    //Assigns specified Computer object based on which button was clicked by the Player
    public void AssignComputer(string pComputer)
    {
        //If ComputerOne is selected
        if (pComputer == "ComputerOne")
        {
            //Assign Comp variable to object of CP1
            Comp = GameManager.Instance.CP1;
        }
        //If ComputerTwo is selected
        else if (pComputer == "ComputerTwo")
        {
            //Assign Comp variable to object of CP2
            Comp = GameManager.Instance.CP2;
        }
        //If ComputerThree is selected
        else if (pComputer == "ComputerThree")
        {
            //Assign Comp variable to object of CP3
            Comp = GameManager.Instance.CP3;
        }
    }

    //ShowBoards() Function:
    public void ShowBoards()
    {
        //If Comp variable is set to object of CP1
        if (Comp == GameManager.Instance.CP1)
        {

            GameManager.Instance.HideShow.ShowCP1();
        }
        //If Comp variable is set to object of CP2
        else if (Comp == GameManager.Instance.CP2)
        {

            GameManager.Instance.HideShow.ShowCP2();
        }
        else if (Comp == GameManager.Instance.CP3)
        {
            GameManager.Instance.HideShow.ShowCP3();
        }
    }

    public void MultiplayerButtonClick(string pComputer)
    {
        Debug.Log("Multi click");
        //assigns the correct computer to the computer object
        AssignComputer(pComputer);
        //calls the test function to carry out the moving of cards
        MoveMultiCard();
    }


    public void MoveMultiCard()
    {
        Debug.Log("begin test function");
        //Displays the appropriate canvas so graphics can be painted
        ShowBoards();
        Debug.Log("show boards was called");
        //SETTING THE FILTERS FOR THE CARDS NAMES SO THAT THEY PRINT OUT PROPERLY
        //Take the current instance (card double clicked) and assign it to a temp object called 'this'
        string nameHolder = this.gameObject.name; //this will be used to hold the name until it is correct
        Transform parentHolder = this.gameObject.transform.parent;

        //Matches the card name, then prints the actions of said card
        for (int i = 0; i < ThePlayer.Hand.Count; i++)
        {
            //If the hand of the player matches the card name
            if (ThePlayer.Hand[i].CardName == nameHolder)
            {
                //Set the CardHoler to the rest of the things needed to be set
                CardHolder = ThePlayer.Hand[i];
            }
        }

        //Special Region Filter
        if (nameHolder.Contains("Special"))
        {
            this.transform.SetParent(GameObject.Find(Comp.SpecialRegionGameObject).transform);
            this.transform.localScale = new Vector3(1.0f, 1.0f, 0);
            Comp.SpecialRegionPlacement.Add(CardHolder);
            ThePlayer.Hand.Remove(CardHolder);
        }
        //Region name filter
        else if (nameHolder.Contains("Region"))
        {
            this.transform.SetParent(GameObject.Find(Comp.RegionGameObject).transform);
            this.transform.localScale = new Vector3(1.0f, 1.0f, 0);
            Comp.RegionPlacement.Add(CardHolder);
            ThePlayer.Hand.Remove(CardHolder);
        }
        //Plant name filter
        else if (nameHolder.Contains("Plant")) //plant name filter
        {
            this.transform.SetParent(GameObject.Find(Comp.PlantGameObject).transform);
            this.transform.localScale = new Vector3(1.0f, 1.0f, 0);
            Comp.PlantPlacement.Add(CardHolder);
            ThePlayer.Hand.Remove(CardHolder);
        }
        //Multiplayer Actions name filter
        else if (nameHolder.Contains("Multi"))
        {
            this.transform.SetParent(GameObject.Find(Comp.MultiplayerGameObject).transform);
            this.transform.localScale = new Vector3(1.0f, 1.0f, 0);
            Comp.MultiPlacement.Add(CardHolder);
            ThePlayer.Hand.Remove(CardHolder);
        }
        //Condition(s) name filter
        else if (nameHolder.Contains("Condition"))
        {
            this.transform.SetParent(GameObject.Find(Comp.ConditionGameObject).transform);
            this.transform.localScale = new Vector3(1.0f, 1.0f, 0);
            Comp.ConditionPlacement.Add(CardHolder);
            ThePlayer.Hand.Remove(CardHolder);
        }
        //Invertebrate name filter
        else if (nameHolder.Contains("Invertebrate"))
        {
            this.transform.SetParent(GameObject.Find(Comp.InvertebrateGameObject).transform);
            this.transform.localScale = new Vector3(1.0f, 1.0f, 0);
            Comp.InvertebratePlacement.Add(CardHolder);
            ThePlayer.Hand.Remove(CardHolder);
        }
        //Fungi name filter
        else if (nameHolder.Contains("Fungi")) //fungi name filter
        {
            this.transform.SetParent(GameObject.Find(Comp.FungiGameObject).transform);
            this.transform.localScale = new Vector3(1.0f, 1.0f, 0);
            Comp.FungiPlacement.Add(CardHolder);
            ThePlayer.Hand.Remove(CardHolder);
        }
        //Human name filter
        else if (nameHolder.Contains("Human")) //humam name filter
        {
            this.transform.SetParent(GameObject.Find(Comp.HumanGameObject).transform);
            this.transform.localScale = new Vector3(1.0f, 1.0f, 0);
            Comp.HumanPlacement.Add(CardHolder);
            ThePlayer.Hand.Remove(CardHolder);
        }
        //Animal name filter
        else if (nameHolder.Contains("Animal")) //animal name filter
        {
            this.transform.SetParent(GameObject.Find(Comp.AnimalGameObject).transform);
            this.transform.localScale = new Vector3(1.0f, 1.0f, 0);
            Comp.AnimalPlacement.Add(CardHolder);
            ThePlayer.Hand.Remove(CardHolder);
        }
        //Microbe name filter
        else if (nameHolder.Contains("Microbe")) //microbe name filter
        {
            this.transform.SetParent(GameObject.Find(Comp.MicrobeGameObject).transform);
            this.transform.localScale = new Vector3(1.0f, 1.0f, 0);
            Comp.MicrobePlacement.Add(CardHolder);
            ThePlayer.Hand.Remove(CardHolder);
        }

        //    //shows the human canvas screen
        GameManager.Instance.HideShow.ShowPlayer();
    }

    //End of Multi-Player Button Effect Section:
    //*******************************************************************************************//

    public void OnPointerClick(PointerEventData eventData)
    {

        //SETTING THE FILTERS FOR THE CARDS NAMES SO THAT THEY PRINT OUT PROPERLY
        //Take the current instance (card double clicked) and assign it to a temp object called 'this'
        string nameHolder = this.gameObject.name; //this will be used to hold the name until it is correct
        Transform parentHolder = this.gameObject.transform.parent;

        //Ensure the card was double-clicked
        if (eventData.clickCount == 2)
        {
            //Display the card information
            GameManager.Instance.HideShow.ShowCardInfo();



            //Assign the card information objects (name, sprite, description) to the UI objects
            ImageOfCard = GameObject.Find("CardImage").GetComponent<Image>();
        }
    }

    //accessors and mutators
    public Player ThePlayer { get => thePlayer; set => thePlayer = value; }
    public Text NameOfCard { get => nameOfCard; set => nameOfCard = value; }
    public Image ImageOfCard { get => imageOfCard; set => imageOfCard = value; }
    public Text DescriptionOfCard { get => descriptionOfCard; set => descriptionOfCard = value; }
    public Text Actions { get => actions; set => actions = value; }
    public Button ButtonActionEnabler { get => buttonActionEnabler; set => buttonActionEnabler = value; }
    public Card CardHolder { get => cardHolder; set => cardHolder = value; }
    public string MultiplayerComputer { get => multiplayerComputer; set => multiplayerComputer = value; }
    public Button ButtonMultiComputerOne { get => buttonMultiComputerOne; set => buttonMultiComputerOne = value; }
    public Button ButtonMultiComputerTwo { get => buttonMultiComputerTwo; set => buttonMultiComputerTwo = value; }
    public Button ButtonMultiComputerThree { get => buttonMultiComputerThree; set => buttonMultiComputerThree = value; }
    public Computer Comp { get => comp; set => comp = value; }
}
