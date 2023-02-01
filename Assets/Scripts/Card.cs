/*
 *  @class      Card.cs
 *  @purpose    Store card information for use by internal game objects
 *  
 *  @author     John Georgvich
 *  @date       2020
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card {

    //  class variable declaration block
    private string cardID;
    private string cardName;
    private string cardType;
    private int pointValue;
    private string kingdom;
    private string subkingdom;
    private string superdivision;
    private string division;
    private string subdivision;
    private string superphylum;
    private string phylum;
    private string subphylum;
    private string cardClass;
    private string superclass;
    private string subclass;
    private string superorder;
    private string order;
    private string suborder;
    private string superfamily;
    private string family;
    private string subfamily;
    private string supergenus;
    private string genus;
    private string subgenus;
    private string superspecies;
    private string species;
    private string subspecies;
    private string animalSize;
    private string animalEnvironment;
    private string animalDiet;
    private string plantType;
    private string regionType;
    private string domain;
    private string infraclass;
    private string infraorder;
    private string section;
    private string tribe;
    private string fungi_type;
    private string standingAction;
    private string specialAction;
    private string cardNotes;

    public List<string> reqID;
    private List<string> actionID;

    //  Accessor and mutator block
    public string CardID { get { return cardID; } set { cardID = value; } }
    public string CardName { get { return cardName; } set { cardName = value; } }
    public string CardType { get { return cardType; } set { cardType = value; } }
    public int PointValue { get { return pointValue; } set { pointValue = value; } }
    public string Kingdom { get { return kingdom; } set { kingdom = value; } }
    public string Subkingdom { get { return subkingdom; } set { subkingdom = value; } }
    public string Superdivision { get { return superdivision; } set { superdivision = value; } }
    public string Division { get { return division; } set { division = value; } }
    public string Subdivision { get { return subdivision; } set { subdivision = value; } }
    public string Superphylum { get { return superphylum; } set { superphylum = value; } }
    public string Phylum { get { return phylum; } set { phylum = value; } }
    public string Subphylum { get { return subphylum; } set { subphylum = value; } }
    public string CardClass { get { return cardClass; } set { cardClass = value; } }
    public string Superclass { get { return superclass; } set { superclass = value; } }
    public string Subclass { get { return subclass; } set { subclass = value; } }
    public string Superorder { get { return superorder; } set { superorder = value; } }
    public string Order { get { return order; } set { order = value; } }
    public string Suborder { get { return suborder; } set { suborder = value; } }
    public string Superfamily { get { return superfamily; } set { superfamily = value; } }
    public string Family { get { return family; } set { family = value; } }
    public string Subfamily { get { return subfamily; } set { subfamily = value; } }
    public string Supergenus { get { return supergenus; } set { supergenus = value; } }
    public string Genus { get { return genus; } set { genus = value; } }
    public string Subgenus { get { return subgenus; } set { subgenus = value; } }
    public string Superspecies { get { return superspecies; } set { superspecies = value; } }
    public string Species { get { return species; } set { species = value; } }
    public string Subspecies { get { return subspecies; } set { subspecies = value; } }
    public string AnimalSize { get { return animalSize; } set { animalSize = value; } }
    public string AnimalEnvironment { get { return animalEnvironment; } set { animalEnvironment = value; } }
    public string AnimalDiet { get { return animalDiet; } set { animalDiet = value; } }
    public string PlantType { get { return plantType; } set { plantType = value; } }
    public string RegionType { get { return regionType; } set { regionType = value; } }
    public string Domain { get { return domain; } set { domain = value; } }
    public string Infraclass { get { return infraclass; } set { infraclass = value; } }
    public string Infraorder { get { return infraorder; } set { infraorder = value; } }
    public string Section { get { return section; } set { section = value; } }
    public string Tribe { get { return tribe; } set { tribe = value; } }
    public string Fungi_type { get { return fungi_type; } set { fungi_type = value; } }
    public List<string> ReqID { get { return reqID; } set { reqID = value; } }
    public List<string> ActionID { get { return actionID; } set { actionID = value; } }
    public string StandingAction { get { return this.standingAction; } set { this.standingAction = value; } }
    public string SpecialAction { get { return this.specialAction; } set { this.specialAction = value; } }
    public string CardNotes { get { return this.cardNotes; } set { this.cardNotes = value; } }

    /*
     *  @name       Card(string[] values)
     *  @author     John Georgvich
     *  @purpose    Class constructor with parameters
     *                  Builds class with string[] passed by CSVParser during deck build
     *                  
     *  @note       There *is* an index offset; values[7] is missing
     */
    public Card(string[] values)
    {
        this.cardID = values[0];
        this.cardName = values[1];
        this.cardType = values[2];
        this.pointValue = int.Parse(values[3]);
        this.kingdom = values[4];
        this.subkingdom = values[5];
        this.superphylum = values[6];
        this.phylum = values[8];
        this.subphylum = values[9];
        this.superclass = values[10];
        this.cardClass = values[11];
        this.subclass = values[12];
        this.superorder = values[13];
        this.order = values[14];
        this.suborder = values[15];
        this.superfamily = values[16];
        this.family = values[17];
        this.subfamily = values[18];
        this.supergenus = values[19];
        this.genus = values[20];
        this.subgenus = values[21];
        this.superspecies = values[22];
        this.species = values[23];
        this.subspecies = values[24];
        this.animalSize = values[25];
        this.animalEnvironment = values[26];
        this.animalDiet = values[27];
        this.plantType = values[28];
        this.regionType = values[29];
        this.domain = values[30];
        this.infraclass = values[31];
        this.infraorder = values[32];
        this.section = values[33];
        this.tribe = values[34];
        this.division = values[35];
        this.superdivision = values[36];
        this.subdivision = values[37];
        this.fungi_type = values[38];
        this.standingAction = values[39].ToString();
        this.specialAction = values[40].ToString();
        this.cardNotes = values[41];

        this.reqID = new List<string>();
        this.actionID = new List<string>();
    }
    
    /*  
     *  @name       Card()
     *  @purpose    Default constructor for Card class
     */
    public Card()
    {
        CardID = "";
        CardName = "";
        CardType = "";
        PointValue = 0;
        Kingdom = "";
        Subkingdom = "";
        Superdivision = "";
        Division = "";
        Subdivision = "";
        Superphylum = "";
        Phylum = "";
        Subphylum = "";
        Superclass = "";
        CardClass = "";
        Subclass = "";
        Superorder = "";
        Order = "";
        Suborder = "";
        Superfamily = "";
        Family = "";
        Subfamily = "";
        Supergenus = "";
        Genus = "";
        Subgenus = "";
        Superspecies = "";
        Species = "";
        Subspecies = "";
        AnimalSize = "";
        AnimalEnvironment = "";
        AnimalDiet = "";
        PlantType = "";
        RegionType = "";
        StandingAction = "";
        SpecialAction = "";
        CardNotes = "";

        reqID = new List<string>();
        actionID = new List<string>();
    }
}
