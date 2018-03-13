using System;
using UnityEngine;

namespace NotificationSquad
{
    [Serializable]
    public abstract class NotificationMessege : MonoBehaviour
    {
        protected NotificationMessege(int description, int userId, int title,
            float arivalTime,
            int response)
        {
            Description = description;
            UserId = userId;
            Title = title;
            ArivalTime = arivalTime;
            //Response = response;
        }

        [SerializeField]
        public int Description { get; private set; }

        [SerializeField]
        public int UserId { get; private set; }

        [SerializeField]
        public int Title { get; private set; }

        [SerializeField]
        public float ArivalTime { get; private set; }

        //[SerializeField] public int Response { get; set; }

        public abstract void Display();

        public abstract string GetTypeString();

        public override string ToString()
        {
            return string.Format(
                "desription:{0}, title ID: {1}, Arrival Time: {2}, Type:{4}",
                Description, Title, UserId, ArivalTime);
        }
    }
}