using System;
using System.Collections.Generic;
using System.Linq;
using BE;
using DAL;
using MongoDB.Bson;

namespace BLL
{
    public class SiteComponent : Component<Site>
    {
        public SiteComponent(IRepository<Site> repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Base Add method overridden for custom validation
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Site</returns>
        public override Site Add(Site item)
        {
            foreach (var i in Get().ToList())
            {
                if (i.Title == item.Title)
                    throw new Exception("Site name already exists.");
            }

            item.DateCreated = item.DateUpdated = DateTime.UtcNow;

            foreach (var page in item.Pages) 
            {
                page.Id = ObjectId.GenerateNewId().ToString();
            }

            return repository.Create(item);
        }

        public new Site Update(ObjectId id, Site item)
        {
            if (GetById(id) != null) 
            {
                foreach (var page in item.Pages)
                {
                    if (string.IsNullOrEmpty(page.Id))
                    {
                        page.Id = ObjectId.GenerateNewId().ToString();
                    }
                }

                return repository.Update(id, item);
            }
            else
            {
                throw new Exception("Item does not exist.");
            }

            throw new Exception("An unspecified error occurred.");
        }
    }
}