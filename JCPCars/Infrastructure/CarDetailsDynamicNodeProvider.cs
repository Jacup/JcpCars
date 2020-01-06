using JCPCars.DAL;
using JCPCars.Models;
using MvcSiteMapProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JCPCars.Infrastructure
{
    public class CarDetailsDynamicNodeProvider : DynamicNodeProviderBase
    {
        private StoreContext db = new StoreContext();

        public override IEnumerable<DynamicNode> GetDynamicNodeCollection(ISiteMapNode node)
        {
            var returnValue = new List<DynamicNode>();

            foreach (Car a in db.Cars)
            {
                DynamicNode n = new DynamicNode();
                n.Title = a.CarModel;
                n.Key = "Car_" + a.CarId;
                n.ParentKey = "Serie_" + a.SerieId;
                n.RouteValues.Add("id", a.CarId);
                returnValue.Add(n);
            }

            return returnValue;
        }
    }
}