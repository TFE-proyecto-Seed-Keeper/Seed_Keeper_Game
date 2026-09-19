using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void ConfirmQuitGame()
    { 
        Application.Quit();
        print("Quit Game Confirmed");
    }
}
