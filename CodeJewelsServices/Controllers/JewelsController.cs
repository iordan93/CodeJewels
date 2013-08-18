using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CodeJewelsData;
using CodeJewelsModels;

namespace CodeJewelsServices.Controllers
{
    public class JewelsController : ApiController
    {
        private CodeJewelsContext db = new CodeJewelsContext();

        // GET api/jewels
        public IEnumerable<CodeJewelGetModel> GetAllCodeJewels()
        {
            HashSet<CodeJewelGetModel> codeJewels = new HashSet<CodeJewelGetModel>();
            foreach (var jewel in db.CodeJewels.Include(cj => cj.Category))
            {
                codeJewels.Add(new CodeJewelGetModel()
                {
                    Id = jewel.Id,
                    AuthorEmail = jewel.AuthorEmail,
                    Category = jewel.Category.Name,
                    Rating = jewel.Rating,
                    SourceCode = jewel.SourceCode
                });
            }

            return codeJewels;
        }

        // GET api/jewels/byCategory?categoryName=csharp
        [ActionName("byCategory")]
        public IEnumerable<CodeJewelGetModel> GetCodeJewelsByCategoryName(string categoryName)
        {
            HashSet<CodeJewelGetModel> codeJewels = new HashSet<CodeJewelGetModel>();
            foreach (var jewel in db.CodeJewels.Include(cj => cj.Category).Where(cj => cj.Category.Name == categoryName))
            {
                codeJewels.Add(new CodeJewelGetModel()
                {
                    Id = jewel.Id,
                    AuthorEmail = jewel.AuthorEmail,
                    Category = jewel.Category.Name,
                    Rating = jewel.Rating,
                    SourceCode = jewel.SourceCode
                });
            }

            return codeJewels;
        }

        // GET api/jewels/bySource?source=if(x>3)
        [ActionName("bySource")]
        public IEnumerable<CodeJewelGetModel> GetCodeJewelsBySourceDode(string source)
        {
            HashSet<CodeJewelGetModel> codeJewels = new HashSet<CodeJewelGetModel>();
            foreach (var jewel in db.CodeJewels.Include(cj => cj.Category).Where(cj => cj.SourceCode.Contains(source)))
            {
                codeJewels.Add(new CodeJewelGetModel()
                {
                    Id = jewel.Id,
                    AuthorEmail = jewel.AuthorEmail,
                    Category = jewel.Category.Name,
                    Rating = jewel.Rating,
                    SourceCode = jewel.SourceCode
                });
            }

            return codeJewels;
        }

        // POST api/jewels
        public HttpResponseMessage PostCodeJewel(CodeJewelPostModel jewel)
        {
            if (ModelState.IsValid)
            {
                Category repositoryCategory = db.Categories.FirstOrDefault(c => c.Name == jewel.Category);
                if (repositoryCategory == null)
                {
                    repositoryCategory = db.Categories.Add(new Category() { Name = jewel.Category });
                    db.SaveChanges();
                }

                CodeJewel newJewel = db.CodeJewels.Add(new CodeJewel()
                {
                    Category = repositoryCategory,
                    AuthorEmail = jewel.AuthorEmail,
                    SourceCode = jewel.SourceCode,
                });
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, jewel);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = newJewel.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }
    }
}
