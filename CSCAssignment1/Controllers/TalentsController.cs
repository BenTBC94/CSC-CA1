using CSCAssignment1.Filters;
using CSCAssignment1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CSCAssignment1.Controllers
{

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RequireHttps]
    public class TalentsController : ApiController
    {
        static readonly TalentRepository repository = new TalentRepository();

        [Route("api/talents")]
        public IEnumerable<Talent> GetAllTalents()
        {
            return repository.GetAll();
        }

        [Route("api/searchTalents/{id:int}")]
        public Talent GetTalent(int id)
        {
            Talent item = repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
        }

        [HttpGet]
        [Route("api/Talents/retrieveTalents/{id:int:min(1)}", Name = "getTalentById")]

        public Talent retrieveTalentfromRepository(int id)
        {

            Talent item = repository.Get(id);

            if (item == null)
            {

                throw new HttpResponseException(HttpStatusCode.NotFound);

            }

            return item;
        }

        [HttpGet]
        [Route("api/Talents/getTalentsByShortName", Name = "getTalentsByShortName")]
        public IEnumerable<Talent> getTalentsByShortName(string shortname)
        {

            return repository.GetAll().Where(
                p => string.Equals(p.ShortName, shortname, StringComparison.OrdinalIgnoreCase)); // ignores any cases.

        }
        
        [HttpPost]
        [Route("api/Talents/addTalents")]
        public HttpResponseMessage PostTalent(Talent item)
        {
            if (ModelState.IsValid)
            {

                item = repository.Add(item);
                var response = Request.CreateResponse<Talent>(HttpStatusCode.Created, item);

                string uri = Url.Link("getTalentById", new { id = item.Id });
                response.Headers.Location = new Uri(uri);
                return Request.CreateResponse(HttpStatusCode.OK, "Id " + item.Id + " " + item.Name + " is successfully added");

            }
            else
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

            }
        }

        [HttpPut]
        [Route("api/Talents/updateTalents/{id:int}")]
        public HttpResponseMessage PutTalent(int id, Talent talent)
        {
            if (ModelState.IsValid)
            {
                talent.Id = id;

                if (!repository.Update(talent))
                {

                    throw new HttpResponseException(HttpStatusCode.NotFound);

                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Id " + talent.Id + " " + talent.Name + " is successfully updated");
                }
            }
            else
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

            }
        }

        [HttpDelete]
        [Route("api/Talents/deleteTalents/{id:int}")]
        public string DeleteTalent(int id)
        {

            Talent item = repository.Get(id);
            String message = "Talent " + id + " has been deleted!"; // Custom message
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            else
            {
                repository.Remove(id);
                return message;
            }

        }

    }
}
