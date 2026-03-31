using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class EmotionController : MonoBehaviour
{
    [Header("Assign in Inspector")]
    public Light moodLight;
    public ParticleSystem particles;
    public TextMeshProUGUI emotionText;
    public BreathingGuide breathingGuide;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip calmMusic;
    public AudioClip anxiousMusic;
    public AudioClip focusedMusic;

    [Header("Skybox")]
    public Material calmSky;
    public Material anxiousSky;
    public Material focusedSky;

    [Header("Emotion Colors")]
    public Color calmColor    = new Color(0.4f, 0.7f, 1.0f);
    public Color anxiousColor = new Color(1.0f, 0.3f, 0.3f);
    public Color focusedColor = new Color(0.4f, 1.0f, 0.6f);

    private Color targetColor;
    private float targetIntensity = 1f;

    void Start()
    {
        targetColor = calmColor;
        if (moodLight != null)
            moodLight.color = calmColor;
        if (emotionText != null)
            emotionText.text = "[X] CALM   [O] ANXIOUS   [T] FOCUSED";
        if (calmSky != null)
            RenderSettings.skybox = calmSky;

        PlayMusic(calmMusic);
    }

    void Update()
    {
        var gamepad = Gamepad.current;
        if (gamepad != null)
        {
            if (gamepad.buttonSouth.wasPressedThisFrame)
                SetEmotion(calmColor, 1f, 5f, "[X] CALM   [O] ANXIOUS   [T] FOCUSED", calmMusic, calmSky, "calm");

            if (gamepad.buttonEast.wasPressedThisFrame)
                SetEmotion(anxiousColor, 2f, 60f, "[X] CALM   [O] ANXIOUS   [T] FOCUSED", anxiousMusic, anxiousSky, "anxious");

            if (gamepad.buttonNorth.wasPressedThisFrame)
                SetEmotion(focusedColor, 1.5f, 25f, "[X] CALM   [O] ANXIOUS   [T] FOCUSED", focusedMusic, focusedSky, "focused");
        }

        if (moodLight != null)
        {
            moodLight.color     = Color.Lerp(moodLight.color, targetColor, Time.deltaTime * 2f);
            moodLight.intensity = Mathf.Lerp(moodLight.intensity, targetIntensity, Time.deltaTime * 2f);
        }
    }

    void SetEmotion(Color color, float intensity, float particleRate, string label, AudioClip clip, Material sky, string emotionKey)
    {
        targetColor     = color;
        targetIntensity = intensity;

        if (emotionText != null)
        {
            emotionText.text  = label;
            emotionText.color = color;
        }

        if (particles != null)
        {
            var emission          = particles.emission;
            emission.rateOverTime = particleRate;
            var main              = particles.main;
            main.startColor       = color;
        }

        if (sky != null)
            RenderSettings.skybox = sky;

        if (breathingGuide != null)
            breathingGuide.SetPattern(emotionKey);

        PlayMusic(clip);
    }

    void PlayMusic(AudioClip clip)
    {
        if (audioSource == null || clip == null) return;
        if (audioSource.clip == clip) return;
        audioSource.clip = clip;
        audioSource.Play();
    }
}