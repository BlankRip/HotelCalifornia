using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Knotgames.Inputs {
    public interface IUi {
        void SwitchToUiInputs();
        void SwitchToGamePlayInputs();
        void SetOnEscapeEvent(UnityAction call);
    }
}
