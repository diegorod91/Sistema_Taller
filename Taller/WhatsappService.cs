using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

public static class WhatsAppService
{
    // URL de tu API local/autoalojada de WhatsApp
    private static readonly string ApiUrl = "http://localhost:3000/api/sendText";

    public static async Task<bool> EnviarMensajeAsync(string telefono, string mensaje)
    {
        using (var client = new HttpClient())
        {
            try
            {
                var payload = new
                {
                    number = telefono, // Formato: 549221xxxxxxxx (Código de país + área sin 15 + número)
                    textMessage = new { text = mensaje }
                };

                var jsonContent = new StringContent(
                    JsonConvert.SerializeObject(payload),
                    Encoding.UTF8,
                    "application/json"
                );

                HttpResponseMessage response = await client.PostAsync(ApiUrl, jsonContent);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                // Registrar el error en el log de tu taller sin interrumpir el flujo del usuario
                return false;
            }
        }
    }
}