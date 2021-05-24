using System;
using MyProject.Events;
using UnityEngine;
using Random = UnityEngine.Random;

public class MobSpawner : Handler<SpawnMobMessage>
{
	[SerializeField] private Mob[] prefabs;

	protected override void Awake()
	{
		base.Awake();
		EventBus.Sub(Unsub, EventBus.PLAYER_DEATH);
	}

	public override void HandleMessage(SpawnMobMessage message)
	{
		var position = new Vector3(Random.value * 11 - 6, 1, Random.value * 11 - 6);
		Instantiate(prefabs[message.Type], position, Quaternion.identity);
	}
}