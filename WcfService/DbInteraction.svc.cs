using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfService
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "DbInteraction" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы DbInteraction.svc или DbInteraction.svc.cs в обозревателе решений и начните отладку.
    public class DbInteraction : IDbInteraction
    {
        public static SqlDataAdapter AdapterEmp;
        public static SqlDataAdapter AdapterDep;
        public static SqlConnection ConnectionEmp;
        public static SqlConnection ConnectionDep;

        public DataTables Init()
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Dz_7;Integrated Security=True";
            ConnectionEmp = new SqlConnection(connectionString);
            AdapterEmp = new SqlDataAdapter();
            SqlCommand command = new SqlCommand(@"SELECT ID, Name, Age, Salary, Department FROM
                                                Employee", ConnectionEmp);
            AdapterEmp.SelectCommand = command;
            //insert
            command = new SqlCommand(@"INSERT INTO Employee (Name, Age, Salary, Department)
                                     VALUES (@Name, @Age, @Salary, @Department); SET @ID = @@IDENTITY;", ConnectionEmp);
            command.Parameters.Add("@Name", SqlDbType.NVarChar, -1, "Name");
            command.Parameters.Add("@Age", SqlDbType.NVarChar, -1, "Age");
            command.Parameters.Add("@Salary", SqlDbType.NVarChar, -1, "Salary");
            command.Parameters.Add("@Department", SqlDbType.NVarChar, -1, "Department");
            SqlParameter param = command.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
            param.Direction = ParameterDirection.Output;
            AdapterEmp.InsertCommand = command;
            // update
            command = new SqlCommand(@"UPDATE Employee SET Name = @Name, Age = @Age,
                                     Salary = @Salary, Department = @Department WHERE ID = @ID", ConnectionEmp);
            command.Parameters.Add("@Name", SqlDbType.NVarChar, -1, "Name");
            command.Parameters.Add("@Age", SqlDbType.NVarChar, -1, "Age");
            command.Parameters.Add("@Salary", SqlDbType.NVarChar, -1, "Salary");
            command.Parameters.Add("@Department", SqlDbType.NVarChar, -1, "Department");
            param = command.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
            param.SourceVersion = DataRowVersion.Original;
            AdapterEmp.UpdateCommand = command;
            //delete
            command = new SqlCommand("DELETE FROM Employee WHERE ID = @ID", ConnectionEmp);
            param = command.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
            param.SourceVersion = DataRowVersion.Original;
            AdapterEmp.DeleteCommand = command;
            DataTable tableEmp = new DataTable();
            tableEmp.TableName = "Employee";
            AdapterEmp.Fill(tableEmp);

            ConnectionDep = new SqlConnection(connectionString);
            AdapterDep = new SqlDataAdapter();
            command = new SqlCommand(@"SELECT ID, Name FROM
                                                Department", ConnectionDep);
            AdapterDep.SelectCommand = command;
            //insert
            command = new SqlCommand(@"INSERT INTO Department (Name)
                                     VALUES (@Name); SET @ID = @@IDENTITY;", ConnectionDep);
            command.Parameters.Add("@Name", SqlDbType.NVarChar, -1, "Name");
            param = command.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
            param.Direction = ParameterDirection.Output;
            AdapterDep.InsertCommand = command;
            // update
            command = new SqlCommand(@"UPDATE Department SET Name = @Name, 
                                     WHERE ID = @ID", ConnectionDep);
            command.Parameters.Add("@Name", SqlDbType.NVarChar, -1, "Name");
            param = command.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
            param.SourceVersion = DataRowVersion.Original;
            AdapterDep.UpdateCommand = command;
            //delete
            command = new SqlCommand("DELETE FROM Department WHERE ID = @ID", ConnectionDep);
            param = command.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
            param.SourceVersion = DataRowVersion.Original;
            AdapterDep.DeleteCommand = command;
            DataTable tableDep = new DataTable();
            AdapterDep.Fill(tableDep);
            tableDep.TableName = "Department";

            DataTables res = new DataTables();
            res.EmpTable = tableEmp;
            res.DepTable = tableDep;
            return res;
        }

        public void Update(DataTables tables)
        {
            AdapterEmp.Update(tables.EmpTable);
            AdapterDep.Update(tables.DepTable);
        }
    }
}
