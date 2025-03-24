using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CheatAttempt : MonoBehaviour
{
    public PaperSlider paperSlider;
    public TeacherAI teacher;
    public float cheatTime = 1.5f;
    
    [Header("UI Elements")]
    public GameObject examPaperPanel;
    public Slider progressBar;

    private bool isCheating = false;
    private bool isPaused = false;
    private float cheatProgress = 0f;

    public bool IsCheating => isCheating;
    public bool IsPaused => isPaused;

    void Update()
    {
        if(paperSlider.isPulledUp)
        {
            if (!teacher.IsFacingPlayer && !isCheating)
            {
                StartCheating();
            }
        }
        // Only progress the cheat if we're not paused.
        if (isCheating && !isPaused)
        {
            cheatProgress += Time.deltaTime;
            progressBar.value = cheatProgress / cheatTime;

            if (teacher.IsFacingPlayer)
            {
                FailCheatAttempt();
                return;
            }

            if (cheatProgress >= cheatTime)
            {
                FinishCheating();
            }
        }
    }

    void StartCheating()
    {
        isCheating = true;
        isPaused = false;
        cheatProgress = 0f;
        examPaperPanel.SetActive(true);
        progressBar.value = 0f;
        Debug.Log("Cheating started...");
    }

    void FinishCheating()
    {
        isCheating = false;
        examPaperPanel.SetActive(false);
        Debug.Log("Cheated successfully!");
    }

    public void FailCheatAttempt()
    {
        isCheating = false;
        examPaperPanel.SetActive(false);
        Debug.Log("Failed! Teacher caught you!");
    }

    // When the paper is pulled down, pause the cheat progress.
    public void PauseCheating()
    {
        if (isCheating && !isPaused)
        {
            isPaused = true;
            Debug.Log("Cheating paused.");
        }
    }

    // Resume cheating when the paper is pulled up again.
    public void ResumeCheating()
    {
        if (isCheating && isPaused)
        {
            isPaused = false;
            Debug.Log("Cheating resumed.");
        }
    }

    // Optionally, you might still want a way to completely stop cheating.
    public void StopCheating()
    {
        isCheating = false;
        examPaperPanel.SetActive(false);
        Debug.Log("Cheating stopped.");
    }
}
