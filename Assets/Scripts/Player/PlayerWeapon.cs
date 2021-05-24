using System;
using System.Threading.Tasks;
using MyProject.Events;
using UnityEngine;


public abstract class PlayerWeapon : MonoBehaviour
{
	[SerializeField] protected Projectile bulletPrefab;
	[SerializeField] protected float reload = 1f;
	[SerializeField] protected Transform firePoint;
	[SerializeField] protected ParticleSystem vfx;
	[SerializeField] protected GameObject model;

	private float lastTime;

	protected virtual void Awake()
	{
		var player = GetComponent<Player>();
		player.AddWeapon(this);
		if (model.activeSelf)
		{
			player.CurrentVeapon = this;
			EventBus<PlayerInputMessage>.Sub(Fire);
		}

		lastTime = Time.time - reload;
	}

	protected virtual void OnDestroy()
	{
		EventBus<PlayerInputMessage>.Unsub(Fire);
	}
	
	public void Change<T>() where  T : PlayerWeapon
	{
		EventBus<PlayerInputMessage>.Unsub(Fire);
		var isCurrentWeapon = typeof(T) == GetType();
		if (isCurrentWeapon)
		{
			EventBus<PlayerInputMessage>.Sub(Fire);
			GetComponent<Player>().CurrentVeapon = this;
		}

		model.SetActive(isCurrentWeapon);
	}

	protected async void Fire(PlayerInputMessage message)
	{
		if (Time.time - reload < lastTime)
		{
			return;
		}
		
		if (!message.Fire)
		{
			return;
		}
		
		lastTime = Time.time;
		GetComponent<PlayerAnimator>().TriggerShoot();
		
		await Task.Delay(16);
		
		FireBase();
		vfx.Play();
	}

	protected abstract void FireBase();

}