using System;
using GraphQL;
using GraphQL.Types;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var shema = Schema.For(@"
            type Query {
                hello: String
            }");

            var root = new { Hello = "Hello world"};
            var json = shema.Execute(_ => {
                _.Query = "{hello}";
                _.Root = root;
            });


            Console.WriteLine(json);
        }
    }
}
