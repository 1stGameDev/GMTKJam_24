using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        points ++;
        if(other.CompareTag("Player") && points == 1){
            gameObject.SetActive(false);
            counter.text = (1 + int.Parse(counter.text)).ToString();
            if(counter.text == "3"){
                SceneManager.LoadScene("EndScene");
            }
        }

    }

}
