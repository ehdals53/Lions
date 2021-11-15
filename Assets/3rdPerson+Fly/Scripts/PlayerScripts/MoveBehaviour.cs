using UnityEngine;

// MoveBehaviour inherits from GenericBehaviour. This class corresponds to basic walk and run behaviour, it is the default behaviour.
public class MoveBehaviour : GenericBehaviour
{
	public float walkSpeed = 0.15f;                 // Default walk speed.
	public float runSpeed = 1.0f;                   // Default run speed.
	public float sprintSpeed = 2.0f;                // Default sprint speed.
	public float speedDampTime = 0.1f;              // Default damp time to change the animations based on current speed.
	public string jumpButton = "Jump";              // Default jump button.
	public float jumpHeight = 1.5f;                 // Default jump height.
	public float jumpIntertialForce = 10f;          // Default horizontal inertial force when jumping.
	private float speed, speedSeeker;               // Moving speed.
	private int jumpBool;                           // Animator variable related to jumping.
	private int groundedBool;                       // Animator variable related to whether or not the player is on ground.
	private bool jump;                              // Boolean to determine whether or not the player started a jump.
	private bool isColliding;                       // Boolean to determine if the player has collided with an obstacle.


	public string dashleftButton = "DashLeft";
	private int dash_Left_Bool;
	private bool dash_Left;

	public string dashRightButton = "DashRight";
	private int dash_Right_Bool;
	private bool dash_Right;

	public string dashFrontButton = "DashFront";
	private int dash_Front_Bool;
	private bool dash_Front;

	public string dashbackButton = "DashBack";
	private int dash_Back_Bool;
	private bool dash_Back;


	public string guardButton = "Guard";
	private int guardBool;
	private bool guard;

	private int jumpattackBool;
	private bool jumpattack;

	MP_Player mp_Player;

	[Header("FootStep Sound")]
	public AudioSource walkSound;
	public AudioSource runSound;
	public AudioSource sprintSound;

	// Start is always called after any Awake functions.
	void Start()
	{
		// Set up the references.
		jumpBool = Animator.StringToHash("Jump");
		guardBool = Animator.StringToHash("Guard");
		dash_Front_Bool = Animator.StringToHash("Dash_Front_Bool");
		dash_Back_Bool = Animator.StringToHash("Dash_Back_Bool");
		dash_Left_Bool = Animator.StringToHash("Dash_Left_Bool");
		dash_Right_Bool = Animator.StringToHash("Dash_Right_Bool");

		jumpattackBool = Animator.StringToHash("JumpAttack");
		groundedBool = Animator.StringToHash("Grounded");
		behaviourManager.GetAnim.SetBool(groundedBool, true);

		// Subscribe and register this behaviour as the default behaviour.
		behaviourManager.SubscribeBehaviour(this);
		behaviourManager.RegisterDefaultBehaviour(this.behaviourCode);
		speedSeeker = runSpeed;

		mp_Player = GetComponent<MP_Player>();
	}

