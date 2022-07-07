namespace Ultima.Extensions.CommandLine;

using System.CommandLine.Invocation;
using System.Threading.Tasks;

/// <summary>
/// An implementation of <see cref="ICommandHandler"/> that automatic handle interupt signal.
/// </summary>
public abstract class CommandHandler : ICommandHandler, IDisposable
{
    private readonly CancellationTokenSource running;
    private bool disposed;

    protected CommandHandler()
    {
        this.running = new();
        Console.CancelKeyPress += this.OnCancelKeyPress;
    }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    public int Invoke(InvocationContext context)
    {
        if (this.disposed)
        {
            throw new ObjectDisposedException(this.GetType().FullName);
        }

        return this.InvokeAsync(context, this.running.Token).Result;
    }

    public abstract Task<int> InvokeAsync(InvocationContext context, CancellationToken cancellationToken = default);

    Task<int> ICommandHandler.InvokeAsync(InvocationContext context)
    {
        if (this.disposed)
        {
            throw new ObjectDisposedException(this.GetType().FullName);
        }

        return this.InvokeAsync(context, this.running.Token);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (this.disposed)
        {
            return;
        }

        if (disposing)
        {
            Console.CancelKeyPress -= this.OnCancelKeyPress;
            this.running.Dispose();
        }

        this.disposed = true;
    }

    private void OnCancelKeyPress(object? sender, ConsoleCancelEventArgs e)
    {
        this.running.Cancel();
        e.Cancel = true;
    }
}
