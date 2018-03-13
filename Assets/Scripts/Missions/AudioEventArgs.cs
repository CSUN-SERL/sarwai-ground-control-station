using System;
using UnityEngine;

namespace Mission
{
    public class AudioEventArgs : EventArgs
    {
        public AudioClip Clip { get; set; }
    }
}