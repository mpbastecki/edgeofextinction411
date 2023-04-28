using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  @class      Requirements
 *  @purpose    This class is used to check requirements and also has another class called reqs in it which has all the requirements
 *  
 *  @author     CIS 411
 *  @date       2020/04/13
 */
public class Requirements : Player
{
    void DestroyGameObject()
    {
        Destroy(gameObject);
    }
    /*
      * @name    requirementCheck()
      * @purpose checks to see if the current card passed in passes reuirements and also assigns the player object of the class
      * 
      * @return  bool
      */
    public bool RequirementCheck(Card card, Player pPlayer)
    {

        //the bool that is returned to say wether or not the rewuirements worked
        bool works = false;

        //creates a list to hold requirements
        List<string> reqs = new List<string>();

        //adds each of the requirements associated with the card to a string list
        foreach (string x in card.ReqID)
        {
            string reqID = x;
            reqs.Add(reqID);
        }

        //goes through each requirement string in the list and checks
        foreach (string x in reqs)
        {
            //creates an object of the type that is a Reqs
            Type type = typeof(Reqs);
            //creates an object of Reqs and passes in the current player parameter
            Reqs ClassObject = new Reqs(pPlayer);
            //finds the method that has the same name as the requirement ID in the type object and assigns the correct requirement method to it
            MethodInfo method = type.GetMethod(x);
            //calls the method that was assigned from the list and passes in the "Class Object" so it know where to get the method from
            //and then in the returned value is false then set the requirements work to false
            //use this debug below to find what card was just played or checked 
            try
            {
                if (method.Invoke(ClassObject, null).ToString() == "False")
                {
                    works = false;
                    //i dont think we need this break not sure why its here but ill keep if for now in case anyhting suacy happens
                    break;
                }
                else 
                {
                  works = true; 
                }
            }
            catch(Exception e)
            {
                Debug.Log("had issues with card :(");
            }

            //if iot didnt return false then it returned true which means the requirements worked 
        }

        //returns if the card works or not
        return works;
    }
}

/*
 *  @class      Reqs
 *  @purpose    This class has all the requiorements and is called in Requirements class to check if the card works
 *  
 *  @author     CIS 411
 *  @date       2020/04/13
 */
public class Reqs
{
    //this is to hold the player object. We are using this that way the computer and humans can each use this class. 
    private Player thePlayer;

    private Human humanPerson = GameManager.Instance.Person;
    private Computer compPerson = GameManager.Instance.CP1;

    //this is the constructor that assigns the currentplayer that was passed in from the requirements 
    public Reqs(Player pPlayer)
    {
        ThePlayer = pPlayer;
        //this casts the player so humans and computers can use this class
        //it probably isnt neccisary because computers and humands both share a lot of the same values but we will use it here for expansion 
        //reasons later on
        CastPlayer(ThePlayer);
    }

        /*
      * @name    CastPlayer()
      * @purpose This downcasts the correct player to wether or not it is a human or computer.
      * 
      * @return  bool
      */
    public void CastPlayer(Player pPlayer)
    {
     
        if (ThePlayer.GetType() == typeof(Human))
        {
            ThePlayer = (Human)pPlayer;
        }
        else
        {
            ThePlayer = (Computer)pPlayer;
        }
    
        }


    /*
      * @name    test()
      * @purpose This is just to test stuff out
      * 
      * @return  bool
      */
    //public bool test() //any 1 region card must be placed
    //{
    //    if (ThePlayer.RegionPlacement.Count >= 1)
    //        return true;
    //    //else if (ThePlayer.SpecialRegionPlacement.Count > 0)
    //        //return true;
    //    else return false;
    //}


    /********************************************************************************/
    //All the methods below thise are to check requirements and then accessors and mutators are at the end
    /********************************************************************************/

    //5 forest, grassland, arid, or sub-zero
    // r001
    public bool r001()
    {
        int count = 0;

        count += ThePlayer.ForestCount;
        count += ThePlayer.GrasslandsCount;
        count += ThePlayer.AridCount;
        count += ThePlayer.SubZeroCount;

        if (count >= 5)
            return true;
        else
            return false;
    }

    //1 running or standing water region
    // r002
    public bool r002()
    {
        int count = 0;

        count += ThePlayer.RunningWaterCount;
        count += ThePlayer.StandingWaterCount;

        if (count >= 1)
            return true;
        else
            return false;
    }

