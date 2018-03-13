using UnityEngine;
using UnityEngine.UI;

namespace Mission
{
	public class NotificationButton : MonoBehaviour
	{
		private static NotificationButton _activeButton;

		private Image _image;
		public Mission.Notification Notification { get; set; }

		// Use this for initialization
		private void Start()
		{
			_image = gameObject.GetComponent<Image>();

			transform.Find("Title").GetComponent<Text>().text =
				string.Format(Notification.Sender);
			transform.Find("Description").GetComponent<Text>().text =
				string.Format(Notification.Message);
			//transform.Find("Description").GetComponent<Text>().text = Notification.ToString();
		}

		public void Deactivate()
		{
			//_image.color = new Color(32.0f / 255.0f, 32.0f / 255.0f, 32.0f / 255.0f);
			GameObject.Destroy(gameObject);
			
		}

		public void Activate()
		{
			//_image.color = new Color(238.0f / 255.0f, 80.0f / 255.0f, 33.0f / 255.0f);
			//Notification.Display();

			//	should delete clicked instance of notification
			//Notification.Destroy(_activeButton); //just another wat to implement the same thing
			GameObject.Destroy(gameObject);
		}

		public void DisplayNotification()
		{
			DisplayEventManager.OnClearDisplay();
			if (_activeButton == null)
			{
				_activeButton = this;
				Activate();
				return;
			}

			if (_activeButton != this)
			{
				_activeButton.Deactivate();
				_activeButton = this;
				Activate();
				return;
			}

			if (_activeButton == this)
			{
				Deactivate();
				_activeButton = null;
				return;
			}

			Debug.Log(_activeButton.gameObject.transform.GetSiblingIndex());
		}
	}
}