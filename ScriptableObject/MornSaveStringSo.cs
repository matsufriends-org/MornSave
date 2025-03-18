using System;
using UnityEngine;

namespace MornSave
{
    [CreateAssetMenu(fileName = nameof(MornSaveStringSo), menuName = "MornSave/" + nameof(MornSaveStringSo))]
    public sealed class MornSaveStringSo : MornSaveBaseSo<string>
    {
        public IObservable<string> AsObservable(IMornSaveManager save) => AsObservable(save.OnLoadString);

        public string Load(IMornSaveManager save)
        {
            return base.Load(save.LoadString);
        }

        public void Save(IMornSaveManager save, string value)
        {
            base.Save(save.SaveString, value);
        }
    }
}