    //3 plants
    //r003
    public bool r003()
    {
        if (ThePlayer.PlantPlacement.Count >= 3)
            return true;
        else
            return false;
    }

    //2 invertebrates
    //r004
    public bool r004()
    {
        if (ThePlayer.InvertebratePlacement.Count >= 2)
            return true;
        else
            return false;
    }

    //2 animals
    //r005
    public bool r005()
    {
        if (ThePlayer.AnimalPlacement.Count >= 2)
            return true;
        else
            return false;
    }

    //1 any region
    //r006
    public bool r006()
    {
        int count = 0;

        count += ThePlayer.AridCount;
        count += ThePlayer.GrasslandsCount;
        count += ThePlayer.ForestCount;
        count += ThePlayer.RunningWaterCount;
        count += ThePlayer.StandingWaterCount;
        count += ThePlayer.SaltWaterCount;
        count += ThePlayer.SubZeroCount;

        if(count >= 1)
            return true;
        else
            return false;
    }

    //1 speicies in play or in discard
    //r007
    public bool r007()
    {
        if (ThePlayer.AnimalPlacement.Count > 0 || ThePlayer.PlantPlacement.Count > 0 || ThePlayer.InvertebratePlacement.Count > 0 || ThePlayer.HumanPlacement.Count > 0)
            return true;

        for (int i = 0; i < ThePlayer.DiscardPlacement.Count; i++)
        {
            if (ThePlayer.DiscardPlacement[i].CardType == "Human" || ThePlayer.DiscardPlacement[i].CardType == "Animal" || ThePlayer.DiscardPlacement[i].CardType == "Plant" || ThePlayer.DiscardPlacement[i].CardType == "Invertebrate")
                return true;
        }

        return false;
    }

    //2 any region
    //r008
    public bool r008()
    {
        int count = 0;

        count += ThePlayer.AridCount;
        count += ThePlayer.GrasslandsCount;
        count += ThePlayer.ForestCount;
        count += ThePlayer.RunningWaterCount;
        count += ThePlayer.StandingWaterCount;
        count += ThePlayer.SaltWaterCount;
        count += ThePlayer.SubZeroCount;

        if (count >= 2)
            return true;
        else
            return false;
    }

    //2 plants
    //r009
    public bool r009()
    {
        if (ThePlayer.PlantPlacement.Count >= 2)
            return true;
        else
            return false;
    }

    //1 invertebrate or animal
    //r010
    public bool r010()
    {
        if (ThePlayer.InvertebratePlacement.Count >= 1 || ThePlayer.AnimalPlacement.Count >= 1)
            return true;
        return false;
    }

    //1 forest or grassland
    //r011
    public bool r011()
    {
        int count = 0;

        count += ThePlayer.ForestCount;
        count += ThePlayer.GrasslandsCount;

        if (count >= 1)
            return true;
        else
            return false;
    }

    //1 canopy or understory plant from the division Coniferophyta or magnoliophyta
    //r012
    public bool r012()
    {
        int count = 0;

        for (int i = 0; i < ThePlayer.PlantPlacement.Count; i++)
        {
            if ((ThePlayer.PlantPlacement[i].PlantType == "Canopy" || ThePlayer.PlantPlacement[i].PlantType == "Understory") && (ThePlayer.PlantPlacement[i].Division == "Magnoliophyta" || ThePlayer.PlantPlacement[i].Division == "Coniferophyta"))
                count++;
        }

        if (count >= 1)
            return true;
        else
            return false;
    }

    //3 forest
    //r013
    public bool r013()
    {
        if (ThePlayer.ForestCount >= 3)
            return true;
        else
            return false;
    }

    //1 canopy plant
    //r014
    public bool r014()
    {
        int count = 0;

        for (int i = 0; i < ThePlayer.PlantPlacement.Count; i++)
        {
            if (ThePlayer.PlantPlacement[i].PlantType == "Canopy")
                count++;
        }

        if (count >= 1)
            return true;
        else
            return false;
    }

    //1 tiny or small invertebrate
    //r015
    public bool r015()
    {
        int count = 0;

        for (int i = 0; i < ThePlayer.InvertebratePlacement.Count; i++)
        {
            if (ThePlayer.InvertebratePlacement[i].AnimalSize == "Tiny" || ThePlayer.InvertebratePlacement[i].AnimalSize == "Small")
                count++;
        }

        if (count >= 1)
            return true;
        else
            return false;
    }

