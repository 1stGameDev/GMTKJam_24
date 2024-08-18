using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escript : MonoBehaviour
{
    // Start is called before the first frame update
    public RectTransform Etextposition;
    void Start()
    {
        Etextposition.position = new Vector3(-1000, -1000, -50);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D (Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Etextposition.position = gameObject.transform.position;
            }
        }

        private void OnTriggerExit2D (Collider2D other)
        {
            Etextposition.position = new Vector3(-1000, -1000, -50);
        }
}
