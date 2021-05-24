using System;
using System.Collections;
using MyProject.Events;
using UnityEngine;

[RequireComponent(typeof(MobMover))]
[RequireComponent(typeof(Mob))]
public class RangeAttack : MonoBehaviour, IMobComponent
{
	[SerializeField] private float AttackDistance = 5f;
	[SerializeField] private float AttackDelay = .5f;
	[SerializeField] private float AttackCooldown = 2f;
	[SerializeField] private Projectile Bullet;

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
		if (playerDistance <= AttackDistance)
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
			attackedUnitPosition: Player.Instance.transform.position));
		
		yield return new WaitForSeconds(AttackDelay);
		var playerDistance = (transform.position - Player.Instance.transform.position).Flat().magnitude;
		if (playerDistance <= AttackDistance)
		{
			var bullet = Instantiate(Bullet, transform.position,
				Quaternion.LookRotation((Player.Instance.transform.position - transform.position).Flat().normalized,
					Vector3.up));
			bullet.Damage = mob.Damage;
		}

		mover.Active = true;
		yield return new WaitForSeconds(AttackCooldown);
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