    //2 tiny or small animals
    //r016
    public bool r016()
    {
        int count = 0;

        for (int i = 0; i < ThePlayer.AnimalPlacement.Count; i++)
        {
            if (ThePlayer.AnimalPlacement[i].AnimalSize == "Tiny" || ThePlayer.AnimalPlacement[i].AnimalSize == "Small")
                count++;
        }

        if (count >= 2)
            return true;
        else
            return false;
    }

    //1 forest
    //r019
    public bool r017()
    {
        if (ThePlayer.ForestCount >= 1)
            return true;
        else
            return false;
    }

    //3 any region except salt water
    //r020
    public bool r018()
    {
        int count = 0;

        count += ThePlayer.AridCount;
        count += ThePlayer.ForestCount;
        count += ThePlayer.GrasslandsCount;
        count += ThePlayer.RunningWaterCount;
        count += ThePlayer.StandingWaterCount;
        count += ThePlayer.SubZeroCount;

        if (count >= 3)
            return true;
        else
            return false;
    }

    //1 forest or arid region
    //r025
    public bool r019()
    {
        int count = 0;

        count += ThePlayer.ForestCount;
        count += ThePlayer.AridCount;

        if (count >= 1)
            return true;
        else
            return false;
    }

    //1 invertebrate
    //r027
    public bool r020()
    {
        if (ThePlayer.InvertebratePlacement.Count >= 1)
            return true;
        else
            return false;
    }

    //1 tiny or small animal
    //r028
    public bool r021()
    {
        int count = 0;

        for (int i = 0; i < ThePlayer.AnimalPlacement.Count; i++)
        {
            if (ThePlayer.AnimalPlacement[i].AnimalSize == "Tiny" || ThePlayer.AnimalPlacement[i].AnimalSize == "Small")
                count++;
        }

        if (count >= 1)
            return true;
        else
            return false;
    }

    //1 forest, arid, sub-zero, or grassland region
    //r031
    public bool r022()
    {
        int count = 0;

        count += ThePlayer.ForestCount;
        count += ThePlayer.GrasslandsCount;
        count += ThePlayer.AridCount;
        count += ThePlayer.SubZeroCount;

        if (count >= 1)
            return true;
        else
            return false;
    }

    //2 forest or grassland
    //r033
    public bool r023()
    {
        int count = 0;

        count += ThePlayer.ForestCount;
        count += ThePlayer.GrasslandsCount;

        if (count >= 2)
            return true;
        else
            return false;
    }

    //1 canopy or understory plant from the division magnoliophyta
    //r039
    public bool r024()
    {
        int count = 0;

        for (int i = 0; i < ThePlayer.PlantPlacement.Count; i++)
        {
            if ((ThePlayer.PlantPlacement[i].PlantType == "Canopy" || ThePlayer.PlantPlacement[i].PlantType == "Understory") && (ThePlayer.PlantPlacement[i].Division == "Magnoliophyta"))
                count++;
        }

        if (count >= 1)
            return true;
        else
            return false;
    }

    //1 any region except salt water
    //r042
    public bool r025()
    {
        int count = 0;

        count += ThePlayer.AridCount;
        count += ThePlayer.ForestCount;
        count += ThePlayer.GrasslandsCount;
        count += ThePlayer.RunningWaterCount;
        count += ThePlayer.StandingWaterCount;
        count += ThePlayer.SubZeroCount;

        if (count >= 1)
            return true;
        else
            return false;
    }

    //1 plant from the division mangoliophyta
    //r043
    public bool r026()
    {
        for (int i = 0; i < ThePlayer.PlantPlacement.Count; i++)
        {
            if (ThePlayer.PlantPlacement[i].Division == "Magnoliophyta")
                return true;
        }

        return false;
    }

    //1 any region except salt water or sub-zero
    //r044
    public bool r027()
    {
        int count = 0;

        count += ThePlayer.AridCount;
        count += ThePlayer.ForestCount;
        count += ThePlayer.GrasslandsCount;
        count += ThePlayer.RunningWaterCount;
        count += ThePlayer.StandingWaterCount;

        if (count >= 1)
            return true;
        else
            return false;
    }

    //1 plant
    //r045
    public bool r028()
    {
        if (ThePlayer.PlantPlacement.Count >= 1)
            return true;
        else
            return false;
    }

