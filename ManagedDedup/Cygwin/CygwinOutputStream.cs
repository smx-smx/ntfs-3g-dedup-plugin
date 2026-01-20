using System;
using System.IO;

namespace ManagedDedup.Cygwin;

public class CygwinOutputStream : Stream
{
    private readonly int fd;

    public CygwinOutputStream(int fd)
    {
        this.fd = fd;
    }

    public override bool CanRead => false;
    public override bool CanSeek => false;
    public override bool CanWrite => true;

    public override long Length
    {
        get
        {
            throw new NotSupportedException();
        }
    }

    public override long Position
    {
        get
        {
            throw new NotSupportedException();
        }

        set
        {
            throw new NotSupportedException();
        }
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
        throw new NotSupportedException();
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
        throw new NotSupportedException();
    }

    public override void SetLength(long value)
    {
        throw new NotSupportedException();
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
        CygwinAPI.Write(fd, buffer, 0, count);
    }

    public override void Flush() { }
}
