using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource[] bgm;
    public AudioSource[] BGM { get { return bgm; } }

    [SerializeField]
    private AudioSource[] sfx;
    public AudioSource[] SFX { get { return sfx; } }

    [SerializeField]
    public AudioMixer audioMixer;

    public static AudioManager instance;

    void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StopAllBGM()
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            bgm[i].Stop();
        }
    }
    public void PlayBGM(int i)
    {
        StopAllBGM();

        if (i < bgm.Length)
        {
            bgm[i].Play();
        }
    }
    public void PlaySFX(int i)
    {
        if (i < sfx.Length)
        {
            sfx[i].PlayOneShot(sfx[i].clip);
        }
    }
    public float LoadCurrentMasterVolumn()
    {
        return PlayerPrefs.GetFloat("master", 0f);
    }
    public void AdjustMasterVolumn(float amount)
    {
        audioMixer.SetFloat("master", amount);
        PlayerPrefs.SetFloat("master", amount);
        PlayerPrefs.Save();
    }
}
