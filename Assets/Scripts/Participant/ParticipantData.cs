namespace Participant
{
    public class ParticipantData
    {
        public int Id { get; set; }

        public bool Adaptive
        {
            get { return Group == 1 || Group == 3; }
        }

        public bool Transparent
        {
            get { return Group == 1 || Group == 2; }
        }

        public int Group { get; set; }
        public string ProctorName { get; set; }
    }
}