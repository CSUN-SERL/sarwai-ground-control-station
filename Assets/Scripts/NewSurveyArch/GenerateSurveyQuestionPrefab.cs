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

namespace NewSurveyArch
{
    public class GenerateSurveyQuestionPrefab : ScriptableObject
    {
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
        private GameObject FreeResponseSetUp(SurveyQuestion questionDetails)
        {
            var tempPrefab =
                InstatiatePrefabAndPopulateAnswer(FreeResponsePrefab/*Reference.Load('')*/,
                    questionDetails.Text);
            return tempPrefab;
        }

        /// <summary>
        ///     Sets up a question prefab with a <see cref="Dropdown" />
        /// </summary>
        /// <param name="questionDetails">A list consisting of a question and the rest is answers</param>
        /// <returns>returns an instantiated GameObject</returns>
        /// <remarks>ToList() is used because Unity's <see cref="Dropdown" /> does not accept Itterables.</remarks>
        private GameObject MultipleSetup(SurveyQuestion questionDetails)
        {
            var tempPrefab =
                InstatiatePrefabAndPopulateAnswer(MultiplePrefab,
                    questionDetails.Text);
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

        /// <summary>
        ///     Sets up a question prefab with a several <see cref="Button" />
        /// </summary>
        /// <param name="questionDetails">A list consisting of a question and the rest is answers</param>
        /// <returns>returns an instantiated GameObject</returns>
        /// <remarks>ToList() is used because Unity's <see cref="Dropdown" /> does not accept Itterables.</remarks>
        private GameObject ScalarSetup(SurveyQuestion questionDetails)
        {
            var tempPrefab =
                InstatiatePrefabAndPopulateAnswer(ScalarPrefab,
                    questionDetails.Text);
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
        private GameObject NumericSetup(SurveyQuestion questionDetails)
        {
            var tempPrefab =
                InstatiatePrefabAndPopulateAnswer(NumericPrefab,
                    questionDetails.Text);
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

        private GameObject MessegeSetup(SurveyQuestion questionDetails)
        {
            var tempPrefab =
                InstatiatePrefabAndPopulateAnswer(MessegePrefab,
                    questionDetails.Text);

            return tempPrefab;
        }

        private GameObject PickAllSetup(SurveyQuestion currentDetails)
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
            SurveyQuestion questionDetails)
        {
            var tempPrefab =
                InstatiatePrefabAndPopulateAnswer(ScalePrefab,
                    questionDetails.Text);

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
        private GameObject FindOutQuestionType(SurveyQuestion currentDetails)
        {
            //Debug.Log(currentDetails.Type);
            switch (currentDetails.Type)
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
                currentDetails.Type));

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
}//                Instantiate(Resources.Load("SurveyQuestion/Multiple") as GameObject);