    //1 any region except sub-zero
    //r048
    public bool r029()
    {
        int count = 0;

        count += ThePlayer.AridCount;
        count += ThePlayer.ForestCount;
        count += ThePlayer.GrasslandsCount;
        count += ThePlayer.RunningWaterCount;
        count += ThePlayer.StandingWaterCount;
        count += ThePlayer.SaltWaterCount;

        if (count >= 1)
            return true;
        else
            return false;
    }

    //1 canopy or understory plant
    //r053
    public bool r030()
    {
        for (int i = 0; i < ThePlayer.PlantPlacement.Count; i++)
        {
            if (ThePlayer.PlantPlacement[i].PlantType == "Canopy"
                || ThePlayer.PlantPlacement[i].PlantType == "Understory")
            {
                return true;
            }
        }

        return false;
    }

    //2 forests
    //r057
    public bool r031()
    {
        if (ThePlayer.ForestCount >= 2)
            return true;
        else
            return false;
    }

    //1 groundcover plant
    //r059
    public bool r032()
    {
        for (int i = 0; i < ThePlayer.PlantPlacement.Count; i++)
        {
            if (ThePlayer.PlantPlacement[i].PlantType == "Groundcover")
                return true;
        }

        return false;
    }

    //1 mountain range
    //r060
    public bool r033()
    {
        for (int i = 0; i < ThePlayer.ConditionPlacement.Count; i++)
        {
            if (ThePlayer.ConditionPlacement[i].CardName.Contains("Mountain"))
                return true;
        }

        return false;
    }

    //2 canopy plants
    //r067
    public bool r034()
    {
        int count = 0;

        for (int i = 0; i < ThePlayer.PlantPlacement.Count; i++)
        {
            if (ThePlayer.PlantPlacement[i].PlantType == "Canopy")
                count++;
        }

        if (count >= 2)
            return true;
        else
            return false;
    }

    //1 mycorrhizal fungus
    //r068
    public bool r035()
    {
        for (int i = 0; i < ThePlayer.FungiPlacement.Count; i++)
        {
            if (ThePlayer.FungiPlacement[i].CardName.Contains("Mycorrhizal"))
                return true;
        }

        return false;
    }

    //2 groundcover plants
    //r078
    public bool r036()
    {
        int count = 0;

        for (int i = 0; i < ThePlayer.PlantPlacement.Count; i++)
        {
            if (ThePlayer.PlantPlacement[i].PlantType == "Groundcover")
                count++;
        }

        if (count >= 2)
            return true;
        else
            return false;
    }

        //1 forest, grassland, or sub-zero region
        //r081
        public bool r037()
        {
            int count = 0;

            count += ThePlayer.ForestCount;
            count += ThePlayer.GrasslandsCount;
            count += ThePlayer.SubZeroCount;

            if (count >= 1)
                return true;
            else
                return false;
        }

        //3 any region except salt water or sub-zero
        //r085
        public bool r038()
        {
            int count = 0;

            count += ThePlayer.AridCount;
            count += ThePlayer.ForestCount;
            count += ThePlayer.GrasslandsCount;
            count += ThePlayer.RunningWaterCount;
            count += ThePlayer.StandingWaterCount;

            if (count >= 3)
                return true;
            else
                return false;
        }

        //1 human
        //r089
        public bool r039()
        {
            if (ThePlayer.HumanPlacement.Count >= 1)
                return true;
            else
                return false;
        }

        //2 water regions
        //r101
        public bool r040()
        {
            int count = 0;

            count += ThePlayer.RunningWaterCount;
            count += ThePlayer.StandingWaterCount;
            count += ThePlayer.SaltWaterCount;

            if (count >= 2)
                return true;
            else
                return false;
        }

        //2 tiny, small, or medium animals
        //r103
        public bool r041()
        {
            int count = 0;

            for (int i = 0; i < ThePlayer.AnimalPlacement.Count; i++)
            {
                if (ThePlayer.AnimalPlacement[i].AnimalSize == "Tiny" || ThePlayer.AnimalPlacement[i].AnimalSize == "Small" || ThePlayer.AnimalPlacement[i].AnimalSize == "Medium")
                    count++;
            }

            if (count >= 2)
                return true;
            else
                return false;
        }

        //1 cliffs and canyons
        //r104
        public bool r042()
        {
            for (int i = 0; i < ThePlayer.ConditionPlacement.Count; i++)
            {
            if (ThePlayer.ConditionPlacement[i].CardName.Contains("Cliff"))
                return true;
            }

            return false;
        }

