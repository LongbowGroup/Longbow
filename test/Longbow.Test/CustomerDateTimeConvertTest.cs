#if NETCore
using Longbow.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Xunit;

namespace Longbow
{
    
    public class CustomerDateTimeConvertTest
    {
        [Theory]
        [InlineData("yyyyMMdd")]
        [InlineData("yyyy-MM-dd")]
        [InlineData("yyyy-MM-dd HH:mm:ss")]
        public void ReadJson_Ok(string format)
        {
            var v = DateTime.Now;
            var json = $"{{\"Dummy\":\"{v.ToString(format)}\"}}";

            var f = JsonConvert.DeserializeObject(json, typeof(Foo), new CustomerDateTimeConvert(format)) as Foo;

            // {"Dummy":"2018-12-09 16:51:23"}
            Assert.Equal(f.Dummy.ToString(format), v.ToString(format));
        }

        [Theory]
        [InlineData("yyyyMMdd")]
        [InlineData("yyyy-MM-dd")]
        [InlineData("yyyy-MM-dd HH:mm:ss")]
        public void WriteJson_Ok(string format)
        {
            var v = DateTime.Now;
            var f = new Foo() { Dummy = v };
            var json = JsonConvert.SerializeObject(f, new CustomerDateTimeConvert(format));

            // {"Dummy":"2018-12-09 16:51:23"}
            Assert.Equal($"{{\"Dummy\":\"{v.ToString(format)}\"}}", json);

            var v1 = DateTimeOffset.Now;
            var f1 = new Foo1() { Dummy = v1 };
            var json1 = JsonConvert.SerializeObject(f1, new CustomerDateTimeConvert(format));

            // {"Dummy":"2018-12-09 16:51:23"}
            Assert.Equal($"{{\"Dummy\":\"{v.ToString(format)}\"}}", json1);
        }

        [Fact]
        public void MinValue_Null()
        {
            var v = DateTime.MinValue;
            var f = new Foo() { Dummy = v };
            var json = JsonConvert.SerializeObject(f, new CustomerDateTimeConvert());
            Assert.Equal($"{{\"Dummy\":null}}", json);

            var v1 = DateTimeOffset.MinValue;
            var f1 = new Foo1() { Dummy = v1 };
            json = JsonConvert.SerializeObject(f1, new CustomerDateTimeConvert());
            Assert.Equal($"{{\"Dummy\":null}}", json);
        }

        [Fact]
        public void JsonConvert_Ok()
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings()
            {
                Converters = new List<JsonConverter>() {
                    new CustomerDateTimeConvert()
                }
            };

            var dummy = new JsonDummy();
            dummy.Dummy = DateTime.MinValue;
            var v1 = JsonConvert.SerializeObject(dummy);
            Assert.Equal($"{{\"Dummy\":null,\"Demo\":null}}", v1);

            var dt = DateTime.Now;
            dummy.Dummy = dummy.Demo = dt;
            var v2 = JsonConvert.SerializeObject(dummy);
            Assert.Equal($"{{\"Dummy\":\"{dt:yyyy-MM-dd HH:mm:ss}\",\"Demo\":\"{dt:yyyy-MM-dd}\"}}", v2);
        }

        private class Foo
        {
            public DateTime Dummy { get; set; }
        }

        private class Foo1
        {
            public DateTimeOffset Dummy { get; set; }
        }

        private class JsonDummy
        {
            public DateTime Dummy { get; set; }

            [JsonConverter(typeof(CustomerDateTimeConvert), "yyyy-MM-dd")]
            public DateTime Demo { get; set; }
        }
    }
}
#endif
