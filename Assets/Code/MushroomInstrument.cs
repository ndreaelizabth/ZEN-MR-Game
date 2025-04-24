using UnityEngine;

public class MushroomInstrument : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private float minVolume = 0.1f;
    [SerializeField] private float maxVolume = 1.0f;
    [SerializeField] private float maxVelocity = 5.0f;
    [SerializeField] private float pitchVariation = 0.1f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource missing on " + gameObject.name);
        }
        else if (audioSource.clip == null)
        {
            Debug.LogError("Audio clip missing on AudioSource for " + gameObject.name);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Collision detected with {collision.gameObject.name}, Tag: {collision.gameObject.tag}, Velocity: {collision.relativeVelocity.magnitude}");

        if (!collision.gameObject.CompareTag("Controller"))
        {
            Debug.Log($"Ignoring collision with non-Controller: {collision.gameObject.name}");
            return;
        }

        float velocity = collision.relativeVelocity.magnitude;
        float volume = Mathf.Lerp(minVolume, maxVolume, velocity / maxVelocity);
        volume = Mathf.Clamp(volume, minVolume, maxVolume);

        audioSource.pitch = 1.0f + Random.Range(-pitchVariation, pitchVariation);
        audioSource.volume = volume;
        audioSource.Play();

        Debug.Log($"Playing sound: Volume={volume}, Pitch={audioSource.pitch}, Velocity={velocity}");
    }
}