	// Update is used to set features regardless the active behaviour.
	void Update()
	{
		// 점프 입력
		if (!jump && Input.GetButtonDown(jumpButton) && behaviourManager.IsCurrentBehaviour(this.behaviourCode) && !behaviourManager.IsOverriding())
		{
			jump = true;
            if (!jumpattack && Input.GetMouseButtonDown(0) && (mp_Player.mp_Cur > mp_Player.SkillMP))
            {
				jumpattack = true;
            }
		}
		// 방어 동작 입력
		if (!jump && !guard && Input.GetButtonDown(guardButton) && (mp_Player.mp_Cur > mp_Player.GuardMP) && behaviourManager.IsCurrentBehaviour(this.behaviourCode) && !behaviourManager.IsOverriding())
		{
			guard = true;
		}

		// 회피 동작 입력
		if (!jump && !dash_Front  && Input.GetButtonDown(dashFrontButton) && (mp_Player.mp_Cur > mp_Player.DodgeMP) && behaviourManager.IsCurrentBehaviour(this.behaviourCode) && !behaviourManager.IsOverriding())
		{
			dash_Front = true;
		}
		if (!jump && !dash_Back && Input.GetButtonDown(dashbackButton) && (mp_Player.mp_Cur > mp_Player.DodgeMP) && behaviourManager.IsCurrentBehaviour(this.behaviourCode) && !behaviourManager.IsOverriding())
		{
			dash_Back = true;
		}
		if (!jump && !dash_Left && Input.GetButtonDown(dashleftButton) && (mp_Player.mp_Cur > mp_Player.DodgeMP) && behaviourManager.IsCurrentBehaviour(this.behaviourCode) && !behaviourManager.IsOverriding())
		{
			dash_Left = true;
		}
		if (!jump && !dash_Right && Input.GetButtonDown(dashRightButton) && (mp_Player.mp_Cur > mp_Player.DodgeMP) && behaviourManager.IsCurrentBehaviour(this.behaviourCode) && !behaviourManager.IsOverriding())
		{
			dash_Right = true;
		}

		// 공격 및 스킬 공격 입력
		if (!jump && Input.GetMouseButtonDown(0) && behaviourManager.IsCurrentBehaviour(this.behaviourCode) && !behaviourManager.IsOverriding())
        {
			AttackManagement();
        }
		if (!jump && Input.GetMouseButtonDown(1) && (mp_Player.mp_Cur > mp_Player.SkillMP) && behaviourManager.IsCurrentBehaviour(this.behaviourCode) && !behaviourManager.IsOverriding())
		{
			SkillManagement();
		}

	}
	// LocalFixedUpdate overrides the virtual function of the base class.
	public override void LocalFixedUpdate()
	{
		// Call the basic movement manager.
		MovementManagement(behaviourManager.GetH, behaviourManager.GetV);

		// Call the jump manager.
		JumpManagement();
		GuardManagement();
		Dash_Front();
		Dash_Back();
		Dash_Left();
		Dash_Right();
	}
	void FootStep_Sprint_Sound()
    {
        if (behaviourManager.IsGrounded())
        {
			if(speed <= 2.0f && speed > 1.0f)
            {
				sprintSound.Play();
            }
        }
    }
	void FootStep_Run_Sound()
    {
        if (behaviourManager.IsGrounded())
        {
			if (speed <= 1.0f && speed > 0.15f)
            {
				runSound.Play();
            }
        }
    }
	void FootStep_Walk_Sound()
    {
        if (behaviourManager.IsGrounded())
        {
			if (speed <= 0.15f && speed > 0.0f)
            {
				walkSound.Play();
            }
        }
    }
	void AttackManagement() // 기본 공격 함수

	{
		if (!behaviourManager.GetAnim.GetBool(jumpBool) && !behaviourManager.GetAnim.GetBool(guardBool)
			&& !behaviourManager.GetAnim.GetBool(dash_Front_Bool) && !behaviourManager.GetAnim.GetBool(dash_Back_Bool)
			&& !behaviourManager.GetAnim.GetBool(dash_Left_Bool) && !behaviourManager.GetAnim.GetBool(dash_Right_Bool)
			&& !behaviourManager.GetAnim.GetBool(jumpattackBool) && behaviourManager.IsGrounded())
		{
			behaviourManager.LockTempBehaviour(this.behaviourCode);
			behaviourManager.GetAnim.SetTrigger("Attack");

			// Temporarily change player friction to pass through obstacles.
			GetComponent<CapsuleCollider>().material.dynamicFriction = 0f;
			GetComponent<CapsuleCollider>().material.staticFriction = 0f;
			// Remove vertical velocity to avoid "super jumps" on slope ends.
			RemoveVerticalVelocity();
		}
	}
	void SkillManagement()  // 스킬 공격 함수

