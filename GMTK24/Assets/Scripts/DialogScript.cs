using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogScript : MonoBehaviour
{


    [SerializeField] private List<string> dialogs;
    [SerializeField] Text dialog;
    [SerializeField] Text nameText;

    private float inputTimer;
    private float inputTime = 0.2f;

    private int numDialogs;
    private int zeroDialog;
    private int currentDialog;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    void Update(){
        if(this.gameObject.activeInHierarchy == true){
            inputTimer -= Time.deltaTime;
            if(Input.GetKeyDown(KeyCode.T) && inputTimer < 0){
                inputTimer = inputTime;
                if(currentDialog < numDialogs){
                    dialog.text = dialogs[zeroDialog + currentDialog];
                }
                else{
                    this.gameObject.SetActive(false);
                }
            }
        }
    }

    public void SetDialog(string source){
        switch (source){
            case "Caterpillar":
                numDialogs = int.Parse(dialogs[0]);
                zeroDialog = 1;
                currentDialog = 0;
                dialog.text = dialogs[1];
                nameText.text = "Caterpillar Lady";
                this.gameObject.SetActive(true);
                break;
        }

    }

}