        //1 grassland region
        //r105
        public bool r043()
        {
            if (ThePlayer.GrasslandsCount >= 1)
                return true;
            else
                return false;
        }

        //2 any region except salt water
        //r108
        public bool r044()
        {
            int count = 0;

            count += ThePlayer.AridCount;
            count += ThePlayer.ForestCount;
            count += ThePlayer.GrasslandsCount;
            count += ThePlayer.RunningWaterCount;
            count += ThePlayer.StandingWaterCount;
            count += ThePlayer.SubZeroCount;

            if (count >= 2)
                return true;
            else
                return false;
        }

        //1 tiny, small, or medium invertebrate
        //r109
        public bool r045()
        {
            for (int i = 0; i < ThePlayer.InvertebratePlacement.Count; i++)
            {
                if (ThePlayer.InvertebratePlacement[i].AnimalSize == "Tiny"
                || ThePlayer.InvertebratePlacement[i].AnimalSize == "Small"
                || ThePlayer.InvertebratePlacement[i].AnimalSize == "Medium")
                    return true;
            }

            return false;
        }

        //2 forest, grassland, arid, or sub-zero
        //r111
        public bool r046()
        {
            int count = 0;

            count += ThePlayer.AridCount;
            count += ThePlayer.ForestCount;
            count += ThePlayer.GrasslandsCount;
            count += ThePlayer.SubZeroCount;

            if (count >= 2)
                return true;
            else
                return false;
        }

        //5 any regions or explorer
        //r115
        public bool r047()
        {
            if (GameManager.Instance.Person.NoConditionRequirements)
                return true;

            int count = 0;

            count += ThePlayer.AridCount;
            count += ThePlayer.ForestCount;
            count += ThePlayer.GrasslandsCount;
            count += ThePlayer.RunningWaterCount;
            count += ThePlayer.SaltWaterCount;
            count += ThePlayer.StandingWaterCount;
            count += ThePlayer.SubZeroCount;

            if (count >= 5)
                return true;
            else
                return false;
        }

        //5 running or standing water
        //r137
        public bool r048()
        {
            int count = 0;

            count += ThePlayer.RunningWaterCount;
            count += ThePlayer.StandingWaterCount;

            if (count >= 5)
                return true;
            else
                return false;
        }

        //3 animals
        //r139
        public bool r049()
        {
            if (ThePlayer.AnimalPlacement.Count >= 3)
                return true;
            else
                return false;
        }

        //1 running water region
        //r145
        public bool r050()
        {
            if (ThePlayer.RunningWaterCount >= 1)
                return true;
            else
                return false;
        }

        //1 tiny or small aquatic invertebrate
        //r147
        public bool r051()
        {
            for (int i = 0; i < ThePlayer.InvertebratePlacement.Count; i++)
            {
                if ((ThePlayer.InvertebratePlacement[i].AnimalSize == "Tiny" || ThePlayer.InvertebratePlacement[i].AnimalSize == "Small") && ThePlayer.InvertebratePlacement[i].AnimalEnvironment == "Aquatic")
                    return true;
            }

            return false;
        }

        //1 tiny or small aquatic animal
        //r148
        public bool r052()
        {
            for (int i = 0; i < ThePlayer.AnimalPlacement.Count; i++)
            {
                if ((ThePlayer.AnimalPlacement[i].AnimalSize == "Tiny" || ThePlayer.AnimalPlacement[i].AnimalSize == "Small") && ThePlayer.AnimalPlacement[i].AnimalEnvironment == "Aquatic")
                    return true;
            }

            return false;
        }

        //1 tiny, small, or medium animal
        //r153
        public bool r053()
        {
            for (int i = 0; i < ThePlayer.AnimalPlacement.Count; i++)
            {
                if (ThePlayer.AnimalPlacement[i].AnimalSize == "Tiny" || ThePlayer.AnimalPlacement[i].AnimalSize == "Small" || ThePlayer.AnimalPlacement[i].AnimalSize == "Medium")
                    return true;
            }

            return false;
        }

        //3 running or standing water regions
        //r156
        public bool r054()
        {
            int count = 0;

            count += ThePlayer.RunningWaterCount;
            count += ThePlayer.StandingWaterCount;

            if (count >= 3)
                return true;
            else
                return false;
        }

