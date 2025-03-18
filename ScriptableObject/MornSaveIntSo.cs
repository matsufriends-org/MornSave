using System;
using UnityEngine;

namespace MornSave
{
    [CreateAssetMenu(fileName = nameof(MornSaveIntSo), menuName = "MornSave/" + nameof(MornSaveIntSo))]
    public sealed class MornSaveIntSo : MornSaveBaseSo<int>
    {
        public IObservable<int> AsObservable(IMornSaveManager save) => AsObservable(save.OnLoadInt);

        public int Load(IMornSaveManager save)
        {
            return base.Load(save.LoadInt);
        }

        public void Save(IMornSaveManager save, int value)
        {
            base.Save(save.SaveInt, value);
        }
    }
}