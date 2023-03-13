/*
 *  @class      OpenURL.cs
 *  @purpose    Takes the player to the clients website to purchase decks or view the store, also houses the close application method
 *  
 *  @author     CIS 411 
 *  @date       2020/4/1
 *  
 *  UPDATED:
 *  @author     CIS 411 Martin Bastecki 
 *  @date       2023/2/21
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenURL : MonoBehaviour
{
    /*
     * @name    AlleghenyBuy(), AppalachianBuy(), PeatBogsBuy(), ClarionRiverBuy()
     * @purpose opens clients website to specific decks of cards to purchae
     * 
     * @return  void
     */
    public void AlleghenyBuy()
    {
        Application.OpenURL("https://www.tswgames.com/products/allegheny-national-forest-starter-deck");
        Application.Quit();
    }
    public void AppalachianBuy()
    {
        Application.OpenURL("https://www.tswgames.com/products/appalachian-homestead-starter-deck");
        Application.Quit();
    }

    public void PeatBogsBuy()
    {
        Application.OpenURL("https://www.tswgames.com/products/peat-bogs-of-the-allegheny-front-starter-deck");
        Application.Quit();
    }

    public void ClarionRiverBuy()
    {
        Application.OpenURL("https://www.tswgames.com/products/clarion-river-starter-deck");
        Application.Quit();
    }

    /*
     * @name    OpenHomePage
     * @purpose opens clients website home page
     * 
     * @return  void
     */
    public void OpenHomePage()
    {
        Application.OpenURL("https://www.tswgames.com/");
        Application.Quit(); //just closes the application
    }

        /*
     * @name    OpenFacebook
     * @purpose opens clients facebook for reviews
     * 
     * @return  void
     */
    public void OpenFacebook()
    {
        Application.OpenURL("https://www.tswgames.com/");
        Application.Quit(); //just closes the application
    }

        /*
     * @name    CloseApplication
     * @purpose Closes the entire game application
     * 
     * @return  void
     */
    public void CloseApplication() //to lcose the application - i put it in here because it was just easier to get access to since the buttons are already connected to this script
    {
        Application.Quit(); //closes
    }
}
