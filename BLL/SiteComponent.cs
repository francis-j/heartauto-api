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
            bool performAdd = true;
            foreach (var i in Get().ToList())
            {
                if (i.Title == item.Title)
                    performAdd = false;
            }

            if (performAdd)
                repository.Create(item);
        }
    }
}