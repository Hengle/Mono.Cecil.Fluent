﻿using System;

namespace Mono.Cecil.Fluent
{
	public static partial class MethodDefinitionExtensions
	{
		public static FluentMethodBody ReturnsVoid(this MethodDefinition method)
		{
			return new FluentMethodBody(method).ReturnsVoid();
		}

		public static FluentMethodBody Returns(this MethodDefinition method, TypeReference type)
		{
			return new FluentMethodBody(method).Returns(type);
		}

		public static FluentMethodBody Returns(this MethodDefinition method, Type type)
		{
			return new FluentMethodBody(method).Returns(type);
		}

		public static FluentMethodBody Returns<T>(this MethodDefinition method)
		{
			return new FluentMethodBody(method).Returns<T>();
		}
	}

	public static partial class FluentMethodBodyExtensions
	{
		public static FluentMethodBody ReturnsVoid(this FluentMethodBody method)
		{
			method.ReturnType = method.Module.TypeSystem.Void;
			return method;
		}

		public static FluentMethodBody Returns(this FluentMethodBody method, TypeReference type)
		{
			method.ReturnType = method.Module.SafeImport(type);
			return method;
		}

		public static FluentMethodBody Returns(this FluentMethodBody method, Type type)
		{
			method.ReturnType = method.Module.SafeImport(type);
			return method;
		}

		public static FluentMethodBody Returns<T>(this FluentMethodBody method)
		{
			method.ReturnType = method.Module.SafeImport(typeof(T));
			return method;
		}
	}
}
