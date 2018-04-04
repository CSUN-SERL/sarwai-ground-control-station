using System;
using System.Collections;
using Mission;
using UnityEngine;
using UnityEngine.UI;

public class FlashIndicator : MonoBehaviour
{
    /// <summary>
    ///     How long should the flash happen.
    /// </summary>
    private const int Wait = 1;

    /// <summary>
    ///     How many flashes should happen.
    /// </summary>
    private const int Duration = 6;

    /// <summary>
    ///     Keeps track of how many flashes are left to happen.
    /// </summary>
    private int _blinks;

    /// <summary>
    ///     Mutex for flashing.
    /// </summary>
    private bool _mutex;

    /// <summary>
    ///     Id for which Robot Feed to manage.
    /// </summary>
    public int RobotId;

    private void Awake()
    {
        gameObject.GetComponent<Image>().enabled = false;
    }

    private void OnEnable()
    {
        SocketEventManager.QueryGenerated += OnQueryGenerated;
    }

    private void OnDisable()
    {
        SocketEventManager.QueryGenerated -= OnQueryGenerated;
    }

    /// <summary>
    ///     Resets the number of flashes to happen and Restarts blinks.
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="args"></param>
    private void OnQueryGenerated(string obj, EventArgs args)
    {
        _blinks = Duration;

        if (int.Parse(obj) != RobotId) return;
        if (_mutex) return;

        StartCoroutine(Blink());
        _mutex = false;
    }

    /// <summary>
    ///     Blinks the indicator.
    /// </summary>
    /// <returns></returns>
    private IEnumerator Blink()
    {
        _mutex = true;
        for (; _blinks > 0; --_blinks)
        {
            gameObject.GetComponent<Image>().enabled = true;
            yield return new WaitForSeconds(Wait);
            gameObject.GetComponent<Image>().enabled = false;
            yield return new WaitForSeconds(Wait);
        }
    }
}