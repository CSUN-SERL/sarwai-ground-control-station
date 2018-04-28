using System;
using System.Collections.Generic;
using Participant;
using UnityEngine;
using UnityEngine.UI;

public class PerformanceScoreController : MonoBehaviour
{

	public StatisticCircle CircleBar;
    public GameObject FailedGameObject;
	
	void OnEnable () {

        // Remove all current performance Metrics.
	    foreach (Transform child in transform)
	    {
	            Destroy(child.gameObject);
	    }

		// Set up listener for when scores have been fetched.
		EventManager.PerformanceMetricsFetched += OnPerformanceMetricsFetched;

        // Set up listener for when score fetching failed.
	    EventManager.PerformanceMetricsFetchFailed += OnPerformanceMetricsFetchFailed;

        // Fetch all performance Measures
        EventManager.OnFetchPerformanceMetrics(ParticipantBehavior.Participant);

	}

    private void OnPerformanceMetricsFetched(object sender, PerformanceScoreEventArgs e)
	{

		// Instantiate a circle for every performance metric.
		foreach (var performanceMetric in e.PerformanceMetric)
		{
			var circleBar = Instantiate(CircleBar, transform);
			circleBar.transform.SetParent(transform, false);
			circleBar.StatNameTransform.GetComponent<Text>().text = performanceMetric.name;
		    circleBar.StatTransform.GetComponent<Text>().text = string.Format("{0}/{1}", performanceMetric.completed, performanceMetric.total);
		    circleBar.BarTransform.GetComponent<Image>().fillAmount = (float)performanceMetric.completed / performanceMetric.total;

		}
	}

    private void OnPerformanceMetricsFetchFailed(object sender, EventArgs e)
    {

        var failedGameObject = Instantiate(FailedGameObject, transform);
        failedGameObject.transform.SetParent(transform, false);
        Debug.Log("Failed");
    }
}
