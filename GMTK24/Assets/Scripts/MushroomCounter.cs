using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MushroomCounter : MonoBehaviour
{

    [SerializeField] private Text text;

    private int mushroomsCount = 0;
    // Start is called before the first frame update
    public void AddMushroom(){
        mushroomsCount++;
        text.text = mushroomsCount.ToString();
    }
}
