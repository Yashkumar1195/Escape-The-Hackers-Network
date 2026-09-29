using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace EscapeTheHackerNetwork
{
    public class PhishingGameManager : MonoBehaviour
    {
        [Header("Email Database")]
        [SerializeField] private List<EmailData> emailList = new List<EmailData>();

        [Header("Active Email UI")]
        [SerializeField] private TextMeshProUGUI senderText;
        [SerializeField] private TextMeshProUGUI subjectText;
        [SerializeField] private TextMeshProUGUI bodyText;
        [SerializeField] private TextMeshProUGUI previewLinkText;
        [SerializeField] private GameObject attachmentObject;
        [SerializeField] private TextMeshProUGUI attachmentText;

        [Header("Stats & HUD")]
        [SerializeField] private TextMeshProUGUI xpText;
        [SerializeField] private TextMeshProUGUI progressText;
        [SerializeField] private Slider firewallIntegritySlider;

        [Header("AIRI Feedback Panel")]
        [SerializeField] private GameObject airiFeedbackPanel;
        [SerializeField] private TextMeshProUGUI airiDialogueText;
        [SerializeField] private Image airiPortraitImage;
        [SerializeField] private Sprite airiHappySprite;
        [SerializeField] private Sprite airiWarningSprite;

        [Header("Screens & Managers")]
        [SerializeField] private GameObject terminalCanvas;
        [SerializeField] private VictoryManager victoryManager; // Screen 4

        private int currentIndex = 0;
        private int currentScore = 0;
        private int correctCount = 0;
        private float firewallHealth = 100f;

        private void Start()
        {
            if (emailList == null || emailList.Count == 0)
            {
                PopulateDefaultEmailDataset();
            }

            UpdateStatsUI();
            LoadEmail(0);
        }

        public void LoadEmail(int index)
        {
            if (index < 0 || index >= emailList.Count) return;

            currentIndex = index;
            EmailData data = emailList[currentIndex];

            if (senderText != null)
                senderText.text = $"<b>From:</b> {data.senderDisplayName} &lt;{data.senderAddress}&gt;";

            if (subjectText != null)
                subjectText.text = $"<b>Subject:</b> {data.subject}";

            if (bodyText != null)
                bodyText.text = data.bodyText;

            if (previewLinkText != null)
            {
                previewLinkText.text = string.IsNullOrEmpty(data.previewLink) 
                    ? "None" 
                    : $"<b>Target URL:</b> {data.previewLink}";
            }

            if (attachmentObject != null)
            {
                attachmentObject.SetActive(data.hasAttachment);
                if (attachmentText != null)
                    attachmentText.text = data.attachmentName;
            }

            if (progressText != null)
                progressText.text = $"Email {currentIndex + 1} / {emailList.Count}";

            if (airiFeedbackPanel != null)
                airiFeedbackPanel.SetActive(false);
        }

        public void OnUserDecision(bool chosenAsPhishing)
        {
            EmailData current = emailList[currentIndex];
            bool isCorrect = (chosenAsPhishing == current.isPhishing);

            if (isCorrect)
            {
                correctCount++;
                currentScore += 100;
                if (AudioManager.Instance != null)
                    AudioManager.Instance.PlaySuccessChime();

                ShowAiriFeedback(true, $"Correct! {current.educationalExplanation}");
            }
            else
            {
                firewallHealth = Mathf.Max(0, firewallHealth - 25f);
                if (AudioManager.Instance != null)
                    AudioManager.Instance.PlayAlertSiren();

                ShowAiriFeedback(false, $"Warning! Security breach risk! {current.educationalExplanation}");
            }

            UpdateStatsUI();
        }

        public void NextEmail()
        {
            if (currentIndex + 1 < emailList.Count)
            {
                LoadEmail(currentIndex + 1);
            }
            else
            {
                CompleteMinigame();
            }
        }

        private void ShowAiriFeedback(bool isPositive, string message)
        {
            if (airiFeedbackPanel != null)
            {
                airiFeedbackPanel.SetActive(true);
                if (airiDialogueText != null)
                    airiDialogueText.text = message;

                if (airiPortraitImage != null)
                {
                    airiPortraitImage.sprite = isPositive ? airiHappySprite : airiWarningSprite;
                }
            }
        }

        private void UpdateStatsUI()
        {
            if (xpText != null)
                xpText.text = $"XP: {currentScore}";

            if (firewallIntegritySlider != null)
                firewallIntegritySlider.value = firewallHealth / 100f;
        }

        private void CompleteMinigame()
        {
            if (terminalCanvas != null)
                terminalCanvas.SetActive(false);

            if (victoryManager != null)
            {
                victoryManager.DisplayVictory(currentScore, correctCount, emailList.Count, firewallHealth);
            }
        }

        private void PopulateDefaultEmailDataset()
        {
            emailList.Add(new EmailData
            {
                emailId = "EML-01",
                senderDisplayName = "Security Support Team",
                senderAddress = "security.support@micr0soft.co",
                subject = "URGENT: Suspicious Activity Detected - Take Action!",
                bodyText = "Dear User,\n\nUnusual login activity detected on your primary account. If this was not you, verify immediately within 15 minutes to avoid complete account termination.",
                previewLink = "https://micr0soft-login.net/verify-token?id=99281",
                attachmentName = "Account_Log.zip",
                hasAttachment = true,
                isPhishing = true,
                educationalExplanation = "Notice the spoofed domain 'micr0soft.co' with a zero, urgent deadline, and deceptive link!"
            });

            emailList.Add(new EmailData
            {
                emailId = "EML-02",
                senderDisplayName = "GitHub Security",
                senderAddress = "notifications@github.com",
                subject = "[GitHub] Dependabot alert for project repository",
                bodyText = "Dependabot detected a known low-severity vulnerability in one of your project manifests. Review the pull request when convenient.",
                previewLink = "https://github.com/my-pbl-project/security/dependabot/1",
                attachmentName = "",
                hasAttachment = false,
                isPhishing = false,
                educationalExplanation = "Official validated domain (github.com), no false urgency, and verified HTTPS link."
            });

            emailList.Add(new EmailData
            {
                emailId = "EML-03",
                senderDisplayName = "Corporate Payroll Dept",
                senderAddress = "billing@payroll-portal-update.xyz",
                subject = "Action Required: Update Bank Details for Month End",
                bodyText = "All employees must update their direct deposit information using the attached utility software before 5:00 PM today.",
                previewLink = "http://payroll-update.xyz/login.php",
                attachmentName = "Salary_Invoice_Updater.exe",
                hasAttachment = true,
                isPhishing = true,
                educationalExplanation = "Dangerous executable (.exe) attachment, unofficial .xyz domain, and generic employee greeting."
            });

            emailList.Add(new EmailData
            {
                emailId = "EML-04",
                senderDisplayName = "University IT Helpdesk",
                senderAddress = "helpdesk@university-portal.edu",
                subject = "Notice: Scheduled Campus Network Maintenance Saturday",
                bodyText = "Campus network infrastructure will be undergoing routine hardware maintenance this Saturday from 2:00 AM to 5:00 AM. No action required from students.",
                previewLink = "",
                hasAttachment = false,
                isPhishing = false,
                educationalExplanation = "Legitimate internal communication (.edu domain), no credentials or passwords requested."
            });

            emailList.Add(new EmailData
            {
                emailId = "EML-05",
                senderDisplayName = "Cloud Storage Support",
                senderAddress = "support@apple-id-recovery-center.info",
                subject = "Storage Capacity Exceeded - Account Flagged for Deletion",
                bodyText = "Your cloud drive is 99% full. All synced databases, code archives, and keys will be permanently deleted unless verified now.",
                previewLink = "https://apple-storage-renewal.info/auth?ref=alert",
                attachmentName = "Storage_Quota.pdf",
                hasAttachment = true,
                isPhishing = true,
                educationalExplanation = "Fear-inducing extortion message with a fake third-party domain mimicking official service."
            });
        }
    }
}