	{
		if (!behaviourManager.GetAnim.GetBool(jumpBool) && !behaviourManager.GetAnim.GetBool(guardBool)
			&& !behaviourManager.GetAnim.GetBool(dash_Front_Bool) && !behaviourManager.GetAnim.GetBool(dash_Back_Bool)
			&& !behaviourManager.GetAnim.GetBool(dash_Left_Bool) && !behaviourManager.GetAnim.GetBool(dash_Right_Bool)
			&& !behaviourManager.GetAnim.GetBool(jumpattackBool) && behaviourManager.IsGrounded())
		{
			behaviourManager.LockTempBehaviour(this.behaviourCode);
			behaviourManager.GetAnim.SetTrigger("Skill");

			// Temporarily change player friction to pass through obstacles.
			GetComponent<CapsuleCollider>().material.dynamicFriction = 0f;
			GetComponent<CapsuleCollider>().material.staticFriction = 0f;
			// Remove vertical velocity to avoid "super jumps" on slope ends.
			RemoveVerticalVelocity();
		}
	}
	void GuardManagement()	// 방어 함수
    {
		if(guard && !behaviourManager.GetAnim.GetBool(jumpBool) && !behaviourManager.GetAnim.GetBool(guardBool)
			&& !behaviourManager.GetAnim.GetBool(dash_Front_Bool) && !behaviourManager.GetAnim.GetBool(dash_Back_Bool)
			&& !behaviourManager.GetAnim.GetBool(dash_Left_Bool) && !behaviourManager.GetAnim.GetBool(dash_Right_Bool)
			&& !behaviourManager.GetAnim.GetBool(jumpattackBool) && behaviourManager.IsGrounded() && (mp_Player.mp_Cur > mp_Player.GuardMP))
        {
			behaviourManager.LockTempBehaviour(this.behaviourCode);
			behaviourManager.GetAnim.SetBool(guardBool, true);

			// Temporarily change player friction to pass through obstacles.
			GetComponent<CapsuleCollider>().material.dynamicFriction = 0f;
			GetComponent<CapsuleCollider>().material.staticFriction = 0f;
			// Remove vertical velocity to avoid "super jumps" on slope ends.
			RemoveVerticalVelocity();
		}
		else if (behaviourManager.GetAnim.GetBool(guardBool))
		{
			// Has landed?
			if ((behaviourManager.GetRigidBody.velocity.y < 0) && behaviourManager.IsGrounded())
			{
				behaviourManager.GetAnim.SetBool(groundedBool, true);
				// Change back player friction to default.
				GetComponent<CapsuleCollider>().material.dynamicFriction = 0.6f;
				GetComponent<CapsuleCollider>().material.staticFriction = 0.6f;
				// Set jump related parameters.
				guard = false;
				behaviourManager.GetAnim.SetBool(guardBool, false);
				behaviourManager.UnlockTempBehaviour(this.behaviourCode);

			}
		}

	}
	void Dash_Front()	// 앞으로 회피
    {
		if (dash_Front && !behaviourManager.GetAnim.GetBool(jumpBool) && !behaviourManager.GetAnim.GetBool(guardBool)
			&& !behaviourManager.GetAnim.GetBool(dash_Front_Bool) && !behaviourManager.GetAnim.GetBool(dash_Back_Bool)
			&& !behaviourManager.GetAnim.GetBool(dash_Left_Bool) && !behaviourManager.GetAnim.GetBool(dash_Right_Bool)
			&& !behaviourManager.GetAnim.GetBool(jumpattackBool) && behaviourManager.IsGrounded() && (mp_Player.mp_Cur > mp_Player.DodgeMP))
		{
			behaviourManager.LockTempBehaviour(this.behaviourCode);
			behaviourManager.GetAnim.SetBool(dash_Front_Bool, true);

			// Temporarily change player friction to pass through obstacles.
			GetComponent<CapsuleCollider>().material.dynamicFriction = 0f;
			GetComponent<CapsuleCollider>().material.staticFriction = 0f;
			// Remove vertical velocity to avoid "super jumps" on slope ends.
			RemoveVerticalVelocity();
		}
		else if (behaviourManager.GetAnim.GetBool(dash_Front_Bool))
		{
			// Has landed?
			if ((behaviourManager.GetRigidBody.velocity.y < 0) && behaviourManager.IsGrounded())
			{
				behaviourManager.GetAnim.SetBool(groundedBool, true);
				// Change back player friction to default.
				GetComponent<CapsuleCollider>().material.dynamicFriction = 0.6f;
				GetComponent<CapsuleCollider>().material.staticFriction = 0.6f;
				// Set jump related parameters.
				dash_Front = false;
				behaviourManager.GetAnim.SetBool(dash_Front_Bool, false);
				behaviourManager.UnlockTempBehaviour(this.behaviourCode);
			}
		}
	}
	void Dash_Back()	// 뒤로 회피
    {
		if (dash_Back && !behaviourManager.GetAnim.GetBool(jumpBool) && !behaviourManager.GetAnim.GetBool(guardBool) 
			&& !behaviourManager.GetAnim.GetBool(dash_Front_Bool) && !behaviourManager.GetAnim.GetBool(dash_Back_Bool)
			&& !behaviourManager.GetAnim.GetBool(dash_Left_Bool) && !behaviourManager.GetAnim.GetBool(dash_Right_Bool)
			&& !behaviourManager.GetAnim.GetBool(jumpattackBool) && behaviourManager.IsGrounded() && (mp_Player.mp_Cur > mp_Player.DodgeMP))
		{
			behaviourManager.LockTempBehaviour(this.behaviourCode);
			behaviourManager.GetAnim.SetBool(dash_Back_Bool, true);

			// Temporarily change player friction to pass through obstacles.
			GetComponent<CapsuleCollider>().material.dynamicFriction = 0f;
			GetComponent<CapsuleCollider>().material.staticFriction = 0f;
			// Remove vertical velocity to avoid "super jumps" on slope ends.
			RemoveVerticalVelocity();
		}
		else if (behaviourManager.GetAnim.GetBool(dash_Back_Bool))
		{
			// Has landed?
			if ((behaviourManager.GetRigidBody.velocity.y < 0) && behaviourManager.IsGrounded())
			{
				behaviourManager.GetAnim.SetBool(groundedBool, true);
				// Change back player friction to default.
				GetComponent<CapsuleCollider>().material.dynamicFriction = 0.6f;
				GetComponent<CapsuleCollider>().material.staticFriction = 0.6f;
				// Set jump related parameters.
				dash_Back = false;
				behaviourManager.GetAnim.SetBool(dash_Back_Bool, false);
				behaviourManager.UnlockTempBehaviour(this.behaviourCode);
			}
		}
	}
	void Dash_Left()	// 왼쪽으로 회피
    {
		if (dash_Left && !behaviourManager.GetAnim.GetBool(jumpBool) && !behaviourManager.GetAnim.GetBool(guardBool)
			&& !behaviourManager.GetAnim.GetBool(dash_Front_Bool) && !behaviourManager.GetAnim.GetBool(dash_Back_Bool)
			&& !behaviourManager.GetAnim.GetBool(dash_Left_Bool) && !behaviourManager.GetAnim.GetBool(dash_Right_Bool)
			&& !behaviourManager.GetAnim.GetBool(jumpattackBool) && behaviourManager.IsGrounded() && (mp_Player.mp_Cur > mp_Player.DodgeMP))
		{
			behaviourManager.LockTempBehaviour(this.behaviourCode);
			behaviourManager.GetAnim.SetBool(dash_Left_Bool, true);

			// Temporarily change player friction to pass through obstacles.
			GetComponent<CapsuleCollider>().material.dynamicFriction = 0f;
			GetComponent<CapsuleCollider>().material.staticFriction = 0f;
			// Remove vertical velocity to avoid "super jumps" on slope ends.
			RemoveVerticalVelocity();
		}
		else if (behaviourManager.GetAnim.GetBool(dash_Left_Bool))
		{
			// Has landed?
			if ((behaviourManager.GetRigidBody.velocity.y < 0) && behaviourManager.IsGrounded())
			{
				behaviourManager.GetAnim.SetBool(groundedBool, true);
				// Change back player friction to default.
				GetComponent<CapsuleCollider>().material.dynamicFriction = 0.6f;
				GetComponent<CapsuleCollider>().material.staticFriction = 0.6f;
				// Set jump related parameters.
				dash_Left = false;
				behaviourManager.GetAnim.SetBool(dash_Left_Bool, false);
				behaviourManager.UnlockTempBehaviour(this.behaviourCode);
			}
		}
	}
	void Dash_Right()	// 오른쪽으로 회피
    {
		if (dash_Right && !behaviourManager.GetAnim.GetBool(jumpBool) && !behaviourManager.GetAnim.GetBool(guardBool)
			&& !behaviourManager.GetAnim.GetBool(dash_Front_Bool) && !behaviourManager.GetAnim.GetBool(dash_Back_Bool)
			&& !behaviourManager.GetAnim.GetBool(dash_Left_Bool) && !behaviourManager.GetAnim.GetBool(dash_Right_Bool)
			&& !behaviourManager.GetAnim.GetBool(jumpattackBool) && behaviourManager.IsGrounded() && (mp_Player.mp_Cur > mp_Player.DodgeMP))
		{
			behaviourManager.LockTempBehaviour(this.behaviourCode);
			behaviourManager.GetAnim.SetBool(dash_Right_Bool, true);

			// Temporarily change player friction to pass through obstacles.
			GetComponent<CapsuleCollider>().material.dynamicFriction = 0f;
			GetComponent<CapsuleCollider>().material.staticFriction = 0f;
			// Remove vertical velocity to avoid "super jumps" on slope ends.
			RemoveVerticalVelocity();
		}
		else if (behaviourManager.GetAnim.GetBool(dash_Right_Bool))
		{
			// Has landed?
			if ((behaviourManager.GetRigidBody.velocity.y < 0) && behaviourManager.IsGrounded())
			{
				behaviourManager.GetAnim.SetBool(groundedBool, true);
				// Change back player friction to default.
				GetComponent<CapsuleCollider>().material.dynamicFriction = 0.6f;
				GetComponent<CapsuleCollider>().material.staticFriction = 0.6f;
				// Set jump related parameters.
				dash_Right = false;
				behaviourManager.GetAnim.SetBool(dash_Right_Bool, false);
				behaviourManager.UnlockTempBehaviour(this.behaviourCode);
			}
		}
	}
	
