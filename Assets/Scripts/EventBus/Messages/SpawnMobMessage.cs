namespace MyProject.Events
{
	public class SpawnMobMessage : Message
	{
		public const int MELEE = 0;
		public const int RANGE = 1;

		public int Type { get; private set; }

		public SpawnMobMessage(int type)
		{
			Type = type;
		}
	}
}