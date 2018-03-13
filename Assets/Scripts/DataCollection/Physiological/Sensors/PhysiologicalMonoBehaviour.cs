using System;
using System.Collections;
using DataCollection.Physiological.Sensors;
using UnityEngine;

namespace Assets.Scripts.DataCollection.Physiological.Sensors
{
    public abstract class PhysiologicalMonoBehaviour<T> : MonoBehaviour,
        IPhysisiologicalDataSensor<T>
    {
        protected T _sensorValue;

        public void StartLogging(object sender, EventArgs e)
        {
            _sensorValue = GetSensorFailureValue();
            StartCoroutine(Log());
        }

        public void StopLogging(object sender, EventArgs e)
        {
            StopCoroutine(Log());
        }

        public T GetSensorValue()
        {
            return _sensorValue;
        }

        public abstract T GetSensorFailureValue();

        private void OnEnable()
        {
            EventManager.StartLogging += StartLogging;
            EventManager.StopLogging += StopLogging;
        }

        private void OnDisable()
        {
            EventManager.StartLogging -= StartLogging;
            EventManager.StopLogging -= StopLogging;
        }

        public abstract IEnumerator Log();
    }
}