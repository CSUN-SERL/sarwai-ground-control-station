using System.Collections.Generic;

namespace Tobii.Framework.Internal
{
    internal class DataProviderStub<T> : IDataProvider<T> where T : ITimestamped
    {
        // --------------------------------------------------------------------
        //  Implementation of IDataProvider<T>
        // --------------------------------------------------------------------

        public T Last { get; protected set; }

        public IEnumerable<T> GetDataPointsSince(ITimestamped dataPoint)
        {
            return new List<T>();
        }

        public T GetFrameConsistentDataPoint()
        {
            return Last;
        }

        public void Start(int subscriberId)
        {
            // no implementation
        }

        public void Stop(int subscriberId)
        {
            // no implementation
        }
    }

    internal class
        GazePointDataProviderStub : DataProviderStub<Framework.GazePoint>
    {
        public GazePointDataProviderStub()
        {
            Last = Framework.GazePoint.Invalid;
        }
    }

    internal class
        HeadPoseDataProviderStub : DataProviderStub<Framework.HeadPose>
    {
        public HeadPoseDataProviderStub()
        {
            Last = Framework.HeadPose.Invalid;
        }
    }
}