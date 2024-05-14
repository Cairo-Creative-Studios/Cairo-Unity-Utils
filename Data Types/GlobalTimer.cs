using UnityEngine;

/// <summary>
/// A class that represents a global timer that can be started, stopped, and ticked.
/// </summary>
public class GlobalTimer
{
    private string _name;
    /// <summary>
    /// Gets the name of the timer.
    /// </summary>
    public string Name => _name;

    private float _time;
    /// <summary>
    /// The time that the timer has elapsed since TimeStarted.
    /// </summary>
    public float Time => _time;

    private float _timeStarted;
    /// <summary>
    /// The <see cref="Time.time"/> the Timer was started.
    /// </summary>
    public float TimeStarted => _timeStarted;

    private float _duration;
    /// <summary>
    /// The length of time it takes for the timer to stop on it's own.
    /// </summary>
    public float Duration => _duration;

    /// <summary>
    /// Gets the alpha value of the timer, which is the ratio of the elapsed time to the duration.
    /// </summary>
    public float Alpha => Mathf.Max(Time, Duration) / Duration;

    public bool Paused = false;

    private bool _complete = false;
    /// <summary>
    /// True if the Time has surpassed Duration.
    /// </summary>
    public bool Complete => _complete;

    /// <summary>
    /// Initializes a new instance of the <see cref="GlobalTimer"/> class with the specified name, start time, and duration.
    /// </summary>
    /// <param name="name">The name of the timer.</param>
    /// <param name="timeStarted">The time when the timer was started.</param>
    /// <param name="duration">The duration of the timer.</param>
    public GlobalTimer(string name, float timeStarted, float duration)
    {
        _name = name;
        _timeStarted = timeStarted;
        _time = 0;
        _duration = duration;

        OnGlobalTimerStarted.Invoke(_name, _timeStarted, _duration);
    }

    /// <summary>
    /// Updates the timer with the current time.
    /// </summary>
    /// <param name="time">The current time.</param>
    public void Tick(float time)
    {
        if (Paused) return;

        _time = time - _timeStarted;

        if (_time > _duration)
        {
            OnGlobalTimerStopped.Invoke(_name);
            return;
        }

        _complete = true;

        OnGlobalTimerTick.Invoke(_name, _time, _duration);
    }
}