        //2 tiny or small invertebrates
        //r158
        public bool r055()
        {
            int count = 0;

            for (int i = 0; i < ThePlayer.InvertebratePlacement.Count; i++)
            {
                if (ThePlayer.InvertebratePlacement[i].AnimalSize == "Tiny" || ThePlayer.InvertebratePlacement[i].AnimalSize == "Small")
                    count++;
            }

            if (count >= 2)
                return true;
            else
                return false;
        }

        //1 aquatic plant
        //r164
        public bool r056()
        {
            for (int i = 0; i < ThePlayer.PlantPlacement.Count; i++)
            {
                if (ThePlayer.PlantPlacement[i].AnimalEnvironment == "Aquatic")
                    return true;
            }

            return false;
        }

        //3 animals that require running or standing water
        //r166
        public bool r057()
        {
            int count = 0;

            for (int i = 0; i < ThePlayer.AnimalPlacement.Count; i++)
            {
                int id = ThePlayer.AnimalPlacement[i].ReqID.Count;

                for (int j = 0; j < id; j++) //goes through and checks the requirements of the animals to see if they need running or standing water
                {
                    if (ThePlayer.AnimalPlacement[i].ReqID[j] == "r002"
                        || ThePlayer.AnimalPlacement[i].ReqID[j] == "r048"
                        || ThePlayer.AnimalPlacement[i].ReqID[j] == "r50"
                        || ThePlayer.AnimalPlacement[i].ReqID[j] == "r54"
                        || ThePlayer.AnimalPlacement[i].ReqID[j] == "r61"
                        || ThePlayer.AnimalPlacement[i].ReqID[j] == "r66")
                    {
                        count++;
                        break;
                    }
                }
            }

            if (count >= 3)
                return true;
            else
                return false;
        }

        //3 species that require running or standing water
        //r173
        public bool r058()
        {
            int count = 0;

            for (int i = 0; i < ThePlayer.AnimalPlacement.Count; i++)
            {
                int id = ThePlayer.AnimalPlacement[i].ReqID.Count;

                for (int j = 0; j < id; j++) //goes through and checks the requirements of the animals to see if they need running or standing water
                {
                    if (ThePlayer.AnimalPlacement[i].ReqID[j] == "r002"
                        || ThePlayer.AnimalPlacement[i].ReqID[j] == "r048"
                        || ThePlayer.AnimalPlacement[i].ReqID[j] == "r50"
                        || ThePlayer.AnimalPlacement[i].ReqID[j] == "r54"
                        || ThePlayer.AnimalPlacement[i].ReqID[j] == "r61"
                        || ThePlayer.AnimalPlacement[i].ReqID[j] == "r66")
                    {
                        count++;
                        break;
                    }
                }
            }

            for (int i = 0; i < ThePlayer.PlantPlacement.Count; i++)
            {
                int id = ThePlayer.PlantPlacement[i].ReqID.Count;

                for (int j = 0; j < id; j++) //goes through and checks the requirements of the animals to see if they need running or standing water
                {
                    if (ThePlayer.PlantPlacement[i].ReqID[j] == "r002"
                        || ThePlayer.PlantPlacement[i].ReqID[j] == "r048"
                        || ThePlayer.PlantPlacement[i].ReqID[j] == "r50"
                        || ThePlayer.PlantPlacement[i].ReqID[j] == "r54"
                        || ThePlayer.PlantPlacement[i].ReqID[j] == "r61"
                        || ThePlayer.PlantPlacement[i].ReqID[j] == "r66")
                    {
                        count++;
                        break;
                    }
                }
            }

            for (int i = 0; i < ThePlayer.InvertebratePlacement.Count; i++)
            {
                int id = ThePlayer.InvertebratePlacement[i].ReqID.Count;

                for (int j = 0; j < id; j++) //goes through and checks the requirements of the animals to see if they need running or standing water
                {
                    if (ThePlayer.InvertebratePlacement[i].ReqID[j] == "r002"
                        || ThePlayer.InvertebratePlacement[i].ReqID[j] == "r048"
                        || ThePlayer.InvertebratePlacement[i].ReqID[j] == "r50"
                        || ThePlayer.InvertebratePlacement[i].ReqID[j] == "r54"
                        || ThePlayer.InvertebratePlacement[i].ReqID[j] == "r61"
                        || ThePlayer.InvertebratePlacement[i].ReqID[j] == "r66")
                    {
                        count++;
                        break;
                    }
                }
            }

            for (int i = 0; i < ThePlayer.FungiPlacement.Count; i++)
            {
                int id = ThePlayer.FungiPlacement[i].ReqID.Count;

                for (int j = 0; j < id; j++) //goes through and checks the requirements of the animals to see if they need running or standing water
                {
                    if (ThePlayer.FungiPlacement[i].ReqID[j] == "r002"
                        || ThePlayer.FungiPlacement[i].ReqID[j] == "r048"
                        || ThePlayer.FungiPlacement[i].ReqID[j] == "r50"
                        || ThePlayer.FungiPlacement[i].ReqID[j] == "r54"
                        || ThePlayer.FungiPlacement[i].ReqID[j] == "r61"
                        || ThePlayer.FungiPlacement[i].ReqID[j] == "r66")
                    {
                        count++;
                        break;
                    }
                }
            }

            for (int i = 0; i < ThePlayer.MicrobePlacement.Count; i++)
            {
                int id = ThePlayer.MicrobePlacement[i].ReqID.Count;

                for (int j = 0; j < id; j++) //goes through and checks the requirements of the animals to see if they need running or standing water
                {
                    if (ThePlayer.MicrobePlacement[i].ReqID[j] == "r002"
                        || ThePlayer.MicrobePlacement[i].ReqID[j] == "r048"
                        || ThePlayer.MicrobePlacement[i].ReqID[j] == "r50"
                        || ThePlayer.MicrobePlacement[i].ReqID[j] == "r54"
                        || ThePlayer.MicrobePlacement[i].ReqID[j] == "r61"
                        || ThePlayer.MicrobePlacement[i].ReqID[j] == "r66")
                    {
                        count++;
                        break;
                    }
                }
            }

            if (count >= 3)
                return true;
            else
                return false;
        }

