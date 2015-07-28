using UnityEngine;

namespace IntrovertStudios.Messaging
{
	/// <summary>
	/// Allows the MessageHub to PostOnUpdate and PostOnFixedUpdate.
	/// 
	/// There can only be one instance of this in the game as you only want
	/// the MessageHub to be updated in one behaviour...
	/// </summary>
	public class MessageBehaviour : MonoBehaviour
	{

		private static MessageBehaviour _instance;

		void OnEnable()
		{
			if(_instance == null)
			{
				_instance = this;
				MessageHub._isPrefabInstalled = true;
				return;
			}

			if(_instance == this)return;

			//Only have one instance of this.
			DestroyImmediate(gameObject);
		}

		void Destroy()
		{
			if(_instance == this)
			{
				_instance = null;
			}
		}
	
		void Update()
		{
			MessageHub.OnUpdate();
		}

		void FixedUpdate()
		{
			MessageHub.OnFixedUpdate();
		}
	}
}