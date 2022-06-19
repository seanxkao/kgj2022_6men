using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class DialogSystem : MonoBehaviour
{
    [Serializable]
    public class ScenarioCommand
    {
        [Serializable]
        public class Next
        {
            public string Condition;
            public string Id;
        }

        public string Id;
        public PlayableDirector Playable;
        public Next[] Nexts;
    }

    // This system
    [Serializable]
    public class OptionCommand
    {
        public class Option
        {
            public string Text;
            public string Eval;
        }

        public Option[] Options;
    }
}
