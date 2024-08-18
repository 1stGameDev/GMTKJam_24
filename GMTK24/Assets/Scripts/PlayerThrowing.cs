using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrowing : MonoBehaviour
{
    [SerializeField]
    private Transform ThrowLocation;

    [SerializeField]
    private Vector2 ThrowForce;
    [SerializeField]
    private Vector3 ThrowableScale;

    private GameObject CurrentThrowable = null;

    private bool CanThrow = false;

    private CharacterController2D CharCont;
    private Rigidbody2D PlayerRigidbody;

    private float ThrowMultiplier = 1.0f;

    private void Start()
    {
        CharCont = GetComponent<CharacterController2D>();
        PlayerRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (CanThrow && CurrentThrowable)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Throw();
            }
        }
    }

    public void MultiplyThrowMultiplier(float multiplier)
    {
        ThrowMultiplier *= multiplier;
    }

    private void Throw()
    {
        if (!CanThrow)
        {
            return;
        }

        if (CurrentThrowable && ThrowLocation)
        {
            CurrentThrowable.transform.position = ThrowLocation.transform.position;
            CurrentThrowable.transform.localScale = ThrowableScale;

            Mushroom mushroom = CurrentThrowable.GetComponent<Mushroom>();
            if (mushroom)
            {
                mushroom.SetMushroomThrown();
            }

            CurrentThrowable.SetActive(true);

            Rigidbody2D rigidbody = CurrentThrowable.GetComponent<Rigidbody2D>();
            if (rigidbody)
            {
                Vector2 force = ThrowForce;
                if (CharCont)
                {
                    bool facingRight = CharCont.GetFacingRight();
                    if (!facingRight)
                    {
                        force.x *= -1;
                    }
                }
                if (PlayerRigidbody)
                {
                    force += PlayerRigidbody.velocity;
                }

                rigidbody.AddForce(force);
            }
        }

        Inventory inv = GetComponent<Inventory>();
        if (inv)
        {
            inv.ConsumeItem();
        }
    }

    public void SetThrowableObject(GameObject obj)
    {
        if (obj)
        {
            CurrentThrowable = obj;
            CanThrow = true;
        }
    }

    public void RemoveThrowableObject()
    {
        CanThrow = false;
        CurrentThrowable = null;
    }
}
