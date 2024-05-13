using GranTitan.Model;
using GranTitan.BLL.Model;
using System.Collections.Generic;
using System.Linq;

namespace GranTitan.Extensions
{
    /// <summary>
    /// Clase para convertir una lista
    /// </summary>
    public static class ResponseExtension
    {
        /// <summary>
        ///  Convierte una List en List(Response)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static IEnumerable<Response<T>> ConvertToListResponse<T>(this IEnumerable<T> list) where T : PayloadBody
        {
            List<Response<T>> lstResponse = new();
            foreach (var item in list)
            {
                lstResponse.Add(item.ConvertToResponse());
            }
            return lstResponse;
        }

        /// <summary>
        ///  Convierte una clase T en Response T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public static Response<T> ConvertToResponse<T>(this T item) where T : PayloadBody
        {
            return new Response<T>() { Body = item };
        }
        /// <summary>
        /// returna si una lista tiene elementos con errores
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool HasErrors<T>(this IEnumerable<T> list) where T : PayloadBody
        {
            return list.Any(x => x.Error != null && x.Error.Any());
        }

        /// <summary>
        /// returna si una todos los elementos de la lista tienen errores
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool HasErrorsAll<T>(this IEnumerable<T> list) where T : PayloadBody
        {
            return list.All(x => x.Errors != null && x.Errors.Any());
        }

        /// <summary>
        /// Retorna si un objeto contiene errores
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool HasErrors<T>(this T obj) where T : PayloadBody
        {
            return obj.Error != null && obj.Error.Any();
        }
    }
}
