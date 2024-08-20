using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{


   public void StartGame(){
        SceneManager.LoadScene("Tutorial_Ferran");
   }

   public void QuitGame(){
       Application.Quit();
   }
}
