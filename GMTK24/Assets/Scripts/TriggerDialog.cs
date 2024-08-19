using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialog : MonoBehaviour
{
    [SerializeField] private DialogScript canvas;
    public string message;
    private bool toTalk = false;

    void Update(){
        if(Input.GetKeyDown(KeyCode.T)){
            toTalk = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision){
        if(collision.CompareTag("Player")){
            if(toTalk){
                canvas.SetDialog(message);
            }
        }
    }
    
}