	// Execute the idle and walk/run jump movements.
	void JumpManagement()
	{
		// Start a new jump.
		if (jump && !behaviourManager.GetAnim.GetBool(jumpBool) && !behaviourManager.GetAnim.GetBool(guardBool)
			&& !behaviourManager.GetAnim.GetBool(dash_Front_Bool) && !behaviourManager.GetAnim.GetBool(dash_Back_Bool)
			&& !behaviourManager.GetAnim.GetBool(dash_Left_Bool) && !behaviourManager.GetAnim.GetBool(dash_Right_Bool)
			&& !behaviourManager.GetAnim.GetBool(jumpattackBool) && behaviourManager.IsGrounded())
		{
			// Set jump related parameters.
			behaviourManager.LockTempBehaviour(this.behaviourCode);
			behaviourManager.GetAnim.SetBool(jumpBool, true);


			// Temporarily change player friction to pass through obstacles.
			GetComponent<CapsuleCollider>().material.dynamicFriction = 0f;
			GetComponent<CapsuleCollider>().material.staticFriction = 0f;
			// Remove vertical velocity to avoid "super jumps" on slope ends.
			RemoveVerticalVelocity();
			// Set jump vertical impulse velocity.
			float velocity = 2f * Mathf.Abs(Physics.gravity.y) * jumpHeight;
			velocity = Mathf.Sqrt(velocity);
			behaviourManager.GetRigidBody.AddForce(Vector3.up * velocity, ForceMode.VelocityChange);

			/*
			// Is a locomotion jump?
			if (behaviourManager.GetAnim.GetFloat(speedFloat) > 0.1)
			{
				// Temporarily change player friction to pass through obstacles.
				GetComponent<CapsuleCollider>().material.dynamicFriction = 0f;
				GetComponent<CapsuleCollider>().material.staticFriction = 0f;
				// Remove vertical velocity to avoid "super jumps" on slope ends.
				RemoveVerticalVelocity();
				// Set jump vertical impulse velocity.
				float velocity = 2f * Mathf.Abs(Physics.gravity.y) * jumpHeight;
				velocity = Mathf.Sqrt(velocity);
				behaviourManager.GetRigidBody.AddForce(Vector3.up * velocity, ForceMode.VelocityChange);
			}
			*/
			
		}
		// Is already jumping?
		
		else if (behaviourManager.GetAnim.GetBool(jumpBool))
		{
			// Keep forward movement while in the air.
			if (!behaviourManager.IsGrounded() && !isColliding && behaviourManager.GetTempLockStatus())
			{
				behaviourManager.GetRigidBody.AddForce(transform.forward * jumpIntertialForce * Physics.gravity.magnitude * sprintSpeed, ForceMode.Acceleration);

                if (Input.GetMouseButtonDown(0) && (mp_Player.mp_Cur > mp_Player.SkillMP))
                {
					behaviourManager.GetAnim.SetBool(jumpattackBool, true);
                }
			}
			// Has landed?
			if ((behaviourManager.GetRigidBody.velocity.y < 0) && behaviourManager.IsGrounded())
			{
				behaviourManager.GetAnim.SetBool(groundedBool, true);
				// Change back player friction to default.
				GetComponent<CapsuleCollider>().material.dynamicFriction = 0.6f;
				GetComponent<CapsuleCollider>().material.staticFriction = 0.6f;
				// Set jump related parameters.
				jump = false;
				jumpattack = false;
				behaviourManager.GetAnim.SetBool(jumpBool, false);
				behaviourManager.GetAnim.SetBool(jumpattackBool, false);
				behaviourManager.UnlockTempBehaviour(this.behaviourCode);
			}
		}
		
	}
	
