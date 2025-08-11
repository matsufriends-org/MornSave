using System;
using System.Collections.Generic;
using MornGlobal;
using UnityEngine;

namespace MornSave
{
    [CreateAssetMenu(fileName = nameof(MornSaveGlobal), menuName = "Morn/" + nameof(MornSaveGlobal))]
    internal sealed class MornSaveGlobal : MornGlobalBase<MornSaveGlobal>
    {
        [Serializable]
        private struct DefaultValueInfo<T>
        {
            public MornSaveFlag Flag;
            public T DefaultValue;
        }

        [SerializeField] private string[] _saveFlags;
        [SerializeField] private List<DefaultValueInfo<int>> _defaultInts;
        [SerializeField] private List<DefaultValueInfo<bool>> _defaultBools;
        [SerializeField] private List<DefaultValueInfo<float>> _defaultFloats;
        [SerializeField] private List<DefaultValueInfo<string>> _defaultStrings;
        [Header("Save設定")]
        public string CorePlayerPrefsKey = "SaveData"; 
        public string CoreFileName = "SaveData"; 
        public string CoreExtensionName = ".sav"; 
        public string SaveDir = "Shared"; 
        public string[] SaveFlagNames => _saveFlags;
        protected override string ModuleName => nameof(MornSave);

        private bool TryGetDefault<T>(List<DefaultValueInfo<T>> list, MornSaveFlag flag, out T defaultValue)
        {
            foreach (var info in list)
            {
                if (info.Flag == flag)
                {
                    defaultValue = info.DefaultValue;
                    return true;
                }
            }

            defaultValue = default;
            return false;
        }

        public bool TryGetDefaultBool(MornSaveFlag flag, out bool defaultValue)
        {
            return TryGetDefault(_defaultBools, flag, out defaultValue);
        }

        public bool TryGetDefaultInt(MornSaveFlag flag, out int defaultValue)
        {
            return TryGetDefault(_defaultInts, flag, out defaultValue);
        }

        public bool TryGetDefaultFloat(MornSaveFlag flag, out float defaultValue)
        {
            return TryGetDefault(_defaultFloats, flag, out defaultValue);
        }

        public bool TryGetDefaultString(MornSaveFlag flag, out string defaultValue)
        {
            return TryGetDefault(_defaultStrings, flag, out defaultValue);
        }

        public static void Log(string message)
        {
            I.LogInternal(message);
        }

        public static void LogWarning(string message)
        {
            I.LogWarningInternal(message);
        }

        public static void LogError(string message)
        {
            I.LogErrorInternal(message);
        }
    }
}