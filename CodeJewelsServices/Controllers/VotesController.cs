using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CodeJewelsData;
using CodeJewelsModels;

namespace CodeJewelsServices.Controllers
{
    public class VotesController : ApiController
    {
        private readonly CodeJewelsContext db = new CodeJewelsContext();

        // GET api/votes/up/5
        [ActionName("up")]
        public HttpResponseMessage GetUpVote([FromUri]int codeJewelId)
        {
            if (ModelState.IsValid)
            {
                CodeJewel jewel = this.db.CodeJewels.Include(cj => cj.Category).FirstOrDefault(cj => cj.Id == codeJewelId);
                if (jewel == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    Vote vote = new Vote()
                    {
                        CodeJewel = jewel,
                        Value = 1,
                    };
                    Vote addedVote = db.Votes.Add(vote);
                    jewel.Rating++;
                    db.SaveChanges();

                    HttpResponseMessage response = Request.CreateResponse(
                        HttpStatusCode.OK,
                        new VoteModel()
                    {
                        CodeJewel = new CodeJewelGetModel()
                        {
                            Id = vote.CodeJewel.Id,
                            AuthorEmail = vote.CodeJewel.AuthorEmail,
                            Category = vote.CodeJewel.Category.Name,
                            Rating = vote.CodeJewel.Rating,
                            SourceCode = vote.CodeJewel.SourceCode
                        },
                        Value = vote.Value
                    });
                    response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = addedVote.Id }));
                    return response;
                }
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // GET api/votes/down/5
        [ActionName("down")]
        public HttpResponseMessage GetDownVote([FromUri]int codeJewelId)
        {
            if (ModelState.IsValid)
            {
                CodeJewel jewel = this.db.CodeJewels.Include(cj => cj.Category).FirstOrDefault(cj => cj.Id == codeJewelId);
                if (jewel == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "The jewel does not exist.");
                }
                else
                {
                    Vote vote = new Vote()
                    {
                        CodeJewel = jewel,
                        Value = -1,
                    };
                    Vote addedVote = db.Votes.Add(vote);
                    jewel.Rating--;
                    db.SaveChanges();

                    if (jewel.Rating <= -5)
                    {
                        var votesToRemove = db.Votes.Where(v => v.CodeJewel.Id == jewel.Id);
                        foreach (var voteToRemove in votesToRemove)
                        {
                            db.Votes.Remove(voteToRemove);
                        }

                        db.CodeJewels.Remove(jewel);
                        db.SaveChanges();

                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "The code jewel received too many down votes and has been deleted.");
                    }

                    HttpResponseMessage response = Request.CreateResponse(
                        HttpStatusCode.OK,
                        new VoteModel()
                        {
                            CodeJewel = new CodeJewelGetModel()
                            {
                                Id = vote.CodeJewel.Id,
                                AuthorEmail = vote.CodeJewel.AuthorEmail,
                                Category = vote.CodeJewel.Category.Name,
                                Rating = vote.CodeJewel.Rating,
                                SourceCode = vote.CodeJewel.SourceCode
                            },
                            Value = vote.Value
                        });
                    response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = addedVote.Id }));
                    return response;
                }
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }
    }
}
