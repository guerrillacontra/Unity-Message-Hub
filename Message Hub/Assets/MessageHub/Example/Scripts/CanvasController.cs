using UnityEngine;

namespace IntrovertStudios.Messaging.Example
{
	public class CanvasController : MonoBehaviour
	{
		//When the button clicks it runs this!
		public void OnJumpButtonClicked()
		{
			//Which the ui-hub will 
			UiContext.Hub.Post<string>(UiContext.MessageId.JumpButtonPressed, "Hello world");
		}
	
	}
}