using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Knotgames.UI {
    public interface IMenuButton
    {
        void Selecte();
        void Deselect();
        void Click();
        void SetOnClick(UnityAction call);
        void SetIndex(int index);
    }
}
