using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioClipController : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _clips;
    private AudioSource _audioSource;
    [SerializeField]private float _maxInclusivePitchVariance;
    private float initialPitch;

    public bool playOnAwake = false;

    private void Awake()
    {
        initialPitch = _audioSource.pitch;
        if (playOnAwake)
        {
            PlayClip();
        }
    }

    public void PlayClip()
    {
        var clip = _clips.GetRandom();

        _audioSource.clip = clip;
        _audioSource.pitch = initialPitch + Random.Range(-_maxInclusivePitchVariance, _maxInclusivePitchVariance);
        _audioSource.Play();
    }

    private void OnValidate()
    {
        _audioSource = GetComponent<AudioSource>();
    }
}
