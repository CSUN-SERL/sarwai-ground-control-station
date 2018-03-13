using System;
using System.Data;

namespace DataCollection.Physiological
{
    public class PhysiologicalDataEventArgs : EventArgs
    {
        public DataTable DataTable { get; set; }
    }
}