using UnityEngine;

public class HealthPack : MonoBehaviour
{
	[SerializeField] private int Health;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			other.GetComponent<Player>().Heal(Health);
			Destroy(gameObject);
		}
	}
}