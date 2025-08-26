#if USE_ARBOR
using Arbor;
using UnityEngine;
using VContainer;

namespace MornSave
{
    public class MornSaveFlagBranchState : StateBehaviour
    {
        [Inject] private MornSaveFlagService _service;
        [SerializeField] private MornSaveFlag _saveFlag;
        [SerializeField] private StateLink _successState;
        [SerializeField] private StateLink _failedState;

        public override void OnStateBegin()
        {
            if (_service.HasFlag(_saveFlag))
            {
                Transition(_successState);
            }
            else
            {
                Transition(_failedState);
            }
        }
    }
}
#endif