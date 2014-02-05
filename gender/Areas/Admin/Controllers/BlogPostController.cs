using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using gender.Models.ViewModels;
using gender.Model;
using System.Xml;
using gender.Models;
using gender.Models.Temp;
using System.Text;
using gender.Tools;


namespace gender.Areas.Admin.Controllers
{
    public class BlogPostController : AdminController
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public ActionResult Index(int id, int page = 1)
        {
            var blog = Repository.Blogs.FirstOrDefault(p => p.ID == id);
            if (blog != null)
            {
                var list = Repository.BlogPosts.Where(p => p.BlogID == blog.ID).OrderByDescending(p => p.AddedDate);
                var data = new PageableData<BlogPost>();
                data.Init(list, page, "Index");
                ViewBag.Blog = blog;
                return View(data);
            }
            return RedirectToNotFoundPage;
        }

        public ActionResult Create(int id)
        {
            var blogpostView = new BlogPostView()
            {
                BlogID = id
            };
            return View("Edit", blogpostView);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var blogPost = Repository.BlogPosts.FirstOrDefault(p => p.ID == id);
            if (blogPost != null)
            {
                var blogpostView = (BlogPostView)ModelMapper.Map(blogPost, typeof(BlogPost), typeof(BlogPostView));
                return View(blogpostView);
            }
            return RedirectToNotFoundPage;
        }

        [HttpPost]
        public ActionResult Edit(BlogPostView blogpostView)
        {
            if (ModelState.IsValid)
            {
                var blogPost = (BlogPost)ModelMapper.Map(blogpostView, typeof(BlogPostView), typeof(BlogPost));
                if (blogPost.ID == 0)
                {
                    Repository.CreateBlogPost(blogPost);
                }
                else
                {
                    Repository.UpdateBlogPost(blogPost);
                }
                var newSubjects = Repository.UpdateBlogPostSubject(blogPost.ID, blogpostView.SubjectList);
                if (newSubjects != null && newSubjects.Count > 0 && blogpostView.ID != 0)
                {
                    Subscription.AddSubject(Repository, newSubjects, blogPost);
                }
                Repository.UpdateBlogPostRegion(blogPost.ID, blogpostView.RegionList);
                Repository.UpdateBlogPostOrganization(blogPost.ID, blogpostView.OrganizationList);
                Repository.UpdateBlogPostPerson(blogPost.ID, blogpostView.PersonList);
                Repository.UpdateBlogPostEvent(blogPost.ID, blogpostView.EventList);

                if (blogpostView.ID == 0)
                {
                    var subscribeAuthors =  new List<Person>();
                    if (blogPost.Blog.User != CurrentUser) 
                    {
                        subscribeAuthors.Add(blogPost.Blog.User.Person);
                    }
                    Subscription.AddMaterial(Repository, newSubjects, blogPost, subscribeAuthors);
                }
                return RedirectToAction("Index", new { id = blogPost.BlogID });
            }
            return View(blogpostView);
        }

        public ActionResult Delete(int id)
        {
            var blogpost = Repository.BlogPosts.FirstOrDefault(p => p.ID == id);
            if (blogpost != null)
            {
                var blogId = blogpost.BlogID;
                Repository.RemoveBlogPost(blogpost.ID);
            }
            return RedirectBack;
        }

        private Blog MakeBlog()
        {
            var blog = new Blog()
            {
                UserID = CurrentUser.ID,
                LastUpdate = DateTime.Now
            };
            Repository.CreateBlog(blog);

            return blog;
        }

        public ActionResult Redirects(int id)
        {
            var list = Repository.BlogPostRecordRedirects.Where(p => p.BlogPostID == id).ToList();
            return View(list);
        }

        public ActionResult AddRedirect(BlogPostRecordRedirect blogPostRecordRedirect)
        {
            Repository.CreateBlogPostRecordRedirect(blogPostRecordRedirect);
            return Json(new { result = "ok" });
        }

