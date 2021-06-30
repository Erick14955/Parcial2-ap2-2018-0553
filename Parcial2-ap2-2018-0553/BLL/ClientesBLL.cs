using Microsoft.EntityFrameworkCore;
using Parcial2_ap2_2018_0553.DAL;
using Parcial2_ap2_2018_0553.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Parcial2_ap2_2018_0553.BLL
{
    public class ClientesBLL
    {
        public static bool Guardar(Clientes clientes)
        {
            if (!Existe(clientes.ClienteId))
                return Insertar(clientes);
            else
                return Modificar(clientes);
        }

        private static bool Existe(int id)
        {
            bool Existencia = false;
            Contexto contexto = new Contexto();

            try
            {
                Existencia = contexto.Clientes.Any(x => x.ClienteId == id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return Existencia;
        }

        private static bool Insertar(Clientes clientes)
        {
            bool Insertado = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Clientes.Add(clientes);
                Insertado = (contexto.SaveChanges() > 0);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return Insertado;
        }

        private static bool Modificar(Clientes clientes)
        {
            bool Modificado = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Entry(clientes).State = EntityState.Modified;
                Modificado = (contexto.SaveChanges() > 0);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return Modificado;
        }

        public static bool Eliminar(int id)
        {
            bool Eliminado = false;
            Contexto contexto = new Contexto();

            try
            {
                var clientes = Buscar(id);

                contexto.Entry(clientes).State = EntityState.Deleted;
                Eliminado = (contexto.SaveChanges() > 0);

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return Eliminado;
        }

        public static Clientes Buscar(int id)
        {
            Clientes clientes = new Clientes();
            Contexto contexto = new Contexto();

            try
            {
                clientes = contexto.Clientes.Find(id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return clientes;
        }

        public static List<Clientes> GetList(Expression<Func<Clientes, bool>> clientes)
        {
            List<Clientes> Lista = new List<Clientes>();
            Contexto contexto = new Contexto();

            try
            {
                Lista = contexto.Clientes.Where(clientes).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return Lista;
        }
    }
}
