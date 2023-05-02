﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.CQRS.Core.Exceptions;

public class ConcurrencyException : Exception
{
	public ConcurrencyException() : base()
	{
	}

	public ConcurrencyException(string message) : base(message)
	{ 
	}
}
