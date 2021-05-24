namespace MyProject.Events
{
	public class FieldCreateMessage : Message
	{
		// Bonus!
		// Incapsulate public fields where is needed with properties/methods 
		private bool[,] _field;

		public bool GetField(int a, int b)
		{
			return _field[a, b];
		}

		public FieldCreateMessage(bool[,] field)
		{
			_field = field;
		}
	}
}