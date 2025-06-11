using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance { get; private set; }

    [Header("SFX Clips")]
    public AudioClip playerShootClip;
    public AudioClip playerDamageClip;
    public AudioClip enemyDamageClip;
    public AudioClip playerDeathClip;

    private AudioSource _audioSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        _audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void PlayPlayerShoot()
    {
        PlayClip(playerShootClip);
    }

    public void PlayPlayerDamage()
    {
        PlayClip(playerDamageClip);
    }

    public void PlayEnemyDamage()
    {
        PlayClip(enemyDamageClip);
    }

    private void PlayClip(AudioClip clip)
    {
        if (clip != null)
        {
            _audioSource.PlayOneShot(clip);
        }
    }
    public void PlayPlayerDeath()
{
    PlayClip(playerDeathClip);
}

}
