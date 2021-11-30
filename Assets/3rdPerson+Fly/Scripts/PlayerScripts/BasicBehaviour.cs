using UnityEngine;
using System.Collections.Generic;

// 이 클래스는 활성 또는 오버라이드 중인 플레이어의 동작을 관리하고 로컬 함수를 호출
// 모든 플레이어 행동에 사용되는 기본 설정 및 공통 기능을 포함
public class BasicBehaviour : MonoBehaviour
{
	public Transform playerCamera;                        // 플레이어의 초점을 맞추는 카메라를 참조
	public float turnSmoothing = 0.06f;                   // 카메라 정면을 맞추기 위해 이동할 때의 회전 속도
	public float sprintFOV = 80f;                         // 플레이어가 전력질주할 때 카메라에서 사용하는 FOV
	public string sprintButton = "Sprint";                // 전력질주 입력 버튼 이름

	private float h;                                      // 가로축
	private float v;                                      // 세로축
	private int currentBehaviour;                         // 현재 플레이어 동작에 대한 참조
	private int defaultBehaviour;                         // 다른 플레이어가 활성화되지 않은 경우 플레이어의 기본 동작
	private int behaviourLocked;                          // 오버라이드를 금지하는 임시 잠금 동작에 대한 참조
	private Vector3 lastDirection;                        // 플레이어가 마지막으로 이동하던 방향
	private Animator anim;                                // Animator 구성 요소를 참조
	private MP_Player mp_Player;						  // MP_Player 에 대한 참조 
	private ThirdPersonOrbitCamBasic camScript;           // 3인칭 카메라 스크립트에 대한 참조
	private bool sprint;                                  // 플레이어가 전력질주 모드를 활성화했는지 여부를 결정하는 bool
	private bool changedFOV;                              // 전력질주 동작이 카메라 FOV를 변경한 경우 저장할 bool
	private int hFloat;                                   // 가로축과 관련된 Animator 변수
	private int vFloat;                                   // 수직 축과 관련된 Animator 변수
	private List<GenericBehaviour> behaviours;            // 활성화된 모든 플레이어의 동작을 포함하는 목록
	private List<GenericBehaviour> overridingBehaviours;  // 현재 오버라이드 동작 목록
	private Rigidbody rBody;                              // 플레이어의 Rigidbody에 대한 참조
	private int groundedBool;                             // 플레이어가 땅에 있는지 여부에 따라 달라지는 Animator 변수
	private Vector3 colExtents;                           // 테스트를 위해 범위를 충돌시킴

	// 현재 가로 및 세로 축을 가져옴
	public float GetH { get { return h; } }
	public float GetV { get { return v; } }
	// 플레이어 카메라 스크립트를 가져옴
	public ThirdPersonOrbitCamBasic GetCamScript { get { return camScript; } }
	// 플레이어의 Rigidbody를 가져옴
	public Rigidbody GetRigidBody { get { return rBody; } }
	// 플레이어의 애니메이터 컨트롤러를 가져옴
	public Animator GetAnim { get { return anim; } }
	// 현재 기본 동작을 가져옴
	public int GetDefaultBehaviour {  get { return defaultBehaviour; } }

	void Awake ()
	{
		// 참조를 설정
		behaviours = new List<GenericBehaviour> ();
		overridingBehaviours = new List<GenericBehaviour>();
		anim = GetComponent<Animator> ();
		hFloat = Animator.StringToHash("H");
		vFloat = Animator.StringToHash("V");
		camScript = playerCamera.GetComponent<ThirdPersonOrbitCamBasic> ();
		rBody = GetComponent<Rigidbody> ();
		mp_Player = GetComponent<MP_Player>();

		// 지면인지 확인하는 변수
		groundedBool = Animator.StringToHash("Grounded");
		colExtents = GetComponent<Collider>().bounds.extents;
	}

	void Update()
	{
		// 입력 축을 저장
		h = Input.GetAxis("Horizontal");
		v = Input.GetAxis("Vertical");

		// Animator 컨트롤러에 입력 축을 설정
		anim.SetFloat(hFloat, h, 0.1f, Time.deltaTime);
		anim.SetFloat(vFloat, v, 0.1f, Time.deltaTime);

		// 전력질주를 입력으로 전환
		sprint = Input.GetButton (sprintButton);

		// 카메라 FOV를 전력질주 모드로 적절하게 설정
		if(IsSprinting() && (mp_Player.mp_Cur > mp_Player.SprintMP))
		{
			changedFOV = true;
			camScript.SetFOV(sprintFOV);
		}
		else if(changedFOV)
		{
			camScript.ResetFOV();
			changedFOV = false;
		}
		// Animator 컨트롤러에서 지면 테스트를 설정
		anim.SetBool(groundedBool, IsGrounded());
	}

