using UnityEngine;
using UnityEngine.Events;


public class ChangeConfigDataAtRuntime : MonoBehaviour
{
    public ConfigDataHandler m_configDataHandler;

    [SerializeField] private UnityEvent ConfigInformationChanged;


    private void Awake()
    {
        m_configDataHandler.LoadConfigFromJson();
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
        if (participantNumber != -1)
        {
            m_configDataHandler.ConfigData.PPN = participantNumber;
        }

        if (videoClipName != null)
        {
            m_configDataHandler.ConfigData.VideoName = videoClipName;
        }

        ConfigInformationChanged?.Invoke();
    }
}