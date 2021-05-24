using UnityEngine;

namespace MyProject.Events
{
	public abstract class Handler<T> : MonoBehaviour where T : Message
	{
		public abstract void HandleMessage(T message);

		protected virtual void Awake()
		{
			Sub();
		}

		protected virtual void OnDestroy()
		{
			Unsub();
		}

		protected void Sub()
		{
			EventBus<T>.Sub(HandleMessage);
		}
		
		protected void Unsub()
		{
			EventBus<T>.Unsub(HandleMessage);
		}
	}
}