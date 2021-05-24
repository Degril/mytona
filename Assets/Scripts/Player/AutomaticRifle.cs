public class AutomaticRifle : PlayerWeapon
{

	protected virtual float GetDamage()
	{
		return GetComponent<Player>().Damage / 5f;
	}

	protected override void FireBase()
	{
		var bullet = Instantiate(bulletPrefab, firePoint.position, transform.rotation);
		bullet.Damage = GetDamage();
	}
}