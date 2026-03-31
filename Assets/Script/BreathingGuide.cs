using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class BreathingGuide : MonoBehaviour
{
    [Header("UI")]
    public GameObject guidePanel;
    public RectTransform breathingCircle;
    public TextMeshProUGUI breathingText;
    public TextMeshProUGUI hintText;

    [Header("Circle Size")]
    public float minSize = 80f;
    public float maxSize = 180f;

    private float[] calmPattern    = { 4f, 4f, 6f, 2f };
    private float[] focusedPattern = { 4f, 4f, 4f, 4f };
    private float[] anxiousPattern = { 4f, 2f, 6f, 0f };
    private string[] phaseNames = { "Breathe In", "Hold", "Breathe Out", "Hold" };

    private float[] currentPattern = null; // null = chưa chọn emotion
    private string currentPatternName = "";
    private bool isActive = false;
    private int phase = 0;
    private float timer = 0f;

    void Start()
    {
        // Ẩn hết lúc đầu
        if (guidePanel != null)
            guidePanel.SetActive(false);
        if (hintText != null)
            hintText.gameObject.SetActive(false);
    }

    public void SetPattern(string emotion)
    {
        // Tắt vòng tròn ngay lập tức
        isActive = false;
        phase = 0;
        timer = 0f;
        if (guidePanel != null)
            guidePanel.SetActive(false);

        // Set pattern theo emotion
        switch (emotion)
        {
            case "calm":
                currentPattern = calmPattern;
                currentPatternName = "4-4-6-2";
                break;
            case "focused":
                currentPattern = focusedPattern;
                currentPatternName = "4-4-4-4";
                break;
            case "anxious":
                currentPattern = anxiousPattern;
                currentPatternName = "4-2-6-0";
                break;
        }

        // Hiện hint text với pattern mới
        if (hintText != null)
        {
            hintText.gameObject.SetActive(true);
            hintText.text = $"Press □ to start breathing ({currentPatternName})";
        }
    }

    void Update()
    {
        var gamepad = Gamepad.current;
        if (gamepad == null) return;

        // Chỉ cho phép bấm Square nếu đã chọn emotion
        if (gamepad.buttonWest.wasPressedThisFrame && currentPattern != null)
        {
            isActive = !isActive;

            if (isActive)
            {
                if (hintText != null)
                    hintText.gameObject.SetActive(false);
                if (guidePanel != null)
                    guidePanel.SetActive(true);
                phase = 0;
                timer = 0f;
            }
            else
            {
                if (guidePanel != null)
                    guidePanel.SetActive(false);
                if (hintText != null)
                {
                    hintText.gameObject.SetActive(true);
                    hintText.text = $"Press □ to start breathing ({currentPatternName})";
                }
            }
        }

        if (!isActive || currentPattern == null) return;

        timer += Time.deltaTime;
        float phaseDuration = currentPattern[phase];

        if (phaseDuration == 0f)
        {
            phase = (phase + 1) % 4;
            timer = 0f;
            return;
        }

        float progress = Mathf.Clamp01(timer / phaseDuration);

        float size = 0f;
        if (phase == 0)      size = Mathf.Lerp(minSize, maxSize, progress);
        else if (phase == 1) size = maxSize;
        else if (phase == 2) size = Mathf.Lerp(maxSize, minSize, progress);
        else                 size = minSize;

        if (breathingCircle != null)
            breathingCircle.sizeDelta = new Vector2(size, size);

        if (breathingText != null)
        {
            int secondsLeft = Mathf.CeilToInt(phaseDuration - timer);
            breathingText.text = $"{phaseNames[phase]}\n{secondsLeft}s";
        }

        if (timer >= phaseDuration)
        {
            phase = (phase + 1) % 4;
            timer = 0f;
        }
    }
}