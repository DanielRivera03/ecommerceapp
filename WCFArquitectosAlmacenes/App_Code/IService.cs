using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
[System.Web.Script.Services.ScriptService]
[ServiceContract]
public interface IService
{

	[OperationContract]
	DataSet ValidarCredencialesUsuarios(string Usuario, string Contrasenia);
}
