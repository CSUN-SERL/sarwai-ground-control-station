using System.Collections.Generic;
using Mission.Display.Queries;
using UnityEngine;

namespace Mission
{
    /// <summary>
    ///     Instantiates GUI elements such as Queries to the participant.
    /// </summary>
    public class QueryContainer : MonoBehaviour
    {
        protected Queue<Query> PendingQueries;
        public GameObject QueryPrefab;

        private void OnEnable()
        {
            SocketEventManager.QueryRecieved += OnQueryReceived;
            PendingQueries = new Queue<Query>();
        }

        private void OnDisable()
        {
            SocketEventManager.QueryRecieved -= OnQueryReceived;
            PendingQueries = null;
        }

        private void Update()
        {
            while (PendingQueries.Count > 0)
            {
                InsertQuery();
            }
        }

        public virtual void InsertQuery()
        {
            var query = PendingQueries.Dequeue();
            var queryprefab = Instantiate(QueryPrefab);
            queryprefab.GetComponent<QueryButton>().query = query;
            queryprefab.name = string.Format("Query{0}", query.QueryId);
            queryprefab.transform.SetParent(gameObject.transform, false);
        }

        public void AddQuery(Query q)
        {
            PendingQueries.Enqueue(q);
            Debug.Log(PendingQueries.Count);
        }

        private void OnQueryReceived(object sender, QueryEventArgs e)
        {
            Debug.Log("Adding Query to Queue.");
            AddQuery(e.Query);   
        }
    }
}