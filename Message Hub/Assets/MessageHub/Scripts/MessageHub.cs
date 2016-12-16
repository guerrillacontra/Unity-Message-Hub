using System;
using System.Collections.Generic;

namespace IntrovertStudios.Messaging
{
	
	/// <summary>
	/// A message hub that should be used to represent a context (ie ui-interaction or enemies-and-player).
	/// 
	/// You could potentially derive this hub and create a Singleton so you can access your context's hub,
	/// among other alternatives.
	/// </summary>
	public class MessageHub<TKey> : IMessageHub<TKey>
	{
		#region IMessageHub implementation


		public void Connect (TKey id, Action handler)
		{
			List<Action> connections = GetConnectionList(id);
			connections.Add(handler);
		}

		public void Connect<T> (TKey id, Action<T> handler) where T : class
		{
			List<object> connections = GetTypedConnectionList(typeof(T), id);
		
			connections.Add(handler);
		}

		public void Disconnect (TKey id, Action handler)
		{
			List<Action> connections = GetConnectionList(id);
			connections.Remove(handler);
		}

		public void Disconnect <T>(TKey id, Action<T> handler) where T:class
		{
			List<object> connections = GetTypedConnectionList(typeof(T), id);

			connections.Remove(handler);
		}

		public void DisconnectAll ()
		{
			_connections.Clear();
			_typedConnections.Clear();
		}

		public void Post (TKey id)
		{
			List<Action> connections = GetConnectionList(id);

			for(int i = 0; i < connections.Count;++i)
			{
				connections[i].Invoke();
			}
		}

		public void Post<T> (TKey id, T content) where T : class
		{
			List<object> connections = GetTypedConnectionList(typeof(T), id);

			for(int i = 0; i < connections.Count;++i)
			{
				(connections[i] as Action<T>).Invoke(content);
			}
			
			Post(id);
		}

		#endregion


		private List<Action> GetConnectionList(TKey id)
		{
			if(_connections.ContainsKey(id))return _connections[id];

			List<Action> connections = new List<Action>();
			_connections.Add(id, connections);


			return connections;
		}


		private List<object> GetTypedConnectionList(Type t, TKey id)
		{
			if(_typedConnections.ContainsKey(id))
			{
				Dictionary<Type, List<object>> sub = _typedConnections[id];
		
				if(sub.ContainsKey(t))
				{
					return sub[t];
				}

				List<object> connections = new List<object>();
				sub.Add(t, connections);
				return connections;
			}
	
			_typedConnections.Add(id, new Dictionary<Type, List<object>>());

	
			return GetTypedConnectionList(t, id);
		}


	


		//id -> Connection[]
		private readonly Dictionary<TKey, List<Action>> _connections = new  Dictionary<TKey, List<Action>>();

		//id -> type -> Connection[]
		private readonly Dictionary<TKey, Dictionary<Type,List<object>>> _typedConnections = new Dictionary<TKey, Dictionary<Type, List<object>>>();

	

	}


	
}
