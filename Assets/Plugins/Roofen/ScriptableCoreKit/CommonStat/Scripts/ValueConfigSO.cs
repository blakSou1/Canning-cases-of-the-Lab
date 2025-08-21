#region

using System.Collections.Generic;
using UnityEngine;

#endregion

namespace RGame.ScriptableCoreKit
{
    /// <summary>
    ///     A list of all values
    ///     Each object that has a value needs to create a configure-all-values form.
    /// </summary>
    [CreateAssetMenu(menuName = "RGame/CommonStat/Value/Value Config")]
    public class ValueConfigSO : ScriptableObject
    {
        public List<SoleValue> ValueDefinitions;
    }
}