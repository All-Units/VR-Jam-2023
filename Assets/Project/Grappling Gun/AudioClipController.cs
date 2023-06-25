using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioClipController : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _clips;
    private AudioSource _audioSource;
    [SerializeField]private float _maxInclusivePitchVariance;

    public void PlayClip()
    {
        var clip = _clips.GetRandom();

        _audioSource.clip = clip;
        _maxInclusivePitchVariance = .3f;
        _audioSource.pitch += Random.Range(-_maxInclusivePitchVariance, _maxInclusivePitchVariance);
        _audioSource.Play();
    }

    private void OnValidate()
    {
        _audioSource = GetComponent<AudioSource>();
    }
}
