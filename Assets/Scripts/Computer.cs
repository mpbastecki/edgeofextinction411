/*
 *  @class      Computer.cs
 *  @purpose    this inherits the player calss and is used to carry out the computer AI
 *  
 *  @author     CIS411
 *  @date       2020/04/14
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Computer : Player
{
   //this is to pass what ever version of computer is being used into methods
    private Computer currentPlayer;
    private Computer computerPerson = GameManager.Instance.CP1;
    private Human humanPerson = GameManager.Instance.Person;

    //these will hold the values to create an object from the script for Requirements
    private GameObject reqGO;
    private Requirements req;
    //will help determine if the card should be placed or not
    private bool requirementsWork;
    //this is for human
    private bool canDraw;
    //i forget what this is used for- so find out
    private bool cardDiscarded;


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
        //info specific to computers
        CurrentPlayer = this;
        RequirementsWork = false;
    }

    public bool foundTemperatureDrop { get => foundTemperatureDrop; set => foundTemperatureDrop = value; }
    public bool foundChildrenAtPlay { get => foundChildrenAtPlay; set => foundChildrenAtPlay = value; }
    //starts the turn of the computer initially dealing 5 cards
    public override void StartTurn()
    {
        if (GameManager.Instance.Round == 0)
        {
            SkipRound();
        }

        //execute parent method
        base.StartTurn();
        //if it is the first round then deal 5 cards automatically
        if (Round == 1) //only happens in the first round
        {
            Draw(4);
        }
            bool foundChildrenAtPlay = false;
            bool foundTemperatureDrop = false;
            for (int i = 0; i < humanPerson.MultiplayerPlacement.Count; i++)
            {
                switch (humanPerson.MultiplayerPlacement[i].CardName)
                {
                    case "Multi-Children-At-Play":

                        foundChildrenAtPlay = true;
                        break;
                    case "Multi-Temperature-Drop":

                        foundTemperatureDrop = true;
                        break;

                    default:
                        break;
                }
            }

            if (foundChildrenAtPlay || foundTemperatureDrop)
            {
                //Debug.Log("Found children at play");
                //Debug.Log("Skip the turn");

                for (int i = 0; i < humanPerson.MultiplayerPlacement.Count; i++)
                {
                    if (humanPerson.MultiplayerPlacement[i].CardName == "Multi-Children-At-Play")
                    {


                        SkipRound();
                        foundChildrenAtPlay = false;

                    }
                    else if (humanPerson.MultiplayerPlacement[i].CardName == "Multi-Temperature-Drop")
                    {

                        SkipRound();
                        foundTemperatureDrop = false;

                    }
                }


            }

            else
            {
                //after the 5 cards aredealt, the procedexd with computer AI alogorithm
                StartCoroutine(ComputerPerforms()); //goes through the function needed for the AI
                                                    //ComputerPerforms();
                //    computerPerson.SkipTurn = false;
            }



        
    }

        /*
    *  @name       Draw(int pAmount) extends from parent
    *  @purpose    gets the card from the deck and calls generate card object and 
    */
    public override void Draw(int pAmount)
    {
        //gets parent frunction
        base.Draw(pAmount);
        //checking to make sure there are cards left in the deck
        if (Deck.Cards.Count != 0)
        {
            //determins how many cards to draw
            for (int i = 0; i < pAmount; i++)
            {
                //retrieving the object created in the form of the "instance" earlier
                Holder = ScriptableObject.FindObjectOfType<CardRetrievalFromDeck>();
                CardParent = GameObject.Find(HandGameObject).transform;
                //calling  the object's CardDrawRandomizer function, which selects a random card from the deck
                Holder.CardDrawRandomizer(CurrentPlayer);
                //changes the sprite so the player cant see the cards in the computers hand
                Holder.CardNameHolder = "back_of_card";
                GenerateCardObject();
                Holder.setSprite(Sr); //generating the card object to be placed into the panel
            }
        }
        else
        {
            //retrieving the object created in the form of the "instance" earlier
            Holder = ScriptableObject.FindObjectOfType<CardRetrievalFromDeck>();
            //sets the parent
            CardParent = GameObject.Find(HandGameObject).transform;
            //calling the object's CardDrawRandomizer function, which selects a random card from the deck
            Holder.CardDrawRandomizer(CurrentPlayer);
            //changes the sprite so the player cant see the cards in the computers hand
            Holder.CardNameHolder = "back_of_card";
            //calling this script's generateCardObject function,  which creates an object to represent the card
            GenerateCardObject();
            //calling the script object's setSprite function, which passes in the SpriteRenderer, and sets it's sprite to the corresponding card chosen in CardDrawRandomizer
            Holder.setSprite(Sr);
        }
    }

    // Update is called once per frame
    void Update()
        {
        Debug.Log("Testing from computer.cs");
        CheckExtinction();
        CheckStandingCards(CurrentPlayer);
    }

    public void CheckExtinction()
    {
        Human humanPerson = GameManager.Instance.Person;
        bool foundExtinction = false;
        for (int i = 0; i < CurrentPlayer.MultiplayerPlacement.Count; i++)
        {
            if (CurrentPlayer.MultiplayerPlacement[i].CardName == "Multi-Extinction")
            {
                foundExtinction = true;
            }
        }

        if (humanPerson.ProtectedFromExtinction && foundExtinction)
        {
            for (int i = 0; i < humanPerson.HumanPlacement.Count; i++)
            {
                if (humanPerson.HumanPlacement[i].CardName == "Human-Two-Sisters-In-The-Wild")
                {

                    Destroy(GameObject.Find("Human-Two-Sisters-In-The-Wild"));
                    MoveCard(i, DiscardGameObject, HumanPlacement, DiscardPlacement, true);

                    //adds the card to the discard list
                    //ThePlayer.HumanPlacement[i].Destroy;

                }
            }
            for (int i = 0; i < CurrentPlayer.MultiplayerPlacement.Count; i++)
            {
                if (CurrentPlayer.MultiplayerPlacement[i].CardName == "Multi-Extinction")
                {
                    Destroy(GameObject.Find("Multi-Extinction"));
                    MoveCard(i, DiscardGameObject, MultiplayerPlacement, DiscardPlacement, true);

                }
            }
            humanPerson.ProtectedFromExtinction = false;
        }
    }

    bool blackberryActivated = false;
    bool whitePineActivated = false;
    bool foundChildrenLock = false;
    bool foundTemperatureDropLock = false;
    int roundSkipCardPlayed;

    //Checks human cards to set the right flags for protection from exinction, invasive species, etc
    public void CheckStandingCards(Computer pCurrentPlayer)
    {
        Reqs req = new Reqs(pCurrentPlayer);
        bool foundBiologist = false;
        bool foundBotanist = false;
        bool foundExplorer = false;
        bool foundRanger = false;
        bool foundTwoSisters = false;
        //Computer computerPerson = GameManager.Instance.CP1;
        Human humanPerson = GameManager.Instance.Person;

        //checks for children at play and temperature drop
        for (int i = 0; i < CurrentPlayer.MultiplayerPlacement.Count; i++)
        {
            if (CurrentPlayer.MultiplayerPlacement[i].CardName == "Multi-Children-At-Play" && !foundChildrenLock)
            {
                foundChildrenLock = true;
                roundSkipCardPlayed = GameManager.Instance.Round;
            }
            else if (CurrentPlayer.MultiplayerPlacement[i].CardName == "Multi-Temperature-Drop" && !foundTemperatureDropLock)
            {
                foundTemperatureDropLock = true;
                roundSkipCardPlayed = GameManager.Instance.Round;
            }
            else if (foundChildrenLock && roundSkipCardPlayed < GameManager.Instance.Round)
            {
                //Debug.Log(GameObject.Find("Multi-Children-At-Play"));
                Destroy(GameObject.Find("Multi-Children-At-Play"));
                //Debug.Log("swag money");
                MoveCard(i, DiscardGameObject, MultiplayerPlacement, DiscardPlacement, true);
                foundChildrenLock = false;
                roundSkipCardPlayed = 10;
            }
            else if (foundTemperatureDropLock && roundSkipCardPlayed < GameManager.Instance.Round)
            {
                //Debug.Log(GameObject.Find("Multi-Children-At-Play"));
                Destroy(GameObject.Find("Multi-Temperature-Drop"));
                //Debug.Log("swag money 2");
                MoveCard(i, DiscardGameObject, MultiplayerPlacement, DiscardPlacement, true);

                foundTemperatureDropLock = false;
                roundSkipCardPlayed = 10;
            }
        }

        //checks for darkling larvae beetle
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
                if (CurrentPlayer.DiscardPlacement[i].CardName == "Invertebrate-Darkling-Beetle-Larvae")
                {
                    MoveCard(i, InvertebrateGameObject, DiscardPlacement, InvertebratePlacement, false);
                }
            }
        }
        //checks for barred owl
        if (req.r246())
        {
            for (int i = 0; i < CurrentPlayer.Deck.Cards.Count; i++)
            {
                if (CurrentPlayer.Deck.Cards[i].CardName == "Animal-Barred-Owl")
                {
                    MoveCard(i, AnimalGameObject, Deck.Cards, AnimalPlacement, false);
                }
            }
            for (int i = 0; i < CurrentPlayer.DiscardPlacement.Count; i++)
            {
                if (CurrentPlayer.DiscardPlacement[i].CardName == "Animal-Barred-Owl")
                {
                    MoveCard(i, AnimalGameObject, DiscardPlacement, AnimalPlacement, false);
                }
            }
        }
        //checks for big tooth aspen and white birch
        if (req.r248())
        {
            for (int i = 0; i < CurrentPlayer.Deck.Cards.Count; i++)
            {
                if (CurrentPlayer.Deck.Cards[i].CardName == "Plant-Bigtooth-Aspen")
                {
                    MoveCard(i, PlantGameObject, Deck.Cards, PlantPlacement, false);
                }
                else if (CurrentPlayer.Deck.Cards[i].CardName == "Plant-White-Birch")
                {
                    MoveCard(i, PlantGameObject, Deck.Cards, PlantPlacement, false);
                }
            }
            for (int i = 0; i < CurrentPlayer.DiscardPlacement.Count; i++)
            {
                if (CurrentPlayer.Deck.Cards[i].CardName == "Plant-Bigtooth-Aspen")
                {
                    MoveCard(i, PlantGameObject, Deck.Cards, PlantPlacement, false);
                }
                else if (CurrentPlayer.Deck.Cards[i].CardName == "Plant-White-Birch")
                {
                    MoveCard(i, PlantGameObject, Deck.Cards, PlantPlacement, false);
                }
            }
        }


        bool foundBlackberry = false;
        bool foundWhitePine = false;


        for (int i = 0; i < CurrentPlayer.PlantPlacement.Count; i++)
        {
            switch (CurrentPlayer.PlantPlacement[i].CardName)
            {

                case "Plant-Allegeny-Blackberry":
                    foundBlackberry = true;
                    break;


                case "Plant-Eastern-White-Pine":
                    foundWhitePine = true;
                    break;

                default: break;

            }
        }

        if (foundBlackberry && !blackberryActivated)
        {
            bool foundCanopy = false;
            blackberryActivated = true;
            for (int i = 0; i < CurrentPlayer.Deck.Cards.Count; i++)
            {
                if (CurrentPlayer.Deck.Cards[i].PlantType == "Canopy" || CurrentPlayer.Deck.Cards[i].PlantType == "Understory")
                {
                    MoveCard(i, PlantGameObject, CurrentPlayer.Deck.Cards, PlantPlacement, false);
                    break;
                }
            }
            if (!foundCanopy)
            {
                for (int i = 0; i < CurrentPlayer.DiscardPlacement.Count; i++)
                {
                    if (CurrentPlayer.Deck.Cards[i].PlantType == "Canopy" || CurrentPlayer.Deck.Cards[i].PlantType == "Understory")
                    {
                        MoveCard(i, PlantGameObject, CurrentPlayer.Deck.Cards, PlantPlacement, false);
                        break;
                    }
                }
            }
        }

        if (foundWhitePine && !whitePineActivated)
        {
            whitePineActivated = true;
            for (int i = 0; i < CurrentPlayer.Deck.Cards.Count; i++)
            {
                if (CurrentPlayer.Deck.Cards[i].AnimalDiet == "Herbivore")
                {
                    switch (CurrentPlayer.Deck.Cards[i].CardType)
                    {
                        case "Animal":
                            MoveCard(i, AnimalGameObject, CurrentPlayer.Deck.Cards, AnimalPlacement, false);
                            break;

                        case "Invertebrate":
                            MoveCard(i, InvertebrateGameObject, CurrentPlayer.Deck.Cards, InvertebratePlacement, false);
                            break;
                    }
                    break;


                }

            }
        }

        
        for (int i = 0; i < CurrentPlayer.HumanPlacement.Count; i++)
        {

            switch (CurrentPlayer.HumanPlacement[i].CardName)
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
            //Debug.Log("biologist");
            CurrentPlayer.ProtectedFromInvasiveAnimal = true;
        }
        else
        {
            //Debug.Log("NOT biologist");
            CurrentPlayer.ProtectedFromInvasiveAnimal = false;
        }

        if (foundBotanist)
        {
            //Debug.Log("botanist");
            CurrentPlayer.ProtectedFromInvasivePlant = true;
        }
        else
        {
            //Debug.Log("NOT botanist");
            CurrentPlayer.ProtectedFromInvasivePlant = false;
        }

        if (foundExplorer)
        {
            //Debug.Log("explorer");
            CurrentPlayer.NoConditionRequirements = true;
        }
        else
        {
            //Debug.Log("NOT explorer");
            CurrentPlayer.NoConditionRequirements = false;
        }

        if (foundRanger)
        {
            //Debug.Log("ranger");
            CurrentPlayer.ProtectedFromBlight = true;
        }
        else
        {
            //Debug.Log("NOT ranger");
            CurrentPlayer.ProtectedFromBlight = false;
        }

        if (foundTwoSisters)
        {
            //Debug.Log("sisters");
            CurrentPlayer.ProtectedFromExtinction = true;
        }
        else
        {
            //Debug.Log("NOT sisters");
            CurrentPlayer.ProtectedFromExtinction = false;
        }

    }


    /*
     *  @name       ThreeCardExecute() extends parent method
     *  @purpose    used once per game per player and will not be able to be used again once used
     */
    public override void ThreeCardExecute()
    {
        base.ThreeCardExecute();
        Draw(3);
        ThreeCardBurstAvailable = false;
    }

    //where the card things will take place
    IEnumerator ComputerPerforms()
    //public void ComputerPerforms()
    {
        //this instantiates an game object of the class to use as an object and access methods
        //creates a gameobjects
        ReqGO = new GameObject("Req");
        //assigns the script to the game object
        ReqGO.AddComponent<Requirements>();
        //assigns the game object to the script withe the game object
        Req = GameObject.Find("Req").GetComponent<Requirements>();

        //this determinines how many cards the computer draws
        DrawAmount();
        //draws the appropriate amount of cards
        Draw(DrawCount);

        //this is where the requirements will be checked
        for (int z = Hand.Count - 1; z > -1; z--) //done this way to avoid exception
        {
            //creates time using the coroutine
            yield return new WaitForSeconds(5);

            //if the current card has requirements associated with it
            if (Hand[z].ReqID.Count != 0)
            {
                //call the function in requirements to check and pass in the current card and the current instance of the player
                if (Req.RequirementCheck(Hand[z], CurrentPlayer))
                {RequirementsWork = true;}
                else 
                {RequirementsWork = false;}
            }
            //if there are no requirements then the card can automatically be played, usually this is the case with region cards
            else
            {RequirementsWork = true; }

            //if the requirement works then the card is placed in the proper area and move to the correct list
            if (RequirementsWork == true)
            {
                if (Hand[z].CardType == "Region") //puts the card into the region placement
                {
                    //checks the region type and changes the variable accordingly
                    if (Hand[z].CardName.Contains("Arid"))
                        AridCount++;
                    else if (Hand[z].CardName.Contains("Forest"))
                        ForestCount++;
                    else if (Hand[z].CardName.Contains("Grasslands"))
                        GrasslandsCount++;
                    else if (Hand[z].CardName.Contains("Running-Water"))
                        RunningWaterCount++;
                    else if (Hand[z].CardName.Contains("Salt-Water"))
                        SaltWaterCount++;
                    else if (Hand[z].CardName.Contains("Standing-Water"))
                        StandingWaterCount++;
                    else if (Hand[z].CardName.Contains("Sub-Zero"))
                        SubZeroCount++;

                    //calls the method to asssigning the correct sprite and update score and passes in z so it knows which card to work with
                    MoveCard(z, RegionGameObject, RegionPlacement, false);
                }
                else if (Hand[z].CardType == "Condition") //puts the card into the condition card
                {
                    //calls the method to asssigning the correct sprite and update score and passes in z so it knows which card to work with
                    MoveCard(z, ConditionGameObject, ConditionPlacement, false);
                }
                else if (Hand[z].CardType == "Plant") //puts the card into the plant type
                {
                    //calls the method to asssigning the correct sprite and update score and passes in z so it knows which card to work with
                    MoveCard(z, PlantGameObject, PlantPlacement, false);
                }
                else if (Hand[z].CardType == "Invertebrate") //puts the card into the invertebrate pile
                {
                    //calls the method to asssigning the correct sprite and update score and passes in z so it knows which card to work with
                    MoveCard(z, InvertebrateGameObject, InvertebratePlacement, false);
                }
                else if (Hand[z].CardType == "Animal") //puts the cards into the animal pile
                {
                    //calls the method to asssigning the correct sprite and update score and passes in z so it knows which card to work with
                    MoveCard(z, AnimalGameObject, AnimalPlacement, false);
                }
                else if (Hand[z].CardType == "Special Region") //puts the card into the special region pile
                {
                    //Adds regions to the total count when special region cards are played
                    if (Hand[z].CardName.Contains("Farmers"))
                    {
                        GrasslandsCount++;
                        RunningWaterCount++;
                    }
                    else if (Hand[z].CardName.Contains("Strohmstead"))
                    {
                        ForestCount++;
                        GrasslandsCount++;
                        RunningWaterCount++;
                        StandingWaterCount++;
                    }
                    else if (Hand[z].CardName.Contains("Hunters"))
                    {
                        ForestCount++;
                        GrasslandsCount++;
                    }

                    //calls the method to asssigning the correct sprite and update score and passes in z so it knows which card to work with
                    MoveCard(z, SpecialRegionGameObject, SpecialRegionPlacement, false);
                }
                else if (Hand[z].CardType == "Multi-Player") //puts the card into the multiplayer pile
                {
                    //calls the method to asssigning the correct sprite and update score and passes in z so it knows which card to work with
                    MoveCard(z, MultiplayerGameObject, MultiplayerPlacement, false);
                }
                else if (Hand[z].CardType == "Microbe") //puts the card into the microbe pile
                {
                    //calls the method to asssigning the correct sprite and update score and passes in z so it knows which card to work with
                    MoveCard(z, MicrobeGameObject, MicrobePlacement, false);
                }

                else if (Hand[z].CardType == "Fungi") //puts the card into the fungi pile
                {
                    //calls the method to asssigning the correct sprite and update score and passes in z so it knows which card to work with
                    MoveCard(z, FungiGameObject, FungiPlacement, false);
                }
                else if (Hand[z].CardType == "Human") //puts the card into the human pile
                {
                    //calls the method to assigning the correct sprite and update score and passes in z so it knows which card to work with
                    MoveCard(z, HumanGameObject, HumanPlacement, false);
                }
            }
        }

        if (Hand.Count > 0) //if there is a card left in the hand, it will discard the firsts one
        {
            //calls the method to asssigning the correct sprite and update score and passes in z so it knows which card to work with
            MoveCard(0, DiscardGameObject, DiscardPlacement, true);

            yield return new WaitForSeconds(2);
        }
        else //if there are no cards left in the hand, will just automatically go to the next player
        {
            yield return new WaitForSeconds(2);
        }

        //calls the game manager instane to change the players turn and passes imn this instances name so we know which player goes nect
        GameManager.Instance.NextPlayer(PlayerName);
    }

    /*
     *  @name       MoveCard() 
     *  @purpose   Moves the card from the hand to the correct placement and takes in the index from the loop so it knows whch card to use
     *  also it updates the score
     */
    public void MoveCard(int pZ, string pParent, List<Card> pListPlacement, bool pDiscard)
    {
        //assigns where the game object with go to a object
        Debug.Log("Moving cards");
        CardParent = GameObject.Find(pParent).transform;
        Debug.Log(CardParent + "Moved");
        //sets the name so the sprite can show the front of card
        Holder.CardNameHolder = Hand[pZ].CardName;
        ////creates a new card object
        GenerateCardObject();
        ////creates the new sprite with the correct image
        Holder.setSprite(Sr);
        ////tells the current game object at play where to go to
        ////CardObject.transform.SetParent(CardParent);
        ////resizes the card so it fits nicely on the placements
        CardObject.transform.localScale = new Vector3(1.0f, 1.0f, 0);

        if (pDiscard == false)
        ChangeScore(Hand[pZ].PointValue);
  
        ////adds the card from the hand to the correct list
        pListPlacement.Add(Hand[pZ]);
        ////removes the card just played from the hand
        Hand.Remove(Hand[pZ]);

        ////resets the card parent that way if anything funky happens it will return to the hand
        ////but since its a computer nothing like that would probably happen casue there is no dragability for the computer
        CardParent = GameObject.Find(HandGameObject).transform;
        GameObject temp = GameObject.Find(pListPlacement[pListPlacement.Count - 1].CardName);
        Destroy(temp.GetComponent<HoverClass>());
        //temp.AddComponent<DoubleClickDescription>();
        ////to keep from a null excpetion error
        if (Hand.Count > 0)
            Destroy(CardParent.GetChild(0).gameObject);
    }
    public void CSkipRound()
    {
        Round = GameManager.Instance.Round;
        RoundText = GameObject.Find(RoundGameObject).GetComponent<Text>();
        RoundText.text = Round.ToString();
    }

    //accessors and mutators
    public bool RequirementsWork { get => requirementsWork; set => requirementsWork = value; }
    public Computer CurrentPlayer { get => currentPlayer; set => currentPlayer = value; }
    public GameObject ReqGO { get => reqGO; set => reqGO = value; }
    public Requirements Req { get => req; set => req = value; }
}