    // Deal with the basic player movement
    void MovementManagement(float horizontal, float vertical)
	{
		// On ground, obey gravity.
		if (behaviourManager.IsGrounded())
			behaviourManager.GetRigidBody.useGravity = true;

		// Avoid takeoff when reached a slope end.
		else if (!behaviourManager.GetAnim.GetBool(jumpBool) && behaviourManager.GetRigidBody.velocity.y > 0)
		{
			RemoveVerticalVelocity();
		}

		// Call function that deals with player orientation.
		Rotating(horizontal, vertical);

		// Set proper speed.
		Vector2 dir = new Vector2(horizontal, vertical);
		speed = Vector2.ClampMagnitude(dir, 1f).magnitude;
		// This is for PC only, gamepads control speed via analog stick.
		speedSeeker += Input.GetAxis("Mouse ScrollWheel");
		speedSeeker = Mathf.Clamp(speedSeeker, walkSpeed, runSpeed);
		speed *= speedSeeker;
		if (behaviourManager.IsSprinting() && (mp_Player.mp_Cur > 1.0f))
		{
			mp_Player.Sprint_MP();
			speed = sprintSpeed;
		}

		behaviourManager.GetAnim.SetFloat(speedFloat, speed, speedDampTime, Time.deltaTime);
	}

