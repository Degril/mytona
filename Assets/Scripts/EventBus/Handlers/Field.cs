using System;
using System.Collections;
using System.Collections.Generic;

namespace MyProject.Events
{
	public class Field : Handler<FieldCreateMessage>
	{
		public int Size = 12;

		public override void HandleMessage(FieldCreateMessage message)
		{
			var childCount = transform.childCount;
			for (int i = 0; i < childCount; i++)
			{
				transform.GetChild(i).gameObject.SetActive(message.GetField(i / Size, i % Size));
			}
		}
	}
}