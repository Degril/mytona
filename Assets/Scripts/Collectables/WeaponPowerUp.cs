using UnityEngine;

public abstract class WeaponPowerUp : MonoBehaviour
{
	public abstract bool IsCurrentWeapon(PlayerWeapon playerWeapon);
}

public abstract class WeaponPowerUp<T> : WeaponPowerUp where T : PlayerWeapon
{
	public override bool IsCurrentWeapon(PlayerWeapon playerWeapon)
	{
		return typeof(T) == playerWeapon.GetType();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!other.TryGetComponent<Player>(out var player)) return;
		
		player.ChangeWeapon<T>();
		Destroy(gameObject);
	}
}