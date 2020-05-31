using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderNowDAL.DAL
{
    public class UsuarioDAL
    {
        private OrderNowBDEntities nowBDEntities = new OrderNowBDEntities();


        public Usuario IsvalidUser(string user)
        {
            var query = nowBDEntities.Usuario.FirstOrDefault(obj => obj.Usuario1 == user);

            return query;

        }
        public int TipoUsuario(string user, string clave)
        {
            var query = from c in nowBDEntities.Usuario
                        where c.Usuario1 == user && c.Contraseña == clave
                        select c.IdTipoUsuario;

            if (Convert.ToInt32(query.SingleOrDefault()) == 1)
            {
                return 1;

            }
            else
            {
                return 2;
            }
        }
        public void Add(Usuario m)
        {
            nowBDEntities.Usuario.Add(m);
            nowBDEntities.SaveChanges();
        }
        public void Remove(int id)
        {
            Usuario c = nowBDEntities.Usuario.FirstOrDefault(x => x.IdUsuario == id);
            nowBDEntities.Usuario.Remove(c);
            nowBDEntities.SaveChanges();
        }
        public Usuario Find(int id)
        {
            Usuario uObj = nowBDEntities.Usuario.FirstOrDefault(u => u.IdUsuario == id);
            return uObj;
        }
        public int ObtenerMaxId()
        {
            var query = from c in nowBDEntities.Usuario
                        select c.IdUsuario;
            int idMax = query.Max();

            return idMax;
        }



    }
}
