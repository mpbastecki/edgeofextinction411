using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is used to make the action botton in the cardinforpanal under the cardinforcanvas to do things


public class CardPickerPanelOpener : Player
{

    private CardPickerPanelOpener currentPlayer;
   
   //CurrentPlayer.this;
    public GameObject CardInfoPanel;
    // public GameObject CardPickerPanel;

    
   
   public void OpenCardPickerPanel()
   {
        if (CardInfoPanel != null)
        {            
            CardInfoPanel.SetActive(false);
        }
        for (int i = 0; i < CurrentPlayer.HumanPlacement.Count; i++)
        {
            if (CurrentPlayer.HumanPlacement[i].CardName == "Human-Two-Sisters-In-The-Wild")
            {

                Destroy(GameObject.Find("Human-Two-Sisters-In-The-Wild"));
                MoveCard(i, DiscardGameObject, HumanPlacement, DiscardPlacement, true);
            }
        }
    }

    public CardPickerPanelOpener CurrentPlayer { get => currentPlayer; set => currentPlayer = value; }

}
