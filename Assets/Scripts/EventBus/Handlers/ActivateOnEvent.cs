using UnityEngine;

namespace MyProject.Events
{
	public class ActivateOnEvent : MonoBehaviour
	{
		[SerializeField] private int eventId = 0;
		[SerializeField] private GameObject button;

		private void Awake()
		{
			EventBus.Sub(HandleMessage, eventId);
		}

		private void OnDestroy()
		{
			EventBus.Unsub(HandleMessage, eventId);
		}


		public void HandleMessage()
		{
			button.SetActive(true);
		}
	}
}