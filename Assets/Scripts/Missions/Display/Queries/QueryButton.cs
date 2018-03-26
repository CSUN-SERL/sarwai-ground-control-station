using FeedScreen.Experiment.Missions.Broadcasts.Events;
using UnityEngine;
using UnityEngine.UI;

namespace Mission.Display.Queries
{
    public class QueryButton : MonoBehaviour
    {
        private static QueryButton _activeButton;

	    private Button _yesButton;
	    private Button _noButton;

		private Image _image;
	    private RectTransform _rectTransform;

		// Keep this named this way
		public Query query { get; set; }

        // Use this for initialization	
        private void Start()
        {
            _image = gameObject.GetComponent<Image>();
	        _rectTransform = transform.GetComponent<RectTransform>();

	        Transform tempGameObject;

			if ((tempGameObject = transform.Find("YesButton")) !=
			    null) {
				_yesButton = tempGameObject.GetComponent<Button>();
				_yesButton.onClick.AddListener(YesButtonOnClick);
			}

	        if ((tempGameObject = transform.Find("NoButton")) !=
	            null) {
		        _noButton = tempGameObject.GetComponent<Button>();
		        _noButton.onClick.AddListener(NoButtonOnClick);
	        }

			transform.Find("Name").GetComponent<Text>().text =
                string.Format("Robot {0}", query.RobotId);
            transform.Find("Type").GetComponent<Text>().text =
                query.GetDisplayName();
        }

		private void YesButtonOnClick() {
		    ButtonOnClick(1);
	    }

	    private void NoButtonOnClick() {
		    ButtonOnClick(0);
	    }

	    private void ButtonOnClick(int answer) {
		    query.Response = answer;
		    MissionEventManager.OnQueryAnswered(query);
		    DisplayEventManager.OnClearDisplay();
	    }

		private void OnEnable()
        {
            MissionEventManager.QueryAnswered += OnQueryAnswered;
        }

        private void OnDisable()
        {
            MissionEventManager.QueryAnswered -= OnQueryAnswered;
        }

        public void Deactivate()
        {
            _image.color = new Color(32.0f / 255.0f, 32.0f / 255.0f,
                32.0f / 255.0f);
	        _rectTransform.sizeDelta = new Vector2(
		        _rectTransform.sizeDelta.x,
		        58.0f);
		}

        public void Activate()
        {
            _image.color = new Color(238.0f / 255.0f, 80.0f / 255.0f,
                33.0f / 255.0f);
			_rectTransform.sizeDelta = new Vector2(
				_rectTransform.sizeDelta.x,
				116.0f);
            query.Display();
        }

        private void OnQueryAnswered(object sender, QueryEventArgs e)
        {
            if (e.Query == query)
            {
	            query.TotalLifeTime = query.ArrivalTime - query.DepartureTime;
	            query.TotalUILifeTime = query.UIDepartureTime - query.UIArrivalTime;


	            query = null;
                Destroy(gameObject);
            }
        }

        public void DisplayQuery()
        {
            // There is a change in display, so clear everything.
            DisplayEventManager.OnClearDisplay();

            // If there is no query being displayed. Set this query to be active.
            if (_activeButton == null)
            {
                _activeButton = this;
                Activate();
                return;
            }

            // Since this point of the code has been reached there MUST be a query being displayed.


            // If this is NOT the active query, deactivate the current query and activate this query
            if (_activeButton != this)
            {
                _activeButton.Deactivate();
                _activeButton = this;
                Activate();
                return;
            }

            // If this IS the current query, then deactivate this query.
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