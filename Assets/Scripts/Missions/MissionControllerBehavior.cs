using System;
using UnityEngine;
using UnityEngine.UI;

// using Assets.Scripts;

namespace Mission
{
    [RequireComponent(typeof(MissionTimer))]
    [RequireComponent(typeof(MissionMetaData))]
    [RequireComponent(typeof(MissionLifeCycleController))]
    public class MissionControllerBehavior : MonoBehaviour
    {
    }
}