using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerControl : MonoBehaviour
{
    public float speed;

    public float upRotation;
    public float downRotation;

    CharacterController characterControl;
    
    ItemInfo iteminfo;
    
    public Transform playerCam;

    Vector3 velocity;

    public float lookSensitivity;

    float xRotation;

    public TMP_Text itemText;
    public string lookingAt = "nothing";

    public bool hasKey;

    
    AudioSource myAudiosource;  
    public AudioClip KeySound;


    //Bullet Reference//

    public GameObject BulletPrefab;
    

    //Bullet List//
    List<GameObject> bullets = new List<GameObject>();
    
    //Bullet Index//
    int BulletIndex = 0;
    public int BulletNum;

    //Camera Reference//
     
     public Camera mainCam;


    // Ray cast distance//

    public float CastDist;

   //This gameobject will create itself whenever the player shoots and where it's location was at//
    public GameObject hitmarker;

    //This vector3 variable holds on top whatever you hit//
    Vector3 pointHit;

    //This boolean tells if the player was able to successfully hit something//
    bool successfulhit; 


    //gravity simulation following Newton's third law of gravity//
    //If set to 0, the player would continue to float up//
    private float gravity = 9.81f;


    //jump speed & gravity influence multiplier//

    private float jumpspeed = 10.89f;

    private float gravinf = 2.8f;


    //temporary replacement varaible for velocity//
    private float _velocityY;


    //GameManager script reference//
    
    GameManager gameManager;

    //bool for pressing the e key//

     bool isKeyPresed;

    //This bool prevents the key from being destroyed at a far distance//
    //This bool is set to true when the raycast touches an object named "key"//
    //I referenced this variable in the iteminfo script by saying if this bool is true, then destroy key//
     public bool rayistouchingKey = false;
  
     // Start is called before the first frame update
    void Start()
    {
        //get the charactercontroller component so you can be able to use it//
        //locks the cursor at the start of the game//
        //gain access to the audiosource on the player in the inspector tab//
        
    
        characterControl = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        itemText.text = lookingAt;
        hasKey = false;
        myAudiosource = GetComponent<AudioSource>();
      
       CreateBulletPool();

       gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

      
       
      }



    // Update is called once per frame
    void Update()
    {
      //these line of code are to rotate the player as the camera rotates, camera sensitivity, etc.//
      
        transform.Rotate(0, Input.GetAxis("Mouse X") * lookSensitivity, 0);
        xRotation -= Input.GetAxis("Mouse Y") * lookSensitivity;
        xRotation = Mathf.Clamp(xRotation, -upRotation, downRotation);
        playerCam.localRotation = Quaternion.Euler(xRotation,0,0);

        velocity.x = Input.GetAxis("Vertical") * speed;
        velocity.z = Input.GetAxis("Horizontal") * speed;

        velocity = transform.TransformDirection(velocity);
        characterControl.Move(velocity * Time.deltaTime);



      //Access the isGrounded function within the charactercontroller component//
      //This checks to see of the gameobject was touching the ground during the last move or frame//
      //This code prevents the player from jumping endlessly//
      
       if(characterControl.isGrounded)
       {
        if(Input.GetKeyDown(KeyCode.Space))
          {
          _velocityY = jumpspeed;
          
        }
       }


    
        //gravity, brings the player back down to the ground when finishing a jump//
        _velocityY -= gravity * Time.deltaTime * gravinf;
        
        //Without this code, the player can't jump fully//
         velocity.y = _velocityY;

        
        
        
        
        

        
        if(Input.GetMouseButtonDown(1))
        {
          GameObject currentbullet = bullets[BulletIndex];
          currentbullet.SetActive(true);
          currentbullet.transform.position = transform.position;
          currentbullet.GetComponent<Rigidbody>().velocity = 19 * transform.forward;
          BulletIndex++;
          

          if(BulletIndex >= bullets.Count)
          {
            BulletIndex = 0;
          }
        }

      //if the bullet reaches a specific distance, it will disappear//
      if(Vector3.Distance(playerCam.transform.position, gameObject.transform.position)> 10f)
      {
        gameObject.SetActive(false);
      }

      if(Input.GetMouseButtonDown(0) && successfulhit)
      {
        Instantiate(hitmarker, pointHit, Quaternion.identity);
     

        

      }

      if(Input.GetKeyDown(KeyCode.E)) 
      {
        isKeyPresed = true;
      }
      else
      {
        isKeyPresed = false;
      }

      }

    //Custom function that plays the ding sound//
    public void playsound()
    {
      
       myAudiosource.PlayOneShot(KeySound);
    }

    void CreateBulletPool()
    {

      for(int i = 0; i< BulletNum; i++)
      {
        GameObject newBullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
        newBullet.SetActive(false);
        bullets.Add(newBullet);
      }

      
    }

    void FixedUpdate()
    {

     RaycastHit Hit;
     Vector3 StartingPosofRay = mainCam.ViewportToWorldPoint(Input.mousePosition);
     if(Physics.Raycast(StartingPosofRay, playerCam.forward, out Hit, CastDist))
     {
      Debug.Log(Hit.transform.name);
      successfulhit = true;

     
     //accessing the vector 3 location of wherever the player hits//
      pointHit = Hit.point;

      if(Hit.transform.tag == "Key" && isKeyPresed)
      {
        rayistouchingKey = true;
        //accessing the keyline string from the iteminfo script//
        string newDialogue = Hit.transform.gameObject.GetComponent<KeyDialogue>().keyLine;
        gameManager.SetDialogueText(newDialogue);
        
        
       
      }
     }

     //this else statement is for if the player wasn't able to hit something, the code above is for when the player hits something//
     else
     {
      successfulhit = false;
     }

     Debug.DrawRay(StartingPosofRay, playerCam.forward * 1000, Color.red);
   }
   
}

   


    

  

    

