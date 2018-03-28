﻿namespace FalkonryClient.Helper.Models
{
  public class Signal
  {

    public string ValueIdentifier
    {
      get;
      set;
    }

    public string SignalIdentifier
    {
      get;
      set;
    }

    public string TagIdentifier
    {
      get;
      set;
    }

    public string Delimiter
    {
      get;
      set;
    }

    public bool IsSignalPrefix
    {
      get;
      set;
    }
  }
}