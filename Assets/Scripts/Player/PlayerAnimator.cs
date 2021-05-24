using System;
using MyProject.Events;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
	[SerializeField] private Animator animator;

	private void Awake()
	{
		EventBus<PlayerInputMessage>.Sub(TriggerRun);
		EventBus.Sub(AnimateDeath, EventBus.PLAYER_DEATH);
	}

	private void OnDestroy()
	{
		EventBus<PlayerInputMessage>.Unsub(TriggerRun);
		EventBus.Unsub(AnimateDeath, EventBus.PLAYER_DEATH);
	}

	private void TriggerRun(PlayerInputMessage message)
	{
		animator.SetBool("IsRun", message.MovementDirection.sqrMagnitude > 0);
	}

	private void AnimateDeath()
	{
		animator.SetTrigger("Death");
	}

	public void TriggerShoot()
	{
		animator.SetTrigger("Shoot");
	}
}