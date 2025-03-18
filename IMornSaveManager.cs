using System;

namespace MornSave
{
    public interface IMornSaveManager
    {
        IObservable<bool> OnLoadBool(MornSaveFlag key);
        IObservable<int> OnLoadInt(MornSaveFlag key);
        IObservable<float> OnLoadFloat(MornSaveFlag key);
        IObservable<string> OnLoadString(MornSaveFlag key);
        bool LoadBool(MornSaveFlag key);
        int LoadInt(MornSaveFlag key);
        float LoadFloat(MornSaveFlag key);
        string LoadString(MornSaveFlag key);
        void SaveBool(MornSaveFlag key, bool value);
        void SaveInt(MornSaveFlag key, int value);
        void SaveFloat(MornSaveFlag key, float value);
        void SaveString(MornSaveFlag key, string value);

        void Reset();
        void Save();
    }
}