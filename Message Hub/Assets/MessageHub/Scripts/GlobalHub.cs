
namespace IntrovertStudios.Messaging
{
	/// <summary>
	/// A static representation of an IntrovertStudios.Messaging.IMessageHub that can be accessed globally.
	/// 
	/// Tip:
	/// A good way to design your hubs us to think of your game as multiple MessageHub's that send messages
	/// within a specific context (like UI to player interaction).
	/// 
	/// They can all access the global-hub for application level messages such as state changes etc
	/// 
	/// </summary>
	public static class GlobalHub 
	{

		static GlobalHub()
		{
			_hub = new MessageHub<object>();
		}

		public static void Connect (object id, System.Action handler)
		{
			_hub.Connect(id, handler);
		}

		public static void Connect<T> (object id, System.Action<T> handler) where T : class
		{
			_hub.Connect<T>(id, handler);
		}

		public static void Disconnect (object id, System.Action handler)
		{
			_hub.Disconnect(id, handler);
		}

		public static void Disconnect<T> (object id, System.Action<T> handler) where T : class
		{
			_hub.Disconnect<T>(id, handler);
		}

		public static void DisconnectAll ()
		{
			_hub.DisconnectAll();
		}

		public static void Post (object id)
		{
			_hub.Post(id);
		}

		public static void Post<T> (object id, T content) where T : class
		{
			_hub.Post<T>(id, content);
		}

	
		private static readonly MessageHub<object> _hub;


	}
}
