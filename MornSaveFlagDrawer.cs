#if UNITY_EDITOR
using MornEnum;
using UnityEditor;
using UnityEngine;

namespace MornSave
{
    [CustomPropertyDrawer(typeof(MornSaveFlag))]
    public class MornSaveFlagDrawer : MornEnumDrawerBase
    {
        protected override string[] Values => MornSaveGlobal.I.SaveFlagNames;
        protected override Object PingTarget => MornSaveGlobal.I;
    }
}
#endif