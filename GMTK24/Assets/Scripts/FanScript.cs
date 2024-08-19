using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanScript : MonoBehaviour
{
    public float windStrength = 100f;

    //Left, this int = 1, and right, -1.
    public int WindLeftRight = 1;

    private Transform trans;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if(rb != null)
            {
                Vector2 windDirection = transform.up;
                Vector2 windForce = windDirection * windStrength * WindLeftRight;
                rb.AddForce(windForce, ForceMode2D.Force);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 windDirection = transform.up;
            Vector2 windForce = windDirection * windStrength * WindLeftRight;
            rb.AddForce(windForce, ForceMode2D.Force);
        }
    }
}
