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
        static void Main(string[] args)
        {
            try
            {
                var fields = new List<Field>() {
    new Field("EmployeeID", typeof(int)),
    new Field("EmployeeName", typeof(string)),
    new Field("Designation", typeof(string))
};

                dynamic obj = new DynamicClass(fields);

                //set
                obj.EmployeeID = 123456;
                obj.EmployeeName = "John";
                obj.Designation = "Tech Lead";

                //obj.Age = 25;             //Exception: DynamicClass does not contain a definition for 'Age'
                //obj.EmployeeName = 666;   //Exception: Value 666 is not of type String

                //get
                Console.WriteLine(obj.EmployeeID);     //123456
                Console.WriteLine(obj.EmployeeName);   //John
                Console.WriteLine(obj.Designation);    //Tech Lead
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //MyClassBuilder MCB = new MyClassBuilder("Student");
            //var myclass = MCB.CreateObject(new string[3] { "ID", "Name", "Address" }, new Type[3] { typeof(int), typeof(string), typeof(string) });
            //Type TP = myclass.GetType();

            //foreach (PropertyInfo PI in TP.GetProperties())
            //{
            //    Console.WriteLine(PI.Name);
            //}
            //Console.ReadLine();



        }

    }
}
