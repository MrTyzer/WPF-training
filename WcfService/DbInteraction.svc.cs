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

        public DataTables Init()
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Dz_7;Integrated Security=True";
            SqlConnection connectionEmp = new SqlConnection(connectionString);
            SqlDataAdapter adapterEmp = new SqlDataAdapter();
            SqlCommand command = new SqlCommand(@"SELECT ID, Name, Age, Salary, Department FROM
                                                Employee", connectionEmp);
            adapterEmp.SelectCommand = command;
            //insert
            command = new SqlCommand(@"INSERT INTO Employee (Name, Age, Salary, Department)
                                     VALUES (@Name, @Age, @Salary, @Department); SET @ID = @@IDENTITY;", connectionEmp);
            command.Parameters.Add("@Name", SqlDbType.NVarChar, -1, "Name");
            command.Parameters.Add("@Age", SqlDbType.NVarChar, -1, "Age");
            command.Parameters.Add("@Salary", SqlDbType.NVarChar, -1, "Salary");
            command.Parameters.Add("@Department", SqlDbType.NVarChar, -1, "Department");
            SqlParameter param = command.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
            param.Direction = ParameterDirection.Output;
            adapterEmp.InsertCommand = command;
            // update
            command = new SqlCommand(@"UPDATE Employee SET Name = @Name, Age = @Age,
                                     Salary = @Salary, Department = @Department WHERE ID = @ID", connectionEmp);
            command.Parameters.Add("@Name", SqlDbType.NVarChar, -1, "Name");
            command.Parameters.Add("@Age", SqlDbType.NVarChar, -1, "Age");
            command.Parameters.Add("@Salary", SqlDbType.NVarChar, -1, "Salary");
            command.Parameters.Add("@Department", SqlDbType.NVarChar, -1, "Department");
            param = command.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
            param.SourceVersion = DataRowVersion.Original;
            adapterEmp.UpdateCommand = command;
            //delete
            command = new SqlCommand("DELETE FROM Employee WHERE ID = @ID", connectionEmp);
            param = command.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
            param.SourceVersion = DataRowVersion.Original;
            adapterEmp.DeleteCommand = command;
            DataTable tableEmp = new DataTable();
            tableEmp.TableName = "Employee";
            adapterEmp.Fill(tableEmp);

            SqlConnection connectionDep = new SqlConnection(connectionString);
            SqlDataAdapter adapterDep = new SqlDataAdapter();
            command = new SqlCommand(@"SELECT ID, Name FROM
                                                Department", connectionDep);
            adapterDep.SelectCommand = command;
            //insert
            command = new SqlCommand(@"INSERT INTO Department (Name)
                                     VALUES (@Name); SET @ID = @@IDENTITY;", connectionDep);
            command.Parameters.Add("@Name", SqlDbType.NVarChar, -1, "Name");
            param = command.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
            param.Direction = ParameterDirection.Output;
            adapterDep.InsertCommand = command;
            // update
            command = new SqlCommand(@"UPDATE Department SET Name = @Name, 
                                     WHERE ID = @ID", connectionDep);
            command.Parameters.Add("@Name", SqlDbType.NVarChar, -1, "Name");
            param = command.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
            param.SourceVersion = DataRowVersion.Original;
            adapterDep.UpdateCommand = command;
            //delete
            command = new SqlCommand("DELETE FROM Department WHERE ID = @ID", connectionDep);
            param = command.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
            param.SourceVersion = DataRowVersion.Original;
            adapterDep.DeleteCommand = command;
            DataTable tableDep = new DataTable();
            adapterDep.Fill(tableDep);
            tableDep.TableName = "Department";

            DataTables res = new DataTables();
            //res.ConnectionEmp = connectionEmp;
            //res.ConnectionDep = connectionDep;
            //res.AdapterEmp = adapterEmp;
            //res.AdapterDep = adapterDep;
            res.EmpTable = tableEmp;
            res.DepTable = tableDep;
            return res;
        }

        public void Update(DataTables tables)
        {
            //tables.AdapterEmp.Update(tables.EmpTable);
            //tables.AdapterDep.Update(tables.DepTable);
        }
    }
}
