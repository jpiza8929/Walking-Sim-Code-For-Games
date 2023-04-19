using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassSpawn : MonoBehaviour
{
  
  public GameObject grass;

  public Transform spawn;
    // Start is called before the first frame update
    void Start()
    {
       for(int i = 0; i<200; i++)
       {
        Instantiate(grass,spawn.transform.position * (i * 5), Quaternion.identity);
       } 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
