using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set; }

    //public float shakeDuration = 1.0f;
    //public float shakeMagnitude = 0.7f;
    public float dampingSpeed = 1.0f;

    private bool isShaking = false;
    private Vector3 initialPosition;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        initialPosition = transform.localPosition;
    }

    public void ShakeCamera(float shakeMagnitude,float shakeDuration)
    {
        if (!isShaking)
        {
            StartCoroutine(Shake(shakeMagnitude, shakeDuration));
        }
    }

    private IEnumerator Shake(float shakeMagnitude, float shakeDuration)
    {
        isShaking = true;
        float elapsedTime = 0f;
        

        while (elapsedTime < shakeDuration)
        {
            Vector3 randomPoint = initialPosition + Random.insideUnitSphere * shakeMagnitude;

            transform.localPosition = Vector3.Lerp(transform.localPosition, randomPoint, Time.deltaTime * dampingSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = initialPosition;
        isShaking = false;
    }
}
