using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerBox : MonoBehaviour
{
    private DialogScript DialogMgr;

    [SerializeField]
    private string DialogCharacter;
    [SerializeField]
    private string DialogLine;

    // If negative, will last forever
    [SerializeField]
    private float DialogueMaxTime = 5.0f;

    private bool IsInBox = false;
    private float TimeInBox = 0.0f;

    private System.Guid BoxID;

    private void Awake()
    {
        BoxID = System.Guid.NewGuid();

        DialogScript[] dialogMgrs = FindObjectsOfType<DialogScript>(true);
        if (dialogMgrs != null && dialogMgrs.Length > 0)
        {
            DialogMgr = dialogMgrs[0];
        }
    }

    private void Update()
    {
        if (IsInBox)
        {
            TimeInBox += Time.deltaTime;

            if (DialogueMaxTime > 0.0f)
            {
                if (TimeInBox >= DialogueMaxTime)
                {
                    if (DialogMgr)
                    {
                        DialogMgr.HideDialog(BoxID);
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (DialogCharacter != "" && DialogLine != "")
            {
                if (DialogMgr)
                {
                    TimeInBox = 0.0f;
                    IsInBox = true;

                    DialogMgr.ShowDialog(BoxID, DialogCharacter, DialogLine);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (DialogMgr)
            {
                DialogMgr.HideDialog(BoxID);

                TimeInBox = 0.0f;
                IsInBox = false;
            }
        }
    }
}
