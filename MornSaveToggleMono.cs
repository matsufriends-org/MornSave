using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace MornSave
{
    [RequireComponent(typeof(Toggle))]
    public sealed class MornSaveToggleMono : MonoBehaviour
    {
        [SerializeField] private Toggle _toggle;
        [SerializeField] private MornSaveBoolSo _saveSo;
        [Inject] private IMornSaveManager _save;
        private bool _selfChangeLock;

        private void Awake()
        {
            ApplyValue(_saveSo.Load(_save));
            _saveSo.AsObservable(_save).Where(x => _selfChangeLock == false).Subscribe(ApplyValue)
                   .AddTo(this);
            _toggle.OnValueChangedAsObservable().Subscribe(
                x =>
                {
                    _selfChangeLock = true;
                    _saveSo.Save(_save.SaveBool, x);
                    _selfChangeLock = false;
                }).AddTo(this);
        }

        private void Reset()
        {
            _toggle = GetComponent<Toggle>();
        }

        private void ApplyValue(bool value)
        {
            _toggle.isOn = value;
        }
    }
}