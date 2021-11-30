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
	public string dashRightButton = "DashRight";
	public string dashFrontButton = "DashFront";
	public string dashbackButton = "DashBack";

	public string guardButton = "Guard";
	private int guardBool;
	private bool guard;

	private int jumpattackBool;
	private bool jumpattack;

	MP_Player mp_Player;

	bool comboPossible;
	public int comboStep;
	bool inputSmash;

	[Header("FootStep Sound")]
	public AudioSource walkSound;
	public AudioSource runSound;
	public AudioSource sprintSound;
	public AudioSource JumpLandSound;

	// Start is always called after any Awake functions.
	void Start()
	{
		// Set up the references.
		jumpBool = Animator.StringToHash("Jump");
		guardBool = Animator.StringToHash("Guard");

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
		if (!jump && !guard && Input.GetButtonDown(guardButton) && behaviourManager.IsCurrentBehaviour(this.behaviourCode) && !behaviourManager.IsOverriding())
		{
			guard = true;
		}
		// 공격 및 스킬 공격 입력
		if (!jump && Input.GetMouseButtonDown(0) && behaviourManager.IsCurrentBehaviour(this.behaviourCode) && !behaviourManager.IsOverriding())
        {
			NormalAttack();
        }
		if (!jump && Input.GetMouseButtonDown(1) && behaviourManager.IsCurrentBehaviour(this.behaviourCode) && !behaviourManager.IsOverriding())
		{
			SmashAttack();
		}
		if (!jump && Input.GetMouseButtonDown(2) && (mp_Player.mp_Cur > mp_Player.Skill_H_MP) && behaviourManager.IsCurrentBehaviour(this.behaviourCode) && !behaviourManager.IsOverriding())
        {
			HyperAttack();
        }
		if(!jump && Input.GetButtonDown(dashFrontButton) && (mp_Player.mp_Cur > mp_Player.DodgeMP) && behaviourManager.IsCurrentBehaviour(this.behaviourCode) && !behaviourManager.IsOverriding())
        {
			DodgeFront();
        }
		if (!jump && Input.GetButtonDown(dashbackButton) && (mp_Player.mp_Cur > mp_Player.DodgeMP) && behaviourManager.IsCurrentBehaviour(this.behaviourCode) && !behaviourManager.IsOverriding())
		{
			DodgeBack();
		}
		if (!jump && Input.GetButtonDown(dashleftButton) && (mp_Player.mp_Cur > mp_Player.DodgeMP) && behaviourManager.IsCurrentBehaviour(this.behaviourCode) && !behaviourManager.IsOverriding())
		{
			DodgeLeft();
		}
		if (!jump && Input.GetButtonDown(dashRightButton) && (mp_Player.mp_Cur > mp_Player.DodgeMP) && behaviourManager.IsCurrentBehaviour(this.behaviourCode) && !behaviourManager.IsOverriding())
		{
			DodgeRight();
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

	}
	public void ComboPossible()
    {
		comboPossible = true;
    }
	public void NextAtk()
    {
        if (!inputSmash)
        {
			if (comboStep == 2)
				behaviourManager.GetAnim.Play("4Combo");
			if (comboStep == 3)
				behaviourManager.GetAnim.Play("5Combo");
        }
        if (inputSmash)
        {
			
			if(comboStep == 1)
				behaviourManager.GetAnim.Play("Skill_A");
			if (comboStep == 2)
				behaviourManager.GetAnim.Play("Skill_B");
			if (comboStep == 3)
				behaviourManager.GetAnim.Play("Skill_C");
        }
    }
    public void ResetCombo()
    {
        comboPossible = false;
        inputSmash = false;
        comboStep = 0;
    }
	void NormalAttack()
    {
		if(comboStep == 0)
        {
			behaviourManager.GetAnim.Play("3Combo");
			comboStep = 1;
			return;
        }
		if(comboStep != 0)
        {
            if (comboPossible)
            {
				comboPossible = false;
				comboStep += 1;
            }
        }
    }
	void SmashAttack()
    {
        if (comboPossible)
        {
			comboPossible = false;
			inputSmash = true;
        }
    }
	void HyperAttack()
    {
		if((mp_Player.mp_Cur > mp_Player.Skill_H_MP))
        {
			behaviourManager.GetAnim.Play("HyperSkill");
        }
    }
	void DodgeFront()
    {
		if(mp_Player.mp_Cur > mp_Player.DodgeMP)
        {
			behaviourManager.GetAnim.Play("Dash_Front");
        }
    }
	void DodgeBack()
	{
		if (mp_Player.mp_Cur > mp_Player.DodgeMP)
		{
			behaviourManager.GetAnim.Play("Dash_Back");
		}
	}
	void DodgeLeft()
	{
		if (mp_Player.mp_Cur > mp_Player.DodgeMP)
		{
			behaviourManager.GetAnim.Play("Dash_Left");
		}
	}
	void DodgeRight()
	{
		if (mp_Player.mp_Cur > mp_Player.DodgeMP)
		{
			behaviourManager.GetAnim.Play("Dash_Right");
		}
	}
	void FootStep_Sprint_Sound()
    {
        if (behaviourManager.IsGrounded())
        {
			if(speed <= 2.0f && speed > 1.3f)
            {
				sprintSound.Play();
            }
        }
    }
	void FootStep_Run_Sound()
    {
        if (behaviourManager.IsGrounded())
        {
			if (speed <= 1.3f && speed > 0.1f)
            {
				runSound.Play();
            }
        }
    }
	void FootStep_Walk_Sound()
    {
        if (behaviourManager.IsGrounded())
        {
			if (speed <= 0.1f && speed > 0.0f)
            {
				walkSound.Play();
            }
        }
    }
	void Jump_Land_Sound()
    {
        if (behaviourManager.IsGrounded())
        {
			JumpLandSound.Play();
        }
    }
	void GuardManagement()	// 방어 함수
    {
		if(guard && !behaviourManager.GetAnim.GetBool(jumpBool) && !behaviourManager.GetAnim.GetBool(guardBool) && behaviourManager.IsGrounded())
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
	// Execute the idle and walk/run jump movements.
	void JumpManagement()
	{
		// Start a new jump.
		if (jump && !behaviourManager.GetAnim.GetBool(jumpBool) && behaviourManager.IsGrounded())
		{
			// Set jump related parameters.
			behaviourManager.LockTempBehaviour(this.behaviourCode);
			behaviourManager.GetAnim.SetBool(jumpBool, true);
			behaviourManager.GetAnim.Play("jump_start");
			
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
	
    // 기본적인 플레이어 이동 처리
    void MovementManagement(float horizontal, float vertical)
	{
		// 지상에서는 중력을 따름
		if (behaviourManager.IsGrounded())
			behaviourManager.GetRigidBody.useGravity = true;

		else if (!behaviourManager.GetAnim.GetBool(jumpBool) && behaviourManager.GetRigidBody.velocity.y > 0)
		{
			RemoveVerticalVelocity();
		}

		// 플레이어의 방향을 다루는 함수 호출
		Rotating(horizontal, vertical);

		// 적정 속도를 설정
		Vector2 dir = new Vector2(horizontal, vertical);
		speed = Vector2.ClampMagnitude(dir, 1f).magnitude;
		
		// 마우스 휠로 이동속도 조절
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
	// 수직 Rigidbody 속도를 제거
	private void RemoveVerticalVelocity()
	{
		Vector3 horizontalVelocity = behaviourManager.GetRigidBody.velocity;
		horizontalVelocity.y = 0;
		behaviourManager.GetRigidBody.velocity = horizontalVelocity;
	}

	// 카메라와 입력 키에 따라 플레이어를 올바른 방향으로 회전시킴
	Vector3 Rotating(float horizontal, float vertical)
	{
		// 수직 구성 요소 없이 카메라 전진 방향을 잡음
		Vector3 forward = behaviourManager.playerCamera.TransformDirection(Vector3.forward);

		// 플레이어가 지면 위에서 이동중이고 카메라의 Y 축은 0으로 설정
		forward.y = 0.0f;
		forward = forward.normalized;

		// 카메라 전진 및 방향키를 기준으로 목표 방향을 계산
		Vector3 right = new Vector3(forward.z, 0, -forward.x);
		Vector3 targetDirection;
		targetDirection = forward * vertical + right * horizontal;

		// 계산된 대상 방향에 대한 Lerp 전류 방향
		if ((behaviourManager.IsMoving() && targetDirection != Vector3.zero))
		{
			Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
			Quaternion newRotation = Quaternion.Slerp(behaviourManager.GetRigidBody.rotation, targetRotation, behaviourManager.turnSmoothing);
			behaviourManager.GetRigidBody.MoveRotation(newRotation);
			behaviourManager.SetLastDirection(targetDirection);
			
		}
		
		// Idle 상태일 경우, 현재 카메라 방향을 무시하고 마지막 이동 방향을 고려함
		if (!(Mathf.Abs(horizontal) > 0.9 || Mathf.Abs(vertical) > 0.9))
		{
			behaviourManager.Repositioning();
		}

		return targetDirection;
	}

	// 충돌 감지
	private void OnCollisionStay(Collision collision)
	{
		isColliding = true;
		// 수직 장애물에서 미끄러짐
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
