using System;
using System.Collections.Generic;
using System.Linq;
using BE;
using DAL;

namespace BLL
{
    public class SiteComponent : Component<Site>
    {
        public SiteComponent(IRepository<Site> repository)
        {
            this.repository = repository;
        }

        public new Site Add(Site item)
        {
            foreach (var i in Get().ToList())
            {
                if (i.Title == item.Title)
                    throw new Exception("Site name already exists.");
            }

            return repository.Create(item);
        }
    }
}