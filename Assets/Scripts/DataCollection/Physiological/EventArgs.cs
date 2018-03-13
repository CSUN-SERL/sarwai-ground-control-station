using System;
using System.Data;

namespace DataCollection.Physiological
{
    public class PhysiologicalEventArgs : EventArgs
    {
        public DataTable DataTable { get; set; }
    }
}