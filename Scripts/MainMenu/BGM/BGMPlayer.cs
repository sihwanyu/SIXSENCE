using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BGMPlayer : MonoBehaviour
{
    public AudioClip bgmClip;

    void Awake()
{
    if (FindObjectsOfType<BGMPlayer>().Length > 1)
    {
        Destroy(gameObject); // 자신 제거
        return;
    }

    DontDestroyOnLoad(gameObject);
}


    void Start()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = bgmClip;
        audioSource.loop = true;
        audioSource.playOnAwake = false;
        audioSource.Play();
    }
}

