#region

using TMPro;
using UnityEngine;

#endregion

namespace RGame.ScriptableCoreKit.Example
{
    public class ShowStatUI : MonoBehaviour
    {
        public CommonStatRuntimeSO ValueSO;

        public string ValueName;

        public bool IsMaxValue = true;

        private TextMeshProUGUI mText;

        private void Awake()
        {
            mText = GetComponent<TextMeshProUGUI>();
        }

        private void Start()
        {
            UpdateUI(ValueSO.GetValue(ValueName), ValueSO.GetMaxValue(ValueName));
        }

        private void OnEnable()
        {
            ValueSO.AddAction(ValueName, UpdateUI);
        }

        private void OnDisable()
        {
            ValueSO.RemoveAction(ValueName, UpdateUI);
        }

        private void UpdateUI(int _value, int _maxValue)
        {
            if (IsMaxValue)
                mText.text = $"{ValueName}: {_value}/{_maxValue}";
            else
                mText.text = $"{ValueName}: {_value}";
        }
    }
}