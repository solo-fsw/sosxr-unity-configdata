using UnityEngine;


namespace SOSXR.ConfigData
{
    /// <summary>
    ///     This demo is maybe a little specific to be in the ConfigData package, but it's a good example of how to use the
    ///     ConfigData in research tasks.
    /// </summary>
    public class BuildQueryURL : MonoBehaviour
    {
        [SerializeField] private DemoConfigData m_demoConfigData;


        private void OnEnable()
        {
            HandleConfigData.OnConfigDataChanged += BuildQuery;
        }


        [ContextMenu(nameof(BuildQuery))]
        public void BuildQuery()
        {
            if (m_demoConfigData == null)
            {
                Debug.LogError("DemoConfigData not assigned.");

                return;
            }

            m_demoConfigData.QueryStringURL = m_demoConfigData.BuildQueryStringURL(m_demoConfigData.BaseURL, m_demoConfigData.TaskName, m_demoConfigData.VideoName, m_demoConfigData.PPN);
        }


        private void OnDisable()
        {
            HandleConfigData.OnConfigDataChanged -= BuildQuery;
        }
    }
}