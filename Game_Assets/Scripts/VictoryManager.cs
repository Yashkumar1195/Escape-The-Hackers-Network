using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace EscapeTheHackerNetwork
{
    public class VictoryManager : MonoBehaviour
    {
        [Header("Screen 4 Canvas")]
        [SerializeField] private GameObject victoryCanvas;

        [Header("UI Fields")]
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI accuracyText;
        [SerializeField] private TextMeshProUGUI gradeText;
        [SerializeField] private TextMeshProUGUI statusBannerText;
        [SerializeField] private Image achievementBadgeImage;

        [Header("Door / World Unlock Trigger")]
        [SerializeField] private SpriteRenderer blastDoorRenderer;
        [SerializeField] private Sprite unlockedDoorSprite;
        [SerializeField] private GameObject passwordFortressTrigger;

        [Header("Player Movement Control")]
        [SerializeField] private PlayerController2D playerController;

        public void DisplayVictory(int finalScore, int correctCount, int totalEmails, float firewallIntegrity)
        {
            if (victoryCanvas != null)
                victoryCanvas.SetActive(true);

            float accuracy = (float)correctCount / totalEmails * 100f;

            if (scoreText != null)
                scoreText.text = $"Total Score: {finalScore} XP";

            if (accuracyText != null)
                accuracyText.text = $"Accuracy: {accuracy:F0}% ({correctCount}/{totalEmails} Threat Vectors Identified)";

            string grade = accuracy >= 80f ? "RANK: S (CYBER ELITE)" : accuracy >= 60f ? "RANK: A (DEFENDER)" : "RANK: B (RE-AUDIT NEEDED)";
            if (gradeText != null)
                gradeText.text = grade;

            if (statusBannerText != null)
                statusBannerText.text = firewallIntegrity > 0 ? "FIREWALL PERIMETER: RESTORED" : "FIREWALL CRITICAL: BREACH MITIGATED";

            // Unlock the blast door in the room
            if (blastDoorRenderer != null && unlockedDoorSprite != null)
            {
                blastDoorRenderer.sprite = unlockedDoorSprite;
            }

            if (passwordFortressTrigger != null)
            {
                passwordFortressTrigger.SetActive(true);
            }

            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlaySuccessChime();
            }
        }

        public void ContinueToExploreRoom()
        {
            if (victoryCanvas != null)
                victoryCanvas.SetActive(false);

            if (playerController != null)
                playerController.SetMovementLocked(false);
        }
    }
}
