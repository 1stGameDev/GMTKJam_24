using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveShroomScript : MonoBehaviour
{

public int points;
    public 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
           collect();
        }       
    }
    }
private void collect()
{
    points++;
    gameObject.SetActive(false);
}

}
