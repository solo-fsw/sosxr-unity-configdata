using UnityEngine;


namespace SOSXR.ConfigData
{
    public class UpdateJsonOnValueChange : MonoBehaviour
    {
        [SerializeField] private BaseConfigData ConfigData;


        private void Awake()
        {
            foreach (var change in ConfigData.UpdateJsonOnValueChange)
            {
                ConfigData.Subscribe(change, obj => UpdateJson(change));
            }
        }


        private void UpdateJson(string propertyName)
        {
            HandleConfigData.UpdateConfigJson(ConfigData);
        }


        private void OnDestroy()
        {
            foreach (var change in ConfigData.UpdateJsonOnValueChange)
            {
                ConfigData.Unsubscribe(change, obj => UpdateJson(change));
            }
        }
    }
}