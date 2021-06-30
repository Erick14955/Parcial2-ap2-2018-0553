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
    public class VentasBLL
    {
        public static bool Guardar(Ventas ventas)
        {
            if (!Existe(ventas.VentaId))
                return Insertar(ventas);
            else
                return Modificar(ventas);
        }

        private static bool Existe(int id)
        {
            bool Existencia = false;
            Contexto contexto = new Contexto();

            try
            {
                Existencia = contexto.Ventas.Any(x => x.VentaId == id);
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

        private static bool Insertar(Ventas ventas)
        {
            bool Insertado = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Ventas.Add(ventas);
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

        private static bool Modificar(Ventas ventas)
        {
            bool Modificado = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Entry(ventas).State = EntityState.Modified;
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
                var ventas = Buscar(id);

                contexto.Entry(ventas).State = EntityState.Deleted;
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

        public static Ventas Buscar(int id)
        {
            Ventas ventas = new Ventas();
            Contexto contexto = new Contexto();

            try
            {
                ventas = contexto.Ventas.Find(id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return ventas;
        }

        public static List<Ventas> GetList(Expression<Func<Ventas, bool>> ventas)
        {
            List<Ventas> Lista = new List<Ventas>();
            Contexto contexto = new Contexto();

            try
            {
                Lista = contexto.Ventas.Where(ventas).ToList();
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
