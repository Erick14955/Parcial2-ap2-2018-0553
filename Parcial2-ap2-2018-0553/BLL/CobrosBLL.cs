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
    public class CobrosBLL
    {
        public static bool Guardar(Cobros cobros)
        {
            if (!Existe(cobros.CobroId))
                return Insertar(cobros);
            else
                return Modificar(cobros);
        }

        private static bool Existe(int id)
        {
            bool Existencia = false;
            Contexto contexto = new Contexto();

            try
            {
                Existencia = contexto.Cobros.Any(x => x.CobroId == id);
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

        private static bool Insertar(Cobros cobros)
        {
            bool Insertado = false;
            Contexto contexto = new Contexto();

            try
            {
                foreach(var item in cobros.Detalle)
                {
                    item.Venta = contexto.Ventas.Find(item.VentaId);
                    item.Venta.Balance -= item.Cobrado;
                    contexto.Entry(item.Venta).State = EntityState.Modified;
                }

                contexto.Cobros.Add(cobros);
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

        private static bool Modificar(Cobros cobros)
        {
            bool Modificado = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Database.ExecuteSqlRaw($"Delete FROM CobrosDetalle Where CobroId = {cobros.CobroId}");

                foreach (var item in cobros.Detalle)
                {
                    contexto.Entry(item).State = EntityState.Added;
                }

                contexto.Entry(cobros).State = EntityState.Modified;
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
                var cobros = Buscar(id);

                contexto.Entry(cobros).State = EntityState.Deleted;
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

        public static Cobros Buscar(int id)
        {
            Cobros cobros = new Cobros();
            Contexto contexto = new Contexto();

            try
            {
                cobros = contexto.Cobros
                    .Include(x => x.Detalle)
                    .ThenInclude(Detalle => Detalle.Venta)
                    .Include(x => x.Cliente)
                    .Where(x => x.CobroId == id)
                    .SingleOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return cobros;
        }

        public static List<Cobros> GetList(Expression<Func<Cobros, bool>> cobros)
        {
            List<Cobros> Lista = new List<Cobros>();
            Contexto contexto = new Contexto();

            try
            {
                Lista = contexto.Cobros.Where(cobros).ToList();
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
