using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FeedScreen.Experiment;
using FeedScreen.Experiment.Missions.Broadcasts.Events;
using Mission;
using Mission.Queries.QueryTypes.Audio;
using Mission.Queries.QueryTypes.Visual;
using Networking;
using Newtonsoft.Json.Linq;
using Participant;
using Survey;
using Tests.Queries;
using Tobii.Plugins;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace TransparencyIrisToOperator
{
    public class TransparencyBrief : MonoBehaviour
    {
        private AnswerButton _backButton;
        private List<GameObject> _go;
        private AnswerButton _nextButton;

        private int _questionIndex = 0;
        private List<Query> _queryList;

        public GameObject MessegePrefab;
        public GameObject DisplayPrefab;
        private bool _queryListMutex = false;

        private int hardcodedQueryNumber = 5;

        private int nLoadedQueries = 0;

        /// <summary>
        ///     Constructor that is 
        /// </summary>
        /// <returns></returns>
        public IEnumerator StartUp()
        {
            _queryList = new List<Query>();
            _go = new List<GameObject>();

            _nextButton = gameObject.transform.parent.GetChild(1).GetChild(0)
                .GetComponent<AnswerButton>();
            _nextButton.BehaviorOfButton = 1;
            _backButton = gameObject.transform.parent.GetChild(1).GetChild(1)
                .GetComponent<AnswerButton>();
            _backButton.BehaviorOfButton = -1;
            _nextButton.BehaviorOfButton = 1;

            _questionIndex = -1;

            //Will work after images are done being fetched;
            _queryListMutex = true;
            StartCoroutine(GetQueries());
            while (_queryListMutex) yield return null;
            

            while (_queryList.Count < hardcodedQueryNumber) yield return null;

            for (int i = 0; i < hardcodedQueryNumber; ++i)
            {
                var tempPrefab = MessegeSetup(_queryList[i]);
                tempPrefab.transform.SetParent(gameObject.transform);
                _go.Add(tempPrefab);
                _go.Last().SetActive(false);

            }

            SocketEventManager.QueryRecieved -= OnArrive;
            _backButton.gameObject.SetActive(true);
            _nextButton.gameObject.SetActive(true);
            NextQuestion();

        }

        private void OnChangeQuestion(object sender, IntEventArgs e)
        {
            if (_questionIndex > -1)
                _go[_questionIndex].SetActive(false);
            _questionIndex += e.intField;
            UpdateLiveFeed();
        }

        private void Start()
        {
            Debug.Log("Transparency brief start.");
            StartCoroutine(StartUp());
            //ActivateSurveyWithNumber.CallListener();
        }

        private void OnEnable()
        {
            EventManager.Load += OnLoad;
            SocketEventManager.QueryRecieved += OnArrive;
            Survey.EventManager.ChangeQuestion += OnChangeQuestion;
        }

        private void OnDisable()
        {
            EventManager.Load -= OnLoad;
            Survey.EventManager.ChangeQuestion -= OnChangeQuestion;
        }

        private void OnArrive(object sender, QueryEventArgs e)
        {
            _queryList.Add(e.Query);
            ++nLoadedQueries;
        }

        private void OnLoad(object sender, EventArgs e)
        {
            StartCoroutine(StartUp());

        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public bool HasNextQuestion()
        {
            return _questionIndex + 1 < _queryList.Count;
        }

        public bool HasPreviousQuestion()
        {
            return _questionIndex - 1 > -1;
        }
        


        /// <summary>
        /// </summary>
        /// <param name="answerSelection"></param>
        /// <param name="answers"></param>
        private static void PopulateAnswers(Component answerSelection,
            IList<string> answers)
        {
            var firstAnswer = answerSelection.gameObject;
            firstAnswer.SetActive(false);
            //Debug.Log(firstAnswer.name + "is firstAnswer");
            //Debug.Log(answerSelection.name + "is firstAnswer");
            foreach (var answer in answers)
            {
                var newAnswer = answer;
                var instance = Instantiate(firstAnswer);
                instance.SetActive(true);
                instance.transform.SetParent(firstAnswer.transform.parent);
                if (newAnswer[0] == '@')
                {
                    newAnswer = answer.Substring(1);
                    instance.GetComponent<InsertInputFieldInParentsParent>()
                        .SpawnInsertFieldOnTrue = true;
                }

                instance.transform.GetChild(0).GetComponent<Text>().text =
                    newAnswer;
            }
        }
        

        public IEnumerator GetQueries()
        {
            Debug.Log("Getting Queries.");

            WWWForm form = new WWWForm();
            form.AddField("participant_id", ParticipantBehavior.Participant.Data.Id);
            Debug.Log(ParticipantBehavior.Participant.Data.Id);
            form.AddField("mission_number", ParticipantBehavior.Participant.CurrentMission - 1);
            form.AddField("num_queries", hardcodedQueryNumber);
            //TODO: change to clusters or some other number thats not hardcoded.
            //TODO: change to clusters or some other number thats not hardcoded.

            using (UnityWebRequest www = UnityWebRequest.Post(ServerURL.RETRIEVE_TRANSPARENCY_BRIEF, form))
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                }

                else
                {
                    var text = www.downloadHandler.text;
                    var result = JObject.Parse(text);
                    Debug.Log(result["data"]);

                    if (result["response"].ToString() != "True")
                    {
                        Debug.Log(result["error"]);
                        _queryListMutex = false;
                        Application.Quit();
                    }
                    else if (result["data"].Count() == 0)
                    {
                        Debug.Log("Empty Transparency");
                        GoToNextScene();
                    }

                    foreach (var query in result["data"])
                    {
                        var temp = JsonToQuery(query);
                        temp.Arrive();
                    }

                    _queryListMutex = false;


                }
            }
        }

        private static Query JsonToQuery(JToken queryJson)
        {
            if (queryJson["type"].ToString() == VisualDetectionQuery.QueryType)
            {
                var temp = new VisualDetectionQuery();
                temp.ImageFileName = queryJson["file_path"].ToString();
                temp.QueryId = int.Parse(queryJson["query_id"].ToString());
                //query.RobotId = int.Parse(queryJson["robot_id"].ToString());
                temp.PreferredLevelOfAutonomy = int.Parse(queryJson["preferred_level_of_autonomy"].ToString());
                Debug.Log(temp.PreferredLevelOfAutonomy);
                temp.LevelOfAutonomy = int.Parse(queryJson["level_of_autonomy"].ToString());
                //query.preferred_level_of_autonomy = int.Parse(queryJson["preferred_level_of_autonomy"].ToString());
                //temp.UserId = int.Parse(queryJson["user_id"].ToString());
                //query.mission_id = int.Parse(queryJson["mission_id"].ToString());
                //query.true_response = int.Parse(queryJson["true_response"].ToString());
                //query = 
                return temp;
            }
            else if (queryJson["type"].ToString() == AudioDetectionQuery.QueryType)
            {
                var temp= new AudioDetectionQuery();
                temp.AudioFileName= queryJson["file_path"].ToString();
                temp.QueryId = int.Parse(queryJson["query_id"].ToString());
                //query.RobotId = int.Parse(queryJson["robot_id"].ToString());
                temp.PreferredLevelOfAutonomy = int.Parse(queryJson["preferred_level_of_autonomy"].ToString());
                Debug.Log(temp.PreferredLevelOfAutonomy);
                temp.LevelOfAutonomy = int.Parse(queryJson["level_of_autonomy"].ToString());
                //query.preferred_level_of_autonomy = int.Parse(queryJson["preferred_level_of_autonomy"].ToString());
                //temp.UserId = int.Parse(queryJson["user_id"].ToString());
                //query.mission_id = int.Parse(queryJson["mission_id"].ToString());
                //query.true_response = int.Parse(queryJson["true_response"].ToString());
                //query = 

                
                return temp;
            }
            else
            {
                return null;
            }
        }

        private static GameObject InstatiatePrefabAndPopulateAnswer(
            GameObject tempPrefab, string question)
        {
            var tempGameObject = Instantiate(tempPrefab);
            tempGameObject.transform.GetChild(0).GetChild(0)
                .GetComponent<Text>().text = question;
            return tempGameObject;
        }


        private GameObject MessegeSetup(Query query)
        {
            Debug.Log(string.Format("Query_id {0} Lvl_autonomy {1}", query.QueryId, query.PreferredLevelOfAutonomy));

            string autoString = "autonomous";
            if (query.LevelOfAutonomy == 0)
            {
                autoString = "non-autonomous";
            }
            var messege =
                string.Format(
                    "This {0} query is has been set to {1}, based on your given preferences.",query.GetDisplayName(),autoString);
            var tempPrefab =
                InstatiatePrefabAndPopulateAnswer(MessegePrefab,
                    messege);
            var display = new GameObject();
            display.AddComponent<RawImage>();
            display.AddComponent<AudioSource>();

            try
            {
                var q = query as VisualDetectionQuery;
                display.GetComponent<RawImage>().texture = q.Texture;

            }
            catch(Exception e)
            {
                Debug.Log(e.Message);
                try
                {
                    var q = query as AudioDetectionQuery;
                    display.GetComponent<AudioSource>().clip = q.Audio;
                }
                catch (Exception e2)
                {
                    Debug.Log(e2.Message);
                    throw;
                }
            }

            display.transform.SetParent(tempPrefab.transform.GetChild(0));
            return tempPrefab;
        }
        

        private void ReloadPrefab()
        {
             _go[_questionIndex].SetActive(true);
        }


        private void UpdateLiveFeed()
        {
            if (_questionIndex == _queryList.Count)
            {
                GoToNextScene();
            }
            else
            {
                _nextButton.GetComponentInChildren<Text>().text =
                    HasNextQuestion() ? "Next" : "Continue";
                if (HasPreviousQuestion())
                {
                    _backButton.Enable();
                }
                else
                {
                    _backButton.Disable();
                    _nextButton.GetComponentInChildren<Text>().text = "Begin";
                }

                Debug.Log(_go.Count + " = Count:Index = " + _questionIndex);

                if (_go.Count <= _questionIndex)
                {
                    _nextButton.Disable();
                    GoToNextScene();
                }
                else
                {
                    ReloadPrefab();
                }
            }
        }
        
        /// <summary>
        ///     Switches to next question in the survey
        /// </summary>
        public void NextQuestion()
        {
            if (_questionIndex > -1)
                _go[_questionIndex].SetActive(false);
            ++_questionIndex;
            UpdateLiveFeed();
        }

        /// <summary>
        ///     Switches to previous question in the survey
        /// </summary>
        public void PreviousQuestion()
        {
            if (_questionIndex > -1)
                _go[_questionIndex].SetActive(false);
            --_questionIndex;
            UpdateLiveFeed();
        }
        /*
        private void OnGUI()
        {
            var e = Event.current;
            if (e.isKey)
                if (e.keyCode == KeyCode.C && hardcodedQueryNumber <= nLoadedQueries)
                {
                    Debug.Log(_queryList.Count);
                    for (; _questionIndex < _queryList.Count() - 1;)
                    {
                        ++_questionIndex;
                        UpdateLiveFeed();
                    }
                }
        }
        */
        private void GoToNextScene()
        {
            Debug.Log("Ending Transparency");
            EventManager.OnEnd();
            SceneFlowController.LoadNextScene();
            //Participant.Instance.CurrentSurvey += 1;


            //SceneManager.getSurveyName();

            // SceneManager.LoadScene(
            //    ParticipantBehavior.Instance.CurrentMission == 6
            //       ? "FinalScene"
            //      : "QueryScreen");
            //_nextButton.Disable();
        }
    }
}