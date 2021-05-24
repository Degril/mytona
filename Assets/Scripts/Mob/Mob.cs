using System;
using System.Collections;
using MyProject.Events;
using UnityEngine;

public class Mob : MonoBehaviour
{
	[SerializeField] private float destroySecondsDelay = 2;
	
	[SerializeField] private float damage = 1;
	[SerializeField] private float moveSpeed = 3.5f;
	[SerializeField] private float health = 3;
	[SerializeField] private float maxHealth = 3;

	public float MaxHealth => maxHealth;
	public float Damage => damage;

	public Action<float, float> OnHPChange = null;

	public void TakeDamage(float amount)
	{
		if (health <= 0)
			return;
		health -= amount;
		OnHPChange?.Invoke(health, -amount);
		if (health <= 0)
		{
			Death();
		}
	}

	public void Heal(float amount)
	{
		if (health <= 0)
			return;
		health += amount;
		if (health > health)
		{
			health = maxHealth;
		}

		OnHPChange?.Invoke(health, amount);
	}

	public void Death()
	{
		EventBus.Pub(EventBus.MOB_KILLED);
		var components = GetComponents<IMobComponent>();
		foreach (var component in components)
		{
			component.OnDeath();
		}

		GetComponent<Collider>().enabled = false;
		GetComponent<Rigidbody>().isKinematic = true;
		StartCoroutine(Destroy());
	}

	private IEnumerator Destroy()
	{
		yield return new WaitForSeconds(destroySecondsDelay);
		Destroy(gameObject);
	}
}