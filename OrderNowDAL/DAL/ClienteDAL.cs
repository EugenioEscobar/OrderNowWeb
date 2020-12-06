using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderNowDAL.DAL
{
    public class ClienteDAL
    {
        private OrderNowBDEntities nowBDEntities = new OrderNowBDEntities();

        public Cliente Add(Cliente m)
        {
            m.FechaCreacion = DateTime.Today;
            m = nowBDEntities.Cliente.Add(m);
            nowBDEntities.SaveChanges();
            return m;
        }
        public void Edit(Cliente m)
        {
            Cliente obj = nowBDEntities.Cliente.FirstOrDefault(x => x.IdCliente == m.IdCliente);
            obj.Nombres = m.Nombres;
            obj.ApellidoPat = m.ApellidoPat;
            obj.ApellidoMat = m.ApellidoMat;
            obj.Direccion = m.Direccion;
            obj.FechaNacimiento = m.FechaNacimiento;
            obj.Correo = m.Correo;
            obj.Estado = m.Estado;
            nowBDEntities.SaveChanges();
        }
        public void Remove(int id)
        {
            Cliente c = nowBDEntities.Cliente.FirstOrDefault(x => x.IdCliente == id);
            nowBDEntities.Cliente.Remove(c);
            nowBDEntities.SaveChanges();
        }
        public Cliente Find(int id)
        {
            return nowBDEntities.Cliente.FirstOrDefault(x => x.IdCliente == id);
        }
        public Cliente FindByUser(int id)
        {
            return nowBDEntities.Cliente.FirstOrDefault(x => x.IdUsuario == id);
        }
        public List<Cliente> GetAll()
        {
            return nowBDEntities.Cliente.ToList();
        }
    }
}
