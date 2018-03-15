using System;
using Mission;
using UnityEngine;

namespace NewSurveyArch
{
    public class ButtonNameEventArgs : System.EventArgs
    {
        public string OldName { get; set; }
        public string NewName { get; set; }
    }

    public class ButtonEventManager : MonoBehaviour
    {
        public static event EventHandler<StringEventArgs> Load;
        //public static event EventHandler<PhysiologicalDataEventArgs> EndSurvey;

        public static void OnLoad(string name)
        {
            var handler = Load;
            if (handler != null) handler(null, new StringEventArgs {StringArgs = name});
        }

        public static event EventHandler<StringEventArgs> End;
        //public static event EventHandler<PhysiologicalDataEventArgs> EndSurvey;

        public static void OnEnd(string name)
        {
            var handler = End;
            if (handler != null) handler(null, new StringEventArgs { StringArgs = name });
        }

        public static event EventHandler<ButtonNameEventArgs> ChangeText;
        //public static event EventHandler<PhysiologicalDataEventArgs> EndSurvey;

        public static void OnChangeText(string oldName, string newName)
        {
            var handler = ChangeText;
            if (handler != null) handler(null, new ButtonNameEventArgs { OldName = oldName,NewName = newName });
        }

       

    }
}
