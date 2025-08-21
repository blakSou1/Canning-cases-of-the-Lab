#region

using UnityEngine;
using UnityEngine.UI;

#endregion

namespace RGame.ScriptableCoreKit.Example
{
    public class BtnModify : MonoBehaviour
    {
        public CommonStatRuntimeSO ValueSO;

        public string ValueName;
        public bool IsModifyMaxValue;
        public int ModifyValue;

        private Button mButton;

        private void Start()
        {
            mButton = GetComponent<Button>();

            mButton.onClick.AddListener(() =>
            {
                ValueSO.ModifyValue(ValueName, ModifyValue, IsModifyMaxValue);
            });
        }
    }
}