using UnityEngine;

namespace Tests.Queries.Tagging
{
    public class TaggingQueryTest : MonoBehaviour
    {
        public Texture ImageTexture;

        public bool On;

        //// Use this for initialization
        //void Start() {
        //    if (!On) return;
        //    StartCoroutine(SendQuery());
        //}


        //IEnumerator SendQuery() {
        //    while (true) {
        //        yield return new WaitForSeconds(1F);
        //        var taggingQuery =
        //            new TaggingQuery {
        //                QueryId = 0,
        //                Texture = ImageTexture,
        //                ArrivalTime = MissionTimer.CurrentTime,
        //                Confidence = 0.6F,
        //                RobotId = 1,
        //                UserId = 0,
        //                Tag = TaggingQuery.Black
        //            };
        //        MissionEventManager.OnSentToUser(taggingQuery);
        //    }
        //}
    }
}