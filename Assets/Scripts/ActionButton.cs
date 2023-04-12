using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionButton : Player
{
    //  private CardPickerPanelOpener currentPlayer;

    //CurrentPlayer.this;
    public GameObject CardInfoPanel;
    public GameObject newObject;

    public void CloseInfoPanal()
    {
        if (CardInfoPanel != null)
        {
            CardInfoPanel.SetActive(false);
        }

    }

    public void logTestfunc()
    {
        int i = 2;
        switch (i)
        {
            case 0:
                TestFuncOne();
                break;
            case 1:
                TestFuncTwo();
                break;
            case 2:
                TestFuncThree();
                break;
        }



    }


    public void TestFuncOne()
    {
        Debug.Log("First test func worked");
    }
    public void TestFuncTwo()
    {
        Debug.Log("Second test func worked");
    }
    public void TestFuncThree()
    {
        Debug.Log("Third test func worked");
    }


    // public CardPickerPanelOpener CurrentPlayer { get => currentPlayer; set => currentPlayer = value; }
}
