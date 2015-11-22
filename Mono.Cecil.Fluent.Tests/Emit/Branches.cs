﻿using System;
using System.Reflection.Emit;
using Machine.Specifications;
using Should.Fluent;

// ReSharper disable InconsistentNaming
// ReSharper disable ArrangeTypeMemberModifiers

namespace Mono.Cecil.Fluent.Tests.Emit
{
	public class FluentMethodBody_Branches : TestsBase
	{
		static readonly TypeDefinition TestType = CreateType();

		static FluentMethodBody NewTestMethod => new FluentMethodBody(CreateMethod());

		It shoud_emit_if_block = () =>
			CreateStaticMethod()
				.Returns<int>()
				.WithParameter<bool>()
				.Ldarg(0)
				.If()
					.Ret(10)
				.EndIf()
				.Ret(100)
			.ToDynamicMethod()
			.Invoke(null, new object[] { false }).Should().Equal(100);

		It shoud_emit_ifnot_block = () =>
			CreateStaticMethod()
				.Returns<int>()
				.WithParameter<bool>()
				.Ldarg(0)
				.IfNot()
					.Ret(10)
				.EndIf()
				.Ret(100)
			.ToDynamicMethod()
			.Invoke(null, new object[] { false }).Should().Equal(10);

		static Func<long, long, bool> LessThenMethod => CreateStaticMethod()
			.Returns<bool>()
			.WithParameter<long>()
			.WithParameter<long>()
				.Ldarg(0)
				.Ldarg(1)
				.Iflt()
					.Ret(true)
				.EndIf()
				.Ret(false)
			.ToDynamicMethod().CreateDelegate(typeof(Func<long, long, bool>)) as Func<long, long, bool>;

		It should_be_less_than = () => LessThenMethod(50, 100).Should().Equal(true);
		It should_not_be_less_than = () => LessThenMethod(500, 100).Should().Equal(false);

		static Func<int, int, bool> GreaterThanMethod => CreateStaticMethod()
			.Returns<bool>()
			.WithParameter<int>()
			.WithParameter<int>()
				.Ldarg(0)
				.Ldarg(1)
				.Ifgt()
					.Ret(true)
				.EndIf()
				.Ret(false)
			.ToDynamicMethod().CreateDelegate(typeof(Func<int, int, bool>)) as Func<int, int, bool>;

		It should_be_greater_than = () => GreaterThanMethod(500, 100).Should().Equal(true);
		It should_not_be_greater_than = () => GreaterThanMethod(50, 100).Should().Equal(false);

		static Func<long, long, bool> LessThenOrEqualMethod => CreateStaticMethod()
			.Returns<bool>()
			.WithParameter<long>()
			.WithParameter<long>()
				.Ldarg(0)
				.Ldarg(1)
				.Iflte()
					.Ret(true)
				.EndIf()
				.Ret(false)
			.ToDynamicMethod().CreateDelegate(typeof(Func<long, long, bool>)) as Func<long, long, bool>;

		It should_be_less_than_or_equal = () => LessThenOrEqualMethod(100, 100).Should().Equal(true);
		It should_be_less_than_or_equal_2 = () => LessThenOrEqualMethod(7, 100).Should().Equal(true);
		It should_not_be_less_than_or_equal = () => LessThenOrEqualMethod(500, 100).Should().Equal(false);

		static  Func<int, int, bool> GreaterThanOrEqualMethod => CreateStaticMethod()
			.Returns<bool>()
			.WithParameter<int>()
			.WithParameter<int>()
				.Ldarg(0)
				.Ldarg(1)
				.Ifgte()
					.Ret(true)
				.EndIf()
				.Ret(false)
			.ToDynamicMethod().CreateDelegate(typeof(Func<int, int, bool>)) as Func<int, int, bool>;

		It should_be_greater_than_or_equal = () => GreaterThanOrEqualMethod(100, 100).Should().Equal(true);
		It should_be_greater_than_or_equal_2 = () => GreaterThanOrEqualMethod(1000, 100).Should().Equal(true);
		It should_not_be_greater_than_or_equal = () => GreaterThanOrEqualMethod(5, 100).Should().Equal(false);
	}
}
