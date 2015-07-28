using UnityEngine;

namespace IntrovertStudios.Messaging.Example
{
	public class PlayerBehaviour : MonoBehaviour
	{
		[Range(1, 10)]
		public float JumpPower = 1;
		
		void Awake()
		{
			_body = GetComponent<Rigidbody2D>();
		}
		
		private Rigidbody2D _body;
		
		void OnEnable()
		{
			UiContext.Hub.Connect<string>(UiContext.MessageId.JumpButtonPressed, OnButtonClicked);
		}

		
		void OnDisable()
		{
			if(UiContext.Hub != null)
			UiContext.Hub.Disconnect<string>(UiContext.MessageId.JumpButtonPressed, OnButtonClicked);
		}
		
		private void OnButtonClicked(string content)
		{
			Debug.Log(content);
			
			_body.AddForce(new Vector2(0, JumpPower), ForceMode2D.Impulse);
		}
	}
}