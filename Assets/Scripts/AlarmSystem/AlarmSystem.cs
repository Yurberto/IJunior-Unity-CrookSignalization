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

    private void Update()
    {
        if (_audioSource.volume == 0)
            _audioSource.Stop();
    }

    private void PlaySound()
    {
        _audioSource.Play();
    }

    private void StartEnterCoroutine()
    {
        StopAllCoroutines();

        float offset = _volumeChangingSpeed;
        float finalVolume = 1;

        StartCoroutine(ChangeVolumeCouroutine(offset, () => _audioSource.volume < finalVolume));
    }

    private void StartLeftCoroutine()
    {
        StopAllCoroutines();

        float offset = _volumeChangingSpeed * -1;
        float finalVolume = 0;

        StartCoroutine(ChangeVolumeCouroutine(offset, () => _audioSource.volume > finalVolume));
    }

    private IEnumerator ChangeVolumeCouroutine(float offset, Func<bool> condition)
    {
        var wait = new WaitForSeconds(_smoothness);

        while (condition())
        {
            _audioSource.volume += offset;
            yield return wait;
        }
    }
}