	void FixedUpdate()
	{
		// 재정의하지 않으면 활성 동작을 호출
		bool isAnyBehaviourActive = false;
		if (behaviourLocked > 0 || overridingBehaviours.Count == 0)
		{
			foreach (GenericBehaviour behaviour in behaviours)
			{
				if (behaviour.isActiveAndEnabled && currentBehaviour == behaviour.GetBehaviourCode())
				{
					isAnyBehaviourActive = true;
					behaviour.LocalFixedUpdate();
				}
			}
		}
		// 우선시 되는 행동이 있으면 호출
		else
		{
			foreach (GenericBehaviour behaviour in overridingBehaviours)
			{
				behaviour.LocalFixedUpdate();
			}
		}

		// 활성화되거나 오버라이딩되는 동작이 없는 경우 플레이어가 지면에 서있는지 확인
		if (!isAnyBehaviourActive && overridingBehaviours.Count == 0)
		{
			rBody.useGravity = true;
			Repositioning ();
		}
	}

	// 활성 또는 우선 동작의 LateUpdate 기능을 호출
	private void LateUpdate()
	{
		// 재정의하지 않으면 활성 동작을 호출
		if (behaviourLocked > 0 || overridingBehaviours.Count == 0)
		{
			foreach (GenericBehaviour behaviour in behaviours)
			{
				if (behaviour.isActiveAndEnabled && currentBehaviour == behaviour.GetBehaviourCode())
				{
					behaviour.LocalLateUpdate();
				}
			}
		}
		// 우선시 되는 행동이 있으면 호출
		else
		{
			foreach (GenericBehaviour behaviour in overridingBehaviours)
			{
				behaviour.LocalLateUpdate();
			}
		}

	}
	// 행동 감시 목록에 새로운 동작을 추가
	public void SubscribeBehaviour(GenericBehaviour behaviour)
	{
		behaviours.Add (behaviour);
	}

	// 기본 플레이어 동작을 설정
	public void RegisterDefaultBehaviour(int behaviourCode)
	{
		defaultBehaviour = behaviourCode;
		currentBehaviour = behaviourCode;
	}

	// 사용자 지정 동작을 활성 동작으로 설정
	// 항상 기본 동작에서 전달된 동작으로 변경
	public void RegisterBehaviour(int behaviourCode)
	{
		if (currentBehaviour == defaultBehaviour)
		{
			currentBehaviour = behaviourCode;
		}
	}
	// 플레이어 동작을 비활성화 하고 기본 동작으로 돌아감
	public void UnregisterBehaviour(int behaviourCode)
	{
		if (currentBehaviour == behaviourCode)
		{
			currentBehaviour = defaultBehaviour;
		}
	}

	// 활성 동작을 대기열에 있는 동작으로 재정의
	// 활성 동작과 겹쳐야 하는 하나 이상의 동작으로 변경할 때 사용
	public bool OverrideWithBehaviour(GenericBehaviour behaviour)
	{
		// Behaviour is not on queue.
		if (!overridingBehaviours.Contains(behaviour))
		{
			// No behaviour is currently being overridden.
			if (overridingBehaviours.Count == 0)
			{
				// Call OnOverride function of the active behaviour before overrides it.
				foreach (GenericBehaviour overriddenBehaviour in behaviours)
				{
					if (overriddenBehaviour.isActiveAndEnabled && currentBehaviour == overriddenBehaviour.GetBehaviourCode())
					{
						overriddenBehaviour.OnOverride();
						break;
					}
				}
			}
			// Add overriding behaviour to the queue.
			overridingBehaviours.Add(behaviour);
			return true;
		}
		return false;
	}

	// Attempt to revoke the overriding behaviour and return to the active one.
	// Called when exiting the overriding behaviour (ex.: stopped aiming).
	public bool RevokeOverridingBehaviour(GenericBehaviour behaviour)
	{
		if (overridingBehaviours.Contains(behaviour))
		{
			overridingBehaviours.Remove(behaviour);
			return true;
		}
		return false;
	}

	// Check if any or a specific behaviour is currently overriding the active one.
	public bool IsOverriding(GenericBehaviour behaviour = null)
	{
		if (behaviour == null)
			return overridingBehaviours.Count > 0;
		return overridingBehaviours.Contains(behaviour);
	}

