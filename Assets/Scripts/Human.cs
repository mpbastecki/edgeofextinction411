﻿/*
 *  @class      Human.cs
 *  @purpose    mextend from player and provides everything needed for the human player inlcuding creating cards
 *  
 *  @author     CIS 411
 *  @date       2020/04/07
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Human : Player
{
    //used to pass in current object to differnt classes
    private Human currentPlayer;
    private Human person = GameManager.Instance.Person;
    //this is for human
    private bool canDraw;
    //i forget what this is used for- so find out
    private bool cardDiscarded;
    //used to change the text on the human layerboard screen
    private Text drawText;

    //to make the three card burst butto visibile ane not visible
    private Button threeCardBurstButton;
    //private GameObject reqGO;
    //private Requirements req;
    
    //assigns the script to the game object

    //assigns the game object to the script withe the game object

    // Start is called before the first frame update
    void Start()
    {
        //ReqGO = new GameObject("Req");
        ////assigns the script to the game object
        //ReqGO.AddComponent<Requirements>();
        ////assigns the game object to the script withe the game object
        //Req = GameObject.Find("Req").GetComponent<Requirements>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Testing in human.cs");
        CheckStandingCards(CurrentPlayer);
        CheckExtinction();
    }

    /*
 *  @name       Initialize Objects() extend from parent class and ads additon info specific to human
 *  @purpose    acts as constuctor since unitiy doesnt let us create objects of classes normally. Is call when created in Game Manager class
 */
    public override void InitializeObjects(string pScoreGameObject, string pRoundGameObject, string pHandGameObject, string pRegionGameObject, string pConditionGameObject,
        string pPlantGameObject, string pInvertebrateGameObject, string pAnimalGameObject, string pSpecialRegionGameObject, string pMultiplayerGameObject,
        string pMicrobeGameObject, string pFungiGameObject, string pDiscardGameObject, string pHumanGameObject, string pDeckColorGameObject, string pDeckTextGameObject,
        string pHumanScoreGameObject, string pCP1ScoreGameObject, string pPlayerName)
    {
        //gets base parent class info
        base.InitializeObjects(pScoreGameObject, pRoundGameObject, pHandGameObject, pRegionGameObject, pConditionGameObject,
        pPlantGameObject, pInvertebrateGameObject, pAnimalGameObject, pSpecialRegionGameObject, pMultiplayerGameObject,
        pMicrobeGameObject, pFungiGameObject, pDiscardGameObject, pHumanGameObject, pDeckColorGameObject, pDeckTextGameObject,
        pHumanScoreGameObject, pCP1ScoreGameObject, pPlayerName);
        //info specific to human
        CurrentPlayer = this;
        CanDraw = true;
        CardDiscarded = false;

    }

    /*
    *  @name       StartTurn() extends from parent
    *  @purpose    deals the player 5 cards if its round one then starts the players turn
    */
    public override void StartTurn()
    {
        //execute parent method
        base.StartTurn();
        //allowing colliders to work
        Physics.queriesHitTriggers = true;
        //after the round has changed the player needs to discard again
        CardDiscarded = false;
        //gets the text component so it can be changed
        DrawText = GameObject.Find("DrawText").GetComponent<Text>(); //gets the text component so it can be changed
        //if it is the first round then deal 5 cards automatically
        if (Round == 1 && CanDraw == true) //only happens in the first round
        {
            CreateButtonObjects();
            Draw(5);
        }
        DrawText.text = "Step 1: Play Card(s) \n Step 2: Discard to End Your Turn";
         //gets the right amount of cards to draw based off regions its a parent function
        DrawAmount();
        //draws the apropriate amount
        Debug.Log("testing");
        Draw(DrawCount);
        //makes the human player unable to draw again
        CanDraw = false;

        

    }
    public void CheckExtinction()
    {
        bool foundExtinction = false;
        for (int i = 0; i < CurrentPlayer.MultiplayerPlacement.Count; i++)
        {
            if (CurrentPlayer.MultiplayerPlacement[i].CardName == "Multi-Extinction")
            {
                foundExtinction = true;
            }
        }

        if (CurrentPlayer.ProtectedFromExtinction && foundExtinction)
        {
            for (int i = 0; i < CurrentPlayer.HumanPlacement.Count; i++)
            {
                if (CurrentPlayer.HumanPlacement[i].CardName == "Human-Two-Sisters-In-The-Wild")
                {
                    
                    Destroy(GameObject.Find("Human-Two-Sisters-In-The-Wild"));
                    MoveCard(i, DiscardGameObject, HumanPlacement, DiscardPlacement, true);
                    
                    //adds the card to the discard list
                                                                                //ThePlayer.HumanPlacement[i].Destroy;

                }
            }
            for (int i = 0; i < MultiplayerPlacement.Count; i++)
            {
                if (CurrentPlayer.MultiplayerPlacement[i].CardName == "Multi-Extinction")
                {
                    Destroy(GameObject.Find("Multi-Extinction"));
                    MoveCard(i, DiscardGameObject, MultiplayerPlacement, DiscardPlacement, true);

                }
            }
            CurrentPlayer.ProtectedFromExtinction = false;
        }
    }


    //Checks human cards to set the right flags for protection from exinction, invasive species, etc
    public void CheckStandingCards(Human pCurrentPlayer)
    {
        Reqs req = new Reqs(pCurrentPlayer);
        bool foundBiologist = false;
        bool foundBotanist = false;
        bool foundExplorer = false;
        bool foundRanger = false;
        bool foundTwoSisters = false;

        if (req.r247())
        {
            for (int i = 0; i < CurrentPlayer.Deck.Cards.Count; i++)
            {
                if (CurrentPlayer.Deck.Cards[i].CardName == "Invertebrate-Darkling-Beetle-Larvae")
                {
                    MoveCard(i, InvertebrateGameObject, Deck.Cards, InvertebratePlacement, false);
                }
            }
            for (int i = 0; i < CurrentPlayer.DiscardPlacement.Count; i++)
            {
                if(CurrentPlayer.DiscardPlacement[i].CardName == "Invertebrate-Darkling-Beetle-Larvae")
                {
                    MoveCard(i, InvertebrateGameObject, DiscardPlacement, InvertebratePlacement, false);
                }
            }
        }

        for (int i = 0; i < CurrentPlayer.HumanPlacement.Count; i++)
        {
            

            
            switch(CurrentPlayer.HumanPlacement[i].CardName)
            {
                case "Human-Biologist":
                    
                    foundBiologist = true;
                    break;
                case "Human-Botanist":
                    
                    foundBotanist = true;
                    break;
                case "Human-Explorer":
                    
                    foundExplorer = true;
                    break;
                case "Human-Ranger":
                    
                    foundRanger = true;
                    break;
                case "Human-Two-Sisters-In-The-Wild":
                    
                    foundTwoSisters = true;
                    break;
                default:
                    
                    break;
            }


        }

        if (foundBiologist)
        {
            Debug.Log("biologist");
            CurrentPlayer.ProtectedFromInvasiveAnimal = true;
        }
        else
        {
            Debug.Log("NOT biologist");
            CurrentPlayer.ProtectedFromInvasiveAnimal = false;
        }

        if (foundBotanist)
        {
            Debug.Log("botanist");
            CurrentPlayer.ProtectedFromInvasivePlant = true;
        }
        else
        {
            Debug.Log("NOT botanist");
            CurrentPlayer.ProtectedFromInvasivePlant = false;
        }

        if (foundExplorer)
        {
            Debug.Log("explorer");
            CurrentPlayer.NoConditionRequirements = true;
        }
        else
        {
            Debug.Log("NOT explorer");
            CurrentPlayer.NoConditionRequirements = false;
        }

        if (foundRanger)
        {
            Debug.Log("ranger");
            CurrentPlayer.ProtectedFromBlight = true;
        }
        else
        {
            Debug.Log("NOT ranger");
            CurrentPlayer.ProtectedFromBlight = false;
        }

        if (foundTwoSisters)
        {
            Debug.Log("sisters");
            CurrentPlayer.ProtectedFromExtinction = true;
        }
        else
        {
            Debug.Log("NOT sisters");
            CurrentPlayer.ProtectedFromExtinction = false;
        }

    }



        /*
     *  @name       GenerateCardObjects() extend from parent class and ads additon info specific to human
     *  @purpose    this gets the card from the deck and assigns it to a game object that will be the card you will see omn the screen
     */
    public override void GenerateCardObject()
    {
        //gets the parent class method stuff
        base.GenerateCardObject();
        //adding the Draggable script to the card object, which allows it to be dragged and placed appropriately
        CardObject.AddComponent<Draggable>();
       // Debug.Log();
        //Debug.Log("testing for error");
        //CardObject.AddComponent<DoubleClickDescription>(); //makes the cards able to be clicked on
        //Debug.Log("testing for error");
        //CardObject.GetComponent<DoubleClickDescription>().CreatePlayer(CurrentPlayer);
        //Debug.Log("testing for anoter error");
    }

    /*
    *  @name       Draw(int pAmount) extends from parent
    *  @purpose    gets the card from the deck and calls generate card object and 
    */
    public override void Draw(int pAmount)
    {
        //gets parent info
        base.Draw(pAmount);
        //makes sure you can opnly draw once basically
        if (CanDraw == true)
        {
            //checking to make sure there are cards left in the deck
            if (Deck.Cards.Count != 0)
            {
                //determins how many cards to draw
                for (int i = 0; i < pAmount; i++)
                {
                    //retrieving the object created in the form of the "instance" earlier
                    Holder = ScriptableObject.FindObjectOfType<CardRetrievalFromDeck>();
                    //sets the parent
                    CardParent = GameObject.Find(HandGameObject).transform;
                    //calling the object's CardDrawRandomizer function, which selects a random card from the deck
                    Holder.CardDrawRandomizer(CurrentPlayer);
                    //calling this script's generateCardObject function,  which creates an object to represent the card
                    GenerateCardObject();
                    //calling the script object's setSprite function, which passes in the SpriteRenderer, and sets it's sprite to the corresponding card chosen in CardDrawRandomizer
                    Holder.setSprite(Sr);
                }
            }
            else
            {
                //calling this script's changeDeck function, which replaces the deck with an out of cards image
                ChangeDeck();
            }
        }
    }

        /*
     *  @name       changeDeck()
     *  @purpose    when the human player is out of sards it replaces the image with an out of cards image
     */
    public void ChangeDeck()
    {
        //creating a new SpriteRenderer for the deck
        SpriteRenderer swap = GameObject.Find("Deck Draw Button").AddComponent<SpriteRenderer>();
        //loading the out of cards texture
        TexSprite = Resources.Load<Texture2D>("Sprites/Out-Of-Cards");
        //creating a sprite out of said texture
        TempSprite = Sprite.Create(TexSprite, new Rect(0, 0, TexSprite.width, TexSprite.height), new Vector2(0.5f, 0.5f), 1.6666f);
        //setting the SpriteRenderer's sprite to the newly created Sprite
        swap.sprite = TempSprite;
        //setting the SpriteRenderer's sorting level so that it appears above the deck, but  below cards
        swap.sortingOrder = 9;
        //setting the SpriteRenderer's sorting level to Default, so that it is in line with other objects
        swap.sortingLayerName = "Default";
    }

    /*
     *  @name       ThreeCardExecute() extends parent method
     *  @purpose    used once per game per player and will not be able to be used again once used
     */
    public override void ThreeCardExecute()
    {
        base.ThreeCardExecute();
        //makes it so player can draw the cards
        CanDraw = true;
        Draw(3);
        ThreeCardBurstAvailable = false;
        //Disables the button 
        ThreeCardBurstButton.gameObject.SetActive(false);
        ThreeCardBurstButton.interactable = false;
    }

    /*The code below is for buttso associated only woith the human, these get rid of the need to have separate classes for each button*/
        /*
     *  @name       CreateButtonObjects()
     *  @purpose    creates the buttons and adds listeners for when they are clicked
     */
    public void CreateButtonObjects()
    {
        //finds the ThreeCardBurstButton in the scene
        ThreeCardBurstButton = GameObject.Find("3CardBurst").GetComponent<Button>();
        //when the button is clicked, this is what occurs
        ThreeCardBurstButton.onClick.AddListener(ThreeCardExecute);
    }

    //accessors and mutators
    public bool CanDraw { get => canDraw; set => canDraw = value; }
    public bool CardDiscarded { get => cardDiscarded; set => cardDiscarded = value; }
    public Text DrawText { get => drawText; set => drawText = value; }
    public Human CurrentPlayer { get => currentPlayer; set => currentPlayer = value; }
    public Button ThreeCardBurstButton { get => threeCardBurstButton; set => threeCardBurstButton = value; }
   // public GameObject ReqGO { get => reqGO; set => reqGO = value; }
   // public Requirements Req { get => req; set => req = value; }
}

