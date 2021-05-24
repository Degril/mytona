using TMPro;
using UnityEngine;

public class PlayerHealthBar : MonoBehaviour
{
	[SerializeField] private GameObject bar;
	[SerializeField] private SpriteRenderer barImg;
	[SerializeField] private TMP_Text text;
	[SerializeField] private TMP_Text damageText;
	
	private float maxHP;
	private Player player;

	private void Awake()
	{
		player = GetComponent<Player>();
		player.OnHPChange += OnHPChange;
	}

	public void OnDeath()
	{
		bar.SetActive(false);
	}

	private void LateUpdate()
	{
		bar.transform.rotation = Camera.main.transform.rotation;
	}

	private void OnHPChange(float health, float diff)
	{
		var frac = health / player.MaxHealth;
		text.text = $"{health:####}/{player.MaxHealth:####}";
		barImg.size = new Vector2(frac, barImg.size.y);
		var pos = barImg.transform.localPosition;
		pos.x = -(1 - frac) / 2;
		barImg.transform.localPosition = pos;
		if (health <= 0)
		{
			bar.SetActive(false);
		}
	}

	private void OnUpgrade()
	{
		damageText.text = $"{player.Damage}";
	}
}