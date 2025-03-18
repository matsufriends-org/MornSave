using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace MornSave
{
    [RequireComponent(typeof(Slider))]
    public sealed class MornSaveSliderMono : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private MornSaveFloatSo _saveSo;
        [Inject] private IMornSaveManager _iMornSave;
        private bool _selfChangeLock;

        private void Awake()
        {
            ApplyValue(_saveSo.Load(_iMornSave.LoadFloat));
            _saveSo.AsObservable(_iMornSave.OnLoadFloat).Where(x => _selfChangeLock == false).Subscribe(ApplyValue)
                   .AddTo(this);
            _slider.OnValueChangedAsObservable().Subscribe(
                x =>
                {
                    _selfChangeLock = true;
                    _saveSo.Save(_iMornSave.SaveFloat, x);
                    _selfChangeLock = false;
                }).AddTo(this);
        }

        private void Reset()
        {
            _slider = GetComponent<Slider>();
        }

        private void ApplyValue(float value)
        {
            _slider.value = value;
        }
    }
}