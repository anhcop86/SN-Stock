using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data
{
    public class DbQueryOption
    {
        public string ConnectionID { get; set; }

        public object ParameterModel { get; set; }

        //public DbQueryOption(string ConnectionID, object ParameterModel)
        //{
        //    this.ConnectionID = ConnectionID;
        //    this.ParameterModel = ParameterModel;
        //}
    }
}