	// Remove vertical rigidbody velocity.
	private void RemoveVerticalVelocity()
	{
		Vector3 horizontalVelocity = behaviourManager.GetRigidBody.velocity;
		horizontalVelocity.y = 0;
		behaviourManager.GetRigidBody.velocity = horizontalVelocity;
	}

	// Rotate the player to match correct orientation, according to camera and key pressed.
	Vector3 Rotating(float horizontal, float vertical)
	{
		// Get camera forward direction, without vertical component.
		Vector3 forward = behaviourManager.playerCamera.TransformDirection(Vector3.forward);

		// Player is moving on ground, Y component of camera facing is not relevant.
		forward.y = 0.0f;
		forward = forward.normalized;

		// Calculate target direction based on camera forward and direction key.
		Vector3 right = new Vector3(forward.z, 0, -forward.x);
		Vector3 targetDirection;
		targetDirection = forward * vertical + right * horizontal;

		// Lerp current direction to calculated target direction.
		if ((behaviourManager.IsMoving() && targetDirection != Vector3.zero))
		{
			Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

			Quaternion newRotation = Quaternion.Slerp(behaviourManager.GetRigidBody.rotation, targetRotation, behaviourManager.turnSmoothing);
			behaviourManager.GetRigidBody.MoveRotation(newRotation);
			behaviourManager.SetLastDirection(targetDirection);
		}
		// If idle, Ignore current camera facing and consider last moving direction.
		if (!(Mathf.Abs(horizontal) > 0.9 || Mathf.Abs(vertical) > 0.9))
		{
			behaviourManager.Repositioning();
		}

		return targetDirection;
	}

	// Collision detection.
	private void OnCollisionStay(Collision collision)
	{
		isColliding = true;
		// Slide on vertical obstacles
		if (behaviourManager.IsCurrentBehaviour(this.GetBehaviourCode()) && collision.GetContact(0).normal.y <= 0.1f)
		{
			GetComponent<CapsuleCollider>().material.dynamicFriction = 0f;
			GetComponent<CapsuleCollider>().material.staticFriction = 0f;
		}
	}
	private void OnCollisionExit(Collision collision)
	{
		isColliding = false;
		GetComponent<CapsuleCollider>().material.dynamicFriction = 0.6f;
		GetComponent<CapsuleCollider>().material.staticFriction = 0.6f;
	}
}
