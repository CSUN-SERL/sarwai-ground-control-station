using System;

namespace DataCollection.Physiological.Sensors
{
    public interface IPhysisiologicalDataSensor<T>
    {
        T GetSensorValue();

        /// <summary>
        ///     This is for testing purposes.  A Sensor is responsible for
        ///     providing its own failure value so then a test can check if the
        ///     sensors current value is equal to the sensors fail value.
        /// </summary>
        /// <remarks>
        ///     A sensor fail value should be a value that the sensor should never read.
        /// </remarks>
        /// <returns>
        ///     A generic T value.
        /// </returns>
        T GetSensorFailureValue();


        /// <summary>
        ///     This will be responsible for starting the logging for every sensor.
        /// </summary>
        void StartLogging(object sender, EventArgs e);


        /// <summary>
        ///     This will be responsible for stopping the logging for every sensor.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void StopLogging(object sender, EventArgs e);
    }
}