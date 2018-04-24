using System;
using System.Collections.Generic;

namespace Participant {
    public class PerformanceScoreEventArgs : EventArgs
    {
        public List<PerformanceMetric> PerformanceMetric { get; set; }
    }
}