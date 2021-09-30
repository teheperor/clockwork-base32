# Clockwork Base32 for C#
A implementation of Clockwork Base32 for C#. See [Clockwork Base32 Specification](https://gist.github.com/szktty/228f85794e4187882a77734c89c384a8)

# Usage
```c#
using System;
using System.Text;
using ClockworkBase32;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var encoded = Base32.Encode(Encoding.ASCII.GetBytes("Hello, World!"));
            var decoded = Base32.Decode(encoded);
            Console.WriteLine(Encoding.ASCII.GetString(decoded));
        }
    }
}
```

# License
MIT
