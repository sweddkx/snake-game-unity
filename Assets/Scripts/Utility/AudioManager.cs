using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip backgroundMusic;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    private void Start()
    {
        if (backgroundMusic && musicSource)
        {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
        
        // Load saved volume
        float savedVolume = PlayerPrefs.GetFloat("Volume", 1f);
        SetVolume(savedVolume);
    }
    
    public void SetVolume(float volume)
    {
        AudioListener.volume = Mathf.Clamp01(volume);
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.Save();
    }
    
    public float GetVolume()
    {
        return AudioListener.volume;
    }
}