using UnityEngine;
using UnityEngine.UI;

namespace FeedScreen.Experiment.Missions.Broadcasts
{
    public class Answer : MonoBehaviour
    {
        private Button _button;
        public int Ans;

        // Use this for initialization
        private void Start()
        {
            _button = gameObject.GetComponent<Button>();
            _button.onClick.AddListener(SendAnswer);
        }

        // Update is called once per frame
        private void Update()
        {
        }

        public void SendAnswer()
        {
            //var query = gameObject.GetComponent<QueryActivity>().query;
            //query.Response = Ans;
            //MissionEventManager.OnQueryAnswered(Ans.ToString());
            //Destroy(gameObject);
        }
    }
}