	// Check if the active behaviour is the passed one.
	public bool IsCurrentBehaviour(int behaviourCode)
	{
		return this.currentBehaviour == behaviourCode;
	}

	// Check if any other behaviour is temporary locked.
	public bool GetTempLockStatus(int behaviourCodeIgnoreSelf = 0)
	{
		return (behaviourLocked != 0 && behaviourLocked != behaviourCodeIgnoreSelf);
	}

	// Atempt to lock on a specific behaviour.
	//  No other behaviour can overrhide during the temporary lock.
	// Use for temporary transitions like jumping, entering/exiting aiming mode, etc.
	public void LockTempBehaviour(int behaviourCode)
	{
		if (behaviourLocked == 0)
		{
			behaviourLocked = behaviourCode;
		}
	}

	// Attempt to unlock the current locked behaviour.
	// Use after a temporary transition ends.
	public void UnlockTempBehaviour(int behaviourCode)
	{
		if(behaviourLocked == behaviourCode)
		{
			behaviourLocked = 0;
		}
	}

	// Common functions to any behaviour:

	// Check if player is sprinting.
	public virtual bool IsSprinting()
	{
		return sprint && IsMoving() && CanSprint() && IsSpintMP();
	}

	// Check if player can sprint (all behaviours must allow).
	public bool CanSprint()
	{
		foreach (GenericBehaviour behaviour in behaviours)
		{
			if (!behaviour.AllowSprint ())
				return false;
		}
		foreach(GenericBehaviour behaviour in overridingBehaviours)
		{
			if (!behaviour.AllowSprint())
				return false;
		}
		return true;
	}

	// Check if the player is moving on the horizontal plane.
	public bool IsHorizontalMoving()
	{
		return h != 0;
	}
	// 질주 마나 체크
	public bool IsSpintMP()
    {
		if (mp_Player.mp_Cur > 1.0f)
        {
			return true;
		}
		else
			return false;
    }
	// Check if the player is moving.
	public bool IsMoving()
	{
		return (h != 0)|| (v != 0);
	}

	// Get the last player direction of facing.
	public Vector3 GetLastDirection()
	{
		return lastDirection;
	}

	// Set the last player direction of facing.
	public void SetLastDirection(Vector3 direction)
	{
		lastDirection = direction;
	}

	// Put the player on a standing up position based on last direction faced.
	public void Repositioning()
	{
		if(lastDirection != Vector3.zero)
		{
			lastDirection.y = 0;
			Quaternion targetRotation = Quaternion.LookRotation (lastDirection);
			Quaternion newRotation = Quaternion.Slerp(rBody.rotation, targetRotation, turnSmoothing);
			rBody.MoveRotation (newRotation);
		}
	}
	// Function to tell whether or not the player is on ground.
	public bool IsGrounded()
	{
		Ray ray = new Ray(this.transform.position + Vector3.up * 2 * colExtents.x, Vector3.down);
		return Physics.SphereCast(ray, colExtents.x, colExtents.x + 0.2f);
	}
}

// This is the base class for all player behaviours, any custom behaviour must inherit from this.
// Contains references to local components that may differ according to the behaviour itself.
public abstract class GenericBehaviour : MonoBehaviour
{
	//protected Animator anim;                       // Reference to the Animator component.
	protected int speedFloat;                      // Speed parameter on the Animator.
	protected BasicBehaviour behaviourManager;     // Reference to the basic behaviour manager.
	protected int behaviourCode;                   // The code that identifies a behaviour.
	protected bool canSprint;                      // Boolean to store if the behaviour allows the player to sprint.

	void Awake()
	{
		// Set up the references.
		behaviourManager = GetComponent<BasicBehaviour> ();
		speedFloat = Animator.StringToHash("Speed");
		canSprint = true;

		// Set the behaviour code based on the inheriting class.
		behaviourCode = this.GetType().GetHashCode();
	}

	// Protected, virtual functions can be overridden by inheriting classes.
	// The active behaviour will control the player actions with these functions:
	
	// The local equivalent for MonoBehaviour's FixedUpdate function.
	public virtual void LocalFixedUpdate() { }
	// The local equivalent for MonoBehaviour's LateUpdate function.
	public virtual void LocalLateUpdate() { }
	// This function is called when another behaviour overrides the current one.
	public virtual void OnOverride() { }

	// Get the behaviour code.
	public int GetBehaviourCode()
	{
		return behaviourCode;
	}

	// Check if the behaviour allows sprinting.
	public bool AllowSprint()
	{
		return canSprint;
	}
}
