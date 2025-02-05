using UnityEngine;


namespace SOSXR.ConfigData.Samples
{
    /// <summary>
    ///     This demo is maybe a little specific to be in the ConfigData package, but it's a good example of how to use the
    ///     ConfigData in research tasks.
    /// </summary>
    public class BuildQueryURL : MonoBehaviour
    {
        [SerializeField] private DemoConfigData m_configData;


        private void OnEnable()
        {
            HandleConfigData.OnConfigDataChanged += BuildQuery;
        }


        [ContextMenu(nameof(BuildQuery))]
        public void BuildQuery()
        {
            if (m_configData == null)
            {
                Debug.LogError("ConfigData not assigned.");

                return;
            }

            m_configData.QueryStringURL = m_configData.BuildQueryStringURL(m_configData.BaseURL, m_configData.TaskName, m_configData.VideoName, m_configData.PPN);
        }


        private void OnDisable()
        {
            HandleConfigData.OnConfigDataChanged -= BuildQuery;
        }
    }
}