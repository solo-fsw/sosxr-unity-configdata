using UnityEngine;


namespace SOSXR.ConfigData
{
    /// <summary>
    ///     This is a wrapper for the ConfigData to ensure that the ConfigData is initialized and destroyed correctly.
    ///     The init and destroy are there to ensure the Json file is updated when the relevant values change.
    /// </summary>
    public class WrapConfigData : MonoBehaviour
    {
        [HideInInspector] public TestConfigData ConfigData;


        private void Awake()
        {
            ConfigData.Initialize();
        }


        private void OnEnable()
        {
            ConfigData.Subscribe(nameof(ConfigData.ShowAffordances), obj => Lemmeno());
        }


        private void Lemmeno()
        {
            Debug.LogFormat(this, "ShowAffordances changed to {0}", ConfigData.ShowAffordances);
        }


        private void OnDisable()
        {
            ConfigData.Unsubscribe(nameof(ConfigData.ShowAffordances), obj => Lemmeno());
        }


        private void OnDestroy()
        {
            ConfigData.Uninitialize();
        }
    }
}