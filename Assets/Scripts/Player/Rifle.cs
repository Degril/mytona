public class Rifle : PlayerWeapon
{
	protected virtual float GetDamage()
	{
		return GetComponent<Player>().Damage;
	}

	
	protected override void FireBase()
	{
		var bullet = Instantiate(bulletPrefab, firePoint.position, transform.rotation);
		bullet.Damage = GetDamage();
	}
}