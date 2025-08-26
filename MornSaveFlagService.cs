using System;

namespace MornSave
{
    public sealed class MornSaveFlagService
    {
        private readonly Func<MornSaveFlag, bool> _hasFlagFunc;
        private readonly Action<MornSaveFlag> _registerCallback;

        public MornSaveFlagService(Func<MornSaveFlag, bool> hasFlagFunc, Action<MornSaveFlag> registerCallback)
        {
            _hasFlagFunc = hasFlagFunc;
            _registerCallback = registerCallback;
        }

        internal bool HasFlag(MornSaveFlag flag)
        {
            return _hasFlagFunc(flag);
        }

        internal void RegisterFlag(MornSaveFlag flag)
        {
            _registerCallback(flag);
        }
    }
}