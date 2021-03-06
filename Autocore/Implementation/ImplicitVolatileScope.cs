﻿// The MIT License (MIT)
// 
// Copyright (c) 2014 Patricio Zavolinsky
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell 
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
// 
using System;
using Autocore.Interfaces;

namespace Autocore.Implementation
{
	/// <summary>
	/// Implicit volatile scope implementation.
	/// </summary>
	public class ImplicitVolatileScope : IDisposable
	{
		IImplicitContext _context;
		IVolatileContainer _lastScope;

		/// <summary>
		/// Initializes a new implicit volatile scope instance.
		/// </summary>
		/// <remarks>Creates a new volatile scope and pushes the scope into the implicit context.</remarks>
		/// <param name="container">Container.</param>
		public ImplicitVolatileScope(IContainer container)
		{
			_context = container.Resolve<IImplicitContext>();
			_lastScope = _context.Container;
			_context.Container = Container = container.CreateVolatileScope();
		}

		/// <summary>
		/// Releases all resource used by the implicit volatile scope.
		/// </summary>
		/// <remarks>Pops the current volatile scope from the implicit context and disposes the scope.</remarks>
		public void Dispose()
		{
			Container.Dispose();
			_context.Container = _lastScope;
		}

		/// <summary>
		/// Gets the current volatile scope container.
		/// </summary>
		/// <value>The container.</value>
		public IVolatileContainer Container { get; protected set; }
	}
}
