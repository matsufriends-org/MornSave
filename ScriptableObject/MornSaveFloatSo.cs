using System;
using UnityEngine;

namespace MornSave
{
    [CreateAssetMenu(fileName = nameof(MornSaveFloatSo), menuName = "MornSave/" + nameof(MornSaveFloatSo))]
    public sealed class MornSaveFloatSo : MornSaveBaseSo<float>
    {
        public IObservable<float> AsObservable(IMornSaveManager save) => AsObservable(save.OnLoadFloat);

        public float Load(IMornSaveManager save)
        {
            return base.Load(save.LoadFloat);
        }

        public void Save(IMornSaveManager save, float value)
        {
            base.Save(save.SaveFloat, value);
        }
    }
}