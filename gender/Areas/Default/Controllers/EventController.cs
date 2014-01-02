using gender.Model;
using gender.Models.ViewModels;
using gender.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gender.Areas.Default.Controllers
{
    public class EventController : DefaultController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(int type)
        {
            IQueryable<Event> list = null;
            switch ((Event.Type)type)
            {
                case Event.Type.All:
                    list = Repository.Events.Where(p => p.ModeratedDate.HasValue);
                    break;
                case Event.Type.Russia:
                    list = Repository.Events.Where(p => p.ModeratedDate.HasValue && p.EventRegions.Any(r => r.Region.ID == 2 || r.Region.ParentID == 2 || string.Compare(r.Region.Name, "СССР", true) == 0));
                    break;
                case Event.Type.Other:
                    list = Repository.Events.Where(p => p.ModeratedDate.HasValue && !p.EventRegions.Any(r => r.Region.ID == 2 || r.Region.ParentID == 2 || string.Compare(r.Region.Name, "СССР", true) == 0));
                    break;
            }
            list = list.OrderByDescending(p => p.EventDate.HasValue ? p.EventDate.Value.Year : p.Year);
            return View(list.ToList());
        }

        public ActionResult Item(string url)
        {
            var item = Repository.Events.FirstOrDefault(p => string.Compare(p.Url, url, true) == 0);
            if (item != null)
            {
                return View(item);
            }
            return RedirectToNotFoundPage;
        }

        public ActionResult AlsoSubject(int id, int? idSubject = null)
        {
            var @event = Repository.Events.FirstOrDefault(p => p.ID == id);

            if (@event != null)
            {
                EventSubject eventSubject = null;
                if (idSubject.HasValue)
                {
                    eventSubject = @event.EventSubjects.FirstOrDefault(p => p.SubjectID == idSubject);
                    var nextEvent = @event.EventSubjects.FirstOrDefault(p => p.ID > eventSubject.ID);
                    if (nextEvent != null)
                    {
                        eventSubject = nextEvent;
                    }
                    else
                    {
                        eventSubject = @event.EventSubjects.FirstOrDefault();
                    }
                }
                else
                {
                    eventSubject = @event.EventSubjects.FirstOrDefault();
                }
                if (eventSubject != null)
                {
                    return View(eventSubject.Subject);
                }
            }
            return null;
        }

        public ActionResult Organization(string url)
        {
            var organization = Repository.Organizations.FirstOrDefault(p => string.Compare(p.Url, url, true) == 0);
            if (organization != null)
            {
                return View(organization);
            }
            return RedirectToNotFoundPage;
        }

        public ActionResult Comments(int id)
        {
            var @event = Repository.Events.FirstOrDefault(p => p.ID == id);
            if (@event != null)
            {
                return View(@event);
            }
            return null;
        }

        [HttpPost]
        public ActionResult CreateComment(CommentView commentView)
        {
            if (CurrentUser != null)
            {
                if (ModelState.IsValid)
                {
                    var comment = (Comment)ModelMapper.Map(commentView, typeof(CommentView), typeof(Comment));
                    comment.UserID = CurrentUser.ID;
                    comment.ID = 0;
                    Repository.CreateComment(comment);
                    var @eventComment = new EventComment
                    {
                        EventID = commentView.OwnerID,
                        CommentID = comment.ID
                    };
                    Repository.CreateEventComment(@eventComment);
                    Subscription.NewComment(Repository, @eventComment);
                    return Json(new { result = "ok" });
                }
                return Json(new { result = "error" });
            }
            return null;
        }

        [Authorize]
        public ActionResult Create()
        {
            if (CurrentUser.CanCreateEvent())
            {
                var eventView = new EventView();
                eventView.Links.Add(Guid.NewGuid().ToString("N"), new LinkView());
                return View("Edit", eventView);
            }
            return RedirectToLoginPage;
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit(int id)
        {
            var @event = Repository.Events.FirstOrDefault(p => p.ID == id);

            if (@event != null && CurrentUser.CanEdit(@event))
            {
                var eventView = (EventView)ModelMapper.Map(@event, typeof(Event), typeof(EventView));
                return View(eventView);
            }
            return RedirectToNotFoundPage;
        }

        [HttpPost]
        [Authorize]
        [ValidateInput(false)]
        public ActionResult Edit(EventView eventView)
        {
            if (!eventView.EventDate.HasValue && !eventView.Year.HasValue)
            {
                ModelState.AddModelError("EventDate", "Введите или дату или год (если событие до 1753го года - то только год)");
            }
            LinksVerification(eventView.Links);
            if (ModelState.IsValid)
            {
                var @event = (Event)ModelMapper.Map(eventView, typeof(EventView), typeof(Event));
                @event.UserID = CurrentUser.ID;
                if (@event.ID == 0)
                {
                    Repository.CreateEvent(@event, CurrentUser.ID);
                }
                else
                {
                    Repository.UpdateEvent(@event);
                }

                if (CurrentUser.Rating >= ModerateRating || CurrentUser.InRoles("admin,moderator") || CurrentUser.InvitedPrivileged)
                {
                    Repository.ModerateEvent(@event.ID);
                }

                var newSubjects = Repository.UpdateEventSubject(@event.ID, eventView.SubjectList);
                if (newSubjects != null && newSubjects.Count > 0 && eventView.ID != 0)
                {
                    Subscription.AddSubject(Repository, newSubjects, @event);
                }
                Repository.UpdateEventRegion(@event.ID, eventView.RegionList);
                Repository.UpdateEventOrganization(@event.ID, eventView.OrganizationList);
                Repository.UpdateEventPerson(@event.ID, eventView.PersonList);
                Repository.ClearEventLinks(@event.ID);
                if (eventView.Links != null)
                {
                    foreach (var linkView in eventView.Links)
                    {
                        var link = (Link)ModelMapper.Map(linkView.Value, typeof(LinkView), typeof(Link));
                        Repository.CreateLink(link);

                        var eventLink = new EventLink()
                        {
                            EventID = @event.ID,
                            LinkID = link.ID
                        };
                        Repository.CreateEventLink(eventLink);
                    }
                }
                Repository.ClearEventFiles(@event.ID);
                if (eventView.Files != null)
                {
                    foreach (var fileView in eventView.Files)
                    {
                        var file = (Model.File)ModelMapper.Map(fileView.Value, typeof(FileView), typeof(Model.File));
                        Repository.CreateFile(file);

                        var eventFile = new EventFile()
                        {
                            EventID = @event.ID,
                            FileID = file.ID
                        };
                        Repository.CreateEventFile(eventFile);
                    }
                }
                if (eventView.ID == 0)
                {
                    Subscription.AddMaterial(Repository, newSubjects, @event, null);
                }

                var newEvent = Repository.Events.FirstOrDefault(p => p.ID == @event.ID);
                if (newEvent != null)
                {
                    return RedirectToAction("Item", new { url = newEvent.Url });
                }
                return RedirectToAction("Index");
            }
            return View(eventView);
        }

        public ActionResult Delete(int id)
        {
            var @event = Repository.Events.FirstOrDefault(p => p.ID == id);
            if (@event != null && CurrentUser.CanDelete(@event))
            {
                Repository.RemoveEvent(@event.ID);
            }
            return RedirectToAction("Index");
        }
    }
}
