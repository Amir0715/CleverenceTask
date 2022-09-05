namespace CleverenceTask;

public class RWLock : IDisposable
{
    public struct WriteLockToken : IDisposable
    {
        private readonly ReaderWriterLockSlim _lockSlim;
        public WriteLockToken(ReaderWriterLockSlim lockSlim)
        {
            _lockSlim = lockSlim;
            lockSlim.EnterWriteLock();
        }
        public void Dispose() => _lockSlim.ExitWriteLock();
    }

    public struct ReadLockToken : IDisposable
    {
        private readonly ReaderWriterLockSlim _lockSlim;
        public ReadLockToken(ReaderWriterLockSlim lockSlim)
        {
            _lockSlim = lockSlim;
            lockSlim.EnterReadLock();
        }
        public void Dispose() => _lockSlim.ExitReadLock();
    }

    private readonly ReaderWriterLockSlim _lockSlim = new ReaderWriterLockSlim();
    
    public ReadLockToken ReadLock() => new ReadLockToken(_lockSlim);
    public WriteLockToken WriteLock() => new WriteLockToken(_lockSlim);

    public void Dispose() => _lockSlim.Dispose();
}