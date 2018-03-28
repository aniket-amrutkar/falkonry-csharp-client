﻿using System;
using System.Runtime.Serialization;

namespace FalkonryClient.Service
{
  [Serializable()]
  public class FalkonryException : ApplicationException
  {
    public FalkonryException()
    {
    }

    public FalkonryException(string message) : base(message)
    {
    }
    public FalkonryException(string message, Exception innerException) :
       base(message, innerException)
    {
    }
    protected FalkonryException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
  }
}
