using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AlarmSystem : MonoBehaviour
{
    [SerializeField] private AlarmTrigger _trigger;
    [SerializeField, Range(0, 1)] private float _increaseRate;
    [SerializeField, Range(0, 1)] float _smoothness = 0.01f;

    private AudioSource _audioSource;

    private Coroutine _increaseVolumeCoroutine;
    private Coroutine _decreaseVolumeCoroutine;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = 0;
    }

    private void OnEnable()
    {
        _trigger.CrookEnteredHouse += StartSignaling;
        _trigger.CrookLeftHouse += StopSignaling;
    }

    private void OnDisable()
    {
        _trigger.CrookEnteredHouse -= StartSignaling;
        _trigger.CrookLeftHouse -= StopSignaling;
    }

    private void StartSignaling()
    {
        if (_decreaseVolumeCoroutine != null)
        {
            StopCoroutine(_decreaseVolumeCoroutine);
            _decreaseVolumeCoroutine = null;
        }

        _increaseVolumeCoroutine = StartCoroutine(IncreaseCouroutine());
    }

    private void StopSignaling()
    {
        if (_increaseVolumeCoroutine != null)
        {
            StopCoroutine(_increaseVolumeCoroutine);
            _increaseVolumeCoroutine = null;
        }

        _increaseVolumeCoroutine = StartCoroutine(DecreaseCouroutine());
    }

    private IEnumerator IncreaseCouroutine()
    {
        _audioSource?.Play();

        var wait = new WaitForSeconds(_smoothness);

        while (_audioSource.volume < 1)
        {
            _audioSource.volume += _increaseRate;
            yield return wait;
        }
    }

    private IEnumerator DecreaseCouroutine()
    {
        var wait = new WaitForSeconds(_smoothness);

        while (_audioSource.volume > 0)
        {
            _audioSource.volume -= _increaseRate;
            yield return wait;
        }

        _audioSource?.Stop();
    }
}
