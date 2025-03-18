using System;
using MornEnum;

namespace MornSave
{
    [Serializable]
    public class MornSaveFlag : MornEnumBase
    {
        protected override string[] Values => MornSaveGlobal.I.SaveFlagNames;
    }
}