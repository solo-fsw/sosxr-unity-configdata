using UnityEngine;


public class DemoConfigDataChangeAtRuntime : MonoBehaviour
{
    [SerializeField] private DemoConfigData m_demoConfigData;


    private void Awake()
    {
        m_demoConfigData.DataHandler.LoadConfigFromJsonFile();
    }


    public void ChangeVideoClipName(string videoClipName)
    {
        AmendConfigData(videoClipName: videoClipName);
    }


    public void AddParticipantNumber(string participantNumber)
    {
        if (int.TryParse(participantNumber, out var result))
        {
            AmendConfigData(result);
        }
        else
        {
            Debug.LogErrorFormat("Participant number is not a number");
        }
    }


    public void AddParticipantNumber(int participantNumber)
    {
        AmendConfigData(participantNumber);
    }


    private void AmendConfigData(int participantNumber = -1, string videoClipName = null)
    {
        if (m_demoConfigData == null)
        {
            Debug.LogError("ConfigData is null or not of type DemoConfigData");

            return;
        }

        if (participantNumber != -1)
        {
            m_demoConfigData.PPN = participantNumber;

            Debug.Log("Participant number changed to " + participantNumber);
        }

        if (videoClipName != null)
        {
            m_demoConfigData.VideoName = videoClipName;

            Debug.Log("Video clip name changed to " + videoClipName);
        }

        m_demoConfigData.DataHandler.AmendConfigJsonFile();
    }
}