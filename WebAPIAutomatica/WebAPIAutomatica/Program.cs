using GraphQL;
using GraphQL.Types;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIAutomatica
{
    class Program
    {
        public enum Tipos
        {
            INT,
            STRING,
            DATETIME,
        }
        public static string[] listaTipos = { "System.Int32", "System.String", "System.DateTime" };

        public class JsonClasse
        {
            public string name { get; set; }
            public Dictionary<string, string> data { get; set; }
        }

        static async Task Main(string[] args)
        {
            try
            {
                //Pessoa digita o json da classe
                var json = "{ \"name\":\"Employee\" , \"data\": { \"EmployeeID\": \"int\",\"EmployeeName\" : \"string\",\"Designation\" : \"string\"} }";

                var jsonClasse = JsonConvert.DeserializeObject<JsonClasse>(json);

                //Salvar essa classe em um Banco de dados (Firebase)



                /* "data"  é interpretado por:
                                             Chave = nome do campo 
                                             Valor = tipo do campo*/

                List<Type> tiposObj = new List<Type>();

                foreach (var value in jsonClasse.data.Values)//Caso nao encontre o tipo, irá ser string
                {
                    switch (value.ToUpper())
                    {
                        case nameof(Tipos.INT):
                            tiposObj.Add(
                                Type.GetType(listaTipos[
                                                        (int)Tipos.INT
                                                        ]));
                            break;

                        case nameof(Tipos.DATETIME):
                            tiposObj.Add(
                                Type.GetType(listaTipos[
                                                        (int)Tipos.DATETIME
                                                        ]));
                            break;
                        case nameof(Tipos.STRING):
                        default:
                            tiposObj.Add(
                                Type.GetType(listaTipos[
                                                        (int)Tipos.STRING
                                                        ]));
                            break;
                    }
                }

                var fields = new List<Field>();

                for (int index = 0; index < jsonClasse.data.Keys.Count; index++)
                {
                    var field = new Field(
                        jsonClasse.data.Keys.ToList()[index],
                        tiposObj[index]
                        );
                    fields.Add(field);
                }

                dynamic objeto = new DynamicClass(fields);//Classe gerada


                //Testando a inserção de dados
                objeto.EmployeeID = 123456;
                objeto.EmployeeName = "John";
                objeto.Designation = "Tech Lead";


                var emplo = new Employee()
                {
                    EmployeeID = 123456,
                    EmployeeName = "John",
                    Designation = "Tech Lead"
                };

                var schema = new Schema { Query = new GenericQuery<Employee>(emplo, "employee") };

                var query = "{ employee { EmployeeID EmployeeName } }";

                var json2 = await schema.ExecuteAsync(_ =>
                {
                    _.Query = query;
                });

                Console.WriteLine(json2);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadLine();
            }

        }

        #region CodigoAntigo
        //MyClassBuilder MCB = new MyClassBuilder("Student");
        //var myclass = MCB.CreateObject(new string[3] { "ID", "Name", "Address" }, new Type[3] { typeof(int), typeof(string), typeof(string) });
        //Type TP = myclass.GetType();

        //foreach (PropertyInfo PI in TP.GetProperties())
        //{
        //    Console.WriteLine(PI.Name);
        //}
        //Console.ReadLine();




        //var fields2 = new List<Field>() {
        //             new Field("EmployeeID", Type.GetType("System.Int32")),
        //             new Field("EmployeeName", Type.GetType("System.String")),
        //             new Field("Designation", Type.GetType("System.String"))
        //        };

        //Console.WriteLine(fields2);

        //objeto.EmployeeID = 123456;
        //objeto.EmployeeName = "John";
        //objeto.Designation = "Tech Lead";

        //obj.Age = 25;             //Exception: DynamicClass does not contain a definition for 'Age'
        //obj.EmployeeName = 666;   //Exception: Value 666 is not of type String

        //Console.WriteLine(obj.EmployeeID);     //123456
        //Console.WriteLine(obj.EmployeeName);   //John
        //Console.WriteLine(obj.Designation);    //Tech Lead
        #endregion

        public class Employee
        {
            public int EmployeeID { get; set; }
            public string EmployeeName { get; set; }
            public string Designation { get; set; }
        }
        public class GenericType<T> : ObjectGraphType<T>
        {
            public GenericType(T obj)
            {
                //foreach (var prop in obj.GetType().GetProperties())
                //{
                //    Field(x => x.);
                //}
                //Field(x => x.Id).Description("The Id of the Droid.");
                //Field(x => x.Name).Description("The name of the Droid.");
            }
        }

        public class GenericQuery<T> : ObjectGraphType
        {
            public GenericQuery(T obj, string nameClass)
            {
                Field<GenericType<T>>(
                  nameClass,
                  resolve: context => obj
                );
            }
        }



    }

}

