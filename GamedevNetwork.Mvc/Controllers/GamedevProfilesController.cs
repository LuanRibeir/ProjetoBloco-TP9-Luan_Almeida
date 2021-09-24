using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using GamedevNetwork.Domain.Models;
using GamedevNetwork.Domain.Interfaces;

namespace GamedevNetwork.Mvc.Controllers
{
    [Authorize]
    public class GamedevProfilesController : Controller
    {
        private readonly IGamedevRepository _repository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public GamedevProfilesController(IGamedevRepository repository,
                                         UserManager<IdentityUser> userManager,
                                         SignInManager<IdentityUser> signInManager)
        {
            _repository = repository;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: GamedevProfiles
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);

            var profile = await _repository.GetProfileAsync(userId);

            return View(profile);
        }

        // GET: GamedevProfiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gamedevProfile = await _repository.GetByIdAsync(id.Value); 
            if (gamedevProfile == null)
            {
                return NotFound();
            }

            return View(gamedevProfile);
        }

        // GET: GamedevProfiles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GamedevProfiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ImagemUri,Nome,Nacionalidade,JogoFavorito,QuantidadeDeJogosPublicados,Nascimento,UserId"),]
                                                IFormCollection form, 
                                                GamedevProfile gamedevProfile,
                                                [FromServices] IHttpClientFactory clientFactory)
        {
            if (ModelState.IsValid)
            {
                using var content = new MultipartFormDataContent();
                foreach (var file in form.Files)
                {
                    content.Add(CreateFileContent(file.OpenReadStream(),
                                                  file.FileName,
                                                  file.ContentType));
                }
                var httpClient = clientFactory.CreateClient("ClientHttp");
                var response = await httpClient.PostAsync("api/Image", content);

                response.EnsureSuccessStatusCode();

                var responseResult = await response.Content.ReadAsStringAsync();
                var imagemUri = JsonConvert.DeserializeObject<string[]>(responseResult).FirstOrDefault();

                var userId = _userManager.GetUserId(User);

                gamedevProfile.UserId = userId;
                gamedevProfile.ImagemUri = imagemUri;

                await _repository.InsertAsync(gamedevProfile);
                return RedirectToAction(nameof(Index));
            }
            return View(gamedevProfile);
        }

        private StreamContent CreateFileContent(Stream stream, string fileName, string contentType)
        {
            var fileContent = new StreamContent(stream);
            fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "\"files\"",
                FileName = "\"" + fileName + "\""
            };

            fileContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            return fileContent;
        }


        // GET: GamedevProfiles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gamedevProfile = await _repository.GetByIdAsync(id.Value);
            if (gamedevProfile == null)
            {
                return NotFound();
            }
            return View(gamedevProfile);
        }

        // POST: GamedevProfiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ImagemUri,Nome,Nacionalidade,JogoFavorito,QuantidadeDeJogosPublicados,Nascimento,UserId")] GamedevProfile gamedevProfile)
        {
            if (id != gamedevProfile.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.UpdateAsync(gamedevProfile);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GamedevProfileExists(gamedevProfile.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(gamedevProfile);
        }

        // GET: GamedevProfiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gamedevProfile = await _repository.GetByIdAsync(id.Value);

            if (gamedevProfile == null)
            {
                return NotFound();
            }

            return View(gamedevProfile);
        }

        // POST: GamedevProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gamedevProfile = await _repository.GetByIdAsync(id);
            await _repository.DeletetAsync(gamedevProfile);
                
            return RedirectToAction(nameof(Index));
        }

        private bool GamedevProfileExists(int id)
        {
            var gamedevProfile = _repository.GetByIdAsync(id).Result;
            return (gamedevProfile != null);
        }
    }
}