        //1 tiny invertebrate
        //r177
        public bool r059()
        {
            for (int i = 0; i < ThePlayer.InvertebratePlacement.Count; i++)
            {
                if (ThePlayer.InvertebratePlacement[i].AnimalSize == "Tiny")
                    return true;
            }

            return false;
        }

        //1 caves and caverns
        //r209
        public bool r060()
        {
            for (int i = 0; i < ThePlayer.ConditionPlacement.Count; i++)
            {
                if (ThePlayer.ConditionPlacement[i].CardName.Contains("Caves"))
        
            return true;
            }

            return false;
        }

        //1 standing water region
        //r211
        public bool r061()
        {
            if (ThePlayer.StandingWaterCount >= 1)
                return true;
            else
                return false;
        }

        //4 any regions
        //r212
        public bool r062()
        {
            int count = 0;

            count += ThePlayer.AridCount;
            count += ThePlayer.ForestCount;
            count += ThePlayer.GrasslandsCount;
            count += ThePlayer.RunningWaterCount;
            count += ThePlayer.SaltWaterCount;
            count += ThePlayer.StandingWaterCount;
            count += ThePlayer.SubZeroCount;

            if (count >= 4)
                return true;
            else
                return false;
        }

        //1 invertebrate in the family aphidiae
        //r215
        public bool r063()
        {
            for (int i = 0; i < ThePlayer.InvertebratePlacement.Count; i++)
            {
                if (ThePlayer.InvertebratePlacement[i].Family == "Aphididae")
                    return true;
            }

            return false;
        }

        //1 plant from the family asclepiadaceae
        //r217
        public bool r064()
        {
            for (int i = 0; i < ThePlayer.PlantPlacement.Count; i++)
            {
                if (ThePlayer.PlantPlacement[i].Family == "Asclepiadaceae - Milkweed family")
                    return true;
            }

            return false;
        }

        //1 tiny invertebrate or microbe
        //r224
        public bool r065()
        {
            for (int i = 0; i < ThePlayer.InvertebratePlacement.Count; i++)
            {
                if (ThePlayer.InvertebratePlacement[i].AnimalSize == "Tiny")
                    return true;
            }
            for (int i = 0; i < ThePlayer.MicrobePlacement.Count; i++)
            {
                if (ThePlayer.MicrobePlacement[i].AnimalSize == "Tiny")
                    return true;
            }

            return false;
        }

        //2 standing water
        //r225
        public bool r066()
        {
            if (ThePlayer.StandingWaterCount >= 2)
                return true;
            else
                return false;
        }

