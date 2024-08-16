using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Wobble : MonoBehaviour
{

    public float speed = 2f;
    public float offsetMax = 0.15f;
    private Transform trans;
    // Start is called before the first frame update
    void Start()
    {
        trans = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        float offset = Mathf.Cos(Time.time*speed) * offsetMax;

        trans.position = new Vector3(transform.position.x, transform.position.y + (offset*Time.deltaTime), transform.position.z);
    }
}
