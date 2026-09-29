using UnityEngine;
using TMPro;

namespace EscapeTheHackerNetwork
{
    public class TerminalInteractable : MonoBehaviour
    {
        [Header("UI Prompts")]
        [SerializeField] private GameObject interactionPromptUI; // "[E] Access Terminal"
        [SerializeField] private GameObject phishingTerminalCanvas; // Screen 3

        [Header("State")]
        private bool isPlayerInRange = false;
        private PlayerController2D cachedPlayer;

        private void Start()
        {
            if (interactionPromptUI != null)
                interactionPromptUI.SetActive(false);

            if (phishingTerminalCanvas != null)
                phishingTerminalCanvas.SetActive(false);
        }

        private void Update()
        {
            if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
            {
                OpenTerminal();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                isPlayerInRange = true;
                cachedPlayer = other.GetComponent<PlayerController2D>();
                if (interactionPromptUI != null)
                    interactionPromptUI.SetActive(true);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                isPlayerInRange = false;
                if (interactionPromptUI != null)
                    interactionPromptUI.SetActive(false);
            }
        }

        public void OpenTerminal()
        {
            if (interactionPromptUI != null)
                interactionPromptUI.SetActive(false);

            if (phishingTerminalCanvas != null)
                phishingTerminalCanvas.SetActive(true);

            if (cachedPlayer != null)
                cachedPlayer.SetMovementLocked(true);

            if (AudioManager.Instance != null)
                AudioManager.Instance.PlayTerminalBoot();
        }

        public void CloseTerminal()
        {
            if (phishingTerminalCanvas != null)
                phishingTerminalCanvas.SetActive(false);

            if (cachedPlayer != null)
                cachedPlayer.SetMovementLocked(false);
        }
    }
}
