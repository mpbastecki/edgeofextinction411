using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPickerPanelOpener : MonoBehaviour
{

    //public override void InitializeObjects(string pCardPickerHand)
  //  {
    //    base.InitializeObject(pCardPickerHand);
   // }


    public GameObject CardPickerPanel;
   public void OpenCardPickerPanel()
    {
        //base.OpenCardPickerPanel(pAmount);

        if (CardPickerPanel != null)
        {
            CardPickerPanel.SetActive(true);
            
        }

       

    }

    public void CloseCardPickerPanel()
    {

        if (CardPickerPanel != null)
        {
            CardPickerPanel.SetActive(false);
        }
    }

}
