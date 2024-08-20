using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveShroomScript : MonoBehaviour
{

public int points;
public Text counter;
    public 
    // Start is called before the first frame update
    void Start()
    {
        counter = GameObject.Find("Counter").GetComponent<Text>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
           collect();
           counter.text = (1 + int.Parse(counter.text)).ToString();
        }

    }
    private void collect()
    {
    points++;
    gameObject.SetActive(false);
    }

}
