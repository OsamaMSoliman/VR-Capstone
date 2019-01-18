using System.Collections;
using UnityEngine;
using UnityEngine.AI;

//TODO: go investegate the light

public class Monster : MonoBehaviour
{
	public static Monster Self { get; private set; }

	private NavMeshAgent navMeshAgent;
	private Animator animator;

	private void Awake()
	{
		Self = this;
		animator = GetComponent<Animator>();
		navMeshAgent = GetComponent<NavMeshAgent>();
	}


	public void GoAfterPlayer()
	{
		navMeshAgent.SetDestination(Player.Self.Transfrom.position);
		navMeshAgent.isStopped = false;
		animator.SetBool("Run" , true);
	}

	public void StopChasing()
	{
		navMeshAgent.isStopped = true;
		animator.SetBool("Run" , false);
	}

	public void AttackPlayer()
	{
		if ( !attacking )
		{
			attacking = true;
			animator.SetInteger("AttackType" , Random.value > 0.5f ? 1 : 0);
			Player.Self.GameOver();
		}
	}

	public void StartPosition( Vector3 vector3 )
	{
		navMeshAgent.enabled = false;
		transform.position = vector3;
		navMeshAgent.enabled = true;
		StartCoroutine(LookForPlayer());
		StartCoroutine(UpdatePlayerPos());

	}

	private bool attacking = false;
	private IEnumerator LookForPlayer()
	{
		while ( true )
		{
			if ( navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance )
				if ( Vector3.Angle(Player.Self.Transfrom.position - transform.position , transform.forward) <= 30 )
					AttackPlayer();
			yield return Wait.ForSeconds(0.1f);
		}
	}

	private IEnumerator UpdatePlayerPos()
	{
		yield return Wait.ForSeconds(2f);
		navMeshAgent.SetDestination(Vector3.zero);
		navMeshAgent.isStopped = true;
		while ( true )
		{
			if ( !navMeshAgent.isStopped )
				navMeshAgent.SetDestination(Player.Self.Transfrom.position);
			yield return Wait.ForSeconds(1f);
		}
	}
}
