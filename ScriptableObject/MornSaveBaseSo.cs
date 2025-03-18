using System;
using System.Runtime.CompilerServices;
using UnityEngine;

[assembly: InternalsVisibleTo("MornSave.Editor")]
namespace MornSave
{
    public abstract class MornSaveBaseSo<T> : ScriptableObject
    {
        [SerializeField] private MornSaveFlag _key;
        public IObservable<T> AsObservable(Func<MornSaveFlag, IObservable<T>> func) => func(_key);

        public T Load(Func<MornSaveFlag, T> loadFunc)
        {
            return loadFunc(_key);
        }

        public void Save(Action<MornSaveFlag, T> onSave, T value)
        {
            onSave(_key, value);
        }
    }
}