        public ActionResult RemoveRedirect(int id)
        {
            var blogPostRecordRedirect = Repository.BlogPostRecordRedirects.FirstOrDefault(p => p.ID == id);
            if (blogPostRecordRedirect != null)
            {
                Repository.RemoveRecordRedirect(blogPostRecordRedirect.RecordRedirectID);
            }
            return Json(new { result = "ok" });
        }

        public ActionResult Ballaeva()
        {
            var xmlDoc = new XmlDocument();

            var comments = new List<gender.Models.Temp.Comment>();
            var nodes = new List<gender.Models.Temp.Node>();
            var nodeRevisions = new List<gender.Models.Temp.NodeRevision>();

            xmlDoc.Load("D://u381884_genforum.xml");

            var root = xmlDoc.GetElementsByTagName("pma_xml_export")[0];


            var db = root.ChildNodes[1];

            var elements = db.ChildNodes;

            foreach (var element in elements)
            {
                if (element is XmlComment)
                {
                    continue;
                }

                if (element is XmlElement)
                {
                    
                    var xmlElement = (XmlElement)element;

                    if (xmlElement.Name == "table")
                    {
                        var tableName = xmlElement.Attributes["name"].Value;

                        if (tableName == "comments")
                        {
                            ParseComment(comments, xmlElement);
                        }

                        if (tableName == "node")
                        {
                            ParseNode(nodes, xmlElement);
                        }

                        if (tableName == "node_revisions")
                        {
                            ParseNodeRevision(nodeRevisions, xmlElement);
                        }
                    }
                }
            }
            var sb = new StringBuilder();
            sb.AppendLine(string.Format("Count Comments {0}<br/>", comments.Count));
            sb.AppendLine(string.Format("Count Nodes {0}<br/>", nodes.Count));
            sb.AppendLine(string.Format("Count Node Revisions {0}<br/>", nodeRevisions.Count));

            foreach (var node in nodes)
            {
                if (node.Uid != 4)
                {
                    continue;
                }

                var nodeRevision = nodeRevisions.Where(p => p.Nid == node.Nid).FirstOrDefault();

                if (nodeRevision != null)
                {
                    if (nodeRevision.Uid != 4)
                    {
                        logger.Debug("А кто тогда? " + nodeRevision.Nid.ToString());
                    }

                    var blogPost = new BlogPost()
                    {
                        BlogID = 2,
                        Header = node.Title,
                        Content = nodeRevision.Body.HttpToHref().NlToPBr(),
                        AddedDate = node.Created,
                        ChangedDate = node.Changed
                    };

                    Repository.CreateBlogPost(blogPost);

                    var redirect = new RecordRedirect()
                    {
                        IsForum = true,
                        Url = "/node/" + node.Nid.ToString()
                    };

                    Repository.CreateRecordRedirect(redirect);

                    var blogPostRedirect = new BlogPostRecordRedirect()
                    {
                        BlogPostID = blogPost.ID,
                        RecordRedirectID = redirect.ID
                    };

                    Repository.CreateBlogPostRecordRedirect(blogPostRedirect);

                    var nodeComments = comments.Where(p => p.Nid == node.Nid).OrderBy(p => p.Cid).ToList();

                    foreach (var comment in nodeComments)
                    {
                        if (comment.Uid == 4) 
                        {
                            var commentRecord = new Model.Comment()
                            {
                                UserID = 5,
                                Text = comment.Text,
                                AddedDate = comment.Timestamp,
                            };
                            Repository.CreateComment(commentRecord);
                            comment.RealID = commentRecord.ID;

                            var blogPostComment = new BlogPostComment()
                            {
                                CommentID = commentRecord.ID,
                                BlogPostID = blogPost.ID
                            };

                            Repository.CreateBlogPostComment(blogPostComment);
                        }
                    }

                    foreach (var comment in nodeComments)
                    {
                        if (comment.Pid > 0)
                        {
                            var parent = comments.FirstOrDefault(p => p.Cid == comment.Pid);

                            if (parent != null)
                            {
                                if (parent.Uid != 4 && comment.RealID.HasValue)
                                {
                                    Repository.RemoveComment(comment.RealID.Value);
                                    continue;
                                }

                                var parentRealID = nodeComments.FirstOrDefault(p => p.Cid == parent.Cid).RealID;

                                if (parentRealID.HasValue && comment.RealID.HasValue)
                                {
                                    var commentRecord = Repository.Comments.FirstOrDefault(p => p.ID == comment.RealID.Value);
                                    if (commentRecord != null)
                                    {
                                        commentRecord.ParentID = parentRealID.Value;
                                        Repository.UpdateComment(commentRecord);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return Content(sb.ToString());
        }

        private static void ParseNodeRevision(List<NodeRevision> nodeRevisions, XmlElement xmlElement)
        {
            var nodeRevision = new NodeRevision();

            foreach (XmlElement column in xmlElement.ChildNodes)
            {
                if (column.Attributes["name"].Value == "nid")
                {
                    nodeRevision.Nid = Int32.Parse(column.InnerText ?? "-1");
                }
                if (column.Attributes["name"].Value == "vid")
                {
                    nodeRevision.Vid = Int32.Parse(column.InnerText ?? "-1");
                }
                if (column.Attributes["name"].Value == "uid")
                {
                    nodeRevision.Uid = Int32.Parse(column.InnerText ?? "-1");
                }
                if (column.Attributes["name"].Value == "title")
                {
                    nodeRevision.Title = column.InnerText;
                }
                if (column.Attributes["name"].Value == "body")
                {
                    nodeRevision.Body = column.InnerText;
                }
                if (column.Attributes["name"].Value == "teaser")
                {
                    nodeRevision.Teaser = column.InnerText;
                }
                if (column.Attributes["name"].Value == "log")
                {
                    nodeRevision.Log = column.InnerText;
                }
                if (column.Attributes["name"].Value == "timestamp")
                {
                    nodeRevision.Timestamp = UnixTimeStampToDateTime(column.InnerText);
                }
                if (column.Attributes["name"].Value == "format")
                {
                    nodeRevision.Format = Int32.Parse(column.InnerText ?? "-1");
                }
            }
            nodeRevisions.Add(nodeRevision);
        }

        private static void ParseNode(List<Node> nodes, XmlElement xmlElement)
        {
            var node = new Node();

            foreach (XmlElement column in xmlElement.ChildNodes)
            {
                if (column.Attributes["name"].Value == "nid")
                {
                    node.Nid = Int32.Parse(column.InnerText ?? "-1");
                }
                if (column.Attributes["name"].Value == "vid")
                {
                    node.Vid = Int32.Parse(column.InnerText ?? "-1");
                }
                if (column.Attributes["name"].Value == "type")
                {
                    node.Type = column.InnerText;
                }
                if (column.Attributes["name"].Value == "language")
                {
                    node.Language = column.InnerText;
                }
                if (column.Attributes["name"].Value == "title")
                {
                    node.Title = column.InnerText;
                }
                if (column.Attributes["name"].Value == "uid")
                {
                    node.Uid = Int32.Parse(column.InnerText ?? "-1");
                }
                if (column.Attributes["name"].Value == "status")
                {
                    node.Status = Int32.Parse(column.InnerText ?? "-1");
                }
                if (column.Attributes["name"].Value == "created")
                {
                    node.Created = UnixTimeStampToDateTime(column.InnerText);
                }
                if (column.Attributes["name"].Value == "changed")
                {
                    node.Changed = UnixTimeStampToDateTime(column.InnerText);
                }
                if (column.Attributes["name"].Value == "comment")
                {
                    node.Comment = Int32.Parse(column.InnerText ?? "-1");
                }
                if (column.Attributes["name"].Value == "promote")
                {
                    node.Promote = Int32.Parse(column.InnerText ?? "-1");
                }
                if (column.Attributes["name"].Value == "moderate")
                {
                    node.Moderate = Int32.Parse(column.InnerText ?? "-1");
                }
                if (column.Attributes["name"].Value == "sticky")
                {
                    node.Sticky = Int32.Parse(column.InnerText ?? "-1");
                }
                if (column.Attributes["name"].Value == "tnid")
                {
                    node.Tnid = Int32.Parse(column.InnerText);
                }
                if (column.Attributes["name"].Value == "translate")
                {
                    node.Translate = Int32.Parse(column.InnerText);
                }
            }
            nodes.Add(node);
        }

        private static void ParseComment(List<Models.Temp.Comment> comments, XmlElement xmlElement)
        {
            var comment = new gender.Models.Temp.Comment();

            foreach (XmlElement column in xmlElement.ChildNodes)
            {
                if (column.Attributes["name"].Value == "cid")
                {
                    comment.Cid = Int32.Parse(column.InnerText ?? "-1");
                }
                if (column.Attributes["name"].Value == "pid")
                {
                    comment.Pid = Int32.Parse(column.InnerText ?? "-1");
                }
                if (column.Attributes["name"].Value == "nid")
                {
                    comment.Nid = Int32.Parse(column.InnerText ?? "-1");
                }
                if (column.Attributes["name"].Value == "uid")
                {
                    comment.Uid = Int32.Parse(column.InnerText ?? "-1");
                }
                if (column.Attributes["name"].Value == "subject")
                {
                    comment.Subject = column.InnerText;
                }
                if (column.Attributes["name"].Value == "comment")
                {
                    comment.Text = column.InnerText;
                }
                if (column.Attributes["name"].Value == "hostname")
                {
                    comment.HostName = column.InnerText;
                }
                if (column.Attributes["name"].Value == "timestamp")
                {
                    comment.Timestamp = UnixTimeStampToDateTime(column.InnerText);
                }
                if (column.Attributes["name"].Value == "status")
                {
                    comment.Status = Int32.Parse(column.InnerText ?? "-1");
                }
                if (column.Attributes["name"].Value == "format")
                {
                    comment.Format = Int32.Parse(column.InnerText ?? "-1");
                }
                if (column.Attributes["name"].Value == "thread")
                {
                    comment.Thread = column.InnerText;
                }
                if (column.Attributes["name"].Value == "name")
                {
                    comment.Name = column.InnerText;
                }
                if (column.Attributes["name"].Value == "mail")
                {
                    comment.Mail = column.InnerText;
                }
                if (column.Attributes["name"].Value == "homepage")
                {
                    comment.HomePage = column.InnerText;
                }
            }
            comments.Add(comment);
        }

        public static DateTime UnixTimeStampToDateTime(string unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            dtDateTime = dtDateTime.AddSeconds(Double.Parse(unixTimeStamp)).ToLocalTime();
            return dtDateTime;
        }

        public ActionResult UpdateComments()
        {
            foreach (var comment in Repository.Comments.ToList())
            {
                comment.Text = comment.Text.StripTags();
                Repository.UpdateComment(comment);
            }
            return null;
        }

        public ActionResult Test()
        {
            var source = @"Так вышло, что в нашем правительстве за самые хлопотные сферы, особенно в кризис, отвечают женщины. Эльвира Набиуллина - министр экономики, Татьяна Голикова - министр здравоохранения и социального развития. Хотя они были назначены на посты еще в 2007 году, когда кризиса вроде бы и не намечалось, но теперь оказались на самой что ни на есть передовой. А недавно на один из самых проблемных участков - сельское хозяйство - тоже была брошена женщина: Елена Скрынник. 

Что же за женщины на топовых позициях в Кабинете министров? 

Эльвира Набиуллина, по гороскопу - Скорпион. Госпожа Набиуллина абсолютно соответствует и своему имени (Эльвира - загадочная, таинственная), и звездному знаку: женщины-Скорпионы закрыты, сдержанны, некомфортно чувствуют себя на публике, но упорны в достижении целей. Цель либерального экономиста Набиуллиной - развитие рыночной конкуренции, новых технологий. Над этим она работала всю жизнь - и в госструктурах, и в бизнесе. Хотя сейчас, в кризис, когда в экономике все больше государства (без его денег многим компаниям не выжить), либеральную идею продвигать трудно. А еще женщины-Скорпионы обладают огромной, на зависть многим мужчинам, работоспособностью. В окружении Набиуллиной нам рассказали: «Уходим со службы - она еще работает, приходим - она уже работает».
 
Набиуллина - личность последовательная даже в выборе супруга. Ее муж - Ярослав Кузьминов, ректор Высшей школы экономики, считающейся главным «гнездом экономического либерализма». Сын - студент. 
Долгие годы Набиуллина работала с Германом Грефом (экс-министром экономики), ее даже называли «серым кардиналом Грефа». Бывший шеф так отзывался о своей соратнице: «Человек очень прозрачный, исключительно честный, работоголик». 
В отличие от бывшего шефа-модника с его яркими галстуками одевается госпожа министр по принципу «не выделяться». Ни намека на гламур. Низкий каблук, спокойных цветов костюмы, блузки под горло, минимум косметики. Не повышает голоса. Пожалуй, самый непубличный человек в Кабмине. Но влиятельный. Все-таки Министерство экономики - одно из важнейших.
Татьяна - красавица и модница
Совершенная противоположность Набиуллиной Татьяна Голикова, министр здравоохранения и соцразвития. Никогда еще в высшей российской власти не было столь эффектной дамы. Ее макияж, рассыпанные по плечам белокурые локоны, модные костюмы и сумки порой контрастируют с ее жестко поджатыми губами и напряженным взглядом, которые появляются у министра в сложные рабочие моменты. Такой яркой блондинке больше подошел бы заливистый смех и кокетство. Но этого у нее и в помине нет. В ее бытность замом министра финансов она отвечала за российский бюджет, королевой которого ее называли, и, говорят, держала в уме все цифры многотомного фолианта. 
Министру, впрочем, не чужда сентиментальность. Когда в одном из городов старушка рассказала Голиковой о своей трудной жизни, министр расплакалась. 
В медицинской среде поначалу скептически отнеслись к назначению министром человека, не имеющего отношения к медицине, - суперфинансиста. Но, как нам удалось узнать, для Путина в 2007 году при этом выборе главным было то, что как честный управленец она сумеет проконтролировать финансовые потоки, чтобы не растеклись, как бывало прежде. Ну а в кризис «социалка» вообще одна из главных тем. Помимо медицины, зарплат, пенсий, пособий, на плечи социального министра взвалена и помощь безработным. 
 
Татьяна Голикова - редкое сочетание звездной внешности и cуперпрофессионализма. Голикова по гороскопу Водолей. Женщины этого знака обладают удивительной притягательностью и обаянием. Тут не поспоришь. Несколько лет назад Голикова вышла замуж за министра промышленности Виктора Христенко. Их роман долгое время был тайной даже для коллег. Христенко, отец троих детей, был женат. И в ту пору ходил в вице-премьерах, постоянно на телеэкранах. Нынче роли поменялись. Не министр Христенко, а министр Голикова входит в президиум правительства, через который премьер Путин руководит всем хозяйством страны. 

Елена Скрынник (девичья фамилия Новицкая) - первая в истории страны женщина - аграрный министр. По образованию кардиолог. Как пошутил по этому поводу один думец, при ее назначении, видимо, учли, что наше сельское хозяйство в предынфарктном состоянии. Хотя большая часть карьеры Скрынник прошла в бизнесе - до назначения министром она руководила компанией «Росагролизинг», поставляющей селу технику. Руководитель, рассказывают, жесткий. Да и в Минсельхозе она сразу объявила, что проверит эффективность работников - останутся только профессионалы. Сельхозчиновники приуныли. 
Скрынник родилась в Челябинске, откуда родом и министр промышленности Христенко. Они вместе учились в Академии народного хозяйства, поговаривали даже об их родстве. А еще вместе со Скрынник в академии учился и экс-министр сельского хозяйства Гордеев, с которым с тех пор Елена Борисовна и оказалась связана по карьере. В одном из интервью она сказала, что среди ее друзей - всего пара человек, с которыми вместе училась. Возможно, как раз эти двое.
 
Скрынник по гороскопу Дева. Она тщательно следит за внешностью, ухожена, говорят, является (скорее являлась, министру нельзя) совладелицей центра красоты. При представлении в Минсельхозе попросила телеоператоров «сделать нормальную картинку», чтобы хорошо выглядеть в телевизоре. Стиль одежды - строгие костюмы, смягченные иногда романтичным бантом, блузками. 
Считается, что женщины, рожденные под знаком Девы, будут бороться за свое счастье до последнего. Это про Елену Скрынник. О жизни министра в официальной биографии - ни слова о семье и детях. Однако нам удалось кое-что узнать. Брат Скрынник Леонид Новицкий - замдиректора «Росагролизинга» - известный автогонщик и вице-президент Федерации спортивного свиноводства (да, есть такая, поросячьи бега организует). 
Елена Скрынник замужем (это ее не первый брак, Скрынник - фамилия еще студенческого мужа). А недавно у Елены Борисовны родились двойняшки - мальчик и девочка. 
МНЕНИЕ ПСИХОЛОГА
Александр КИЧАЕВ, психолог: Министерши и богини 
- Забавно, что сферы руководства женщин-министров в точности соответствуют распределению ролей среди богинь древней мифологии. Сельское хозяйство - это сфера интересов Деметры, у древних римлян - богини плодородия и земледелия. Социальная сфера и здравоохранение - это епархия Гигиеи, богини здоровья. Ну а сферы влияния Софии, богини мудрости, вполне можно увязать с задачами Минэкономики.
Согласно законам психологии, в команде должны быть и генераторы идей, и вдохновители, и завершители, и стабилизаторы отношений и пр. Женщины во власти хорошо подходят на роль завершителей, стабилизаторов отношений внутри властной команды и отношений власти с народом, так как по природе женщинам более присуще сострадание. К женщине простые люди испытывают больше доверия. Обычно женщины-руководительницы пытаются выработать компромиссный стиль управления - сочетание мягких, «женских», качеств и жестких, «мужских», что полностью соответствует стилю руководства этих трех женщин-министров. 
МНЕНИЕ ПОЛИТИКА
Ирина ХАКАМАДА, в 1997 году - министр, глава Госкомитета по поддержке малого бизнеса: 
Пашут по-черному
- Эти три женщины-министра очень симпатичны своим трудолюбием, профессионализмом, отсутствием необоснованных амбиций. Именно в эпоху кризиса возрастает роль женщин-управленцев, потому их сейчас и стали привлекать во власть. Женщины очень ответственны, более кропотливы, не впутаны во всякие кланы, хотя бы эти трое - Голикова, Набиуллина, Скрынник. И пашут по-черному. Женщины - отличные исполнители. Если бы еще мужчины во власти вырабатывали идеи, которые заслуживали бы отличного женского исполнения. Или уж дали бы возможность женщинам генерировать идеи. Тогда бы у нас вообще все было иначе. 
ЦИТАТА В ТЕМУ: 
«Если вам нужно написать речь - обратитесь к мужчине. Но если вам нужно что-то сделать - обратитесь к женщине». 
(Маргарет Тэтчер, экс-премьер Великобритании.) 


http://www.kp.ru/daily/24267.3/462904/print/


";

            var result = source.HttpToHref().NlToPBr();

            return Content(result);
        }
    }

    
}