namespace Participant
{
    public class ParticipantData
    {
        public int Id { get; set; }

        public bool Adaptive
        {
            get { return Group == 1 || Group == 2; }
        }

        public bool Transparent
        {
            get { return Group == 1; }
        }

        public int Group { get; set; }
        public string ProctorName { get; set; }
    }
}