#if UNITY_EDITOR
using MornEnum;
using UnityEditor;

namespace MornSave
{
    [CustomPropertyDrawer(typeof(MornSaveFlag))]
    public class MornSaveFlagDrawer : MornEnumDrawerBase
    {
        protected override string[] Values => MornSaveGlobal.I.SaveFlagNames;
    }
}
#endif