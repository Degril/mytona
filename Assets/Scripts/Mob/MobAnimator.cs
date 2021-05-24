using UnityEngine;

public class MobAnimator : MonoBehaviour, IMobComponent
{
	[SerializeField] private Animator animator;
	[SerializeField] private string attackTrigger = "MeleeAttack";

	public void StartAttackAnimation()
	{
		animator.SetTrigger(attackTrigger);
	}

	public void SetIsRun(bool isRun)
	{
		animator.SetBool("Run", isRun);
	}

	public void OnDeath()
	{
		animator.SetTrigger("Death");
	}
}