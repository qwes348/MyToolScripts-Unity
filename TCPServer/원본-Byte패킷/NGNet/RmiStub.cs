using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NGNet
{
    public abstract class RmiStub
    {
        public RmiStub()
        {
        }
        ~RmiStub()
        {
        }

        public abstract bool ProcessReceivedMessage( Message msg );
    }
}
