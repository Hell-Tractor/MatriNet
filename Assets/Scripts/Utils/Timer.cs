using UnityEngine;

public class Timer : MonoBehaviour {
    public float duration = 1f;
    public bool loop = false;
    public bool autoStart = true;

    public delegate void TimerCallback();
    public event TimerCallback OnTimerComplete;

    private float _elapsedTime = 0f;

    private void Start() {
        if (autoStart) {
            StartTimer();
        }
    }

    private void Update() {
        if (_elapsedTime > 0f) {
            _elapsedTime -= Time.deltaTime;
            if (_elapsedTime <= 0f) {
                _elapsedTime = 0f;
                if (OnTimerComplete != null) {
                    OnTimerComplete();
                }
                if (loop) {
                    StartTimer();
                }
            }
        }
    }

    public void StartTimer() {
        _elapsedTime = 0f;
    }
}
