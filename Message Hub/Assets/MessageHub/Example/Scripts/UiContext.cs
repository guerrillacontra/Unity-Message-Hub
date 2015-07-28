using UnityEngine;

namespace IntrovertStudios.Messaging.Example
{
	public class UiContext : MonoBehaviour
	{
		public static IMessageHub<MessageId> Hub{get;private set;}

		//An enum to represent 
		public enum MessageId
		{
			JumpButtonPressed 
		}

		void Awake()
		{
			Hub = new MessageHub<MessageId>();
		}

		void Destroy()
		{
			Hub.DisconnectAll();
			Hub = null;
		}

	}
	
}