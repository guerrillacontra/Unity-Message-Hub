using UnityEngine;
using System;
using System.Collections.Generic;

namespace IntrovertStudios.Messaging
{
	/// <summary>
	/// A ceneralized hub where you can add/remove listeners that
	/// are listening to messages (id based).
	/// 
	/// When a message is invoked, all listeners that are observing the id
	/// will execute a callback function that looks like:
	/// 
	/// void OnSomethingHappened(object optionalData)
	/// 
	/// You can also Invoke messages on the next Update/FixedUpdate as long as the MessageHub prefab has
	/// been installed.
	/// 
	/// Usage example:
	/// 
	/// //Enums are a clean way to represent message id's
	/// enum UiMessage
	/// {
	/// 	ButtonClicked
	/// }
	/// 
	/// //Listen for when a button has been clicked by the UI
	/// MessageHub.AddListener((int)UiMessage.ButtonClicked, OnButtonClicked);
	/// 
	/// //The callback handler that simply prints what has been sent
	/// private void OnButtonClicked(object data)
	/// {
	/// 	Debug.Log(data);
	/// }
	/// 
	/// //Somewhere in your project a button has been clicked...
	/// MessageHub.Invoke((int)UiMessage.ButtonClicked, "Hello World!");
	/// 
	/// BOOM all listeners will execute their handlers.
	/// 
	/// 
	/// Tip : MonoBehaviour's that use this should Add/Remove there listeners on OnEnable and OnDisable
	/// to not break hot-reload features in Unity.
	/// </summary>
	public sealed class MessageHub
	{
	
		/// <summary>
		/// Add a listener that will execute a callback when the message id has been invoked.
		/// </summary>
		public static void AddListener (int id, Action<object> callback)
		{
			if (!_listeners.ContainsKey (id)) {
				_listeners.Add (id, new List<Action<object>> ());
			}

			List<Action<object>> callbacks = _listeners [id];
			callbacks.Add (callback);
		}

		/// <summary>
		/// Removes the listener.
		/// </summary>
		public static void RemoveListener (int id, Action<object> callback)
		{
			List<Action<object>> callbacks = _listeners [id];
			callbacks.Remove (callback);

			if (callbacks.Count == 0) {
				_listeners.Remove (id);
			}
		}


		/// <summary>
		/// Post a message with some optional data to send to the listeners.
		/// </summary>
		public static void Post (int id, object data = null)
		{
			if(!_listeners.ContainsKey(id))
			{
				return;
			}

			List<Action<object>> callbacks = _listeners [id];

			for(int i = 0; i < callbacks.Count;i++)
			{
				callbacks[i].Invoke(data);
			}
		}
		/// <summary>
		/// Post a message with optional data on the next update tick.
		/// </summary>
		public static void PostOnUpdate(int id, object data = null)
		{
			if(!_isPrefabInstalled)
			{
#if UNITY_EDITOR
				Debug.LogWarning("Cannot invoke PostOnUpdate without the MessageHub prefab being installed.");
#endif
				return;
			}

			_updates.Add(new UpdateHook(){Id = id, Data = data});
		}

		/// <summary>
		/// Post a message with optional data on the next fixed-update tick.
		/// </summary>
		public static void PostOnFixedUpdate(int id, object data = null)
		{
			if(!_isPrefabInstalled)
			{
#if UNITY_EDITOR
				Debug.LogWarning("Cannot invoke PostOnFixedUpdate without the MessageHub prefab being installed.");
#endif
				return;
			}

			_fixed.Add(new UpdateHook(){Id = id, Data = data});
		}



		
		internal static void OnUpdate()
		{
			for(int i = 0; i < _updates.Count;i++)
			{
				Post(_updates[i].Id, _updates[i].Data);
			}

			_updates.Clear();
		}
		
		internal static void OnFixedUpdate()
		{
			for(int i = 0; i < _fixed.Count;i++)
			{
				Post(_fixed[i].Id, _fixed[i].Data);
			}
			
			_fixed.Clear();
		}


		struct UpdateHook
		{
			public int Id;
			public object Data;
		}


		static internal bool _isPrefabInstalled;

		
		//key -> Listener callback
		private static Dictionary<int, List<Action<object>>> _listeners = new Dictionary<int, List<Action<object>>> ();


		private static List<UpdateHook> _updates = new List<UpdateHook>();
		private static List<UpdateHook> _fixed = new List<UpdateHook>();
	}


	
}