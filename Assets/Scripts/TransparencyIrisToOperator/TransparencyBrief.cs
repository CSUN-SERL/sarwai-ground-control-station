using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FeedScreen.Experiment;
using FeedScreen.Experiment.Missions.Broadcasts.Events;
using MediaDownload;
using Mission;
using Mission.Queries.QueryTypes;
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
        private List<GameObject> _display;
        private AnswerButton _nextButton;

        private int _questionIndex = 0;
        private List<JToken> _queryList;
        private List<Guid> _dataDownloadId;

        public GameObject MessegePrefab;
        public GameObject DisplayPrefab;
        private bool _queryListMutex = false;

        private int hardcodedQueryNumber = 0;

        private int nLoadedQueries = 0;

        /// <summary>
        ///     Constructor that is 
        /// </summary>
        /// <returns></returns>
        public IEnumerator StartUp()
        {
            _queryList = new List<JToken>();
            _go = new List<GameObject>();
            _dataDownloadId = new List<Guid>();

            AddIntro();

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
            foreach (var query in _queryList)
            {
                var tempPrefab = MessegeSetup(query);
                tempPrefab.transform.SetParent(gameObject.transform);
                _go.Add(tempPrefab);
                _go.Last().SetActive(false);

            }

            for (int i = 0; i < _queryList.Count; ++i)
            {
                if (_queryList[i]["type"].ToString() == AudioDetectionQuery.QueryType)
                {
                    _dataDownloadId[i] = Guid.NewGuid();
                    MediaDownload.EventManager.OnDownloadMedia(new DownloadMediaEventArgs
                    {
                        DownloadGuid = _dataDownloadId[i],
                        FileName = _queryList[i]["file_path"].ToString(),
                        MediaType = MediaDownloader.AudioClipType
                    });

                    /*
                    MediaDownload.EventManager.OnDownloadMedia(new Dow)
                    var temp = new AudioDetectionQuery();
                    temp.AudioFileName = _queryList[i]["file_path"].ToString();
                    temp.QueryId =int.Parse(_queryList[i]["query_id"].ToString());
                    temp.Arrive();
                    _dataDownload.Add(temp);
                    */
                } else
                {
                    _dataDownloadId.Add(Guid.NewGuid());
                    MediaDownload.EventManager.OnDownloadMedia(new DownloadMediaEventArgs
                    {
                        DownloadGuid = _dataDownloadId[_dataDownloadId.Count-1],
                        FileName = _queryList[i]["file_path"].ToString(),
                        MediaType = MediaDownloader.ImageType
                    });
                    /*
                    var temp = new VisualQuery();
                    temp.ImageFileName = _queryList[i]["file_path"].ToString();
                    temp.QueryId = int.Parse(_queryList[i]["query_id"].ToString());
                    temp.Arrive();
                    _dataDownloadId.Add(temp);
                    */
                }

            }

            while (_queryList.Count < hardcodedQueryNumber) yield return null;
            

            SocketEventManager.QueryRecieved -= OnArrive;
            _backButton.gameObject.SetActive(true);
            _nextButton.gameObject.SetActive(true);
            AddOutro();
            NextQuestion();

        }

        /// <summary>
        ///     Adding an intro messege to transparency brief.
        /// </summary>
        private void AddIntro()
        {
            var messege =
                "Iris has used your feedback to improve your experience in the next mission. " +
                "Please click 'Begin' to see how this will affects you.";
            var tempPrefab = InstatiatePrefabAndPopulateMessege(Resources.Load<GameObject>("SurveyQuestion/Messege"),messege);
            tempPrefab.transform.SetParent(gameObject.transform);
            _go.Add(tempPrefab);
            _go.Last().SetActive(false);

        }

        /// <summary>
        ///     Adding an outro messege to transperency brief.
        /// </summary>
        private void AddOutro()
        {
            var messege =
                "Iris has used your feedback to improve your experience in the next mission. " +
                "Please click 'Continue' to go on to your next mission.";
            var tempPrefab = InstatiatePrefabAndPopulateMessege(Resources.Load<GameObject>("SurveyQuestion/Messege"), messege);
            tempPrefab.transform.SetParent(gameObject.transform);
            _go.Add(tempPrefab);
            _go.Last().SetActive(false);

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
            MediaDownload.EventManager.TextureDownloaded += OnDownloadedImage;
            MediaDownload.EventManager.AudioClipDownloaded += OnDownloadedAudio;
        }
        /*
            display.AddComponent<RawImage>();
            display.AddComponent<AudioSource>(); 
         */
        private void OnDownloadedAudio(object sender, DownloadAudioClipEventArgs e)
        {
            Debug.Log("Audio Downloaded");
            int index = indexOfGuid(e.DownloadGuid);
            _display[index].AddComponent<AudioSource>();
            var eventArgs = e as DownloadAudioClipEventArgs;
            _display[index].GetComponent<AudioSource>().clip = eventArgs.Clip;
        }
        private int indexOfGuid(Guid guidid)
        {
            for (int i = 0; i < _dataDownloadId.Count; ++i)
            {
                if (guidid == _dataDownloadId[i])
                    return i;
            }
            throw new IndexOutOfRangeException (string.Format("no guid found in transparency {0}",guidid.ToString()));
        }

        private void OnDownloadedImage(object sender, DownloadTextureEventArgs e)
        {
            Debug.Log("Image Downloaded");
            int index = indexOfGuid(e.DownloadGuid);
            _display[index].AddComponent<RawImage>();
            var eventArgs = e as DownloadTextureEventArgs;
            _display[index].GetComponent<RawImage>().texture = eventArgs.ImageTexture;
        }

        private void OnDisable()
        {
            EventManager.Load -= OnLoad;
            Survey.EventManager.ChangeQuestion -= OnChangeQuestion;
            MediaDownload.EventManager.TextureDownloaded -= OnDownloadedImage;
            MediaDownload.EventManager.AudioClipDownloaded -= OnDownloadedAudio;
        }

        private void OnArrive(object sender, QueryEventArgs e)
        {
            //_queryList.Add(e.Query as ConfidenceQuery);
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
            return _questionIndex + 1 < _go.Count;
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
                        yield return null;
                        
                    }

                    foreach (var query in result["data"])
                    {
                        _queryList.Add(query);
                        ++hardcodedQueryNumber;

                    }

                    _queryListMutex = false;


                }
            }
        }
        /*
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
                temp.RobotId = int.Parse(queryJson["robot_id"].ToString());
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
        }*/

        private static GameObject InstatiatePrefabAndPopulateMessege(
            GameObject tempPrefab, string question)
        {
            var tempGameObject = Instantiate(tempPrefab);
            tempGameObject.transform.GetChild(0).GetChild(0)
                .GetComponent<Text>().text = question;
            return tempGameObject;
        }


        private GameObject MessegeSetup(JToken query)
        {
            Debug.Log(string.Format("Query_id {0} Lvl_autonomy {1}", query["query_id"].ToString(), query["level_of_autonomy"].ToString()));


            var type = "saw";
            if (query["type"].ToString() == AudioDetectionQuery.QueryType)
                type = "heard";

            string autoString = "Iris has decided to continue to handle" +
              " this type of query in the next mission";//autonomous

            if (query["preferred_level_of_autonomy"].ToString() == "0")
            {
                autoString = "Iris has decided that you will handle" + 
                  " this type of query in the next mission";//non-autonomous
            }
           
            var messege ="Rover {0} incorrectly identified a human, " +
              "with {1}% confidence, where there wasn't a human victim. " +
              "In this case, Rover {0} mistake was based on what it {2}. " +
              "However, based on the query responses in the previous mission " +
              autoString;
            if ("1" == query["true_response"].ToString())
            {
     
                messege ="Rover {0} correctly identified a human, " +
                  "with {1}% confidence." +
                  "Based on the query responses in the previous mission " +
                  autoString;


            }

            messege = string.Format(
                    messege,query["robot_id"].ToString(), query["confidence"].ToString(), type);
            var tempPrefab =
                InstatiatePrefabAndPopulateMessege(Resources.Load<GameObject>("SurveyQuestion/Messege"),
                    messege);
            var display = new GameObject();
            
            /*
            try
            {
                Debug.Log("Not trying to get images");
                var q = _dataDownloadId.Add(temp) as VisualDetectionQuery;
                //var q = query as VisualQuery;
                //display.GetComponent<RawImage>().texture = q.Texture;

            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
                try
                {
                    Debug.Log("Not trying to get audio");

                    //var q = query as AudioDetectionQuery;
                    //display.GetComponent<AudioSource>().clip = q.Audio;
                }
                catch (Exception e2)
                {
                    Debug.Log(e2.Message);
                    throw;
                }
            }*/
            

            display.transform.SetParent(tempPrefab.transform.GetChild(0));
            _display.Add(display);
            return tempPrefab;
        }
        
        

        private void ReloadPrefab()
        {
             _go[_questionIndex].SetActive(true);
        }


        private void UpdateLiveFeed()
        {
            Debug.Log(_queryList.Count + " =_queryList.Count:Index = " + _questionIndex);
            if (_questionIndex == _queryList.Count + 2)
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

                Debug.Log(_go.Count + " = _go.Count:Index = " + _questionIndex);

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
