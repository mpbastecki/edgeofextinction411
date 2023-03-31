using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPickerPanelOpener : MonoBehaviour
{
    public GameObject CardPickerPanel;
   public void OpenCardPickerPanel()
    {
        
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
