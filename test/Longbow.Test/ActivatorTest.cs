using System;
using Xunit;

namespace Longbow
{

    public class ActivatorTest
    {
        private readonly string _assemblyName;

        public ActivatorTest() => _assemblyName = GetType().Assembly.GetName().Name;

        [Fact]
        public void CreateInstance_Ok()
        {
            // 创建内部私有类
            var foo = LgbActivator.CreateInstance<Foo>();
            Assert.True(foo.Dummy);

            // 创建共有类
            Assert.NotNull(LgbActivator.CreateInstance<ActivatorTest>(_assemblyName, "Longbow.ActivatorTest"));
            Assert.NotNull(LgbActivator.CreateInstance<ActivatorTest>());
        }

        [Fact]
        public void CreateInstance_Error()
        {
            Assert.Throws<ArgumentNullException>(() => LgbActivator.CreateInstance<Foo>(_assemblyName));

            // 类内部类，不能使用本方法创建
            Assert.Throws<TypeLoadException>(() => LgbActivator.CreateInstance<Foo>(_assemblyName, "Longbow.ActivatorTest.Foo"));
        }

        private class Foo
        {
            public bool Dummy { get; set; } = true;
        }
    }
}
