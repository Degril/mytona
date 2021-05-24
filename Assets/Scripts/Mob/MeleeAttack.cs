using System;
using System.Collections;
using MyProject.Events;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(MobMover))]
[RequireComponent(typeof(Mob))]
public class MeleeAttack : MonoBehaviour, IMobComponent
{
	[SerializeField] private float attackDistance = 1f;
	[SerializeField] private float damageDistance = 1f;
	[SerializeField] private float attackDelay = 1f;

	private MobMover mover;
	private Mob mob;
	private MobAnimator mobAnimator;
	private bool attacking = false;
	private Coroutine _attackCoroutine = null;

	private void Awake()
	{
		mob = GetComponent<Mob>();
		mover = GetComponent<MobMover>();
		mobAnimator = GetComponent<MobAnimator>();
		EventBus.Sub(OnDeath, EventBus.PLAYER_DEATH);
	}

	private void OnDestroy()
	{
		EventBus.Unsub(OnDeath, EventBus.PLAYER_DEATH);
	}

	private void Update()
	{
		if (attacking)
		{
			return;
		}

		var playerDistance = (transform.position - Player.Instance.transform.position).Flat().magnitude;
		if (playerDistance <= attackDistance)
		{
			attacking = true;
			_attackCoroutine = StartCoroutine(Attack());
		}
	}

	private IEnumerator Attack()
	{
		mobAnimator.StartAttackAnimation();
		mover.Active = false;
		EventBus<AttackMessage>.Pub(new AttackMessage(
			isMobAttacking: true,
			isPrepareAttack: true,
			damageCount: mob.Damage,
			attackingUnitPosition: transform.position,
			attackedUnitPosition: Player.Instance.transform.position
			));
		
		yield return new WaitForSeconds(attackDelay);
		var playerDistance = (transform.position - Player.Instance.transform.position).Flat().magnitude;
		if (playerDistance <= damageDistance)
		{
			EventBus<AttackMessage>.Pub(new AttackMessage(
				isMobAttacking: true,
				isPrepareAttack: false,
				damageCount: mob.Damage,
				attackingUnitPosition: transform.position,
				attackedUnitPosition: Player.Instance.transform.position
				));
			Player.Instance.TakeDamage(mob.Damage);
		}

		mover.Active = true;
		attacking = false;
		_attackCoroutine = null;
	}

	public void OnDeath()
	{
		enabled = false;
		if (_attackCoroutine != null)
		{
			StopCoroutine(_attackCoroutine);
		}
	}
}