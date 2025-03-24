using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PaperSlider : MonoBehaviour
{
    [Header("References")]
    public TeacherAI teacher;           // Reference to check if teacher sees you.
    public CheatAttempt cheatAttempt;   // Reference to the cheat manager.
    public RectTransform paperRect;     // The RectTransform of the paper UI element.

    [Header("Positions & Timing")]
    public Vector2 onScreenPosition;    // Where the paper is fully pulled up.
    public Vector2 offScreenPosition;   // Where the paper is hidden (e.g., off the bottom).
    public float slideUpDuration = 1.5f;  // Duration for the smooth, dynamic pull-up.
    public float slideDownSpeed = 1000f;  // Speed for quickly sliding down.

    // Private state
    private float lerpTimer = 0f;
    private bool slidingUp = false;
    private bool slidingDown = false;
    public bool isPulledUp = false;    // Indicates if the paper is fully up.

    private void Update()
    {
        // Start pulling up if W is pressed and paper is not already up.
        if (Input.GetKeyDown(KeyCode.W) && !isPulledUp)
{
    // Resume cheat progress if it was paused.
    if (cheatAttempt != null && cheatAttempt.IsCheating && cheatAttempt.IsPaused)
    {
        cheatAttempt.ResumeCheating();
    }
    slidingUp = true;
    slidingDown = false;
    lerpTimer = 0f;
}
else if (Input.GetKeyDown(KeyCode.S))
{
    slidingDown = true;
    slidingUp = false;

    // Instead of stopping, pause the cheat attempt.
    if (cheatAttempt != null && cheatAttempt.IsCheating)
    {
        cheatAttempt.PauseCheating();
    }
}


        // Handle sliding up with Lerp (dynamic movement).
        if (slidingUp)
        {
            lerpTimer += Time.deltaTime;
            float t = Mathf.Clamp01(lerpTimer / slideUpDuration);
            paperRect.anchoredPosition = Vector2.Lerp(offScreenPosition, onScreenPosition, t);

            if (t >= 1f)
            {
                slidingUp = false;
                isPulledUp = true;
            }
        }
        // Handle sliding down quickly.
        if (slidingDown)
        {
            paperRect.anchoredPosition = Vector2.MoveTowards(
                paperRect.anchoredPosition,
                offScreenPosition,
                slideDownSpeed * Time.deltaTime
            );
            if (Vector2.Distance(paperRect.anchoredPosition, offScreenPosition) < 0.1f)
            {
                paperRect.anchoredPosition = offScreenPosition;
                slidingDown = false;
                isPulledUp = false;
            }
        }

        // If the paper is pulled up and the teacher sees you, catch you.
        if (isPulledUp && teacher != null && teacher.IsFacingPlayer)
        {
            Debug.Log("Teacher caught you with the paper pulled up!");
            // Call the failure logic in your cheat attempt manager.
            if (cheatAttempt != null)
            {
                cheatAttempt.FailCheatAttempt();
            }
        }
    }

    // Expose whether the paper is pulled up.
    public bool IsPulledUp => isPulledUp;
}
