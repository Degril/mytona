using System;
using MyProject.Events;
using UnityEngine;
using Random = UnityEngine.Random;

public class MobMover : MonoBehaviour, IMobComponent
{
	[SerializeField] private float sightDistance = 5f;
	[SerializeField] private float moveSpeed;
	[SerializeField] private bool active = true;
	
	private Vector3 targetPosition = Vector3.zero;
	public bool Active
	{
		get => active;
		set => active = value;
	}

	private void Awake()
	{
		PickRandomPosition();
		EventBus.Sub(OnDeath, EventBus.PLAYER_DEATH);
	}

	private void OnDestroy()
	{
		EventBus.Unsub(OnDeath, EventBus.PLAYER_DEATH);
	}

	private void Update()
	{
		if (Active)
		{
			var playerDistance = (transform.position - Player.Instance.transform.position).Flat().magnitude;
			var targetDistance = (transform.position - targetPosition).Flat().magnitude;
			if (sightDistance >= playerDistance)
			{
				targetPosition = Player.Instance.transform.position;
			}
			else if (targetDistance < 0.2f)
			{
				PickRandomPosition();
			}

			var direction = (targetPosition - transform.position).Flat().normalized;

			transform.SetPositionAndRotation(transform.position + direction * Time.deltaTime * moveSpeed,
				Quaternion.LookRotation(direction, Vector3.up));
		}

		GetComponent<MobAnimator>().SetIsRun(Active);
	}


	private void PickRandomPosition()
	{
		targetPosition.x = Random.value * 11 - 6;
		targetPosition.z = Random.value * 11 - 6;
	}

	public void OnDeath()
	{
		enabled = false;
	}
}