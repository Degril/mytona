using System;
using System.Collections.Generic;
using MyProject.Events;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
	public static Player Instance;
	[SerializeField] private float damage = 1;
	[SerializeField] private float moveSpeed = 3.5f;
	[SerializeField] private float health = 3;
	[SerializeField] private float maxHealth = 3;
	[SerializeField] private List<PlayerWeapon> playerWeapons = new List<PlayerWeapon>();

	public float MaxHealth => maxHealth;
	public float MoveSpeed
	{
		get => moveSpeed;
		private set => moveSpeed = value;
	}
	public float Damage
	{
		get => damage;
		private set => damage = value;
	}

	public PlayerWeapon CurrentVeapon;
	public event Action<float,float> OnHPChange  = null;
	public event Action OnUpgrade = null;

	private void Awake()
	{
		if (Instance != null)
		{
			DestroyImmediate(gameObject);
		}
		else
		{
			Instance = this;
		}
	}

	private void OnDestroy()
	{
		if (Instance == this)
		{
			Instance = null;
		}
	}

	public void AddWeapon(PlayerWeapon playerWeapon)
	{
		playerWeapons.Add(playerWeapon);
		if (playerWeapon.gameObject.activeSelf)
			CurrentVeapon = playerWeapon;
	}

	public void TakeDamage(float amount)
	{
		if (health <= 0)
			return;
		health -= amount;
		if (health <= 0)
		{
			EventBus.Pub(EventBus.PLAYER_DEATH);
		}

		OnHPChange?.Invoke(health, -amount);
	}

	public void Heal(float amount)
	{
		if (health <= 0)
			return;
		health += amount;
		if (health > maxHealth)
		{
			health = maxHealth;
		}

		OnHPChange?.Invoke(health, amount);
	}


	public void Upgrade(float hp, float dmg, float ms)
	{
		Damage += dmg;
		health += hp;
		maxHealth += hp;
		MoveSpeed += ms;
		OnUpgrade?.Invoke();
		OnHPChange?.Invoke(health, 0);
	}

	public void ChangeWeapon<T>() where T : PlayerWeapon
	{
		foreach (var weapon in playerWeapons)
			weapon.Change<T>();
	}
}