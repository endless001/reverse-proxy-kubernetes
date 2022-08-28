using Microsoft.Extensions.Internal;

namespace Kubernetes.Gateway.Rate;

public class Reservation
{
    private readonly ISystemClock _clock;
    private readonly Limiter _limiter;
    private readonly Limit _limit;
    private readonly double _tokens;


    public Reservation(
        ISystemClock clock,
        Limiter limiter,
        bool ok,
        double tokens = default,
        DateTimeOffset timeToAct = default,
        Limit limit = default)
    {
        _clock = clock;
        _limiter = limiter;
        Ok = ok;
        _tokens = tokens;
        TimeToAct = timeToAct;
        _limit = limit;
    }


    public bool Ok { get; }

    public DateTimeOffset TimeToAct { get; }


    public TimeSpan Delay()
    {
        return DelayFrom(_clock.UtcNow);
    }


    public TimeSpan DelayFrom(DateTimeOffset now)
    {
        // https://github.com/golang/time/blob/master/rate/rate.go#L134
        if (!Ok)
        {
            return TimeSpan.MaxValue;
        }

        var delay = TimeToAct - now;
        return delay < TimeSpan.Zero ? TimeSpan.Zero : delay;
    }
}