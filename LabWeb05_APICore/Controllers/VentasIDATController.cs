using LabWeb05_APICore.Entidades;
using LabWeb05_APICore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabWeb05_APICore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentasIDATController : ControllerBase
    {
        VentasIDATContext db = new VentasIDATContext();
        [HttpGet("Empleados")]
        public ActionResult<IEnumerable<ListarEmpleados>> ListarEmpleado() 
        {
            var query = from e in db.Empleados
                        select new ListarEmpleados 
                        {
                            CodEmp=e.IdEmpleado,
                            empleado=e.Nombre+","+e.Apellidos
                        };
            return query.ToList();
        }
        [HttpGet("PedidosporEmplado/{codigo}")] //parametros(uno o mas, ejm {codigo})
        public ActionResult<IEnumerable<PedidosporEmpleado>> PedidosporEmpleado(int codigo) 
        {
            var query = from p in db.Pedidos join e in db.Empleados on
                        p.IdEmpleado equals e.IdEmpleado
                        where p.IdEmpleado.Equals(codigo)
                        select new PedidosporEmpleado
                        {
                            IdPedidos=p.IdPedido,
                            fechaOrden=p.FechaPedido.Value.Day+"/"+
                                       p.FechaPedido.Value.Month + "/" +
                                       p.FechaPedido.Value.Year,
                            Anio=p.FechaPedido.Value.Year,
                            CodEmp=e.IdEmpleado,
                            Apellidos=e.Apellidos,
                            CiudadEnvio=p.CiudadDestinatario
                        };
            return query.ToList();
        }



        //Buscando pedidos por Codigo y mes de Venta

        [HttpGet("Meses")]
        public ActionResult<IEnumerable<ListarMes>> MesesBag()
        {
            var query = (from e in db.Pedidos
                        orderby e.FechaPedido.Value.Month descending
                        select new ListarMes
                        {
                            NumMes = e.FechaPedido.Value.Month
                        }).Distinct();
            return query.ToList();
        }

        [HttpGet("PedidosporEmpladoyMes/{codigo}/{mes}")] //parametros(uno o mas, ejm {codigo})
        public ActionResult<IEnumerable<PedidosCodigoyMes>> PedidosporCodigoyMes(int codigo, int mes)
        {
            var query = from p in db.Pedidos
                        join e in db.Empleados on
                        p.IdEmpleado equals e.IdEmpleado
                        where p.IdEmpleado.Equals(codigo) && p.FechaPedido.Value.Month.Equals(mes)
                        select new PedidosCodigoyMes
                        {
                            IdPedidos = p.IdPedido,
                            fechaOrden = p.FechaPedido.Value.Day + "/" +
                                       p.FechaPedido.Value.Month + "/" +
                                       p.FechaPedido.Value.Year,
                            Anio = p.FechaPedido.Value.Year,
                            CodEmp = e.IdEmpleado,
                            Apellidos = e.Apellidos,
                            CiudadEnvio = p.CiudadDestinatario
                        };
            return query.ToList();
        }
    }
}
