using FlowWebApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlowWebApi.Repositories
{
    public interface IFlowRepository
    {
        IEnumerable<Flow> GetFlows();
        Flow GetFlow(int flowId);
        Flow GetFlow(int flowId,bool includeMaterials);
        IEnumerable<Material> GetMaterials(int flowId);
        Material GetMaterialForFlow(int flowId, int materialId);
        bool FlowExist(int flowId);
        void AddFlow(Flow flow);
        bool save();
        void DeleteFlow(Flow flow);
    }
}
