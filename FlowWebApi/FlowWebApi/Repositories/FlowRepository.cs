using FlowWebApi.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlowWebApi.Repositories
{
    public class FlowRepository: IFlowRepository
    {
        private readonly MyContext _myContext;

        public FlowRepository(MyContext myContext)
        {
            _myContext = myContext;
        }

        public IEnumerable<Flow> GetFlows()
        {
            return _myContext.flows.OrderBy(x => x.Name).ToList();
        }

        public Flow GetFlow(int flowId, bool includeMaterials)
        {
            if (includeMaterials)
            {
                return _myContext.flows
                    .Include(x => x.Materials).FirstOrDefault(x => x.Id == flowId);
            }
            return _myContext.flows.Find(flowId);
        }

        public IEnumerable<Material> GetMaterials(int flowId)
        {
            return _myContext.materials.Where(x => x.FlowId == flowId);
        }

        public Material GetMaterialForFlow(int flowId, int materialId)
        {
            return _myContext.materials.SingleOrDefault(x=>x.Id==materialId&&x.FlowId==flowId);
        }

        public bool FlowExist(int flowId)
        {
            return _myContext.flows.Any(x => x.Id == flowId);
        }

        public void AddFlow(Flow flow)
        {
            _myContext.flows.Add(flow);
        }

        public bool save()
        {
           return _myContext.SaveChanges() >= 0;
        }

        public Flow GetFlow(int flowId)
        {
            return _myContext.flows.SingleOrDefault(x=>x.Id==flowId);
        }

        public void DeleteFlow(Flow flow)
        {
            _myContext.flows.Remove(flow);
        }
    }
}
