using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TMP_Text keyDialogueText;

    float timer;
    public float resetTime;

    //this gameobject is a reference to the panel gameobject which holds the dialogue as a xhild//
    public GameObject dialogueObj;
   

   //this function activates the dialogue text//
   public void SetDialogueText(string newDialogueline)
   {
    dialogueObj.SetActive(true);
    keyDialogueText.text = newDialogueline;
    timer = resetTime;
   }


   void Update()
   {
    timer -=Time.deltaTime;
    if(timer <0)
    {
        if(dialogueObj.activeSelf)
        {
            dialogueObj.SetActive(false);
        }
    }
   }
}
