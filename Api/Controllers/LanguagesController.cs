using Api.Repository.LanguageRepository;
using DataAccessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/language")]
    [ApiController]
    public class LanguagesController : Controller
    {
        private readonly ILanguageRepository _LanguageRepository;
        public LanguagesController(ILanguageRepository LanguageRepository)
        {
            _LanguageRepository = LanguageRepository;
        }
        [HttpGet]
        public async Task<ActionResult> GetLanguages()
        {
            try
            {
                return Ok(await _LanguageRepository.GetLanguages());
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error in retrieving data from database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Languages>> GetLanguages(int id)
        {
            try
            {
                var result = await _LanguageRepository.GetLanguages(id);
                if (result == null)
                {
                    return NotFound();
                }
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in retrieving data from database");
            }
        }
        [HttpPost]
        public async Task<ActionResult> AddLanguages(Languages language)
        {
            try
            {
                if (language == null)
                {
                    return BadRequest();
                }
                var CreateLanguage = await _LanguageRepository.AddLanguages(language);
                return CreatedAtAction(nameof(AddLanguages), new { id = CreateLanguage.Id }, CreateLanguage);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in retrieving data from database");
            }
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Languages>> UpdateLanguages(int id,Languages language)
        {
            try
            {
                if (id != language.Id)
                {
                    return BadRequest("Id is not match");
                }
                var languageUpdated = await _LanguageRepository.GetLanguages(id);
                if (languageUpdated == null)
                {
                    return NotFound($"Language {id} is not found");
                }
                var UpdatedLanguage = await _LanguageRepository.UpdateLanguages(language);
                return UpdatedLanguage;
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in retrieving data from database");
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Languages>> DeleteLanguages(int Id)
        {
            try
            {

                var languageDelete = await _LanguageRepository.GetLanguages(Id);

                if (languageDelete != null)
                {
                    var language = await _LanguageRepository.DeleteLanguages(Id);
                    return language;
                }
                return null;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error in retrieving data from database");
            }
        }
        [HttpGet("{Search}")]
        public async Task<ActionResult<IEnumerable<Languages>>> Search(string Language)
        {
            try
            {
                var result = await _LanguageRepository.Search(Language);
                if (result.Any())
                {
                    return Ok(result);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in retrieving data from database");
            }
        }
    }
}
