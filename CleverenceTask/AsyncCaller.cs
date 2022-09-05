namespace CleverenceTask;

public class AsyncCaller
{
    private EventHandler _eventHandler;
    
    public AsyncCaller(EventHandler eventHandler)
    {
        _eventHandler = eventHandler ?? throw new ArgumentNullException(nameof(eventHandler));
    }

    public bool Invoke(long timeoutMs, object? sender, EventArgs eventArgs)
    {
        var task = new Task(() => _eventHandler.Invoke(sender, eventArgs));
        task.Start();
        return task.Wait(TimeSpan.FromMilliseconds(timeoutMs));
    }
}