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
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IDbInteraction" в коде и файле конфигурации.
    [ServiceContract]
    public interface IDbInteraction
    {
        [OperationContract]
        void Update(DataTables tables);

        [OperationContract]
        DataTables Init();
    }

    [DataContract]
    public class DataTables
    {
        private SqlConnection _connectionEmp = new SqlConnection();
        //private SqlConnection _connectionDep = new SqlConnection();
        //private SqlDataAdapter _adapterEmp = new SqlDataAdapter();
        //private SqlDataAdapter _adapterDep = new SqlDataAdapter();
        private DataTable _empTable = new DataTable();
        private DataTable _depTable = new DataTable();

        [DataMember]
        public SqlConnection ConnectionEmp
        {
            get { return _connectionEmp; }
            set { _connectionEmp = value; }
        }

        //[DataMember]
        //public SqlConnection ConnectionDep
        //{
        //    get { return _connectionDep; }
        //    set { _connectionDep = value; }
        //}

        //[DataMember]
        //public SqlDataAdapter AdapterEmp
        //{
        //    get { return _adapterEmp; }
        //    set { _adapterEmp = value; }
        //}

        //[DataMember]
        //public SqlDataAdapter AdapterDep
        //{
        //    get { return _adapterDep; }
        //    set { _adapterDep = value; }
        //}


        [DataMember]
        public DataTable EmpTable
        {
            get { return _empTable; }
            set { _empTable = value; }
        }

        [DataMember]
        public DataTable DepTable
        {
            get { return _depTable; }
            set { _depTable = value; }
        }
    }
}
