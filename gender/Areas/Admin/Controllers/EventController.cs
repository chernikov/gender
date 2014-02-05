using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using gender.Models.ViewModels;
using gender.Model;
using gender.Global;
using gender.Tools;


namespace gender.Areas.Admin.Controllers
{
    public class EventController : AdminController
    {
        public ActionResult Index(int page = 1)
        {
            var list = Repository.Events.OrderByDescending(p => p.ID);
            var data = new PageableData<Event>();
            data.Init(list, page, "Index");
            return View(data);
        }

        public ActionResult Create()
        {
            var eventView = new EventView();
            eventView.Links.Add(Guid.NewGuid().ToString("N"), new LinkView());
            return View("Edit", eventView);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var @event = Repository.Events.FirstOrDefault(p => p.ID == id);

            if (@event != null)
            {
                var eventView = (EventView)ModelMapper.Map(@event, typeof(Event), typeof(EventView));
                return View(eventView);
            }
            return RedirectToNotFoundPage;
        }

        [HttpPost]
        public ActionResult Edit(EventView eventView)
        {
            LinksVerification(eventView.Links);
            if (!eventView.EventDate.HasValue && !eventView.Year.HasValue)
            {
                ModelState.AddModelError("EventDate", "Введите или дату или год (если событие до 1753го года - то только год)");
            }
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
                Repository.ModerateEvent(@event.ID);
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

                return RedirectToAction("Index");
            }
            return View(eventView);
        }

        public ActionResult Delete(int id)
        {
            var @event = Repository.Events.FirstOrDefault(p => p.ID == id);
            if (@event != null)
            {
                Repository.RemoveEvent(@event.ID);
            }
            return RedirectBack;
        }

        public ActionResult SelectEvent(string term)
        {
            var list = Repository.Events;

            var searchList = SearchEngine.Get(term, list);

            return Json(new
            {
                result = "ok",
                data = searchList.Select(p => new
                {
                    id = p.ID,
                    name = p.Header
                }),
                term = term
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Add(string name)
        {
            var @event = new Event()
            {
                Header = name,
                Teaser = string.Empty,
                Image = string.Empty,
            };

            Repository.CreateEvent(@event, CurrentUser.ID);
            return Json(new
            {
                result = "ok",
                data = new
                {
                    id = @event.ID,
                    name = @event.Header
                }
            });
        }

        public ActionResult Access(int id)
        {
            var list = Repository.EventAccesses.Where(p => p.EventID == id).ToList();
            return View(list);
        }

        public ActionResult AddAccess(EventAccess eventAccess)
        {
            if (eventAccess.UserID != 0 && eventAccess.EventID != 0)
            {
                var exist = Repository.EventAccesses.Any(p => p.EventID == eventAccess.EventID && p.UserID == eventAccess.UserID);

                if (!exist)
                {
                    Repository.CreateEventAccess(eventAccess);
                    Subscription.GiveRight(Repository, eventAccess);
                }
                return Json(new { result = "ok" });
            }
            return Json(new { result = "error" });
        }

        public ActionResult RemoveAccess(int id)
        {
            var exist = Repository.EventAccesses.Any(p => p.ID == id);
            if (exist)
            {
                Repository.RemoveEventAccess(id);
            }
            return Json(new { result = "ok" });
        }

        public ActionResult Redirects(int id)
        {
            var list = Repository.EventRecordRedirects.Where(p => p.EventID == id).ToList();
            return View(list);
        }

        public ActionResult AddRedirect(EventRecordRedirect eventRecordRedirect)
        {
            Repository.CreateEventRecordRedirect(eventRecordRedirect);
            return Json(new { result = "ok" });
        }

        public ActionResult RemoveRedirect(int id)
        {
            var eventRecordRedirect = Repository.EventRecordRedirects.FirstOrDefault(p => p.ID == id);
            if (eventRecordRedirect != null)
            {
                Repository.RemoveRecordRedirect(eventRecordRedirect.RecordRedirectID);
            }
            return Json(new { result = "ok" });
        }

        public ActionResult Moderate(int id)
        {
            Repository.ModerateEvent(id);
            return RedirectBack;
        }

    }
}