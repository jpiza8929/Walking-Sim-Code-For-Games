using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
   PlayerControl playercontrol;

   public string Door, notdoor;

   

   
   
    // Start is called before the first frame update
    void Start()
    {
       playercontrol = GameObject.Find("Player").GetComponent<PlayerControl>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //This function destroys the key gameobject when the boolean from the playercontrol script is true//
    void OnMouseDown()
    {
        if(playercontrol.hasKey == true)
        {
            //Destroys the door gameobject//
            Destroy(this.gameObject);
            

        }
    }

    // Displays door text when hovering the mouse over the door//
    void OnMouseOver()
    {
        playercontrol.itemText.text = Door; 
    }

   // When not hovering the mouse over the door gameobject, display notdoor text//
    void OnMouseExit()
    {
        playercontrol.itemText.text = notdoor; 
    }

    
}
