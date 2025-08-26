#if USE_ARBOR
using Arbor;
using UnityEngine;
using VContainer;

namespace MornSave
{
    public class MornSaveFlagRegisterState : StateBehaviour
    {
        [Inject] private MornSaveFlagService _service;
        [SerializeField] private MornSaveFlag _saveFlag;

        public override void OnStateBegin()
        {
            _service.RegisterFlag(_saveFlag);
        }
    }
}
#endif