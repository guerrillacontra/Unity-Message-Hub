using UnityEngine;

namespace IntrovertStudios.Messaging.Example
{
	public class UiController : MonoBehaviour
	{
		//Canvas will point to this when clicked.
		public void OnButtonClickedHandler()
		{
			MessageHub.Post((int)MessageID.ButtonClicked, "Hello World!");
		}
	}
}