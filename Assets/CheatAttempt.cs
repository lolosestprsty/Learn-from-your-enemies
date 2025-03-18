using UnityEngine;
using TMPro; // Correct namespace for TextMeshPro
using UnityEngine.UI; // Needed for Slider

public class CheatAttempt : MonoBehaviour
{
    public TeacherAI teacher;
    public float cheatTime = 1.5f;
    
    [Header("UI Elements")]
    public GameObject examPaperPanel; // Renamed to single reference
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI[] answerTexts;
    public Slider progressBar;

    private bool isCheating = false;
    private float cheatProgress = 0f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!teacher.IsFacingPlayer && !isCheating)
            {
                StartCheating();
            }
        }

        if (isCheating)
        {
            // Update progress bar
            cheatProgress += Time.deltaTime;
            progressBar.value = cheatProgress / cheatTime;

            // Check if teacher looks at player while cheating
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
        // Add success logic
    }

    public void FailCheatAttempt()
    {
        isCheating = false;
        examPaperPanel.SetActive(false);
        Debug.Log("Failed! Teacher caught you!");
        // Add failure logic
    }
}