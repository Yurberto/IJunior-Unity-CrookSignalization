using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AlarmSystem : MonoBehaviour
{
    [SerializeField] private AlarmTrigger _trigger;
    [SerializeField, Range(0, 1)] private float _volumeChangingSpeed = 0.016f;
    [SerializeField, Range(0, 1)] float _smoothness = 0.4f;

    private AudioSource _audioSource;
    private Coroutine _volumeCoroutine;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = 0;
    }

    private void OnEnable()
    {
        _trigger.CrookEnteredHouse += StartEnterCoroutine;
        _trigger.CrookEnteredHouse += PlaySound;
        _trigger.CrookLeftHouse += StartLeftCoroutine;
    }

    private void OnDisable()
    {
        _trigger.CrookEnteredHouse -= StartEnterCoroutine;
        _trigger.CrookEnteredHouse -= PlaySound;
        _trigger.CrookLeftHouse -= StartLeftCoroutine;
    }

    private void PlaySound()
    {
        _audioSource.Play();
    }

    private void StartEnterCoroutine()
    {
        if (_volumeCoroutine != null)
            StopCoroutine(_volumeCoroutine);

        float offset = _volumeChangingSpeed;
        float finalVolume = 1;

        _volumeCoroutine = StartCoroutine(ChangeVolumeCouroutine(offset, () => _audioSource.volume < finalVolume));
    }

    private void StartLeftCoroutine()
    {
        if (_volumeCoroutine != null)
            StopCoroutine(_volumeCoroutine);

        float offset = _volumeChangingSpeed * -1;
        float finalVolume = 0;

        _volumeCoroutine = StartCoroutine(ChangeVolumeCouroutine(offset, () => _audioSource.volume > finalVolume));
    }

    private IEnumerator ChangeVolumeCouroutine(float offset, Func<bool> condition)
    {
        var wait = new WaitForSeconds(_smoothness);

        while (condition())
        {
            _audioSource.volume += offset;
            yield return wait;
        }

        if (_audioSource.volume == 0)
            _audioSource.Stop();
    }
}
