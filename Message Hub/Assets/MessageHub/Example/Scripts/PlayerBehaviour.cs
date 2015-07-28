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
			MessageHub.AddListener((int)MessageID.ButtonClicked, OnButtonClicked);
		}


		void OnDisable()
		{
			MessageHub.RemoveListener((int)MessageID.ButtonClicked, OnButtonClicked);
		}

		private void OnButtonClicked(object data = null)
		{
			Debug.Log(data);

			_body.AddForce(new Vector2(0, JumpPower), ForceMode2D.Impulse);
		}
	}
}