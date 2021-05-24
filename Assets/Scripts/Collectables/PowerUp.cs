using UnityEngine;

public class PowerUp : MonoBehaviour
{
	[SerializeField] private int Health;
	[SerializeField] private int Damage;
	[SerializeField] private float MoveSpeed;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			other.GetComponent<Player>().Upgrade(Health, Damage, MoveSpeed);
			Destroy(gameObject);
		}
	}
}