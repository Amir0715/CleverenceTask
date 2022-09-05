namespace CleverenceTask;

public static class MockServer
{
    private static int _count;
    private static readonly RWLock _rwLock = new();

    public static int GetCount()
    {
        using (_rwLock.ReadLock())
        {
            return _count;
        }
    }

    public static void AddToCount(int value)
    {
        using (_rwLock.WriteLock())
        {
            _count += value;
        }
    }
}