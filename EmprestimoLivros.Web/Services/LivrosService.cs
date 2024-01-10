using EmprestimoLivros.Web.Models;
using System.Text.Json;
using System.Threading.Tasks;

#nullable disable

namespace EmprestimoLivros.Web.Services
{
    public class LivrosService : ILivrosService
    {
        private readonly IHttpClientFactory _clientFactory;
        private const string apiEndpoint = "api/Livro/";
        private readonly JsonSerializerOptions JsonOpt = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
        private IEnumerable<Livro> LivrosAPI;
        private Livro LivroAPI;


        private string caminhoServidor;

        public LivrosService(IHttpClientFactory clientFactory, IWebHostEnvironment sistema)
        {
            _clientFactory = clientFactory;
            caminhoServidor = sistema.WebRootPath;
        }

        public async Task<IEnumerable<Livro>> SelecionarTodosOsLivros()
        {
            var livroConexao = _clientFactory.CreateClient("LivroApi");

            using (var respostaSolicitacao = await livroConexao.GetAsync(apiEndpoint + "SelecionarTodos"))
            {
                if (respostaSolicitacao.IsSuccessStatusCode)
                {
                    var respostaTratada = await respostaSolicitacao.Content.ReadAsStreamAsync();
                    LivrosAPI = await JsonSerializer
                        .DeserializeAsync<IEnumerable<Livro>>(respostaTratada, JsonOpt);
                }
                else
                {
                    return null;
                }
            }
            return LivrosAPI;

        }
        public async Task<Livro> BuscarLivroPorId(int id)
        {
            var livroConexao = _clientFactory.CreateClient("LivroApi");

            using (var respostaSolicitacao = await livroConexao.GetAsync(apiEndpoint + "BuscarUsuarioId/" + id))
            {
                if (respostaSolicitacao.IsSuccessStatusCode)
                {
                    var respostaTratada = await respostaSolicitacao.Content.ReadAsStreamAsync();
                    LivroAPI = await JsonSerializer
                        .DeserializeAsync<Livro>(respostaTratada, JsonOpt);
                }
                else
                {
                    return null;
                }
            }
            return LivroAPI;
        }
        public async Task<IEnumerable<Livro>> BuscarLivroPorNome(string nome)
        {
            var livroConexao = _clientFactory.CreateClient("LivroApi");

            using (var respostaSolicitacao = await livroConexao.GetAsync(apiEndpoint + "BuscarLivroNome/" + nome))
            {
                if (respostaSolicitacao.IsSuccessStatusCode)
                {
                    var respostaTratada = await respostaSolicitacao.Content.ReadAsStreamAsync();
                    LivrosAPI = await JsonSerializer
                        .DeserializeAsync<IEnumerable<Livro>>(respostaTratada, JsonOpt);
                }
                else
                {
                    return null;
                }
            }
            return LivrosAPI;
        }
        public async Task<bool> Incluir(Livro livroCriar)
        {
            var livroConexao = _clientFactory.CreateClient("LivroApi");
            using (var respostaSolicitacao = await livroConexao.PostAsJsonAsync(apiEndpoint + "Cadastrar", livroCriar))
            {
                return respostaSolicitacao.IsSuccessStatusCode;
            }
        }
        public async Task<bool> Alterar(Livro livroPost)
        {
            var livroConexao = _clientFactory.CreateClient("LivroApi");
            using (var respostaSolicitacao = await livroConexao.PutAsJsonAsync(apiEndpoint + "Alterar", livroPost))
            {
                return respostaSolicitacao.IsSuccessStatusCode;
            }
        }
        public async Task<bool> Excluir(int id)
        {
            var livroConexao = _clientFactory.CreateClient("LivroApi");
            using (var respostaSolicitacao = await livroConexao.DeleteAsync(apiEndpoint + $"Deletar/{id}"))
            {
                return respostaSolicitacao.IsSuccessStatusCode;
            }
        }

        public string ImagemUpload(IFormFile foto)
        {
            string caminhoParaSalvarImagem = caminhoServidor + "\\imagem\\";
            string novoNomeImagem = Guid.NewGuid().ToString() + "_" + foto.FileName;
            if (!Directory.Exists(caminhoParaSalvarImagem))
            {
                Directory.CreateDirectory(caminhoParaSalvarImagem);
            }

            using(var stream = File.Create(caminhoParaSalvarImagem + novoNomeImagem))
            {
                foto.CopyToAsync(stream);
            }
            return novoNomeImagem;
        }
    }
}
