using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using FeedScreen.Experiment.Missions.Broadcasts.Events;
using MediaDownload;
using Mission;
using Mission.Display.Queries;
using Mission.Queries.QueryTypes.Audio;
using Mission.Queries.QueryTypes.Visual;
using Networking;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Survey
{
    public class GenericSurvey : MonoBehaviour
    {
        private AnswerButton _backButton;
        private List<GameObject> _go;
        private AnswerButton _nextButton;

        private int _questionIndex;
        private int _surveyNumber;
        private List<QuestionDetails> _surveyQuestionList;

        private string filePath = "";

        public GameObject FreeResponsePrefab;
        public GameObject MessegePrefab;
        public GameObject MultiplePrefab;
        public GameObject NumericPrefab;
        public GameObject QueryImagePrefab;
        public GameObject ScalarPrefab;
        public GameObject ScalePrefab;

        public IEnumerator StartUp(int tempSurveyNumber)
        {
            //Debug.Log("Starting Survey");

            _surveyNumber = tempSurveyNumber;
            _go = new List<GameObject>();
            var downloadManager = gameObject.AddComponent<LoadSurveyFromWeb>();

            while (!downloadManager.Loading)
                yield return new WaitForEndOfFrame();
            //Debug.Log(downloadManager.Loading + " is 1st state of loading");

            StartCoroutine(downloadManager.LoadSurveyEnumerator(_surveyNumber));

            _nextButton = gameObject.transform.parent.GetChild(1).GetChild(0)
                .GetComponent<AnswerButton>();
            _nextButton.BehaviorOfButton = 1;
            _backButton = gameObject.transform.parent.GetChild(1).GetChild(1)
                .GetComponent<AnswerButton>();
            _backButton.BehaviorOfButton = -1;
            _nextButton.BehaviorOfButton = 1;

            _questionIndex = -1;

            while (downloadManager.Loading)
                yield return new WaitForEndOfFrame();
            // Debug.Log(downloadManager.Loading + " is 2nd state of loading");

            _surveyQuestionList = downloadManager.SurveyList;
            if (_surveyQuestionList.Count == 0)
                GoToNextScene();

            //Debug.Log(HasNextQuestion());
            _backButton.gameObject.SetActive(true);
            _nextButton.gameObject.SetActive(true);
            NextQuestion();
        }

        private void Start()
        {
            //Debug.Log("GenericSurvey Start");
            ActivateSurveyWithNumber.CallListener();
        }

        private void OnEnable()
        {
            EventManager.Load += OnLoad;
            EventManager.ChangeQuestion += OnChangeQuestion;
            MediaDownload.EventManager.AudioClipDownloaded +=
                OnAudioClipDownloaded;
            MediaDownload.EventManager.TextureDownloaded +=
                OnTextureDownloaded;
        }

        private void OnDisable()
        {
            EventManager.Load -= OnLoad;
            EventManager.ChangeQuestion -= OnChangeQuestion;
            MediaDownload.EventManager.AudioClipDownloaded -=
                OnAudioClipDownloaded;
            MediaDownload.EventManager.TextureDownloaded -=
                OnTextureDownloaded;
        }

        private void OnTextureDownloaded(object sender,
            DownloadTextureEventArgs e)
        {
            Debug.Log("Image Received");
            DisplayEventManager.OnDisplayImage(e.ImageTexture);
        }

        private void OnAudioClipDownloaded(object sender,
            DownloadAudioClipEventArgs e)
        {
            Debug.Log(string.Format("Audio Received {0}", e.Clip.length));
            DisplayEventManager.OnDisplayAudioClip(e.Clip);
        }

        private void OnChangeQuestion(object sender, IntEventArgs e)
        {
            Debug.Log(string.Format("Changing Index to {0}", e.intField));
            if (_questionIndex > -1)
                _go[_questionIndex].SetActive(false);
            _questionIndex += e.intField;
            UpdateLiveFeed();
        }

        private void OnLoad(object sender, IntEventArgs e)
        {
            //Debug.Log(e.intField);
            StartCoroutine(StartUp(e.intField));
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public bool HasNextQuestion()
        {
            return _questionIndex + 1 < _surveyQuestionList.Count;
        }

        public bool HasPreviousQuestion()
        {
            return _questionIndex - 1 > -1;
        }

        /// <summary>
        ///     Sets up a question with a given prefab />
        /// </summary>
        /// <param name="tempPrefab"></param>
        /// <param name="question"></param>
        /// <returns>returns an instantiated GameObject</returns>
        private static GameObject InstatiatePrefabAndPopulateAnswer(
            GameObject tempPrefab, string question)
        {
            var answerSelection = Instantiate(tempPrefab);
            answerSelection.transform.GetChild(0).GetChild(0)
                .GetComponent<Text>().text = question;
            return answerSelection;
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


        /// <summary>
        ///     Sets up a question with an <see cref="InputField" />
        /// </summary>
        /// <param name="questionDetails">A list consisting of a question and the rest is answers</param>
        /// <returns>returns an instantiated GameObject</returns>
        private GameObject FreeResponseSetUp(QuestionDetails questionDetails)
        {
            var tempPrefab =
                InstatiatePrefabAndPopulateAnswer(FreeResponsePrefab,
                    questionDetails.QuestionString);
            return tempPrefab;
        }

        /// <summary>
        ///     Sets up a question prefab with a <see cref="Dropdown" />
        /// </summary>
        /// <param name="questionDetails">A list consisting of a question and the rest is answers</param>
        /// <returns>returns an instantiated GameObject</returns>
        /// <remarks>ToList() is used because Unity's <see cref="Dropdown" /> does not accept Itterables.</remarks>
        private GameObject MultipleSetup(QuestionDetails questionDetails)
        {
            var tempPrefab =
                InstatiatePrefabAndPopulateAnswer(MultiplePrefab,
                    questionDetails.QuestionString);
            var answerSelection = tempPrefab.transform.GetChild(1).GetChild(0)
                .GetChild(1);
            //Debug.Log(answerSelection.name + " is in MultipleSetup");
            PopulateAnswers(answerSelection, questionDetails.OfferedAnswerList);
            return tempPrefab;
        }

        private static GameObject InstantiateQueryImage(
            GameObject tempPrefab, string questionId)
        {
            //tempPrefab.transform.GetChild(0).GetChild(0)
            //   .GetComponent<Text>().text = questionId;

            return tempPrefab;
        }


        //public IEnumerator RequestImage(DownloadMediaEventArgs media)
        //{
        //    Debug.Log("Requesting Image");

        //    if (media.fileName.Length <= 0 || media.fileName == null)
        //        yield break;
        //    var www =
        //        UnityWebRequestTexture.GetTexture(
        //            ServerURL.DownloadMediaUrl(media.fileName));
        //    yield return www.SendWebRequest();

        //    if (www.isNetworkError || www.isHttpError)
        //    {
        //        Debug.Log(www.error);
        //        yield break;
        //    }

        //    Debug.Log("Image Download Successful.");

        //    var query = (VisualDetectionQuery)media.query;

        //    System.Diagnostics.Debug.Assert(query != null, "query != null");

        //    var texture =
        //        ((DownloadHandlerTexture)www.downloadHandler).texture;

        //    if (texture != null)
        //    {
        //        query.Texture = ((DownloadHandlerTexture)www.downloadHandler)
        //            .texture;
        //        EventManager.OnImageDownloaded(query);
        //    }
        //    else
        //    {
        //        Debug.Log("Could not download Media");
        //    }
        //}
        public IEnumerator LoadQueryImageEnumerator(string questionId)
        {
            var form = new WWWForm();
            form.AddField("question_id", questionId);

            using (var www = UnityWebRequest.Post(ServerURL.RETRIEVE_IMAGE,
                form))
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                }

                else
                {
                    var response = www.downloadHandler.text;
                    var result = JObject.Parse(response);
                    Debug.Log(result);
                    var filePath = result["data"][0]["file_path"];
                    var type = result["data"][0]["type"].ToString();
                    var guid = Guid.NewGuid();

                    Debug.Log(filePath + " is image path");
                    Debug.Log(guid + " is guid");
                    var media = new DownloadMediaEventArgs();
                    media.FileName = filePath.ToString();

                    if (type == VisualDetectionQuery.QueryType)
                        media.MediaType = MediaDownloader.ImageType;
                    else if (type == AudioDetectionQuery.QueryType)
                        media.MediaType = MediaDownloader.AudioClipType;

                    media.DownloadGuid = guid;
                    MediaDownload.EventManager.OnDownloadMedia(media);
                }
            }
        }

        private GameObject DebriefSetup(QuestionDetails questionDetails)
        {
            //make sure prefab works in the scene where it should be placed. drag and drop a prefab question in test explorer.
            var tempPrefab = MultipleSetup(questionDetails);

            var imageSelection = tempPrefab.transform.GetChild(1);
            imageSelection.gameObject.GetComponent<HorizontalLayoutGroup>()
                .childAlignment = TextAnchor.MiddleCenter;

            var temp = new GameObject();
            var img = temp.AddComponent<RawImage>();

            img.GetComponent<RectTransform>().sizeDelta =
                new Vector2(Screen.width / 3, Screen.height / 3);

            temp.AddComponent<ImageDisplay>();
            temp.AddComponent<AudioPlayer>();
            var questionId = questionDetails.QuestionId;
            //DICK
            //get image location given question_id

            StartCoroutine(LoadQueryImageEnumerator(questionId));

            //}

            temp.transform.SetParent(imageSelection, false);
            temp.transform.SetAsFirstSibling();
            return tempPrefab;
        }

        /// <summary>
        ///     Sets up a question prefab with a several <see cref="Button" />
        /// </summary>
        /// <param name="questionDetails">A list consisting of a question and the rest is answers</param>
        /// <returns>returns an instantiated GameObject</returns>
        /// <remarks>ToList() is used because Unity's <see cref="Dropdown" /> does not accept Itterables.</remarks>
        private GameObject ScalarSetup(QuestionDetails questionDetails)
        {
            var tempPrefab =
                InstatiatePrefabAndPopulateAnswer(ScalarPrefab,
                    questionDetails.QuestionString);
            var answerSelection = tempPrefab.transform.GetChild(1).GetChild(0)
                .GetChild(1);
            //Debug.Log(answerSelection.name + " is in ScalarSetup");
            PopulateAnswers(answerSelection, questionDetails.OfferedAnswerList);
            return tempPrefab;
        }

        /// <summary>
        ///     Sets up a question prefab with a several <see cref="Button" />
        /// </summary>
        /// <param name="questionDetails">A list consisting of a question and the rest is answers</param>
        /// <returns>returns an instantiated GameObject</returns>
        /// <remarks>ToList() is used because Unity's <see cref="Dropdown" /> does not accept Itterables.</remarks>
        private GameObject NumericSetup(QuestionDetails questionDetails)
        {
            var tempPrefab =
                InstatiatePrefabAndPopulateAnswer(NumericPrefab,
                    questionDetails.QuestionString);
            var answerSelection = tempPrefab.transform.GetChild(1).GetChild(0)
                .GetChild(0);
            //Debug.Log(answerSelection.name);
            var answerList = new List<string>();
            foreach (var answer in questionDetails.OfferedAnswerList)
            {
                var rangeOrAnswer = Regex.Split(answer, @"-");
                if (rangeOrAnswer.Length > 1)
                {
                    int initialRange;
                    int.TryParse(rangeOrAnswer[0], out initialRange);
                    int finalRange;
                    int.TryParse(rangeOrAnswer[1], out finalRange);
                    for (var i = initialRange; i < finalRange; ++i)
                        answerList.Add(i.ToString());
                }
                else
                {
                    answerList.Add(answer);
                }
            }

            var dropdown = answerSelection.GetChild(0).GetChild(0)
                .GetComponent<Dropdown>();
            dropdown.AddOptions(answerList);
            return tempPrefab;
        }

        private GameObject MessegeSetup(QuestionDetails questionDetails)
        {
            var tempPrefab =
                InstatiatePrefabAndPopulateAnswer(MessegePrefab,
                    questionDetails.QuestionString);

            return tempPrefab;
        }

        private GameObject PickAllSetup(QuestionDetails currentDetails)
        {
            var tempPrefab = MultipleSetup(currentDetails);
            Destroy(tempPrefab.transform.GetChild(1).GetChild(0).GetChild(0)
                .gameObject);
            return tempPrefab;
        }

        /// <summary>
        ///     Sets up the Scale Prefab in unity with answers and a questinon/
        /// </summary>
        /// <param name="questionDetails"></param>
        /// <returns></returns>
        private GameObject ScaleSetup(
            QuestionDetails questionDetails)
        {
            var tempPrefab =
                InstatiatePrefabAndPopulateAnswer(ScalePrefab,
                    questionDetails.QuestionString);

            //Sets the text on the right side of the scale.
            tempPrefab.transform.GetChild(1).GetChild(0)
                    .GetChild(0).GetChild(0).GetChild(0).GetChild(0)
                    .GetComponent<Text>().text =
                questionDetails.OfferedAnswerList[0];

            //Sets the text on the right side of the scale.
            tempPrefab.transform.GetChild(1).GetChild(0)
                    .GetChild(0).GetChild(0).GetChild(0).GetChild(1)
                    .GetComponent<Text>().text =
                questionDetails.OfferedAnswerList[1];
            return tempPrefab;
        }


        /// <summary>
        ///     Calls specific method based on which question type it is.
        /// </summary>
        /// <param name="currentDetails"></param>
        /// <returns></returns>
        private GameObject FindOutQuestionType(QuestionDetails currentDetails)
        {
            //Debug.Log(currentDetails.QuestionType);
            switch (currentDetails.QuestionType)
            {
                case "FreeResponse":
                    return FreeResponseSetUp(currentDetails);
                case "Multiple":
                    return MultipleSetup(currentDetails);
                case "Scalar":
                    return ScalarSetup(currentDetails);
                case "Numerical":
                    return NumericSetup(currentDetails);
                //special cases bellow

                case "Intro":
                    return MessegeSetup(currentDetails);
                case "Outro":
                    return MessegeSetup(currentDetails);
                case "Debrief":
                    return DebriefSetup(currentDetails);
                case "IfYesRespond":
                    return MultipleSetup(currentDetails);
                case "IfNoRespond":
                    return MultipleSetup(currentDetails);
                case "IfScalarLessThan3Respond":
                    return MultipleSetup(currentDetails);
                case "Scale":
                    return ScaleSetup(currentDetails);
                case "TLX":
                    return ScaleSetup(currentDetails);
                case "PickAll":
                    return PickAllSetup(currentDetails);
            }

            Debug.Log(string.Format("Question of type '{0}' does not exist",
                currentDetails.QuestionType));

            return FreeResponseSetUp(currentDetails);
        }

        /// <summary>
        ///     Loads a prefab based on List of info about the question.
        /// </summary>
        /// <param name="questionDetails">
        ///     Follows the format of :
        ///     question_type, question string, answer, ..., answer
        /// </param>
        private void LoadPrefab(QuestionDetails questionDetails)
        {
            var go = FindOutQuestionType(questionDetails);
            go.transform.SetParent(gameObject.transform);
            go.name = _questionIndex.ToString();
            go.GetComponent<RectTransform>().sizeDelta = go.transform.parent
                .GetComponent<RectTransform>().sizeDelta;
            go.GetComponent<RectTransform>().position = go.transform.parent
                .GetComponent<RectTransform>().position;
            if (_go.Count != 0)
            {
                var x = _go.Last();
                x.SetActive(false);
            }

            _go.Add(go);
        }

        private void ReloadPrefab()
        {
            var go = _go[_questionIndex];
            go.transform.SetAsLastSibling();
            go.SetActive(true);
        }

        //possibly different implemintations based on survey
        /// <summary>
        /// </summary>
        /// <param name="path"></param>
        /// TODO:help me
        //private IEnumerable Load(int path)
        //{
        //    var downloadManager = gameObject.AddComponent<LoadSurveyFromWeb>();
        //    downloadManager.SurveyList
        //    return stuff;

        //    //read survey from path (textfile)
        //}
        /// <summary>
        ///     Changes the question prefab to a new prefab.
        /// </summary>
        /// TODO: make it so that if there is no previous question, button doenst exist, else
        /// there is a previous button
        /// TODO: when there are no next questions, have an end button.
        private void UpdateLiveFeed()
        {
            //Debug.Log(_questionIndex +"Index == quuestion list Count" + _surveyQuestionList.Count);
            if (_questionIndex == _surveyQuestionList.Count)
            {
                //Debug.Log("questionIndex = surveyQuestionList");

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

                if (_go.Count == _questionIndex)
                {
                    LoadPrefab(_surveyQuestionList[_questionIndex]);
                }
                else if (_go.Count < _questionIndex)
                {
                    //Debug.Log("is in go.Count < questionIndex");
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
        ///     Switches to a specific survey question
        /// </summary>
        /// <param name="quetionIndex"></param>
        public void PickQuestion(int quetionIndex)
        {
            _go[_questionIndex].SetActive(false);
            _questionIndex = quetionIndex;
            UpdateLiveFeed();
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

        internal void AnswerQuestion(string response)
        {
            //record answer
        }

        /// <summary>
        ///     Loads the next scene/kills this one.
        /// </summary>
        /// <remarks>
        ///     Because Collin wanted this.
        /// </remarks>
        /// <remarks>
        ///     Last Modified: Collin Miller
        ///     Date: 1/12/18
        ///     Reason now the survey will fork to the final screen if the participant is done with experiment.
        /// </remarks>
        /*
        private void OnGUI()
        {
            var e = Event.current;
            if (e.isKey)
                if (e.keyCode == KeyCode.C)
                {
                    Debug.Log(_surveyQuestionList.Count);
                    for (; _questionIndex < _surveyQuestionList.Count() - 1;)
                    {
                        ++_questionIndex;
                        UpdateLiveFeed();
                    }
                }
        }
        */
        private void GoToNextScene()
        {
            var ag = gameObject.AddComponent<AnswerGatherer>();

            ag.GatherAnswers(ref _go, ref _surveyQuestionList, _surveyNumber);
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