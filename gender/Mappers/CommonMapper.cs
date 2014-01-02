using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using gender.Model;
using gender.Models.ViewModels;


namespace gender.Mappers
{
    public class CommonMapper : IMapper
    {
        static CommonMapper()
        {
            MapperCollection.LoginUserMapper.Init();
            MapperCollection.UserMapper.Init();
            MapperCollection.PublicationMapper.Init();
            MapperCollection.RegionMapper.Init();
            MapperCollection.StudyMaterialMapper.Init();
            MapperCollection.SubjectMapper.Init();
            MapperCollection.WebLinkMapper.Init();
            MapperCollection.DocumentMapper.Init();
            MapperCollection.EventMapper.Init();
            MapperCollection.ImageMapper.Init();
            MapperCollection.ContactMapper.Init();
            MapperCollection.PersonMapper.Init();
            MapperCollection.OrganizationMapper.Init();
            MapperCollection.ArticleMapper.Init();
            MapperCollection.BlogPostMapper.Init();
            MapperCollection.LinkMapper.Init();
            MapperCollection.BlogMapper.Init();
            MapperCollection.FileMapper.Init();
            MapperCollection.RedirectMapper.Init();
            MapperCollection.UserEmailMapper.Init();
            MapperCollection.CommentMapper.Init();
            MapperCollection.PageMapper.Init();
            MapperCollection.SubscriptionTemplateMapper.Init();
            MapperCollection.DistributionMapper.Init();
        }

        public object Map(object source, Type sourceType, Type destinationType)
        {
            return Mapper.Map(source, sourceType, destinationType);
        }
    }
}