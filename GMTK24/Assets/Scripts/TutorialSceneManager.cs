using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialSceneManager : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collider){
        if(collider.CompareTag("Player")){
            // Load the tutorial scene
            SceneManager.LoadScene("GameScene");
        }
    }
}
