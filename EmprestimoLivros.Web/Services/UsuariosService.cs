using EmprestimoLivros.Web.Models;
using System.Text.Json;

#nullable disable
namespace EmprestimoLivros.Web.Services
{
    public class UsuariosService : IUsuariosService
    {
        private readonly IHttpClientFactory _clientFactory;
        private const string apiEndpoint = "api/Usuario/";
        private readonly JsonSerializerOptions JsonOpt = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
        private IEnumerable<Usuario> usuariosAPI;
        private Usuario usuarioAPI;

        public UsuariosService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IEnumerable<Usuario>> SelecionarTodosUsuarios()
        {
            var usuario = _clientFactory.CreateClient("UsuarioApi");

            using (var response = await usuario.GetAsync(apiEndpoint + "SelecionarTodos"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    usuariosAPI = await JsonSerializer
                        .DeserializeAsync<IEnumerable<Usuario>>(apiResponse, JsonOpt);
                }
                else
                {
                    return null;
                }
            }
            return usuariosAPI;

        }
        public async Task<Usuario> BuscarPorId(int id)
        {
            var usuario = _clientFactory.CreateClient("UsuarioApi");

            using (var response = await usuario.GetAsync(apiEndpoint + "BuscarUsuarioId/" + id))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    usuarioAPI = await JsonSerializer
                        .DeserializeAsync<Usuario>(apiResponse, JsonOpt);
                }
                else
                {
                    return null;
                }
            }
            return usuarioAPI;
        }
        public async Task<bool> Incluir(Usuario usuarioCriar)
        {
            var usuario = _clientFactory.CreateClient("UsuarioApi");
            using (var response = await usuario.PostAsJsonAsync(apiEndpoint + "Cadastrar", usuarioCriar))
            {
                return response.IsSuccessStatusCode;
            }
        }
        public async Task<bool> Alterar(Usuario usuarioPost)
        {
            var usuario = _clientFactory.CreateClient("UsuarioApi");
            using (var response = await usuario.PutAsJsonAsync(apiEndpoint + "Alterar", usuarioPost))
            {
                return response.IsSuccessStatusCode;
            }
        }
        public async Task<bool> Excluir(int id)
        {
            var usuario = _clientFactory.CreateClient("UsuarioApi");
            using var response = await usuario.DeleteAsync(apiEndpoint + $"Deletar/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
