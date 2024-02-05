using AutoMapper;
using MacrixPracticalTask.Logger.ILog;
using MacrixPracticalTask.Models;
using MacrixPracticalTask.Models.DTO;
using MacrixPracticalTask.Models.Paging;
using MacrixPracticalTask.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace MacrixPracticalTask.Controllers
{
    [ApiController]
    [Route("api/[action]")]
    public class PersonController : ControllerBase
    {
        private ILoggerManager _logger;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public PersonController(ILoggerManager logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] PersonParameters personParameters)
        {
            try
            {
                var people = _unitOfWork.Person.GetAllPeople(personParameters).ToList();

                _logger.LogInfo($"GetAll() - Returned {people.Count} ids from database: {string.Join(", ", people.Select(x => x.Id).ToArray())}");

                var peopleResult = _mapper.Map<IEnumerable<PersonDTO>>(people);

                return Ok(peopleResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetAll() - Error happened while performing action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{personId}", Name = "PersonById")]
        public IActionResult GetPersonById(int personId)
        {
            try
            {
                var person = _unitOfWork.Person.GetPersonById(personId);

                if (person == null)
                {
                    var errorMessage = $"GetPersonById() - Unable to find person with id = {personId} in database.";

                    _logger.LogError(errorMessage);
                    return NotFound(errorMessage);
                }
                else
                {
                    _logger.LogInfo($"GetPersonById() - Returned person with id: {personId}");

                    var personResult = _mapper.Map<PersonDTO>(person);

                    return Ok(personResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetPersonById() - Error happened while performing action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult CreatePerson([FromBody] PersonForCreationDTO person)
        {
            try
            {
                if (person == null)
                {
                    _logger.LogError("CreatePerson() - Person object sent from client is null.");
                    return BadRequest("Person object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("CreatePerson() - Person object sent from client is invalid.");
                    return BadRequest("Invalid model object");
                }

                var personEntity = _mapper.Map<Person>(person);

                _unitOfWork.Person.CreatePerson(personEntity);
                _unitOfWork.Save();

                var createdPerson = _mapper.Map<PersonDTO>(personEntity);

                return CreatedAtRoute("PersonById", new { PersonId = createdPerson.Id }, createdPerson);
            }
            catch (Exception ex)
            {
                _logger.LogError($"CreatePerson() - Error happened while performing action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{personId}")]
        public IActionResult UpdatePerson(int personId, [FromBody] PersonForUpdateDTO person)
        {
            try
            {
                if (person == null)
                {
                    _logger.LogError("UpdatePerson() - Person object sent from client is null.");
                    return BadRequest("Person object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("UpdatePerson() - Person object sent from client is invalid.");
                    return BadRequest("Invalid model object");
                }

                var personEntity = _unitOfWork.Person.GetPersonById(personId);
                if (personEntity == null)
                {
                    var errorMessage = $"UpdatePerson() - Unable to find person with id = {personId} in database.";

                    _logger.LogError(errorMessage);
                    return NotFound(errorMessage);
                }

                _mapper.Map(person, personEntity);

                _unitOfWork.Person.UpdatePerson(personEntity);
                _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"UpdatePerson() - Error happened while performing action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{personId}")]
        public IActionResult DeletePerson(int personId)
        {
            try
            {
                var person = _unitOfWork.Person.GetPersonById(personId);
                if (person == null)
                {
                    var errorMessage = $"DeletePerson() - Unable to find person with id = {personId} in database.";

                    _logger.LogError(errorMessage);
                    return NotFound(errorMessage);
                }

                _unitOfWork.Person.DeletePerson(person);
                _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"DeletePerson() - Error happened while performing action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
