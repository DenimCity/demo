using System;
using System.Collections.Generic;
using System.Linq;
using GraphQL;
using GraphQL.Types;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {

            var schema = Schema.For(@"
             type Jedi {
            name: String,
             side: String
             id: Int
            }
             type Query {
                hello: String,
                jedis: [Jedi]
                jedi(id: ID): Jedi
          
            }", _ => {
                _.Types.Include<Query>();
            });

            // var root = new { Hello = "Hello world"};
           var json = schema.Execute(_ =>{
                _.Query = "{ jedis { name, side } }";
            });
           var json2 = schema.Execute(_ =>{
                _.Query = "{ jedi(id: 1) { name } }";
            });
           var json3 = schema.Execute(_ =>{
                _.Query = "hello";
            });


            Console.WriteLine(json);
            Console.WriteLine(json2);
            Console.WriteLine(json3);
        }
    }
}

public class StarWarsDB {
    public static IEnumerable<Jedi> GetJedis() 
{
    return new List<Jedi>() {
        new Jedi(){ Name ="Luke", Side="Light", Id=0},
        new Jedi(){ Name ="Yoda", Side="Light", Id=1},
        new Jedi(){ Name ="Darth Vader", Side="Dark",Id=2}
    };
}

}
public class Query 
{
    [GraphQLMetadata("jedis")]
    public IEnumerable<Jedi> GetJedis() 
    {
        return StarWarsDB.GetJedis();
    }
    
    [GraphQLMetadata("jedi")]
    public Jedi GetJedi(int id)
    {
    return StarWarsDB.GetJedis().SingleOrDefault(j => j.Id == id);
    }
    

    [GraphQLMetadata("hello")]
    public string GetHello() 
    {
        return "Hello Query class";
    }

}



public class Jedi 
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Side { get; set; }
}