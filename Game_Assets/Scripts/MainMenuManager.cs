using UnityEngine;
using UnityEngine.SceneManagement;

namespace EscapeTheHackerNetwork
{
    public class MainMenuManager : MonoBehaviour
    {
        [Header("Scene Loading")]
        [SerializeField] private string gameSceneName = "CyberRoomScene";

        [Header("Panels")]
        [SerializeField] private GameObject settingsPanel;
        [SerializeField] private GameObject missionBriefingPanel;

        public void StartMission()
        {
            if (AudioManager.Instance != null)
                AudioManager.Instance.PlayTerminalBoot();

            if (!string.IsNullOrEmpty(gameSceneName))
            {
                SceneManager.LoadScene(gameSceneName);
            }
        }

        public void OpenSettings()
        {
            if (settingsPanel != null)
                settingsPanel.SetActive(true);
        }

        public void CloseSettings()
        {
            if (settingsPanel != null)
                settingsPanel.SetActive(false);
        }

        public void OpenBriefing()
        {
            if (missionBriefingPanel != null)
                missionBriefingPanel.SetActive(true);
        }

        public void CloseBriefing()
        {
            if (missionBriefingPanel != null)
                missionBriefingPanel.SetActive(false);
        }

        public void QuitGame()
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}
