/*
 *  @class      CSVParser
 *  @purpose    Request card information from locally-stored CSV files and populate card list
 *  
 *  @author     John Georgvich
 */

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CSVParser
{
    //  string declaration block
    //      strings are required; class variables hold project referenced to csv datafiles
    private string actionPath, requirementPath, deckOnePath, deckTwoPath, deckThreePath, deckFourPath; 
    
    /*
     *  @name       LoadFiles()
     *  @purpose    Sets appropriate directory locations for CSV datafiles
     *                  Separate method for a reason; if CSV locations are altered/new CSVs added, change here
     */
    public void LoadFiles()
    {
        //  @note:  root directory is project directory, as per Unity
        this.actionPath = "Assets/Resources/Data/Actions.csv";
        this.requirementPath = "Assets/Resources/Data/Requirements.csv";
        this.deckOnePath = "Assets/Resources/Data/DeckOne.csv";
        this.deckTwoPath = "Assets/Resources/Data/DeckTwo.csv";
        this.deckThreePath = "Assets/Resources/Data/DeckThree.csv";
        this.deckFourPath = "Assets/Resources/Data/DeckFour.csv";
    }

    /*
     *  @name       ParseRequirements()
     *  @purpose    Parses Requirements.csv; returns List<string[]> of card requirements for use by GameManager obj.
     *                  List<string[]> *is* necessary because requirements (requirement ID & card ID) are stored in pairs
     *                      Each entry in the list will be as follows:
     *                          Index 0: requirement ID
     *                          Index 1: card ID
     *                  Returned List<string[]> is also used to populate requirements in Deck construction
     *                  See below:  GetDecks()
     *  
     *  @return     List<string[]> containing requirement refs.
     */
    public List<string[]> ParseRequirements()
    {
        List<string[]> requirementIDs = new List<string[]>();
        using (var parser = new StreamReader(this.requirementPath))
        {
            while (!parser.EndOfStream)
            {
                var line = parser.ReadLine();
                string[] requirementCard = line.Split(',');
                requirementIDs.Add(requirementCard);
            }
        }

        return requirementIDs;
    }

    /*
     *  @name       ParseActionIDs()
     *  @purpose    Parses Actions.csv; returns string[] of card actions for use by GameManager obj.
     *                  List<string[]> *is* necessary because requirements (action ID & card ID) are stored in pairs
     *                      Each entry in the list will be as follows:
     *                          Index 0: action ID
     *                          Index 1: card ID
     *                  
     *                  Returned string[] is also used to populate actionIDs in Deck construction
     *                  See below:  GetDecks()
     *                  
     *  @return     List<string[]> containing actionID refs.
     */
    public List<string[]> ParseActionIDs()
    {
        List<string[]> actionIDs = new List<string[]>();
        using (var parser = new StreamReader(this.actionPath))
        {
            while (!parser.EndOfStream)
            {
                var line = parser.ReadLine();
                string[] actionCard = line.Split(',');
                actionIDs.Add(actionCard);
            }
        }

        return actionIDs;
    }

    /*
     *  @name       DeckParse()
     *  @param      string deckPath                 filepath of the deck-specific CSV to be parsed
     *              List<string[]> actionList       list of action IDs to populate card-specific actions
     *              List<string[]> requirementList  list of requirement IDs to populate card-specific requirements
     *              
     *  @purpose    Builds individual, specified deck
     *                  Populates a single deck with cards and returns
     *  @return     Deck object
     */
     
    private Deck DeckParse(string deckPath, List<string[]> actionList, List<string[]> requirementList)
    {
        //  temporary Deck obj. for return
        Deck parsedDeck = new Deck();

        using (var parser = new StreamReader(deckPath))
        {
            //  reads single Deck from specified filepath (@param deckpath), adds parsed cards to temporary Deck obj
            while (!parser.EndOfStream)
            {
                var line = parser.ReadLine();
                /*  
                 *  calls Card constructor with parameter of line.Split(',')
                 *      line.Split splits a line of the document into an array
                 *      see Card overloaded constructor for contents of array and indices
                 */
                Card tempCard = new Card(line.Split(','));
                parsedDeck.Cards.Add(tempCard);
            }
        }

        /*
         *  populates actionIDs and requirementIDs for each card in temporary deck obj.
         *      action/requirement arrays are searched (two loops because array-size is drastically different)
         *      if cardID from array matches cardID from the foreach-specified Card object in the temporary Deck obj, 
         *          the associated action/requirement ID is added to the foreach-specified Card objects' appropriate List
         *              See Card class for List
         */
        foreach(Card card in parsedDeck.Cards)
        {
            for(int i = 0; i < actionList.Count; i++)
            {
                string[] actionArray = actionList[i];
                string actionID = actionArray[0];
                string cardID = actionArray[1];

                if(card.CardID == cardID)
                {
                    card.ActionID.Add(actionID);
                }
            }
            for(int i = 0; i < requirementList.Count; i++)
            {
                string[] requirementArray = requirementList[i];
                string requirementID = requirementArray[0];
                string cardID = requirementArray[1];

                if(card.CardID == cardID)
                {
                    card.ReqID.Add(requirementID);
                }
            }
        }

        //  temporary deck object is returned
        return parsedDeck;
    }


    /*
     *  @name       GetDecks()
     *  @param      List<string[]> actionList       list of action IDs to populate card-specific actions
     *              List<string[]> requirementList  list of requirement IDs to populate card-specific requirements
     *              
     *  @purpose    Parses each deck-specific CSV to create a permanent Deck object, 
     *                  populates Deck object data-members
     *  @return     List<Deck> containing all decks parsed
     */
    public List<Deck> GetDecks(List<string[]> actionList, List<string[]> requirementList)
    {
        //  temporary list of decks; to be returned
        List<Deck> parsedDecks = new List<Deck>();
        //  array is used to parse-by-loop; see below
        string[] deckPaths = new string[] { this.deckOnePath, this.deckTwoPath, this.deckThreePath, this.deckFourPath };

        //  used for deck ID numbering; see below
        int deckCount = 0;

        /*
         *  MAIN LOOP:
         *      deckCount is incremented (starting value is 0; value is 1 on first deck parse)
         *      temporary Deck object is created from specified CSV (deckPaths)
         *      Deck.DeckID is assigned via string concatenation w/ int deckCount
         *          value is 1 on first deck parse, DeckID will be "D00" + "1" = "D001"
         *      Based on DeckID value, Deck.DeckName and Deck.DeckColor are assigned
         *          these are hard-coded, predetermined values
         *      Parsed and complete deck is added to temporary list of decks for return
         */
        foreach (string deckPath in deckPaths)
        {
            deckCount++;
            Deck tempDeck = DeckParse(deckPath, actionList, requirementList);
            tempDeck.DeckId = "D00" + deckCount;
            switch (tempDeck.DeckId)
            {
                case ("D001"):
                    tempDeck.DeckName = "Allegheny Forest";
                    tempDeck.DeckColor = new Color32(58, 102, 44, 128);
                    break;
                case ("D002"):
                    tempDeck.DeckName = "Appalachian Homestead";
                    tempDeck.DeckColor = new Color32(166, 135, 82, 128);
                    break;
                case ("D003"):
                    tempDeck.DeckName = "Peat Bogs";
                    tempDeck.DeckColor = new Color32(124, 56, 58, 128);
                    break;
                case ("D004"):
                    tempDeck.DeckName = "Clarion River";
                    tempDeck.DeckColor = new Color32(116, 126, 140, 128);
                    break;
                case (null):
                    Debug.Log("You shouldn't have come here (CSVParser.GetDecks()).");
                    //  null case is debug only; you shouldn't reach this position
                    break;
            }
            //  add temporary deck to list of temporary deck objects
            parsedDecks.Add(tempDeck);
        }

        //  return temporary list of deck objects; see GameManager
        return parsedDecks;
    }
}
