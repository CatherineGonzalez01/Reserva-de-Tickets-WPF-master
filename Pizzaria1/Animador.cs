using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Pizzaria1
{
   public class Animador
    {
        public int animadorID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }


        public static async Task<List<Animador>> ObtenerTodos()
        {
            List<Animador> lstanimadores = new List<Animador>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44348/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage respuesta = await client.GetAsync("Api/Animador");
                if (respuesta.IsSuccessStatusCode)
                {
                    lstanimadores = await respuesta.Content.ReadAsAsync<List<Animador>>();
                }
            }

            return lstanimadores;
        }

        public static async Task<bool> AgregarAnimador(Animador a)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44348/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage respuesta = await client.PostAsJsonAsync("Api/Animador", a);
                return respuesta.IsSuccessStatusCode;
            }
        }

        public static async Task<bool> ModificarAnimador(Animador a)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44348/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage respuesta = await client.PutAsJsonAsync("Api/Animador/" + a.animadorID, a); 
                return respuesta.IsSuccessStatusCode;
            }
        }

        public static async Task<bool> EliminarAnimador(Animador a)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44348/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage respuesta = await client.DeleteAsync("Api/Animador/" + a.animadorID); 
                return respuesta.IsSuccessStatusCode;
            }
        }

    }
}
