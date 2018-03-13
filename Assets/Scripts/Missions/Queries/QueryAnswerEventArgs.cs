using System;

namespace Mission.Queries
{
    public class QueryAnswerEventArgs : EventArgs
    {
        public int QueryId { get; set; }
        public string Answer { get; set; }
    }
}