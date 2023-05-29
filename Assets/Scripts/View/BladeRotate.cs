using DG.Tweening;
using UnityEngine;

public class BladeRotate : MonoBehaviour
{
    [SerializeField] private AudioSource _bladeSound;

    private bool _isRotate = false;

    private void Update()
    {
        if (_isRotate)
            transform.Rotate(0, 0, (transform.localEulerAngles.z + 1) * 100 * Time.deltaTime);
    }

    public void StartRotate()
    {
        _bladeSound.Play();
        _isRotate = true;
    }

    public void StopRotate()
    {
        _bladeSound.Stop();
        _isRotate = false;
    }
}
