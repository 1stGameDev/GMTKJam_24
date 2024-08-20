using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogScript : MonoBehaviour
{
    [SerializeField] Text dialog;
    [SerializeField] Text nameText;

    private System.Guid CurrentID;

    private void Awake()
    {
        CleanDialog();
    }

    //void Update(){
    //    if(this.gameObject.activeInHierarchy == true){
    //        inputTimer -= Time.deltaTime;
    //        if(Input.GetKeyDown(KeyCode.T) && inputTimer < 0){
    //            inputTimer = inputTime;
    //            if(currentDialog < numDialogs){
    //                dialog.text = dialogs[zeroDialog + currentDialog];
    //            }
    //            else{
    //                this.gameObject.SetActive(false);
    //            }
    //        }
    //    }
    //}

    public void ShowDialog(System.Guid id, string charName, string dialogue)
    {
        if (dialog)
        {
            dialog.text = dialogue;
        }
        if (nameText)
        {
            nameText.text = charName;
        }

        CurrentID = id;

        gameObject.SetActive(true);
    }

    public void CleanDialog()
    {
        if (dialog)
        {
            dialog.text = "";
        }
        if (nameText)
        {
            nameText.text = "";
        }

        gameObject.SetActive(false);
    }

    public void HideDialog(System.Guid id)
    {
        if (id == CurrentID)
        {
            CleanDialog();
        }
    }
}
