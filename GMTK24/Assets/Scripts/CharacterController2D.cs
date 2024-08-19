using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{

	public float m_JumpForce = 400f;							// Amount of force added when the player jumps.
	//[SerializeField] private float m_MovementSpeedMaximum = 2.5f;				// Rudi - Maximum speed so we don't go as fast
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;			// Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;							// Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private LayerMask m_NoControlMask;
	[SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_CeilingCheck;							// A position marking where to check for ceilings
	[SerializeField] private Collider2D m_CrouchDisableCollider;				// A collider that will be disabled when crouching

	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.
	const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;

	private float coyoteTime = 0.2f;
	private float coyoteTimer;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent OnCrouchEvent;
	private bool m_wasCrouching = false;

	private Animator m_CharAnimator;
	

	public bool GetFacingRight()
    {
		return m_FacingRight;
    }

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		m_CharAnimator = GetComponent<Animator>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();
	}

	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;
		m_AirControl = true;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		Collider2D[] colliders2 = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_NoControlMask);
		for (int i = 0; i < colliders2.Length; i++){
			if (!colliders2[i].isTrigger)
            {
                if (colliders2[i].gameObject!= gameObject)
                {
                    m_AirControl = false;
				}
			}
		}
		for (int i = 0; i < colliders.Length; i++)
		{
			if (!colliders[i].isTrigger)
			{
				if (colliders[i].gameObject != gameObject)
				{
					m_Grounded = true;
					if (!wasGrounded)
                    {
						OnLandEvent.Invoke();
						coyoteTimer = coyoteTime;  // Reset coyote time when grounded


						CameraScript camScript = Camera.main.GetComponent<CameraScript>();
						if (camScript)
                        {
							camScript.OnLanded();
                        }
					}
				}
			}
		}

		// If the player is not grounded, start reducing the coyote timer
		if (!m_Grounded)
		{
			coyoteTimer -= Time.fixedDeltaTime;
		}

		if (m_CharAnimator)
		{
			m_CharAnimator.SetBool("IsGrounded", m_Grounded);
		}
	}


	public void Move(float move, bool crouch, bool jump)
	{
		// If crouching, check to see if the character can stand up
		if (!crouch)
		{
			// If the character has a ceiling preventing them from standing up, keep them crouching
			if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
			{
				crouch = true;
			}
		}

		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{
			// If crouching
			if (crouch)
			{
				if (!m_wasCrouching)
				{
					m_wasCrouching = true;
					OnCrouchEvent.Invoke(true);
				}

				// Reduce the speed by the crouchSpeed multiplier
				move *= m_CrouchSpeed;

				// Disable one of the colliders when crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = false;
			} else
			{
				// Enable the collider when not crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = true;

				if (m_wasCrouching)
				{
					m_wasCrouching = false;
					OnCrouchEvent.Invoke(false);
				}
			}

			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			if (m_CharAnimator)
            {
				m_CharAnimator.SetFloat("WalkSpeed", Mathf.Abs(m_Rigidbody2D.velocity.x));
			}

			// Rudi - replaced this with AddForce so we get a bit of momentum
			//Vector3 newVelocity = new Vector2(move * 10.0f, 0.0f);
			//m_Rigidbody2D.AddForce(newVelocity);

			// Rudi - clamp speed so we don't gain too much momentum
			//float clampedSpeed = Mathf.Clamp(m_Rigidbody2D.velocity.x, -m_MovementSpeedMaximum, m_MovementSpeedMaximum);
			//m_Rigidbody2D.velocity = new Vector2(clampedSpeed, m_Rigidbody2D.velocity.y);

			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
		}
		// If the player should jump...
		if ((m_Grounded || coyoteTimer > 0f) && jump)
		{
			// Add a vertical force to the player
			m_Grounded = false;
			coyoteTimer = 0f;  // Reset coyote timer when jumping
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
			
			if (m_CharAnimator)
            {
				m_CharAnimator.Play("Jumping", m_CharAnimator.GetLayerIndex("Walking"));
            }
		}
	}


	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}