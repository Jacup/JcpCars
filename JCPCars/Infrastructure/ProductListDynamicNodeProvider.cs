using JCPCars.DAL;
using JCPCars.Models;
using MvcSiteMapProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JCPCars.Infrastructure
{
    public class ProductListDynamicNodeProvider : DynamicNodeProviderBase
    {
        private StoreContext db = new StoreContext();

        public override IEnumerable<DynamicNode> GetDynamicNodeCollection(ISiteMapNode node)
        {
            var returnValue = new List<DynamicNode>();

            foreach (Serie a in db.Series)
            {
                DynamicNode n = new DynamicNode();
                n.Title = a.Name;
                n.Key = "Serie_" + a.SerieId;
                n.RouteValues.Add("seriename", a.Name);
                returnValue.Add(n);
            }

            return returnValue;
        }
    }
}