#region

using TMPro;
using UnityEngine;
using UnityEngine.UI;

#endregion

namespace RGame.ScriptableCoreKit.Example
{
    public class BtnModifyReference : MonoBehaviour
    {
        public CommonStatRuntimeSO ValueSO;

        public string ValueName;
        public bool IsMaxValue;
        public StatModifyType ModifyType;
        public int ModifyValue;

        private Button mButton;

        private bool mIsUse;

        private ModifyReference mModifyReference;
        private TextMeshProUGUI mText;

        private void Start()
        {
            mButton = GetComponent<Button>();
            mText = GetComponentInChildren<TextMeshProUGUI>();

            mButton.onClick.AddListener(() =>
            {
                if (mIsUse)
                {
                    ValueSO.RemoveReferenceModifyValue(ValueName, mModifyReference);

                    mText.text = mText.text.Replace("Equipped", "Unequipped");

                    mIsUse = false;
                }
                else
                {
                    mModifyReference = ValueSO.ReferenceModifyValue(ValueName, ModifyType, ModifyValue);

                    mText.text = mText.text.Replace("Unequipped", "Equipped");

                    mIsUse = true;
                }
            });
        }
    }
}