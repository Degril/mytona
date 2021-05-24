using MyProject.Events;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	[SerializeField] protected float damage;
	[SerializeField] protected float speed = 8;
	[SerializeField] protected bool damagePlayer = false;
	[SerializeField] protected bool damageMob;
	[SerializeField] protected float timeToLive = 5f;
	[SerializeField] protected bool destroyAfterTrigger = true;
	protected float timer = 0f;
	protected bool destroyed = false;

	public float Damage
	{
		get => damage;
		set => damage = value;
	}

	protected virtual void OnTriggerEnter(Collider other)
	{
		if (destroyed)
		{
			return;
		}

		var triggerPlayer = damagePlayer && other.CompareTag("Player");
		var triggerMob = damageMob && other.CompareTag("Mob");
		
		if (!triggerMob && !triggerPlayer) return;
		
		if (triggerPlayer)
		{
			other.GetComponent<Player>().TakeDamage(damage);
			if (destroyAfterTrigger)
				destroyed = true;
		}

		if (triggerMob)
		{
			var mob = other.GetComponent<Mob>();
			mob.TakeDamage(damage);
			if (destroyAfterTrigger)
				destroyed = true;
		}
		EventBus<AttackMessage>.Pub(new AttackMessage(
			isMobAttacking: triggerPlayer,
			isPrepareAttack: false,
			damageCount: damage,
			attackingUnitPosition: transform.position,
			attackedUnitPosition: other.transform.position
		));
	}

	protected virtual void Update()
	{
		if (!destroyed)
		{
			transform.position += transform.forward * speed * Time.deltaTime;
		}

		timer += Time.deltaTime;
		if (timer > timeToLive)
		{
			Destroy(gameObject);
		}
	}
}