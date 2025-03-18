using System;
using UnityEngine;

namespace MornSave
{
    [CreateAssetMenu(fileName = nameof(MornSaveBoolSo), menuName = "MornSave/" + nameof(MornSaveBoolSo))]
    public sealed class MornSaveBoolSo : MornSaveBaseSo<bool>
    {
        public IObservable<bool> AsObservable(IMornSaveManager save) => AsObservable(save.OnLoadBool);

        public bool Load(IMornSaveManager save)
        {
            return base.Load(save.LoadBool);
        }

        public void Save(IMornSaveManager save, bool value)
        {
            base.Save(save.SaveBool, value);
        }
    }
}