        //1 plant from the genus sphagnum
        //r226
        public bool r067()
        {
            for (int i = 0; i < ThePlayer.PlantPlacement.Count; i++)
            {
            if (ThePlayer.PlantPlacement[i].Genus.Contains("Sphagnum"))
                return true;
            }

            return false;
        }

        //1 peat bog
        //r228
        public bool r068()
        {
            for (int i = 0; i < ThePlayer.ConditionPlacement.Count; i++)
            {
            if (ThePlayer.ConditionPlacement[i].CardName.Contains("Peat-Bog"))
                return true;
            }

            return false;
        }

        //1 any region except arid or salt water
        public bool r069()
        {
            int count = 0;

            count += ThePlayer.ForestCount;
            count += ThePlayer.GrasslandsCount;
            count += ThePlayer.RunningWaterCount;
            count += ThePlayer.StandingWaterCount;
            count += ThePlayer.SubZeroCount;

            if (count >= 1)
                return true;
            else
                return false;
        }


    //these still need made. I havr them all return false for now so they can be played. This can be expanded in a later release
    //REQUIREMENTS R070 - R079 ARE FOR THE MULTIPLAYER CARDS

    public bool r070() //acidic waters
    {
        return true;
    }

    public bool r071() //children at play
    {
        return true;
    }

    public bool r072() //extinction - as of right now there should be no requirements, i just jave this here as a precautionary detail for further use
    {
        
            return true;
        
    }

    public bool r073() //isolated ecosystems
    {
        return true;
    }

    public bool r074() //temperature drop
    {
        return true;
    }

    public bool r075() //ideal conditions
    {
        return true;
    }

    public bool r076() //invasive invertebrate species
    {
        
        return true;
    }

    public bool r077() //web of life
    {
        return true;
    }

    public bool r078() //relocate species
    {
        return true;
    }

    public bool r079() //forest fire
    {
        return true;
    }

    //Checks to see if the explorer is played in the field. Condition card can be played without necessary requirements if Explorer is played

    public bool r246() //Barred Owl to be played from deck or discard
    {
        //Variables hold the number of canopy plants and if the requirement is satisfied
        bool canopyCheck = false;
        int canopyCount = 0;

        //Checks through each played plant for type canopy
        for (int i = 0; i < ThePlayer.PlantPlacement.Count; i++)
        {
            //Counts each canopy plant
            if (ThePlayer.PlantPlacement[i].PlantType == "Canopy")
                canopyCount++;
        }

        //Satisfies the requirement if there are at least three played
        if (canopyCount >= 3)
            canopyCheck = true;

        //Variables hold the number of tiny or small animals and if the requirement is satisfied
        bool animalCheck = false;
        int animalCount = 0;

        //Checks through each played animal for tiny or small size
        for (int i = 0; i < ThePlayer.AnimalPlacement.Count; i++)
        {
            //Counts each tiny or small animal
            if (ThePlayer.AnimalPlacement[i].AnimalSize == "Tiny" || ThePlayer.AnimalPlacement[i].AnimalSize == "Small")
                animalCount++;
        }

        //Satisfies the requirement if there are at least two tiny or small animals
        if (animalCount >= 2)
            animalCheck = true;

        //Checks all requirements including playable requirements
        if (canopyCheck && animalCheck )//&& r013() && r014() && r015() && r016())
            return true;

        return false;

    }


    public bool r247() //Darkling beetle larvae to be played from deck or discard
    {
        //Variable holds if the requirement is satisfied
        bool humanCheck = false;

        //Checks if three humans are played
        
        if (thePlayer.HumanPlacement.Count >= 1)
        {
            humanCheck = true;
        }
        //Checks all requirements
        if (humanCheck)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool r248()//for big tooth aspen and white birch
    {
        
        for (int i = 0; i < humanPerson.MultiplayerPlacement.Count; i++)
        {
            //Counts each tiny or small animal
            if (humanPerson.MultiplayerPlacement[i].CardName == "Multi-Forest-Fire")
                return true;
        }
        for (int i = 0; i < compPerson.MultiplayerPlacement.Count; i++)
        {
            //Counts each tiny or small animal
            if (compPerson.MultiplayerPlacement[i].CardName == "Multi-Forest-Fire")
                return true;
        }



        return false;
    }


        
    //accessors and mutators
    public Player ThePlayer { get => thePlayer; set => thePlayer = value; }
    
}



